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
            var bookingStep1 = _pageFactory.NavigateToAndInit<OnlineBookingStep1Page>();
            bookingStep1.SelectOnlineAdBooking();
            bookingStep1.Proceed();

            var bookingStep2 = _pageFactory.Init<OnlineBookingStep2Page>();
            bookingStep2.SelectParentCategory("Selenium Parent");
            bookingStep2.SelectSubCategory("Selenium Child");
            bookingStep2.Proceed();

            var bookingStep3 = _pageFactory.Init<OnlineBookingStep3Page>();
            bookingStep3.FillOnlineHeader(adTitle);
            bookingStep3.FillOnlineDescription(adTitle);
        }
    }
}
