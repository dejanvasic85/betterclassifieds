using System;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.Tests.BusinessModel.Broadcast
{
    [TestClass]
    public class EmailDeliveryTests
    {
        [TestMethod]
        public void BuildWithTemplate_EmailTemplateIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var registration = new NewRegistration();

            // Act
            // Assert
            Expect.Exception<ArgumentNullException>(() => EmailDelivery.BuildWithTemplate(registration, null, new Guid(), "someone@somewhere.com"));
        }

        [TestMethod]
        public void BuildWithTemplate_NewRegistration_CreatesDeliveryInstance()
        {
            // Arrange
            var registration = new NewRegistration { FirstName = "Mister", LastName = "Smith" };
            var template = new EmailTemplate
            {
                DocType = registration.DocumentType,
                BodyTemplate = "Hello [/FirstName/] [/LastName/]",
                SubjectTemplate = "Hello [/FirstName/] [/LastName/]",
                FromAddress = "blah@email.com"
            };


            // Act 
            var result = EmailDelivery.BuildWithTemplate(registration, template, Guid.NewGuid(), "someone@somewhere.com");

            // Assert
            result.IsNotNull();
            result.Attempts.IsEqualTo(0);
            result.Body.IsEqualTo("Hello Mister Smith");
            result.Subject.IsEqualTo("Hello Mister Smith");
            result.DocType.IsEqualTo(registration.DocumentType);
            result.From.IsEqualTo("blah@email.com");
        }

        [TestMethod]
        public void BuildWithTemplate_DefaultGuid_ThrowsArgumentException()
        {
            // Arrange
            var registration = new NewRegistration();
            var template = new EmailTemplate();

            // Act
            // Assert
            Expect.Exception<ArgumentException>(() => EmailDelivery.BuildWithTemplate(registration, template, new Guid(), "someone@somewhere.com"));
        }

        [TestMethod]
        public void BuildWithTemplate_BadParserSupplied_ThrowsArgumentException()
        {
            // Arrange
            var registration = new NewRegistration();
            var template = new EmailTemplate("BadParser");

            // Act
            // Assert
            Expect.Exception<ArgumentException>(() => EmailDelivery.BuildWithTemplate(registration, template, Guid.NewGuid(), "someone@somewhere.com"),
                "'BadParser' is not a registered template parser");
        }

        [TestMethod]
        public void BuildWithTemplate_NoRecipientSupplied_ThrowsArgumentException()
        {
            // Arrange
            var registration = new NewRegistration();
            var template = new EmailTemplate();

            // Act
            // Assert
            Expect.Exception<ArgumentException>(() => EmailDelivery.BuildWithTemplate(registration, template, Guid.NewGuid()));
        }

        [TestMethod]
        public void IncrementAttempts_PropertiesAreSet()
        {
            // Arrange
            EmailDelivery delivery = new EmailDelivery(Guid.NewGuid(),
                "Subject 123", "Body 123", false, "unknown-doctype", "sender@somewhere.com", "destination@somewhere.com");

            // Act - Assert
            delivery.IncrementAttempts();
            delivery.Attempts.IsEqualTo(1);
            delivery.LastAttemptDate.IsNotNull();
            delivery.LastAttemptDateUtc.IsNotNull();

            // Act - Assert
            delivery.IncrementAttempts();
            delivery.Attempts.IsEqualTo(2);
            delivery.LastAttemptDate.IsNotNull();
            delivery.LastAttemptDateUtc.IsNotNull();
        }

        [TestMethod]
        public void LogException_PropertiesAreSet()
        {
            // Arrange
            EmailDelivery delivery = new EmailDelivery(Guid.NewGuid(),
                "Subject 123", "Body 123", false, "unknown-doctype", "sender@somewhere.com", "destination@somewhere.com");

            // Act
            delivery.LogException(new SmtpException(SmtpStatusCode.MailboxBusy, "Unable to send email"));
            delivery.LastExceptionMessage.IsEqualTo("Unable to send email");
        }

        [TestMethod]
        public void LogException_MarkAsSent()
        {
            // Arrange
            EmailDelivery delivery = new EmailDelivery(Guid.NewGuid(),
                "Subject 123", "Body 123", false, "unknown-doctype", "sender@somewhere.com", "destination@somewhere.com");

            // Act
            // Assert
            delivery.SentDate.IsNull("Sent date should be null when object is first created");
            delivery.SentDateUtc.IsNull("Sent date should be null when object is first created");
            delivery.MarkAsSent();
            delivery.SentDate.IsNotNull();
            delivery.SentDateUtc.IsNotNull();
        }
    }
}
