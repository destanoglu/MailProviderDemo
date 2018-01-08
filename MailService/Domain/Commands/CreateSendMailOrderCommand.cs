using System;
using Paramore.Brighter;

namespace Mail.Host.Domain.Commands
{
    public class CreateSendMailOrderCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Sender { get; }
        public string Destination { get; }
        public string Body { get; }
        public string Type { get; }
        public DateTime ScheduleAt { get; }
        
        public CreateSendMailOrderCommand(string sender, string destination, string body, string type, DateTime scheduleAt)
        {
            Id = Guid.NewGuid();
            Sender = sender;
            Destination = destination;
            Body = body;
            Type = type;
            ScheduleAt = scheduleAt;
        }
    }
}
