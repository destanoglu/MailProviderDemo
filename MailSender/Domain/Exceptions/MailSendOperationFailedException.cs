using System;

namespace Mail.Sender.Domain.Exceptions
{
    public class MailSendOperationFailedException : Exception
    {
        public MailSendOperationFailedException(string sender, string receiver) 
            : base($"Mail, that was sent from {sender} to {receiver} is failed at {DateTime.Now.ToString("G")}")
        {
        }
    }
}
