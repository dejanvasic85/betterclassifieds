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
        private readonly ITestDataRepository _repository;

        public BookingSteps(PageBrowser pageBrowser, ITestDataRepository repository)
        {
            _pageBrowser = pageBrowser;
            _repository = repository;
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

            var bookingStep4Page = _pageBrowser.Init<BookingStep4Page>();
            ScenarioContext.Current["BookingReference"] = bookingStep4Page.GetBookingReference();
            bookingStep4Page
                .AgreeToTermsAndConditions()
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
