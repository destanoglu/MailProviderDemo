using System;
using System.Collections.Generic;
using Mail.Sender.Adapters.MailProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mail.Sender.Ports.ProviderFactories;
using Mail.Shared.Contracts;
using Moq;

namespace Mail.UnitTests.Ports.ProviderFactories
{
    [TestClass]
    public class When_requesting_mail_providers
    {
        private MailProviderFactory factory;
        private readonly Mock<ISendMail> mockProvider1 = new Mock<ISendMail>();
        private readonly Mock<ISendMail> mockProvider2 = new Mock<ISendMail>();
        private readonly Mock<ISendMail> mockProvider3 = new Mock<ISendMail>();

        [TestInitialize]
        public void SetUp()
        {
            mockProvider1.Setup(x => x.AssociatedMailTypes)
                .Returns(new List<MessageType> {MessageType.LostPasswordMail});
            mockProvider2.Setup(x => x.AssociatedMailTypes)
                .Returns(new List<MessageType> { MessageType.OrderMail });
            mockProvider3.Setup(x => x.AssociatedMailTypes)
                .Returns(new List<MessageType> { MessageType.ShipmentMail });

            factory = new MailProviderFactory(new List<ISendMail>
            {
                mockProvider1.Object,
                mockProvider2.Object,
                mockProvider3.Object
            });
        }

        [TestMethod]
        public void It_should_return_correct_provider_for_loss_password_mail_type()
        {
            var provider = factory.GetMailProvider(MessageType.LostPasswordMail);
            Assert.IsTrue(provider.AssociatedMailTypes.Contains(MessageType.LostPasswordMail));
        }

        [TestMethod]
        public void It_should_return_correct_provider_for_order_mail_type()
        {
            var provider = factory.GetMailProvider(MessageType.OrderMail);
            Assert.IsTrue(provider.AssociatedMailTypes.Contains(MessageType.OrderMail));
        }

        [TestMethod]
        public void It_should_return_correct_provider_for_shipment_mail_type()
        {
            var provider = factory.GetMailProvider(MessageType.ShipmentMail);
            Assert.IsTrue(provider.AssociatedMailTypes.Contains(MessageType.ShipmentMail));
        }
    }
}
