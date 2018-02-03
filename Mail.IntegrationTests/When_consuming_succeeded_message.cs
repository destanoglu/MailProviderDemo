using System;
using GreenPipes;
using Mail.IntegrationTests.Consumer;
using Mail.IntegrationTests.Data;
using Mail.IntegrationTests.Exceptions;
using Mail.IntegrationTests.Helper;
using MassTransit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mail.IntegrationTests
{
    [TestClass]
    public class When_consuming_succeeded_message
    {
        private IBusControl _busController;
        private int _retryCount;
        private string _testing_queue;
        private TestingConsumer _consumer;
        private TestData _testData;
        private QueueManager _queueManager;

        [TestInitialize]
        public void TestInitialize()
        {
            var hostAddr = "192.168.99.100";
            var userName = "sa";
            var pass = "Sa123456";
            _queueManager = new QueueManager(hostAddr, userName, pass);
            _testData = new TestData(5, "testing");
            _consumer = new TestingConsumer(failure: false, handling: false);
            _testing_queue = "mail_test";
            _retryCount = 5;


            _busController = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://" + hostAddr), h =>
                {
                    h.Username(userName);
                    h.Password(pass);
                });

                cfg.ReceiveEndpoint(host, _testing_queue, e =>
                {
                    e.UseRetry(retryConfig =>
                    {
                        retryConfig.Handle<HandledException>();
                        retryConfig.Ignore<IgnoredException>();
                        retryConfig.Interval(_retryCount, TimeSpan.FromMilliseconds(10));
                    });
                    e.Consumer(() => _consumer);
                });
            });

            _busController.Start();

            _busController.Publish<IData>(new TestData(_testData));

            Wait.For(TimeSpan.FromSeconds(5)).Until(() => _consumer.Count > 0);
        }

        [TestMethod]
        public void It_should_consume_one_message()
        {
            Assert.IsTrue(_consumer.Count == 1);
        }

        [TestMethod]
        public void It_should_consume_the_message_correctly()
        {
            Assert.IsTrue(_testData.Equals(_consumer.TestData));
        }

        [TestCleanup]
        public void CleanUp()
        {
            _busController.Stop();
            _queueManager.ClearQueue(_testing_queue);
        }
    }
}
