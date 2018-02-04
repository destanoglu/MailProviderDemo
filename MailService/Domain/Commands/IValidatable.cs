using System.Collections.Generic;

namespace Mail.Host.Domain.Commands
{
    public interface IValidatable
    {
        IEnumerable<string> Validate();
    }
}
