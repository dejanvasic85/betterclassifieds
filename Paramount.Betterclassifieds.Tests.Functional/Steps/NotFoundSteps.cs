using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class NotFoundSteps
    {
        private readonly PageBrowser _pageBrowser;

        public NotFoundSteps(PageBrowser pageBrowser)
        {
            _pageBrowser = pageBrowser;
        }

        [Given(@"I navigate to a page that does not exist")]
        public void GivenINavigateToAPageThatDoesNotExist()
        {
            _pageBrowser.NavigateTo("/something/ThatDoes/NotExist");
        }

        [Then(@"I should see a not found page")]
        public void ThenIShouldSeeANotFoundPage()
        {
            var page = _pageBrowser.Init<NotFoundTestPage>();
            Assert.That(page.GetHeadingText(), Is.EqualTo("Oooops. It's a 404! The page you are looking for does not exist."));
            Assert.That(page.GetDescriptionText(), Is.EqualTo("Say what? This is a very common problem on the internet that appears when something you have clicked on or provided an address to something that seems to have moved or never existed..."));
        }
    }
}