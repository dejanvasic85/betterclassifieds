using NUnit.Framework;
using System.IO;

namespace Paramount.Betterclassifieds.Tests.TheMusicHtmlScraper
{
    [TestFixture]
    public class TheMusicHtmlScraperTests
    {
        private string GetContent(string fileName)
        {
            return File.ReadAllText("TheMusicHtmlScraper\\" + fileName);
        }

        [Test]
        public void ParseMenuItems_Real_20140823_Success()
        {
            // arrange
            var fileContent = GetContent("theMusic20140823.html");

            const string httpFakehomeurl = "http://fakeHomeUrl";

            var scraper = new BetterClassified.Repository.TheMusicHtmlScraper(fileContent, httpFakehomeurl);

            // Act
            var menuItems = scraper.ParseMenuItems();

            // Assert
            menuItems.IsNotNull();
            menuItems.Count.IsLargerThan(0);
        }
    }
}
