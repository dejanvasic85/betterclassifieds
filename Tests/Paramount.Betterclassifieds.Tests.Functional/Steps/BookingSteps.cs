using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class BookingSteps
    {
        private readonly PageFactory _pageFactory;
        
        public BookingSteps(PageFactory pageFactory)
        {
            _pageFactory = pageFactory;
        }

        [When(@"I submit a new Online Ad titled ""(.*)"" starting from today")]
        public void WhenISubmitANewOnlineAdTitledStartingFromToday(string adTitle)
        {
            var bookingStep1 = _pageFactory.NavigateToAndResolve<BookingStep1>();
            bookingStep1.SelectOnlineAdBooking();
            bookingStep1.Proceed();

            var bookingStep2 = _pageFactory.NavigateToAndResolve<BookingStep2>();
            bookingStep2.SelectParentCategory("Selenium Parent");
            bookingStep2.SelectSubCategory("Selenium Child");
            bookingStep2.Proceed();
        }
    }
}
