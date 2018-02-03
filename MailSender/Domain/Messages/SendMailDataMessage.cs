using Mail.Shared.Contracts;

namespace Mail.Sender.Domain.Messages
{
    public class SendMailDataMessage : IMailContent
    {
        public string Sender { get; }
        public string Destination { get; }
        public string Body { get; }
        public MessageType Type { get; }

        public SendMailDataMessage(string sender, string destination, string body, MessageType type)
        {
            Sender = sender;
            Destination = destination;
            Body = body;
            Type = type;
        }
    }
}
