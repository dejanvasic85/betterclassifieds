using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Features.BookEventTickets;
using Paramount.Betterclassifieds.Tests.Functional.Features.Events;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Features.EventInvitation
{
    [Binding]
    internal class BookTicketsFromInviteSteps
    {
        private readonly ContextData<EventAdContext> _eventContextData;
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _repository;

        public BookTicketsFromInviteSteps(ContextData<EventAdContext> eventContextData, PageBrowser pageBrowser, ITestDataRepository repository)
        {
            _eventContextData = eventContextData;
            _pageBrowser = pageBrowser;
            _repository = repository;
        }

        [Given(@"the event has an invite for ""(.*)"" with email ""(.*)""")]
        public void GivenTheEventHasAnInviteForWithEmail(string fullName, string email)
        {
            var eventContextData = _eventContextData.Get();

            var userNetworkId = _repository.AddUserNetworkIfNotExists(TestData.DefaultUsername, email, fullName);
            eventContextData.EventInvitationId = _repository.AddEventInvitationIfNotExists(eventContextData.EventId, userNetworkId);
        }

        [When(@"I navigate to the page for the new invitaton")]
        public void WhenINavigateToThePageForTheNewInvitaton()
        {
            _pageBrowser.GoTo<EventInvitePage>(_eventContextData.Get().EventInvitationId);
        }

        [When(@"selecting to purchase the ""(.*)"" ticket")]
        public void WhenSelectingToPurchaseTheVIPTicket(string ticketName)
        {
            var eventInvitePage = _pageBrowser.Init<EventInvitePage>(query: _eventContextData.Get().EventInvitationId);

            eventInvitePage.SelectTicket(ticketName);
        }

        [Then(@"It should display the invitation page with event title ""(.*)""")]
        public void ThenItShouldDisplayTheInvitationPageWithEventTitle(string expectedEventName)
        {
            var page = _pageBrowser.Init<EventInvitePage>(query: _eventContextData.Get().EventInvitationId);

            Assert.That(page.GetEventName(), Is.EqualTo(expectedEventName));
        }
        
        [Then(@"I should be on the book tickets page")]
        public void ThenIShouldBeOnTheBookTicketsPage()
        {
            var bookTickets = _pageBrowser.Init<BookTicketsPage>();
            Assert.That(bookTickets, Is.Not.Null);
        }
    }
}
