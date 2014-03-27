using System;
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
    }
}
