using System;
using Common.Logging;
using Nancy.Hosting.Self;

namespace Mail.Host.Config
{
    public class MailDeliveryService : IDisposable
    {
        private static readonly ILog _logger = LogManager.GetLogger<MailDeliveryService>();

        private readonly Bootstrapper _bootstrapper;
        private NancyHost _nancyHost;

        public MailDeliveryService()
            : this(new Bootstrapper())
        {}

        public MailDeliveryService(Bootstrapper bootstrapper)
        {
            _bootstrapper = bootstrapper;
        }

        public void StartService(MailDeliveryServiceSettings settings)
        {
            _nancyHost = new NancyHost(_bootstrapper, new HostConfiguration
            {
                UrlReservations = new UrlReservations { CreateAutomatically = true}
            }, settings.BaseUrlsAsArray());
            
            _logger.Info("Starting the service");
            _nancyHost.Start();
            _logger.Info("Service is running");
        }

        public void StopService()
        {
            _logger.Info("Stopping the service");
            _nancyHost.Stop();
        }

        public void Dispose()
        {
            _logger.Info("Disposing the service");
            StopService();
        }
    }
}