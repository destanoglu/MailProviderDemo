using System;

namespace Mail.Host.Domain.Exceptions
{
    public class MailTypeIsUndefinedException : Exception
    {
        public MailTypeIsUndefinedException(string type) 
            : base($"Given mail type {type} is not defined")
        {
        }
    }
}
