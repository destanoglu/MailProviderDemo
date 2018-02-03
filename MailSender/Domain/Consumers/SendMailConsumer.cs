using System.Threading.Tasks;
using Common.Logging;
using Mail.Sender.Adapters.ProviderFactories;
using Mail.Shared.Contracts;
using MassTransit;

namespace Mail.Sender.Domain.Consumers
{
    public class SendMailConsumer : IConsumer<IMailContent>
    {
        private readonly IMailProviderFactory _factory;
        private readonly ILog _logger;

        public SendMailConsumer(IMailProviderFactory providerFactory, ILog logger)
        {
            _factory = providerFactory;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IMailContent> context)
        {
            var mailProvider = _factory.GetMailProvider(context.Message.Type);
            mailProvider.SendMail(context.Message);
            return context.CompleteTask;
        }
    }
}
