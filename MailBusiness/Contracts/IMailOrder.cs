using System;

namespace Mail.Shared.Contracts
{
    public interface IMailOrder
    {
        string Sender { get; }
        string Destination { get; }
        string Body { get; }
        MessageType Type { get; }
        DateTime ScheduleAt { get; }
    }
}
