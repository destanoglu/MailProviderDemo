using System;
using System.Configuration.Abstractions;
using System.Linq;

namespace Mail.Sender.Config
{
    public class MailSenderSettings
    {
        private static MailSenderSettings _serviceSettings;
        public string ApplicationBaseUrls { get; set; }
        public BrokerSettings BrokerSettings { get; set; }
        public RetrySettings RetrySettings { get; set; }


        public static MailSenderSettings Current()
        {
            return _serviceSettings ?? (_serviceSettings = ConfigurationManager.Instance.AppSettings.Map<MailSenderSettings>());
        }
        public Uri[] BaseUrlsAsArray()
        {
            return ApplicationBaseUrls.Split(',').Select(url => new Uri(url)).ToArray();
        }
    }
}
