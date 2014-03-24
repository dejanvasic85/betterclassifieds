using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Tests.BusinessModel.Broadcast
{
    [TestClass]
    public class AccountConfirmationTests
    {
        [TestMethod]
        public void GetPlaceholders_ForAccountConfirmation_ReturnsDictionary()
        {
            // Arrange
            AccountConfirmationBroadcast broadcast = new AccountConfirmationBroadcast
            {
                FirstName = "Foo",
                LastName = "Bar"
            };

            // Act
            var result = broadcast.GetPlaceholders();

            // Assert
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result["FirstName"], "Foo");
            Assert.AreEqual(result["LastName"], "Bar");
        }
    }
}
