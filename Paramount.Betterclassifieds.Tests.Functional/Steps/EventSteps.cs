using System;
using System.Linq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.ContextData;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using Paramount.Betterclassifieds.Tests.Functional.Pages.Events;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    internal class EventSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _repository;
        private readonly ContextData<EventAdContext> _contextData;

        public EventSteps(PageBrowser pageBrowser, ITestDataRepository repository, ContextData<EventAdContext> contextData)
        {
            _pageBrowser = pageBrowser;
            _repository = repository;
            _contextData = contextData;
        }

        [Given(@"an event ad titled ""(.*)"" exists")]
        public void GivenAnEventAdTitledExists(string adTitle)
        {
            GivenAnEventAdTitledExistsForUser(adTitle, TestData.DefaultUsername);
        }

        [Given(@"an event ad titled ""(.*)"" exists for user ""(.*)""")]
        public void GivenAnEventAdTitledExistsForUser(string adTitle, string username)
        {
            var eventAdContext = _contextData.Get();

            eventAdContext.AdId = _repository.DropCreateOnlineAd(adTitle, TestData.ParentEventCategory,
                TestData.SubEventCategory, username);

            // Get the online ad Id
            eventAdContext.OnlineAdId = _repository.GetOnlineAdForBookingId(eventAdContext.AdId);

            // Create the event 
            eventAdContext.EventId = _repository.AddEventIfNotExists(eventAdContext.OnlineAdId);
        }

        [Given(@"the event does not include a transaction fee")]
        public void GivenTheEventDoesNotIncludeATransactionFee()
        {
            _repository.SetEventIncludeTransactionFee(_contextData.Get().EventId, false);
        }

        [Given(@"with a ticket option ""(.*)"" for ""(.*)"" dollars each and ""(.*)"" available")]
        public void GivenWithATicketOptionForDollars(string ticketName, decimal amount, int availableQty)
        {
            // Create the event tickets
            _repository.AddEventTicketType(_contextData.Get().EventId, ticketName, amount, availableQty);
        }

        [Given(@"the event has a group ""(.*)"" for ticket ""(.*)"" and allows up to ""(.*)"" guests")]
        public void GivenTheEventHasAGroupForTicketAndAllowsUpToGuests(string groupName, string ticketName, int maxGuests)
        {
            _repository.AddEventGroup(_contextData.Get().EventId,
                groupName,
                ticketName, 
                maxGuests);
        }

        [Given(@"I navigate to ""(.*)""")]
        [When(@"I navigate to ""(.*)""")]
        public void GivenINavigateTo(string url)
        {
            var relativePath = url.Replace("adId", _contextData.Get().AdId.ToString());
            _pageBrowser.NavigateTo(relativePath);
        }

        [When(@"I select ""(.*)"" ""(.*)"" tickets")]
        public void WhenISelectTickets(int numberOfTickets, string ticketType)
        {
            var eventPage = _pageBrowser.Init<EventDetailsPage>(ensureUrl: false);
            eventPage.SelectTickets(numberOfTickets, ticketType)
                .ConfirmTicketSelection();
        }

        [When(@"enter the email ""(.*)"" and name ""(.*)"" for the second guest")]
        public void WhenEnterTheEmailAndNameForTheSecondGuest(string email, string fullName)
        {
            _pageBrowser.Init<BookTicketsPage>()
                .WithSecondGuest(email, fullName);
        }

        [When(@"choose to email all guests")]
        public void WhenChooseToEmailAllGuests()
        {
            _pageBrowser.Init<BookTicketsPage>()
                .WithEmailGuests();
        }

        [When(@"my details are prefilled so I proceed to payment")]
        public void WhenMyDetailsArePrefilledSoIProceedToPayment()
        {
            var bookingTicketPage = _pageBrowser.Init<BookTicketsPage>();
            bookingTicketPage.WithPhone("0433 095 822");
            bookingTicketPage.ProceedToPayment();

            _pageBrowser.Init<MakeTicketPaymentPage>()
                .PayWithPayPal()
                .WaitForPayPal();
        }

        [Then(@"I should see a ticket purchased success page")]
        public void ThenIShouldSeeATicketPurchasedSuccessPage()
        {
            var eventBookedPage = _pageBrowser.Init<EventBookedPage>();
            Assert.That(eventBookedPage.GetSuccessText(), Is.EquivalentTo("You have tickets to The Opera"));
        }

        [Then(@"When selecting ""(.*)"" for guest ""(.*)""")]
        public void ThenWhenSelectingForGuest(string groupName, string guestFullName)
        {
            var eventBookedPage = _pageBrowser.Init<EventBookedPage>();
            eventBookedPage.AssignGroup(groupName, guestFullName);
        }


        [Then(@"the tickets should be booked")]
        public void ThenTheTicketsShouldBeBooked()
        {
            var testContext = _contextData.Get();
            var eventBooking = _repository.GetEventBooking(testContext.EventId);
            var eventBookingTickets = _repository.GetPurchasedTickets(eventBooking.EventBookingId);

            Assert.That(eventBooking, Is.Not.Null);
            Assert.That(eventBooking.TotalCost, Is.EqualTo(10)); // 10 dollars - 5 bucks x 2 tickets
            Assert.That(eventBookingTickets.Count, Is.EqualTo(2));
        }

        [Then(@"ticket with full name ""(.*)"" should be assigned to a group")]
        public void ThenTicketWithFullNameShouldBeAssignedToAGroup(string guestFullName)
        {
            var testContext = _contextData.Get();
            var eventBooking = _repository.GetEventBooking(testContext.EventId);
            var eventBookingTickets = _repository.GetPurchasedTickets(eventBooking.EventBookingId);

            var ticket = eventBookingTickets.Single(t => t.GuestFullName.Equals(guestFullName));
            Assert.That(ticket.EventGroupId, Is.Not.Null);
        }


        [Then(@"the ticket ""(.*)"" price should be ""(.*)""")]
        public void ThenTheTicketPriceShouldBe(string ticketName, string expectedPrice)
        {
            var eventDetailsPage = _pageBrowser.Init<EventDetailsPage>(ensureUrl: false);
            if (expectedPrice.EqualTo("free"))
            {
                Assert.That(eventDetailsPage.IsPriceFreeForTicket(ticketName));
            }
            else
            {
                var currentPrice = eventDetailsPage.GetPriceForTicket(ticketName);
                Assert.That(currentPrice, Is.EqualTo(expectedPrice));
            }
        }

        [When(@"I navigate to event ""(.*)""")]
        public void WhenINavigateToEvent(string url)
        {
            _pageBrowser.GoTo<EventDashboardPage>(_contextData.Get().AdId);
        }
        
        [When(@"I choose to add guest manually from the dashboard with details ""(.*)"" ""(.*)"" ""(.*)""")]
        public void WhenIChooseToAddGuestManuallyFromTheDashboardWithDetails(string fullName, string email, string ticketType)
        {
            var contextData = _contextData.Get();
            _pageBrowser.Init<EventDashboardPage>(query: contextData.AdId).AddGuest();
            _pageBrowser.Init<AddGuestPage>(contextData.AdId, contextData.EventId)
                .WithGuest(fullName, email, ticketType)
                .Add()
                ;
        }

        [Then(@"the new guest with email ""(.*)"" should have a ticket ""(.*)"" to the event")]
        public void ThenTheNewGuestShouldHaveATicketToTheEvent(string guestEmail, string ticketName)
        {
            var guestTicket = _repository.GetPurchasedTicketsForEvent(_contextData.Get().EventId)
                .FirstOrDefault(t => t.GuestEmail.Equals(guestEmail, StringComparison.OrdinalIgnoreCase));
                

            Assert.That(guestTicket, Is.Not.Null);
            Assert.That(guestTicket.TicketName, Is.EqualTo(ticketName));
            Assert.That(guestTicket.GuestEmail, Is.EqualTo(guestEmail));
        }

        [When(@"I go the event dashboard for the current ad")]
        public void WhenIGoTheEventDashboardForTheCurrentAd()
        {
            _pageBrowser.NavigateTo<EventDashboardPage>(_contextData.Get().AdId);
        }

        [When(@"I select to manage groups")]
        public void WhenISelectToManageGroups()
        {
            _pageBrowser.Init<EventDashboardPage>(_contextData.Get().AdId)
                .ManageGroups();
        }

    }
}
