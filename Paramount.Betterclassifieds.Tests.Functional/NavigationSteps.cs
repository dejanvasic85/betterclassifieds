using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    [Binding]
    public class NavigationSteps
    {
        private readonly PageBrowser _pageBrowser;

        public NavigationSteps(PageBrowser pageBrowser)
        {
            _pageBrowser = pageBrowser;
        }

        [When(@"I navigate to relative url ""(.*)""")]
        public void WhenINavigateToRelativeUrl(string relativeUrl)
        {
            _pageBrowser.NavigateTo(relativeUrl);
        }

    }
}