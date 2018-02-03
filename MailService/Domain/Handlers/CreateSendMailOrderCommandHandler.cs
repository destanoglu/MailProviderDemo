using System;
using Mail.Host.Domain.Commands;
using Mail.Host.Domain.Handlers.Attributes;
using Mail.Host.Domain.Messages;
using Mail.Shared.Contracts;
using MassTransit;
using Paramore.Brighter;
using MessageType = Mail.Shared.Contracts.MessageType;

namespace Mail.Host.Domain.Handlers
{
    public class CreateSendMailOrderCommandHandler : RequestHandler<CreateSendMailOrderCommand>
    {
        private readonly IBusControl _busController;

        public CreateSendMailOrderCommandHandler(IBusControl busController)
        {
            _busController = busController;
        }

        [RequestValidation(step:1, timing:HandlerTiming.Before)]
        public override CreateSendMailOrderCommand Handle(CreateSendMailOrderCommand command)
        {
            MessageType type;
            Enum.TryParse(command.Type, true, out type);

            _busController.Publish<IMailOrder>(new SendMailOrderDataMessage(
                command.Sender,
                command.Destination,
                type,
                command.ScheduleAt,
                command.Body
            ));

            return base.Handle(command);
        }
    }
}
