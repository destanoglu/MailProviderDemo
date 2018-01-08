using System.Configuration.Abstractions;
using Mail.Host.Config;
using Topshelf;

namespace Mail.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = ConfigurationManager.Instance.AppSettings.Map<MailDeliveryServiceSettings>();

            HostFactory.Run(config =>
            {
                config.Service<MailDeliveryService>(selfHost =>
                {
                    selfHost.ConstructUsing(() => new MailDeliveryService());
                    selfHost.WhenStarted(s => s.StartService(settings));
                    selfHost.WhenStopped(s => s.StopService());
                });
                config.SetServiceName("MailDeliveryAPI");
                config.SetDisplayName("Mail Delivery API");
                config.SetDescription("Async mail delivery API");
                config.RunAsLocalSystem();
            });
        }
    }
}
