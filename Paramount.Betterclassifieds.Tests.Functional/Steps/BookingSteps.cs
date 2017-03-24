using System;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.ContextData;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using Paramount.Betterclassifieds.Tests.Functional.Pages.Booking;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    internal class BookingSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _repository;
        private readonly ContextData<AdBookingContext> _adContext;

        public BookingSteps(PageBrowser pageBrowser, ITestDataRepository repository, ContextData<AdBookingContext> adContext)
        {
            _pageBrowser = pageBrowser;
            _repository = repository;
            _adContext = adContext;
        }

        [Given(@"I start a new booking")]
        public void GivenIStartANewBooking()
        {
            _pageBrowser.GoTo<ApplicationPage>().PlaceNewAd();
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
                .WithStartDate(DateTime.Today.Date)
                .Proceed();
        }

        [When(@"the booking details are confirmed")]
        public void WhenTheBookingDetailsAreConfirmed()
        {
            var step3Page = _pageBrowser.Init<BookingStep3Page>();
            _adContext.Get().BookReference = step3Page.GetBookingReference();

            step3Page
                .AgreeToTermsAndConditions()
                .Proceed();
        }
        
        [When(@"I book an event ad titled ""(.*)"" starting from today and ticketing ""(.*)"" enabled")]
        public void WhenIBookAnEventAdTitledStartingFromToday(string title, string enableTicketingYesNot)
        {
            _pageBrowser.Init<BookingStep1Page>()
                .WithParentCategory(TestData.ParentEventCategory)
                .WithSubCategory(TestData.SubEventCategory)
                .Proceed();

            var ticketingEnabled = ArgumentParser.Is(enableTicketingYesNot);
            _pageBrowser.Init<BookingEventStep>()
                .WithAdDetails(title, description: title)
                .WithLocation("10 Melbourne Place, Melbourne, Victoria")
                .WithOrganiser("Mister Tee", "0433555999")
                .WithAdStartDateToday()
                .WithTicketingEnabled(ticketingEnabled)
                .Proceed();
        }

        [When(@"event is setup with 2 tickets and 2 fields")]
        public void WhenTicketingWithFieldsAreSetup()
        {
            _pageBrowser.Init<BookingEventTicketingStep>()
                   .AddTicketType("Child", 0, 50)
                   .AddTicketType("Adult", 0, 50)
                   .AddDynamicField("Gender", isRequired: false)
                   .AddDynamicField("Age", isRequired: false)
                   .Proceed();
        }
        
        [When(@"I notify my friend ""(.*)"" ""(.*)"" about my add")]
        public void WhenINotifyMyFriendAboutMyAdd(string fullName, string email)
        {
            _pageBrowser.Init<BookingCompletePage>()
                .AddFriend(fullName, email)
                .NotifyFriends();
        }

        [Then(@"the booking should be successful")]
        public void ThenTheBookingShouldBeSuccessful()
        {
            _pageBrowser.Init<BookingCompletePage>();

            // Go to the database and check whether the booking has been inserted
            var adContext = _adContext.Get();
            var adBookingContext = _repository.GetAdBookingContextByReference(adContext.BookReference);
            Assert.That(adBookingContext, Is.Not.Null);

            _adContext.Set(adBookingContext);
        }

        [When("checking out the ad on success page")]
        public void WhenCheckingOutTheAdOnSuccessPage()
        {
            _pageBrowser.Init<BookingCompletePage>().CheckoutAd();
        }
    }
}
