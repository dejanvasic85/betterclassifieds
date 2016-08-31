using System.Linq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.ContextData;
using Paramount.Betterclassifieds.Tests.Functional.Features.EventGroups;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [Binding]
    internal class ManageGroupsSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ContextData<EventAdContext> _eventAdContext;

        public ManageGroupsSteps(PageBrowser pageBrowser, ContextData<EventAdContext> eventAdContext)
        {
            _pageBrowser = pageBrowser;
            _eventAdContext = eventAdContext;
        }

        [When(@"I create a new group ""(.*)"" for all tickets and unlimited guests")]
        public void WhenICreateANewGroupForAllTicketsAndUnlimitedGuests(string groupName)
        {
            var manageGroupsPage = _pageBrowser.Init<ManageGroupsPage>(_eventAdContext.Get().AdId, _eventAdContext.Get().EventId);

            manageGroupsPage.CreateGroup()
                .WithNewGroupName(groupName)
                .SaveGroup();
        }

        [When(@"I create a new group ""(.*)"" for ticket ""(.*)"" and ""(.*)"" guests")]
        public void WhenICreateANewGroupForTicketAndGuests(string groupName, string ticket, int maxGuests)
        {
            var manageGroupsPage = _pageBrowser.Init<ManageGroupsPage>(_eventAdContext.Get().AdId, _eventAdContext.Get().EventId);

            manageGroupsPage.CreateGroup()
                .WithNewGroupName(groupName)
                .WithSelectedTickets(ticket)
                .WithMaxGuests(maxGuests)
                .SaveGroup();
        }

        [Then(@"the group ""(.*)"" should be created")]
        public void ThenTheGroupShouldBeCreated(string groupname)
        {
            var manageGroupsPage = _pageBrowser.Init<ManageGroupsPage>(_eventAdContext.Get().AdId, _eventAdContext.Get().EventId);
            var currentGroups = manageGroupsPage.GetCurrentGroups();

            Assert.That(currentGroups.Any(g => g.GroupName == groupname));
        }
    }
}
