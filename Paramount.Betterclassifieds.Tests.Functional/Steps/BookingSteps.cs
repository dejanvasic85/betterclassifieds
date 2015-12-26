using System;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
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

            _pageBrowser.Init<BookingEventStep>()
                .WithAdDetails(title, description: title)
                .WithLocation("10 Melbourne Place, Melbourne, Victoria")
                .WithOrganiser("Mister Tee", "0433555999")
                .WithAdStartDateToday()
                .WithTicketingEnabled(ArgumentParser.Is(enableTicketingYesNot))
                .Proceed();

            var page3 = _pageBrowser.Init<BookingStep3Page>();
            _adContext.Get().BookReference = page3.GetBookingReference();

            page3.AgreeToTermsAndConditions()
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

        [Then(@"my friends email ""(.*)"" should receive the notification")]
        public void ThenMyFriendsEmailShouldReceiveTheNotification(string email)
        {
            var emails = _repository.GetSentEmailsFor(email);
            Assert.That(emails.Count, Is.GreaterThan(0));
        }
    }
}
