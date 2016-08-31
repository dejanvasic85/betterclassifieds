using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
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
            _pageBrowser.NavigateTo("something/ThatDoes/NotExist");
        }

        [Then(@"I should see a not found page")]
        public void ThenIShouldSeeANotFoundPage()
        {
            

#if DEBUG
            Assert.That(_pageBrowser.IsAspNotFoundDisplayed(), Is.EqualTo(true));
#else
{
            var page = _pageBrowser.Init<NotFoundTestPage>();
           Assert.That(page.GetHeadingText(), Is.EqualTo("404 Not Found"));
           Assert.That(page.GetDescriptionText(), Is.EqualTo("A 404 error status implies that the file or page that you're looking for could not be found.")); 
}
#endif
        }
    }
}