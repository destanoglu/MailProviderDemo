using System;
using Paramore.Brighter;

namespace Mail.Host.Domain.Handlers.Attributes
{
    public class RequestValidationAttribute : RequestHandlerAttribute
    {
        public RequestValidationAttribute(int step, HandlerTiming timing = HandlerTiming.Before) : base(step, timing)
        {
        }

        public override object[] InitializerParams()
        {
            return new object[] {Timing};
        }

        public override Type GetHandlerType()
        {
            return typeof(RequestValidationHandler<>);
        }
    }
}
