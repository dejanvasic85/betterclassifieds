using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    internal class EventAdSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _repository;
        private readonly ContextData<EventAdContext> _contextData;

        public EventAdSteps(PageBrowser pageBrowser, ITestDataRepository repository, ContextData<EventAdContext> contextData)
        {
            _pageBrowser = pageBrowser;
            _repository = repository;
            _contextData = contextData;
        }

        [Given(@"an event ad titled ""(.*)"" exists")]
        public void GivenAnEventAdTitledExists(string adTitle)
        {
            var eventAdContext = _contextData.Get();

            var adId = _repository.DropCreateOnlineAd(adTitle, TestData.ParentEventCategory,
                TestData.SubEventCategory, TestData.DefaultUsername);

            // Get the online ad Id
            eventAdContext.OnlineAdId = _repository.GetOnlineAdForBookingId(adId);

            // Create the event 
            //eventAdContext.EventId = _repository.DropCreateEventAd(
            //    eventAdContext.OnlineAdId);

        }
    }
}
