using System;

namespace Mail.Sender.Domain.Exceptions
{
    public class ExistingMailProviderRegisteredException : Exception
    {
        public ExistingMailProviderRegisteredException(string type) 
            : base($"An existing mail provider has already been registered for type {type}")
        {
        }
    }
}
