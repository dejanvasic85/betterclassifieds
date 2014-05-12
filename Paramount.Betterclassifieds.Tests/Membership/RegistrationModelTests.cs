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
    }
}
