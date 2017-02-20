using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.ContextData;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    internal class EventOrganisersSteps
    {
        private readonly ITestDataRepository _repository;
        private readonly ContextData<EventAdContext> _contextData;

        public EventOrganisersSteps(ITestDataRepository repository,
            ContextData<EventAdContext> contextData)
        {
            _repository = repository;
            _contextData = contextData;
        }

        [Given(@"event titled ""(.*)"" is assigned to organiser ""(.*)""")]
        public void GivenEventTitledIsAssignedToOrganiser(string eventTitle, string eventOrganiser)
        {
            var eventData = _repository.GetEventByName(eventTitle);

            _repository.AddEventOrganiser(eventData.EventId, eventOrganiser);
        }
    }

}
