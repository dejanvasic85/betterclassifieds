using System;
using System.Collections.Generic;
using BetterClassified.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Paramount.Betterclassifieds.Tests
{
    [TestClass]
    public class TheMusicMenuTests
    {
        // This is the item we always expect to be in the list! Otherwise some has been naughty and changed theMusic website menu
        private const string ClassiesItemKey = "Classies";
        readonly MockRepository repository = new MockRepository(MockBehavior.Strict);

        [TestMethod]
        public void ParseMenuItems_ValidHtml_ReturnsDictionary()
        {
            const string html = "<goodhtmlcomeshere>";
            const string baseUrl = "http://randomBaseUrl";

            var mockConfig = repository.Create<BetterClassified.Repository.IConfigSettings>();
            mockConfig.Setup(prop=>prop.BaseUrl).Returns(baseUrl).Verifiable();

            // Create the parser and inject!
            var theMusicMenu = new TheMusicMenuParser(html, mockConfig.Object);

            Dictionary<string, string> result = theMusicMenu.GetMenuItemPairs();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.ContainsKey(ClassiesItemKey));

            string parsedBaseUrl;
            Assert.IsTrue(result.TryGetValue(ClassiesItemKey, out parsedBaseUrl));
            Assert.AreEqual(baseUrl, parsedBaseUrl);
        }
    }
}
