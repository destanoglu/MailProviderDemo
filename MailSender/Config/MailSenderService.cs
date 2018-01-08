using System;
using Autofac;
using Common.Logging;
using MassTransit;

namespace Mail.Sender.Config
{
    public class MailSenderService : IDisposable
    {
        private static readonly ILog _logger = LogManager.GetLogger<MailSenderService>();

        private readonly Bootstrapper _bootstrapper;

        public MailSenderService()
            : this(new Bootstrapper())
        {}

        public MailSenderService(Bootstrapper bootstrapper)
        {
            _bootstrapper = bootstrapper;
        }

        public void StartService(MailSenderSettings settings)
        {
            var busController = _bootstrapper.Container.Resolve<IBusControl>();

            _logger.Info("Starting service");
            busController.Start();
            _logger.Info("Starting is running");
        }

        public void StopService()
        {
            _logger.Info("Stopping service");
            var busController = _bootstrapper.Container.Resolve<IBusControl>();
            busController.Stop();
        }

        public void Dispose()
        {
            _logger.Info("Disposing service");
            StopService();
        }
    }
}