using System;
using System.Configuration.Abstractions;
using System.Linq;
using Mail.Shared.Config;

namespace Mail.Host.Config
{
    public class MailDeliveryServiceSettings
    {
        private static MailDeliveryServiceSettings _serviceSettings;
        public string ApplicationBaseUrls { get; set; }
        public BrokerSettings BrokerSettings { get; set; }

        public static MailDeliveryServiceSettings Current()
        {
            return _serviceSettings ?? (_serviceSettings = ConfigurationManager.Instance.AppSettings.Map<MailDeliveryServiceSettings>());
        }
        public Uri[] BaseUrlsAsArray()
        {
            return ApplicationBaseUrls.Split(',').Select(url => new Uri(url)).ToArray();
        }
    }
}
