using System;
using Autofac;
using Paramore.Brighter;

namespace Mail.Host.Config
{
    public class MailServiceHandlerFactory : IAmAHandlerFactory
    {
        private readonly IComponentContext _context;
        public MailServiceHandlerFactory(IComponentContext context)
        {
            _context = context;
        }

        public IHandleRequests Create(Type handlerType)
        {
            return (IHandleRequests)_context.Resolve(handlerType);
        }

        public void Release(IHandleRequests handler)
        {
        }
    }
}