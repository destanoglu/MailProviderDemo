using System.Threading.Tasks;
using Mail.IntegrationTests.Data;
using Mail.IntegrationTests.Exceptions;
using MassTransit;

namespace Mail.IntegrationTests.Consumer
{
    public class TestingConsumer : IConsumer<IData>
    {
        public int Count { get; set; }
        public TestData TestData { get; set; }

        private readonly bool _failure;
        private readonly bool _handling;

        public TestingConsumer(bool failure = false, bool handling = true)
        {
            _failure = failure;
            _handling = handling;
            Count = 0;
        }

        public Task Consume(ConsumeContext<IData> context)
        {
            ++Count;

            if (_failure)
            {
                if (_handling)
                {
                    throw new HandledException();
                }
                throw new IgnoredException();
            }

            TestData = new TestData(context.Message.Value1, context.Message.Value2);
            return context.CompleteTask;
        }
    }
}
