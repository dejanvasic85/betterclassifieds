using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Tests.Membership
{
    [TestClass]
    public class RegistrationModelTests
    {
        [TestMethod]
        public void GenerateUniqueUsername_EmailIsNull_ThrowsArgumentException()
        {
            // Arrange
            RegistrationModel model = new RegistrationModel();

            // Act
            // Assert
            Expect.Exception<ArgumentNullException>( () => model.GenerateUniqueUsername(s => true) );
        }

        [TestMethod]
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
