using System;
using Mail.Shared.Contracts;

namespace Mail.Host.Domain.Messages
{
    public class SendMailOrderDataMessage : IMailOrder
    {
        public string Sender { get; }
        public string Destination { get; }
        public string Body { get; }
        public MessageType Type { get; }
        public DateTime ScheduleAt { get; }

        public SendMailOrderDataMessage(string sender, string destination, MessageType type, DateTime scheduleAt, string body)
        {
            Sender = sender;
            Destination = destination;
            Type = type;
            ScheduleAt = scheduleAt;
            Body = body;
        }
    }
}
