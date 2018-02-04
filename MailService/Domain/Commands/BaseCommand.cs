using System;
using Paramore.Brighter;

namespace Mail.Host.Domain.Commands
{
    public abstract class BaseCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
