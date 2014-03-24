using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.Tests.BusinessModel.Broadcast
{
    [TestClass]
    public class PlaceholderExtensionsTests
    {
        [TestMethod]
        public void ToPlaceholderDictionary_ReturnsDictionary()
        {
            // Arrange a class that uses placeholder attribute
            AccountConfirmation broadcast = new AccountConfirmation
            {
                FirstName = "Foo",
                LastName = "Bar"
            };

            // Act
            var result = broadcast.ToPlaceholderDictionary();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(result["FirstName"], "Foo");
            Assert.AreEqual(result["LastName"], "Bar");
        }
        
        [TestMethod]
        public void ToPlaceholderDictionary_WithDifferentNames_ReturnsDictionary()
        {
            // Arrange a class that uses placeholder attribute
            PropertyNamesAreDifferentToPlaceholders target
                = new PropertyNamesAreDifferentToPlaceholders
                {
                    Location = "Somewhere",
                    Cost = "100"
                };

            // Act
            var result = target.ToPlaceholderDictionary();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(result["Where"], "Somewhere");
            Assert.AreEqual(result["Amount"], "100");
        }


        private class PropertyNamesAreDifferentToPlaceholders
        {
            [Placeholder("Where")]
            public string Location { get; set; }

            [Placeholder("Amount")]
            public string Cost { get; set; }
        }
    }
}
