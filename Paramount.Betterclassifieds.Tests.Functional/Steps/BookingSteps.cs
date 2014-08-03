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
        private readonly PageBrowser _pageBrowser;

        public BookingSteps(PageBrowser pageBrowser)
        {
            _pageBrowser = pageBrowser;
        }

        [When(@"I submit a new Online Ad titled ""(.*)"" starting from today")]
        public void WhenISubmitANewOnlineAdTitledStartingFromToday(string adTitle)
        {
            var bookingStep1 = _pageBrowser.GoTo<BookingStep1TestPage>();
            bookingStep1.SelectOnlineAdBooking();
            bookingStep1.Proceed();
            var bookingStep2 = _pageBrowser.Init<BookingStep2TestPage>();
            bookingStep2.SelectParentCategory(TestData.ParentCategory);
            bookingStep2.SelectSubCategory(TestData.SubCategory);
            bookingStep2.Proceed();

            var bookingStep3 = _pageBrowser.Init<BookingStep3TestPage>();
            bookingStep3.FillOnlineHeader(adTitle);
            bookingStep3.FillOnlineDescription(adTitle);
            bookingStep3.Proceed();

            var bookingStep4 = _pageBrowser.Init<BookingStep4TestPage>();
            bookingStep4.SelectOnlineStartDate(DateTime.Today.Date);
            bookingStep4.Proceed();

            var bookingStep5 = _pageBrowser.Init<BookingStep5TestPage>();
            bookingStep5.AgreeToTermsAndConditions();
            bookingStep5.Proceed();
        }

        [When(@"I submit a new Bundled Ad titled ""(.*)"" starting from the next edition")]
        public void WhenISubmitANewBundledAdTitledStartingFromTheNextEdition(string adTitle)
        {
            var bookingStep1 = _pageBrowser.GoTo<BookingStep1TestPage>();
            bookingStep1.SelectBundleBooking();
            bookingStep1.Proceed();

            var bookingStep2 = _pageBrowser.Init<BookingStep2TestPage>();
            bookingStep2.SelectParentCategory(TestData.ParentCategory);
            bookingStep2.SelectSubCategory(TestData.SubCategory);
            bookingStep2.SelectSeleniumPublication();
            bookingStep2.Proceed();

            var bookingStep3 = _pageBrowser.Init<BookingStep3TestPage>();
            bookingStep3.FillLineAdHeader(adTitle);
            bookingStep3.FillLineAdDescription(adTitle);
            bookingStep3.FillOnlineHeader(adTitle);
            bookingStep3.FillOnlineDescription(adTitle);
            bookingStep3.Proceed();

            var bookingStep4 = _pageBrowser.Init<BookingStep4TestPage>();
            bookingStep4.SelectFirstEditionDate();
            bookingStep4.SelectInsertionCount(5);
            bookingStep4.Proceed();

            var bookingStep5 = _pageBrowser.Init<BookingStep5TestPage>();
            bookingStep5.AgreeToTermsAndConditions();
            bookingStep5.Proceed();
        }

        [Then(@"the booking should be successful")]
        public void ThenTheBookingShouldBeSuccessful()
        {
            var bookingCompletePage = _pageBrowser.Init<BookingCompleteTestPage>();
            Assert.IsTrue(bookingCompletePage.IsDisplayed());
        }
    }
}
