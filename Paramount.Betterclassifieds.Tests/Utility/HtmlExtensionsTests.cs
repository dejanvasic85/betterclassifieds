using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests.Utility
{
    [TestFixture]
    public class HtmlExtensionsTests
    {
        [Test]
        public void FromHtmlToPlaintext_ValidHtml_ReturnsPlaintext()
        {
            // arrange
            var htmlText = "<h3>No Idea...</h3><p><strong>who man</strong></p><p><em>why does this not work</em></p>";

            // act
            var result = htmlText.FromHtmlToPlaintext();

            Assert.That(result, Is.EqualTo("No Idea... who man why does this not work"));
        }
    }
}