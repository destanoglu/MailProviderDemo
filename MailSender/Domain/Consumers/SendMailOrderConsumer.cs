using System;
using System.Threading.Tasks;
using Mail.Sender.Domain.Messages;
using Mail.Shared.Contracts;
using MassTransit;

namespace Mail.Sender.Domain.Consumers
{
    public class SendMailOrderConsumer : IConsumer<IHoldMailOrderData>
    {
        public Task Consume(ConsumeContext<IHoldMailOrderData> context)
        {
            Console.WriteLine($"Rescheduling mail from {context.Message.Sender} to {context.Message.Destination} at {context.Message.ScheduleAt.ToString("G")}");
            context.SchedulePublish<IHoldMailData>(context.Message.ScheduleAt,
                new SendMailDataMessage(
                    context.Message.Sender,
                    context.Message.Destination,
                    context.Message.Body,
                    context.Message.Type));

            return context.CompleteTask;
        }
    }
}
