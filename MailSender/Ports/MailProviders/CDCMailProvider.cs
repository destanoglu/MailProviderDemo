using System;
using System.Collections.Generic;
using Mail.Sender.Adapters.MailProviders;
using Mail.Shared.Contracts;

namespace Mail.Sender.Ports.MailProviders
{
    public class CDCMailProvider : ISendMail
    {
        public CDCMailProvider()
        {
            AssociatedMailTypes = new List<MessageType>
            {
                MessageType.LostPasswordMail
            };
        }

        public IList<MessageType> AssociatedMailTypes { get; }

        public void SendMail(IMailContent data)
        {
            Console.WriteLine($"Sending mail using {this.GetType().Name}, from {data.Sender} to {data.Destination}");
        }
    }
}
