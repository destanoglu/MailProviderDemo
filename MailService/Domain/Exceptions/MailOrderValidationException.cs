using System;

namespace Mail.Host.Domain.Exceptions
{
    public class MailOrderValidationException : Exception
    {
        public MailOrderValidationException(string error) 
            : base($"Given mail order is not valid. Error : {error}")
        {
        }
    }
}
