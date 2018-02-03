using System;
using System.Threading;
using System.Threading.Tasks;
using Mail.Host.Domain.Commands;
using Mail.Host.Domain.Handlers;
using Mail.Shared.Contracts;
using MassTransit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Mail.UnitTests.Domain.Handlers
{
    [TestClass]
    public class When_handling_send_mail_order_command
    {
        private readonly Mock<IBusControl> busControl = new Mock<IBusControl>();
        private CreateSendMailOrderCommandHandler handler;
        private CreateSendMailOrderCommand _successfullCommand;

        [TestInitialize]
        public void SetUp()
        {
            busControl.Setup(
                x => 
                x.Publish(It.IsAny<IMailOrder>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(""));

            handler = new CreateSendMailOrderCommandHandler(busControl.Object);
            _successfullCommand = new CreateSendMailOrderCommand("od", "ck", "body", MessageType.OrderMail.ToString(), DateTime.Now);
        }

        [TestMethod]
        public void it_should_successfully_execute_a_valid_command()
        {
            handler.Handle(_successfullCommand);
        }
    }
}
