using System;
using System.Linq;
using Mail.Host.Domain.Commands;
using Mail.Host.Domain.Exceptions;
using Paramore.Brighter;

namespace Mail.Host.Domain.Handlers
{
    public class RequestValidationHandler<TRequest> : RequestHandler<TRequest> where TRequest : BaseCommand, IValidatable
    {
        public override TRequest Handle(TRequest command)
        {
            var errors = command.Validate().ToList();

            if (errors.Any())
            {
                throw new MailOrderValidationException(string.Join(Environment.NewLine, errors));
            }
                
            return base.Handle(command);
        }
    }
}
