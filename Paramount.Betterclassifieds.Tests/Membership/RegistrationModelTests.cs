using System;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Tests.Membership
{
    [TestFixture]
    public class RegistrationModelTests
    {
        [Test]
        public void GenerateUniqueUsername_EmailIsNull_ThrowsArgumentException()
        {
            // Arrange
            RegistrationModel model = new RegistrationModel();

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>( () => model.GenerateUniqueUsername(s => true) );
        }

        [Test]
        public void GenerateUniqueUsername_EmailHasValue_ReturnsSelf()
        {
            // Arrange
            RegistrationModel model = new RegistrationModel
            {
                Email = "foo@bar.com",
                FirstName = "hello",
                LastName = "world",
                PostCode = "3000"
            };
            // Act
            var result = model.GenerateUniqueUsername(s => false);

            // Assert
            result.IsTypeOf<RegistrationModel>();
            result.Username.IsEqualTo("foo");
        }

        [Test]
        public void Confirm_TokenMatches()
        {
            // Arrange
            RegistrationModel model = new RegistrationModel
            {
                Email = "foo@bar.com",
                FirstName = "hello",
                LastName = "world",
                PostCode = "3000",
            };

            // Act
            var result = model.GenerateToken().Confirm(model.Token);

            // Assert
            result.IsEqualTo(true);
            model.ConfirmationAttempts.IsNull();
            model.ConfirmationDate.IsNotNull();
            model.ConfirmationDateUtc.IsNotNull();
        }

        [Test]
        public void Confirm_Does_Not_Match()
        {
            // Arrange
            RegistrationModel model = new RegistrationModel
            {
                Email = "foo@bar.com",
                FirstName = "hello",
                LastName = "world",
                PostCode = "3000",
            };

            // Act
            model.ConfirmationAttempts.IsNull();
            var result = model.GenerateToken().Confirm(model.Token + "Bad Value");

            // Assert
            result.IsEqualTo(false);
            model.ConfirmationAttempts.IsEqualTo(1);
            model.ConfirmationDate.IsNull();
            model.ConfirmationDateUtc.IsNull();
        }

        [Test]
        public void SetConfirmationCode_SetsToken_And_Expiration()
        {
            // Arrange 
            var model = new RegistrationModel();
            var confirmationCodeResult = new ConfirmationCodeResult("1000", DateTime.Today, DateTime.UtcNow);
            
            // Act 
            model.SetConfirmationCode(confirmationCodeResult);

            // Assert
            model.Token.IsNotNull();
            model.ExpirationDate.IsNotNull();
            model.ExpirationDateUtc.IsNotNull();
        }
    }
}
