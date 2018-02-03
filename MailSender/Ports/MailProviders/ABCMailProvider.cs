using System;
using System.Collections.Generic;
using Mail.Sender.Adapters.MailProviders;
using Mail.Shared.Contracts;

namespace Mail.Sender.Ports.MailProviders
{
    public class ABCMailProvider : ISendMail
    {
        public ABCMailProvider()
        {
            AssociatedMailTypes = new List<MessageType>
            {
                MessageType.OrderMail,
                MessageType.ShipmentMail
            };
        }
        
        public IList<MessageType> AssociatedMailTypes { get; }

        public void SendMail(IMailContent data)
        {
            Console.WriteLine($"Sending mail using {this.GetType().Name}, from {data.Sender} to {data.Destination}");
        }
    }
}
