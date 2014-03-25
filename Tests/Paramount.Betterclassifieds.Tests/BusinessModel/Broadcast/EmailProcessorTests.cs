using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using System;

namespace Paramount.Betterclassifieds.Tests.BusinessModel.Broadcast
{
    [TestClass]
    public class EmailProcessorTests
    {
        [TestMethod]
        public void Send_AccountConfirmation_ReturnsTrue()
        {
            // Arrange
            var emailTemplate = new EmailTemplate
            {
                BodyTemplate = "Hello [/FirstName/] [/LastName/]",
                SubjectTemplate = "Hello [/FirstName/] [/LastName/]",
                FromAddress = "sender@fake.com",
                ParserName = "SquareBracketParser"
            };
            var broadcastRepository = new Mock<IBroadcastRepository>(MockBehavior.Strict);
            broadcastRepository.Setup(call => call.GetTemplateByName("NewRegistration")).Returns(emailTemplate);
            broadcastRepository.Setup(call => call.CreateOrUpdateEmail(It.IsAny<EmailDelivery>()));

            NewRegistration registration = new NewRegistration { FirstName = "Mister", LastName = "Fake" };
            var smtpMock = new Mock<ISmtpMailer>();
            smtpMock.Setup(call => call.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>()));

            // Act
            EmailProcessor processor = new EmailProcessor(broadcastRepository.Object, smtpMock.Object); // Inject manually for testing only
            var result = processor.Send(registration, Guid.NewGuid(), registration.ToPlaceholderDictionary(), to: "recipient@fake.com");

            // Assert
            broadcastRepository.Verify(a => a.GetTemplateByName(It.IsAny<string>()));
            broadcastRepository.Verify(call => call.CreateOrUpdateEmail(It.IsAny<EmailDelivery>()));
            smtpMock.Verify(call => call.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>()));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Send_MailerThrowsException_ReturnsFalse()
        {
            // Arrange
            var emailTemplate = new EmailTemplate
            {
                BodyTemplate = "Hello [/FirstName/] [/LastName/]",
                SubjectTemplate = "Hello [/FirstName/] [/LastName/]",
                FromAddress = "sender@fake.com",
                ParserName = "SquareBracketParser"
            };
            var broadcastRepository = new Mock<IBroadcastRepository>(MockBehavior.Strict);
            broadcastRepository.Setup(call => call.GetTemplateByName("NewRegistration")).Returns(emailTemplate);
            broadcastRepository.Setup(call => call.CreateOrUpdateEmail(It.IsAny<EmailDelivery>()));

            NewRegistration registration = new NewRegistration { FirstName = "Mister", LastName = "Fake" };
            var smtpMock = new Mock<ISmtpMailer>();
            smtpMock.Setup(call => call.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>()))
                .Throws<SmtpException>();

            // Act
            EmailProcessor processor = new EmailProcessor(broadcastRepository.Object, smtpMock.Object); // Inject manually for testing only
            var result = processor.Send(registration, Guid.NewGuid(), registration.ToPlaceholderDictionary(), to: "recipient@fake.com");

            // Assert
            broadcastRepository.Verify(a => a.GetTemplateByName(It.IsAny<string>()));
            broadcastRepository.Verify(call => call.CreateOrUpdateEmail(It.IsAny<EmailDelivery>()));
            smtpMock.Verify(call => call.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>()));
            
            Assert.IsFalse(result);
        }
    }
}
