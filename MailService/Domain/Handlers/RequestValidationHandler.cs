using System;
using Mail.Host.Domain.Commands;
using Mail.Host.Domain.Exceptions;
using Paramore.Brighter;
using MessageType = Mail.Shared.Contracts.MessageType;

namespace Mail.Host.Domain.Handlers
{
    public class RequestValidationHandler<TRequest> : RequestHandler<TRequest> where TRequest : CreateSendMailOrderCommand
    {
        public override TRequest Handle(TRequest command)
        {
            MessageType type;
            if (!Enum.TryParse(command.Type, out type))
            {
                throw new MailTypeIsUndefinedException(command.Type);
            }
            return base.Handle(command);
        }
    }
}
