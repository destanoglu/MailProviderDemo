using System.Collections.Generic;
using Mail.Shared.Contracts;

namespace Mail.Sender.Adapters.MailProviders
{
    public interface ISendMail
    {
        IList<MessageType> AssociatedMailTypes { get; }
        void SendMail(IMailContent data);
    }
}
