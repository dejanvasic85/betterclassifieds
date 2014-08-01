using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class BookingExtensionSteps
    {
        private readonly PageBrowser _pageBrowser;

        public BookingExtensionSteps(PageBrowser pageBrowser)
        {
            _pageBrowser = pageBrowser;
        }

        
    }
}