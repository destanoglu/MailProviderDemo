using System;
using Mail.Shared.Contracts;

namespace Mail.Sender.Domain.Messages
{
    public class SendMailOrderDataMessage : IHoldMailOrderData
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
