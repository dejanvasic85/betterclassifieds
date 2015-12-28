using System;
using System.Linq;
using System.Net.Mail;
using NUnit.Framework;
using Moq;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.Tests.Broadcast
{
    [TestFixture]
    public class EmailTests
    {
        [Test]
        public void BuildWithTemplate_EmailTemplateIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var registration = new NewRegistration();

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => Email.BuildWithTemplate(registration, null, new Guid(), "someone@somewhere.com"));
        }

        [Test]
        public void BuildWithTemplate_NewRegistration_CreatesDeliveryInstance()
        {
            // Arrange
            var registration = new NewRegistration { FirstName = "Mister", LastName = "Smith" };
            var template = new EmailTemplate
            {
                DocType = registration.DocumentType,
                BodyTemplate = "Hello [/FirstName/] [/LastName/]",
                SubjectTemplate = "Hello [/FirstName/] [/LastName/]",
                From = "blah@email.com"
            };


            // Act 
            var result = Email.BuildWithTemplate(registration, template, Guid.NewGuid(), "someone@somewhere.com");

            // Assert
            result.IsNotNull();
            result.Attempts.IsEqualTo(0);
            result.Body.IsEqualTo("Hello Mister Smith");
            result.Subject.IsEqualTo("Hello Mister Smith");
            result.DocType.IsEqualTo(registration.DocumentType);
            result.From.IsEqualTo("blah@email.com");
        }

        [Test]
        public void BuildWithTemplate_DefaultGuid_ThrowsArgumentException()
        {
            // Arrange
            var registration = new NewRegistration();
            var template = new EmailTemplate();

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Email.BuildWithTemplate(registration, template, new Guid(), "someone@somewhere.com"));
        }

        [Test]
        public void BuildWithTemplate_BadParserSupplied_ThrowsArgumentException()
        {
            // Arrange
            var registration = new NewRegistration();
            var template = new EmailTemplate("BadParser");

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Email.BuildWithTemplate(registration, template, Guid.NewGuid(), "someone@somewhere.com"),
                "'BadParser' is not a registered template parser");
        }

        [Test]
        public void BuildWithTemplate_NoRecipientSupplied_ThrowsArgumentException()
        {
            // Arrange
            var registration = new NewRegistration();
            var template = new EmailTemplate();

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Email.BuildWithTemplate(registration, template, Guid.NewGuid()));
        }

        [Test]
        public void Send_WithFakeMailer_PropertiesAreSet()
        {
            // Arrange
            Email email = new Email(Guid.NewGuid(),
                "Subject 123", "Body 123", false, "unknown-doctype", "sender@somewhere.com", "destination@somewhere.com");

            Mock<ISmtpMailer> mailer = new Mock<ISmtpMailer>(MockBehavior.Strict);
            mailer.Setup(call => call.SendEmail(email.Subject,
                email.Body, email.From, email.EmailAttachments.ToArray(), email.To));

            // Act - Assert
            email.Send(mailer.Object);
            email.Attempts.IsEqualTo(1);
            email.LastAttemptDate.IsNotNull();
            email.LastAttemptDateUtc.IsNotNull();
            email.SentDate.IsNotNull();
            email.SentDateUtc.IsNotNull();

            // Act - Assert
            email.Send(mailer.Object);
            email.Attempts.IsEqualTo(2);
        }

        [Test]
        public void LogException_PropertiesAreSet()
        {
            // Arrange
            Email email = new Email(Guid.NewGuid(),
                "Subject 123", "Body 123", false, "unknown-doctype", "sender@somewhere.com", "destination@somewhere.com");

            // Act
            email.LogException(new SmtpException(SmtpStatusCode.MailboxBusy, "Unable to send email"));
            email.LastErrorMessage.IsEqualTo("Unable to send email");
        }

        [Test]
        public void IsComplete_NoAttempts_ReturnsFalse()
        {
            // Arrange
            Email email = new Email(Guid.NewGuid(),
                "Subject 123", "Body 123", false, "unknown-doctype", "sender@somewhere.com", "destination@somewhere.com");

            // Act
            var result = email.IsComplete;

            // Assert
            result.IsFalse();
        }

        [Test]
        public void IsComplete_MaxAttemptsReachedWithEvilMailer_ReturnsTrue()
        {
            // Arrange
            Email email = new Email(Guid.NewGuid(),
                "Subject 123", "Body 123", false, "unknown-doctype", "sender@somewhere.com", "destination@somewhere.com");

            Mock<ISmtpMailer> evilMailer = new Mock<ISmtpMailer>(MockBehavior.Strict);
            evilMailer.Setup(call => call.SendEmail(email.Subject,
                email.Body,
                email.From,
                email.EmailAttachments.ToArray(),
                email.To)).Throws<SmtpException>();

            // Act - 3 times (max)
            email.Send(evilMailer.Object);
            email.Send(evilMailer.Object);
            email.Send(evilMailer.Object);
            var result = email.IsComplete;

            // Assert
            result.IsTrue();
            email.SentDate.IsNull();
        }

        [Test]
        public void IsComplete_MaxAttemptsNotReachedWithEvilMailer_ReturnsFalse()
        {
            // Arrange
            Email email = new Email(Guid.NewGuid(),
                "Subject 123", "Body 123", false, "unknown-doctype", "sender@somewhere.com", "destination@somewhere.com");

            Mock<ISmtpMailer> evilMailer = new Mock<ISmtpMailer>(MockBehavior.Strict);
            evilMailer.Setup(call => call.SendEmail(email.Subject,
                email.Body,
                email.From,
                email.EmailAttachments.ToArray(),
                email.To)).Throws<SmtpException>();

            // Act - 2 times ( less than max -3 )
            email.Send(evilMailer.Object);
            email.Send(evilMailer.Object);

            // Assert
            email.IsComplete.IsFalse();
            email.SentDate.IsNull();
        }


        [Test]
        public void IsComplete_GoodMailerSendsSuccessfully_ReturnsTrue()
        {
            // Arrange
            Email email = new Email(Guid.NewGuid(),
                "Subject 123", "Body 123", false, "unknown-doctype", "sender@somewhere.com", "destination@somewhere.com");

            Mock<ISmtpMailer> smtpMock = new Mock<ISmtpMailer>(MockBehavior.Strict);
            smtpMock.Setup(call => call.SendEmail(email.Subject,
                email.Body,
                email.From,
                email.EmailAttachments.ToArray(),
                email.To));

            // Act - 3 times (max)
            email.Send(smtpMock.Object);
            var result = email.IsComplete;

            // Assert
            result.IsTrue();
            email.SentDate.IsNotNull();
        }
    }
}
