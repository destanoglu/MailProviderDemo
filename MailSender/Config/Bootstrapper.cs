using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Common.Logging;
using Mail.Sender.Adapters.BusManager;
using Mail.Sender.Adapters.MailProviders;
using Mail.Sender.Adapters.ProviderFactories;
using Mail.Sender.Domain.Consumers;
using Mail.Sender.Ports.BusManager;
using Mail.Sender.Ports.MailProviders;
using Mail.Sender.Ports.ProviderFactories;
using MassTransit;

namespace Mail.Sender.Config
{
    public class Bootstrapper
    {
        private readonly IContainer _container;
        private IBusManager _busManager;

        public Bootstrapper()
        {
            _container = ConfigureApplicationContainer();
        }
        
        private IContainer ConfigureApplicationContainer()
        {
            var builder = new ContainerBuilder();
            
            RegisterConfig(builder);
            RegisterConsumers(builder);
            RegisterLoggers(builder);
            RegisterMessaging(builder);
            RegisterMailProviders(builder);

            return builder.Build();
        }

        private void RegisterConfig(ContainerBuilder builder)
        {
            builder.Register(context => MailSenderSettings.Current())
                .As<IMailSenderSettings>()
                .SingleInstance();
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

        private void RegisterMessaging(ContainerBuilder builder)
        {
            builder.Register(context => new BusManager(
                context.Resolve<SendMailConsumer>(),
                context.Resolve<SendMailOrderConsumer>(),
                context.Resolve<IMailSenderSettings>()
                ))
                .SingleInstance()
                .As<IBusManager>();
        }

        public void Start()
        {
            _busManager = _container.Resolve<IBusManager>();
            _busManager.Start();
        }

        public void Stop()
        {
            _busManager?.Stop();
        }
    }
}