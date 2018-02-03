using System;
using Common.Logging;

namespace Mail.Sender.Config
{
    public class MailSenderService : IDisposable
    {
        private readonly ILog _logger = LogManager.GetLogger<MailSenderService>();

        private readonly Bootstrapper _bootstrapper;

        public MailSenderService()
            : this(new Bootstrapper())
        {}

        public MailSenderService(Bootstrapper bootstrapper)
        {
            _bootstrapper = bootstrapper;
        }

        public void StartService()
        {
            _logger.Info("Starting service");
            _bootstrapper.Start();
            _logger.Info("Starting is running");
        }

        public void StopService()
        {
            _logger.Info("Stopping service");
            _bootstrapper.Stop();
        }

        public void Dispose()
        {
            _logger.Info("Disposing service");
            StopService();
        }
    }
}