using System;

namespace Mail.Sender.Domain.Exceptions
{
    public class MailProviderRegistrationMissingException : Exception
    {
        public MailProviderRegistrationMissingException(string type) 
            : base($"There is no registered mail provider exist for {type}")
        {
        }
    }
}
