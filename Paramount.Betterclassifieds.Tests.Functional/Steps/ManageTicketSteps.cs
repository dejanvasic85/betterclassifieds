using System.Linq;
using System.Threading;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.ContextData;
using Paramount.Betterclassifieds.Tests.Functional.Pages.Events;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    internal class ManageTicketSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ContextData<EventAdContext> _eventAdContext;
        private readonly ITestDataRepository _repository;

        public ManageTicketSteps(PageBrowser pageBrowser, ContextData<EventAdContext> eventAdContext,
            ITestDataRepository repository)
        {
            _pageBrowser = pageBrowser;
            _eventAdContext = eventAdContext;
            _repository = repository;
        }

        [When(@"a new ticket is created titled ""(.*)"" with price ""(.*)"" and quantity ""(.*)"" and field ""(.*)""")]
        public void WhenANewTicketIsCreatedTitledWithPriceAndQuantityAndField(string ticketName, int price, int quantity, string fieldName)
        {
            var page = _pageBrowser.Init<ManageTicketsPage>(_eventAdContext.Get().AdId, _eventAdContext.Get().EventId);

            page.WithNewTicket()
                .WithTicketName(ticketName)
                .WithPrice(price)
                .WithQuantity(quantity)
                .WithField(fieldName, false)
                .Save();
        }

        [When(@"I edit the ticket ""(.*)""")]
        public void WhenIEditTheTicket(string ticketName)
        {
            var page = _pageBrowser.Init<ManageTicketsPage>(_eventAdContext.Get().AdId, _eventAdContext.Get().EventId);
            int ticketId;
            page.EditTicket(ticketName, out ticketId);
            ScenarioContext.Current.Add("editTicketId", ticketId);
        }

        [When(@"I change the name to ""(.*)"" price to ""(.*)"" and quantity to ""(.*)""")]
        public void WhenIChangeTheNameToPriceToAndQuantityToAndField(string name, int price, int quantity)
        {
            var ticketId = ScenarioContext.Current.Get<int>("editTicketId");
            var editTicketPage = _pageBrowser.Init<EditTicketPage>(_eventAdContext.Get().AdId, ticketId);

            editTicketPage.WithTicketName(name)
                .WithPrice(price)
                .WithQuantity(quantity)
                .Save();
        }

        [Then(@"a ticket ""(.*)"" exists with price ""(.*)"" quantity ""(.*)"" and field ""(.*)""")]
        public void ThenATicketExistsWithPriceQuantityAndField(string ticketName, int price, int quantity, string fieldName)
        {
            // Wait a second until the data is persisted in db
            Thread.Sleep(500);

            var ticket = _repository.GetEventTicketByName(_eventAdContext.Get().EventId, ticketName);
            Assert.That(ticket, Is.Not.Null);
            Assert.That(ticket.Price, Is.EqualTo(price));
            Assert.That(ticket.AvailableQuantity, Is.EqualTo(quantity));

            var ticketFields = _repository.GetEventTicketFieldNames(ticket.EventTicketId);
            Assert.That(ticketFields.Count, Is.EqualTo(1));
            Assert.That(ticketFields.Single(), Is.EqualTo(fieldName));
        }
    }
}
