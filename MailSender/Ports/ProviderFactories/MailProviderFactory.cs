using System.Collections.Generic;
using System.Linq;
using Mail.Sender.Adapters.MailProviders;
using Mail.Sender.Adapters.ProviderFactories;
using Mail.Sender.Domain.Exceptions;
using Mail.Shared.Contracts;

namespace Mail.Sender.Ports.ProviderFactories
{
    public class MailProviderFactory : IMailProviderFactory
    {
        private readonly IList<ISendMail> _providerList;

        public MailProviderFactory(IList<ISendMail> providers)
        {
            _providerList = providers;
        }

        public ISendMail GetMailProvider(MessageType type)
        {
            var selectedProvider = _providerList.FirstOrDefault(x => x.AssociatedMailTypes.Contains(type));
            if (selectedProvider == null)
            {
                throw new MailProviderRegistrationMissingException(type.ToString());
            }
            return selectedProvider;
        }
    }
}
