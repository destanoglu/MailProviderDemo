using Mail.Sender.Adapters.MailProviders;
using Mail.Shared.Contracts;

namespace Mail.Sender.Adapters.ProviderFactories
{
    public interface IMailProviderFactory
    {
        ISendMail GetMailProvider(MessageType type);
    }
}
