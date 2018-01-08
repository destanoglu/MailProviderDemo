using System;
using Mail.Host.Domain.Commands;
using Mail.Host.Domain.Exceptions;
using Mail.Host.Domain.Handlers;
using Mail.Shared.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mail.UnitTests.Domain.Handlers
{
    [TestClass]
    public class When_validating_commands
    {
        private RequestValidationHandler<CreateSendMailOrderCommand> _validationHandler;
        private CreateSendMailOrderCommand _failedCommand;
        private CreateSendMailOrderCommand _successfullCommand;

        [TestInitialize]
        public void SetUp()
        {
            _validationHandler = new RequestValidationHandler<CreateSendMailOrderCommand>();
            _successfullCommand = new CreateSendMailOrderCommand("od", "ck", "body", MessageType.OrderMail.ToString(), DateTime.Now);
            _failedCommand = new CreateSendMailOrderCommand("od", "ck", "body", "NotExistingMailType", DateTime.Now);
        }

        [TestMethod]
        public void it_should_successfully_validate_a_valid_command()
        {
            _validationHandler.Handle(_successfullCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(MailTypeIsUndefinedException))]
        public void it_should_not_validate_a_command_with_undefined_mail_type()
        {
            _validationHandler.Handle(_failedCommand);
        }
    }
}
