using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class BookingExtensionSteps
    {
        private readonly PageFactory _pageFactory;

        public BookingExtensionSteps(PageFactory pageFactory)
        {
            _pageFactory = pageFactory;
        }

        
    }
}