namespace Mail.Sender.Config
{
    public interface IMailSenderSettings
    {
        string ApplicationBaseUrls { get; set; }
        BrokerSettings BrokerSettings { get; set; }
        RetrySettings RetrySettings { get; set; }
    }
}
