using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Common.Logging;
using GreenPipes;
using Mail.Sender.Adapters.MailProviders;
using Mail.Sender.Adapters.ProviderFactories;
using Mail.Sender.Domain.Consumers;
using Mail.Sender.Domain.Exceptions;
using Mail.Sender.Ports.MailProviders;
using Mail.Sender.Ports.ProviderFactories;
using MassTransit;

namespace Mail.Sender.Config
{
    public class Bootstrapper
    {
        private ILog _logger = LogManager.GetLogger<Bootstrapper>();
        public IContainer Container { get; }

        public Bootstrapper()
        {
            Container = ConfigureApplicationContainer();
        }
        
        private IContainer ConfigureApplicationContainer()
        {
            var settings = MailSenderSettings.Current();

            var builder = new ContainerBuilder();

            RegisterConsumers(builder);
            RegisterLoggers(builder);
            RegisterMessaging(builder, settings);
            RegisterMailProviders(builder);

            return builder.Build();
        }

        private void RegisterMailProviders(ContainerBuilder builder)
        {
            builder.Register(context => new MailProviderFactory(new List<ISendMail>
                {
                    new ABCMailProvider(),
                    new CDCMailProvider()
                }))
                .As<IMailProviderFactory>()
                .SingleInstance();
        }

        private void RegisterConsumers(ContainerBuilder builder)
        {
            builder.RegisterConsumers(Assembly.GetExecutingAssembly());
        }

        private void RegisterLoggers(ContainerBuilder builder)
        {
            builder.RegisterInstance(LogManager.GetLogger<Bootstrapper>()).As<ILog>();
        }

        private void RegisterMessaging(ContainerBuilder builder, MailSenderSettings settings)
        {
            builder.Register(context =>
                {
                    var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        var innerContext = context.Resolve<IComponentContext>();
                        var host = cfg.Host(new Uri("rabbitmq://" + settings.BrokerSettings.HostName), h =>
                        {
                            h.Username(settings.BrokerSettings.UserName);
                            h.Password(settings.BrokerSettings.Password);
                        });

                        cfg.ReceiveEndpoint(host, "send_mail_queue", e =>
                        {
                            e.UseRetry(retryConfig =>
                            {
                                retryConfig.Handle<MailSendOperationFailedException>();
                                retryConfig.Ignore<MailProviderRegistrationMissingException>();
                                retryConfig.Interval(settings.RetrySettings.Count, TimeSpan.FromMilliseconds(settings.RetrySettings.Interval));
                            });
                            e.Consumer(innerContext.Resolve<SendMailConsumer>);
                        });

                        cfg.ReceiveEndpoint(host, "send_mail_order_queue", e =>
                        {
                            e.UseRetry(retryConfig =>
                            {
                                retryConfig.Interval(settings.RetrySettings.Count, TimeSpan.FromMilliseconds(settings.RetrySettings.Interval));
                            });
                            e.Consumer(innerContext.Resolve<SendMailOrderConsumer>);
                        });

                        cfg.UseInMemoryScheduler();
                    });

                    return busControl;
                })
                .SingleInstance()
                .As<IBusControl>();
        }
    }
}