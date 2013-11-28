using System;
using System.Collections.Generic;
using System.IO;
using BetterClassified;
using BetterClassified.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;

namespace Paramount.Betterclassifieds.Tests
{
    [TestClass]
    public class TheMusicHtmlScraperTests
    {
        // This is the item we always expect to be in the list! Otherwise some has been naughty and changed theMusic website menu
        private const string ClassiesItemKey = "Classies";
        readonly MockRepository repository = new MockRepository(MockBehavior.Strict);

        [TestMethod]
        [DeploymentItem("Artefacts\\TheMusicValidHtml.html")]
        public void ParseMenuItems_ValidHtml_ReturnsDictionary()
        {
            string html = File.ReadAllText("TheMusicValidHtml.html");
            const string baseUrl = "http://classies.themusic.com.au";
            const string publisherUrl = "http://themusic.com.au";

            var mockConfig = repository.Create<BetterClassified.Repository.IConfigSettings>();
            mockConfig.Setup(prop => prop.BaseUrl).Returns(baseUrl).Verifiable();
            mockConfig.Setup(prop => prop.PublisherHomeUrl).Returns(publisherUrl).Verifiable();
            
            // Create the parser and inject!
            var theMusicMenu = new TheMusicHtmlScraper(html, mockConfig.Object);

            Dictionary<string, string> result = theMusicMenu.GetMenuItemPairs();

            Assert.IsNotNull(result);
            Assert.AreEqual(13, result.Count);
            Assert.IsTrue(result.ContainsKey(ClassiesItemKey));
        }
    }
}
