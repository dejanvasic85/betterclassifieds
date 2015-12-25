using System;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class BookingSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _repository;

        public BookingSteps(PageBrowser pageBrowser, ITestDataRepository repository)
        {
            _pageBrowser = pageBrowser;
            _repository = repository;
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

            var bookingStep4Page = _pageBrowser.Init<BookingStep3Page>();
            ScenarioContext.Current["BookingReference"] = bookingStep4Page.GetBookingReference();
            
            bookingStep4Page
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

            _pageBrowser.Init<BookingStepEventDetails>()
                .WithAdDetails(title, description: title)
                .WithLocation("10 Melbourne Place, Melbourne, Victoria")
                .WithOrganiser("Mister Tee", "0433555999")
                .WithAdStartDateToday()
                .WithTicketingEnabled(ArgumentParser.Is(enableTicketingYesNot))
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
            // Go to the database and check whether the booking has been inserted
            var bookingReference = ScenarioContext.Current["BookingReference"].ToString();
            var result = _repository.IsAdBookingCreated(bookingReference);
            Assert.That(result, Is.True);
        }

        [Then(@"my friends email ""(.*)"" should receive the notification")]
        public void ThenMyFriendsEmailShouldReceiveTheNotification(string email)
        {
            var emails = _repository.GetSentEmailsFor(email);
            Assert.That(emails.Count, Is.GreaterThan(0));
        }
    }
}
