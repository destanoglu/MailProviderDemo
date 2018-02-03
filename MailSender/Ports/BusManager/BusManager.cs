using System;
using GreenPipes;
using Mail.Sender.Adapters.BusManager;
using Mail.Sender.Config;
using Mail.Sender.Domain.Consumers;
using Mail.Sender.Domain.Exceptions;
using MassTransit;

namespace Mail.Sender.Ports.BusManager
{
    public class BusManager : IBusManager
    {
        private readonly IBusControl _busControl;

        public BusManager(SendMailConsumer sendMailConsumer, SendMailOrderConsumer sendMailOrderConsumer, IMailSenderSettings settings)
        {
            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
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
                    e.Consumer(() => sendMailConsumer);
                });

                cfg.ReceiveEndpoint(host, "send_mail_order_queue", e =>
                {
                    e.UseRetry(retryConfig =>
                    {
                        retryConfig.Interval(settings.RetrySettings.Count, TimeSpan.FromMilliseconds(settings.RetrySettings.Interval));
                    });
                    e.Consumer(() => sendMailOrderConsumer);
                });

                cfg.UseInMemoryScheduler();
            });
        }

        public void Start()
        {
            _busControl.Start();
        }

        public void Stop()
        {
            _busControl.Stop();
        }
    }
}
