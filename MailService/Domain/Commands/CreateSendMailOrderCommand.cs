using System;
using System.Collections.Generic;
using Mail.Shared.Contracts;

namespace Mail.Host.Domain.Commands
{
    public class CreateSendMailOrderCommand : BaseCommand, IValidatable
    {
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

        public IEnumerable<string> Validate()
        {
            var errors = new List<string>();
            MessageType type;
            if (!Enum.TryParse(Type, out type))
            {
                errors.Add($"Mail type is not suitable {Type}");
            }

            return errors;
        }
    }
}
