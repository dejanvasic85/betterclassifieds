using Paramount.Betterclassifieds.Tests.Functional.Features.Events;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Features.EventInvitation
{
    [Binding]
    internal class BookTicketsFromInviteSteps
    {
        private readonly ContextData<EventAdContext> _evenContextData;
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _repository;

        public BookTicketsFromInviteSteps(ContextData<EventAdContext> evenContextData, PageBrowser pageBrowser, ITestDataRepository repository)
        {
            _evenContextData = evenContextData;
            _pageBrowser = pageBrowser;
            _repository = repository;
        }

        [Given(@"the event has an invite for ""(.*)"" with email ""(.*)""")]
        public void GivenTheEventHasAnInviteForWithEmail(string fullName, string email)
        {
            var eventContextData = _evenContextData.Get();

            var userNetworkId = _repository.AddUserNetworkIfNotExists(TestData.DefaultUsername, email, fullName);
            var eventInvitationId = _repository.AddEventInvitationIfNotExists(eventContextData.EventId, userNetworkId);
        }
    }
}
