using System;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
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
            var bookingStep1 = _pageFactory.NavigateToAndInit<OnlineBookingStep1TestPage>();
            bookingStep1.SelectOnlineAdBooking();
            bookingStep1.Proceed();

            var bookingStep2 = _pageFactory.Init<OnlineBookingStep2TestPage>();
            bookingStep2.SelectParentCategory(TestData.ParentCategory);
            bookingStep2.SelectSubCategory(TestData.SubCategory);

            var bookingStep3 = _pageFactory.Init<OnlineBookingStep3TestPage>();
            bookingStep3.FillOnlineHeader(adTitle);
            bookingStep3.FillOnlineDescription(adTitle);
            bookingStep3.Proceed();

            var bookingStep4 = _pageFactory.Init<OnlineBookingStep4TestPage>();
            bookingStep4.SelectOnlineStartDate(DateTime.Today.Date);
            bookingStep4.Proceed();

            var bookingStep5 = _pageFactory.Init<OnlineBookingStep5TestPage>();
            bookingStep5.AgreeToTermsAndConditions();
            bookingStep5.Proceed();
        }

        [When(@"I submit a new Bundled Ad titled ""(.*)"" starting from the next edition")]
        public void WhenISubmitANewBundledAdTitledStartingFromTheNextEdition(string adTitle)
        {
            var bookingStep1 = _pageFactory.NavigateToAndInit<OnlineBookingStep1TestPage>();
            bookingStep1.SelectBundleBooking();
            bookingStep1.Proceed();

            var bookingStep2 = _pageFactory.Init<OnlineBookingStep2TestPage>();
            bookingStep2.SelectParentCategory(TestData.ParentCategory);
            bookingStep2.SelectSubCategory(TestData.SubCategory);
            bookingStep2.SelectSeleniumPublication();
            bookingStep2.Proceed();

            ScenarioContext.Current.Pending();
        }

        [Then(@"the booking should be successful")]
        public void ThenTheBookingShouldBeSuccessful()
        {
            var bookingCompletePage = _pageFactory.Init<BookingCompleteTestPage>();
            Assert.IsTrue(bookingCompletePage.IsDisplayed());
        }
    }
}
