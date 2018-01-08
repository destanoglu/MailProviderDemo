using System.Configuration.Abstractions;
using Mail.Sender.Config;
using Topshelf;

namespace Mail.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = ConfigurationManager.Instance.AppSettings.Map<MailSenderSettings>();

            HostFactory.Run(config =>
            {
                config.Service<MailSenderService>(selfHost =>
                {
                    selfHost.ConstructUsing(() => new MailSenderService());
                    selfHost.WhenStarted(s => s.StartService(settings));
                    selfHost.WhenStopped(s => s.StopService());
                });
                config.SetServiceName("MailSender");
                config.SetDisplayName("Mail Sender Service");
                config.SetDescription("Async mail sender service");
                config.RunAsLocalSystem();
            });
        }
    }
}
