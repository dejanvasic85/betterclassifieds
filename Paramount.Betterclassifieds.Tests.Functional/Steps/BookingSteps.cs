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
            _pageBrowser.GoTo<BookingStep1Page>()
                .WithParentCategory(TestData.ParentCategory)
                .WithSubCategory(TestData.SubCategory)
                .Proceed();

            _pageBrowser.Init<BookingStep2Page>()
                .WithOnlineHeader(adTitle)
                .WithOnlineDescription(adTitle)
                .Proceed();

            _pageBrowser.Init<BookingStep3Page>()
                .WithStartDate(DateTime.Today.Date)
                .Proceed();

            _pageBrowser.Init<BookingStep4Page>()
                .AgreeToTermsAndConditions()
                .Proceed();
        }

        [Then(@"the booking should be successful")]
        public void ThenTheBookingShouldBeSuccessful()
        {
            // Just confirm that we are on the success page
            var bookingCompletePage = _pageBrowser.Init<BookingCompletePage>();
            Assert.IsTrue(bookingCompletePage.IsDisplayed());
        }
    }
}
