using System;
using System.Reflection;
using Autofac;
using Common.Logging;
using Mail.Host.Domain.Commands;
using Mail.Host.Domain.Handlers;
using MassTransit;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Paramore.Brighter;

namespace Mail.Host.Config
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            var busController = container.Resolve<IBusControl>();
            busController.Start();
        }
        
        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            var settings = MailDeliveryServiceSettings.Current();

            var builder = new ContainerBuilder();

            RegisterConsumers(builder);
            RegisterLoggers(builder);
            RegisterCommandProcessor(builder);
            RegisterMessaging(builder, settings);
            
            builder.Update(existingContainer.ComponentRegistry);
        }

        private void RegisterCommandProcessor(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var registry = new SubscriberRegistry();
                registry.Register<CreateSendMailOrderCommand, RequestHandler<CreateSendMailOrderCommand>>();

                var cpBuilder = CommandProcessorBuilder.With()
                    .Handlers(new HandlerConfiguration(
                        subscriberRegistry: registry,
                        handlerFactory: new MailServiceHandlerFactory(context.Resolve<IComponentContext>())
                    ))
                    .DefaultPolicy()
                    .NoTaskQueues()
                    .RequestContextFactory(new InMemoryRequestContextFactory());

                return cpBuilder.Build();
            }).SingleInstance().As<IAmACommandProcessor>();

            builder.RegisterType<CreateSendMailOrderCommandHandler>().As<RequestHandler<CreateSendMailOrderCommand>>();
            builder.RegisterType<RequestValidationHandler<CreateSendMailOrderCommand>>();
        }
        private void RegisterConsumers(ContainerBuilder builder)
        {
            builder.RegisterConsumers(Assembly.GetExecutingAssembly());
        }
        private void RegisterLoggers(ContainerBuilder builder)
        {
            builder.RegisterInstance(LogManager.GetLogger<Bootstrapper>()).As<ILog>();
        }
        private void RegisterMessaging(ContainerBuilder builder, MailDeliveryServiceSettings settings)
        {
            builder.Register(context =>
                {
                    var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host(new Uri("rabbitmq://" + settings.BrokerSettings.HostName), h =>
                        {
                            h.Username(settings.BrokerSettings.UserName);
                            h.Password(settings.BrokerSettings.Password);
                        });
                    });

                    return busControl;
                })
                .SingleInstance()
                .As<IBusControl>();
        }
    }
}