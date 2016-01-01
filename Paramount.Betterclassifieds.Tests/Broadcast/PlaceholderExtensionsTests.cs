using System;
using System.Collections.Generic;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.Tests.Broadcast
{
    [TestFixture]
    public class PlaceholderExtensionsTests
    {
        [Test]
        public void ToPlaceholderDictionary_ReturnsDictionary()
        {
            // Arrange a class that uses placeholder attribute
            NewRegistration broadcast = new NewRegistration
            {
                FirstName = "Foo",
                LastName = "Bar"
            };

            // Act
            var result = broadcast.ToPlaceholderDictionary();

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(result["FirstName"], "Foo");
            Assert.AreEqual(result["LastName"], "Bar");
        }

        [Test]
        public void ToPlaceholderDictionary_NullProperties_ReturnsDictionaryWithEmptyValues()
        {
            // Arrange a class that uses placeholder attribute
            NewRegistration broadcast = new NewRegistration
            {
                FirstName = null,
                LastName = null
            };

            // Act
            var result = broadcast.ToPlaceholderDictionary();

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(result["FirstName"], string.Empty);
            Assert.AreEqual(result["LastName"], string.Empty);
        }

        [Test]
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


        private class PropertyNamesAreDifferentToPlaceholders : IDocType
        {
            [Placeholder("Where")]
            public string Location { get; set; }

            [Placeholder("Amount")]
            public string Cost { get; set; }

            public string DocumentType { get { return "Fake"; } }
            public IList<EmailAttachment> Attachments { get; set; }
        }
    }
}
