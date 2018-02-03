using System.Configuration.Abstractions;
using Mail.Sender.Config;
using Topshelf;

namespace Mail.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(config =>
            {
                config.Service<MailSenderService>(selfHost =>
                {
                    selfHost.ConstructUsing(() => new MailSenderService());
                    selfHost.WhenStarted(s => s.StartService());
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
