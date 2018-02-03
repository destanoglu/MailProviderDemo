using System;
using System.Threading.Tasks;
using Mail.Sender.Domain.Messages;
using Mail.Shared.Contracts;
using MassTransit;

namespace Mail.Sender.Domain.Consumers
{
    public class SendMailOrderConsumer : IConsumer<IMailOrder>
    {
        public Task Consume(ConsumeContext<IMailOrder> context)
        {
            Console.WriteLine($"Rescheduling mail from {context.Message.Sender} to {context.Message.Destination} at {context.Message.ScheduleAt.ToString("G")}");
            context.SchedulePublish<IMailContent>(context.Message.ScheduleAt,
                new SendMailDataMessage(
                    context.Message.Sender,
                    context.Message.Destination,
                    context.Message.Body,
                    context.Message.Type));

            return context.CompleteTask;
        }
    }
}
