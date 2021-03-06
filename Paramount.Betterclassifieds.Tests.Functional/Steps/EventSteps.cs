﻿using System;
using System.Linq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.ContextData;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using Paramount.Betterclassifieds.Tests.Functional.Mocks.Models;
using Paramount.Betterclassifieds.Tests.Functional.Pages.Events;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    internal class EventSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _repository;
        private readonly ContextData<EventAdContext> _contextData;
        private readonly UserContext _userContext;

        public EventSteps(PageBrowser pageBrowser, ITestDataRepository repository, ContextData<EventAdContext> contextData, UserContext userContext)
        {
            _pageBrowser = pageBrowser;
            _repository = repository;
            _contextData = contextData;
            _userContext = userContext;
        }

        [Given(@"an event ad titled ""(.*)"" exists")]
        public void GivenAnEventAdTitledExists(string adTitle)
        {
            GivenAnEventAdTitledExistsForUser(adTitle, TestData.DefaultUsername);
        }

        [Given(@"the event has seating setup")]
        public void GivenTheEventHasSeatingSetup()
        {
            var eventAdContext = _contextData.Get();
            
            _repository.SetEventSeatedEvent(eventAdContext.EventId, true);
        }
        

        [Given(@"the event ticket fee is ""(.*)"" percent and ""(.*)"" cents")]
        public void GivenTheEventTicketFeeIsPercentAndCents(string percent, int cents)
        {
            _repository.SetClientConfig("EventTicketFee", percent);
            _repository.SetClientConfig("EventTicketFeeCents", cents.ToString());
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

        [Given(@"the event requires group selection")]
        public void GivenTheEventRequiresGroupSelection()
        {
            _repository.SetEventGroupsRequired(_contextData.Get().EventId);
        }


        [Given(@"with a ticket option ""(.*)"" for ""(.*)"" dollars each and ""(.*)"" available")]
        public void GivenWithATicketOptionForDollars(string ticketName, decimal amount, int availableQty)
        {
            // Create the event tickets
            _repository.AddEventTicketType(_contextData.Get().EventId, ticketName, amount, availableQty);
        }

        [Given(@"the event seating has ""(.*)"" rows with ""(.*)"" seats assigned to ""(.*)"" ticket")]
        public void GivenTheEventSeatingHasRowsWithSeatsAssignedToTicket(int rowCount, int seatCount, string ticketName)
        {
            var eventId = _contextData.Get().EventId;
            _repository.AddEventSeats(eventId, rowCount, seatCount, ticketName);
        }


        [Given(@"a guest name ""(.*)"" and email ""(.*)"" with a ""(.*)"" ticket to ""(.*)""")]
        public void GivenAGuestNameAndEmailWithATicketTo(string guestFullName, string guestEmail, string ticketName, string eventName)
        {
            _repository.AddGuestToEvent(_userContext.Username, guestFullName, guestEmail, ticketName, _contextData.Get().EventId);
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

        [When(@"I select group ""(.*)""")]
        public void WhenISelectGroup(string groupName)
        {
            var eventPage = _pageBrowser.Init<EventDetailsPage>(ensureUrl: false);
            eventPage.SelectGroup(groupName);
        }

        [When(@"I select ""(.*)"" ""(.*)"" tickets")]
        public void WhenISelectTickets(int numberOfTickets, string ticketType)
        {
            var eventPage = _pageBrowser.Init<EventDetailsPage>(ensureUrl: false);
            eventPage.SelectTickets(numberOfTickets, ticketType);
        }

        [When(@"proceed to order the tickets")]
        public void WhenProceedToOrderTheTickets()
        {
            _pageBrowser.Init<EventDetailsPage>(ensureUrl: false).ConfirmTicketSelection();
        }
        
        [When(@"enter the email ""(.*)"" and name ""(.*)"" for the second guest")]
        public void WhenEnterTheEmailAndNameForTheSecondGuest(string email, string fullName)
        {
            _pageBrowser.Init<BookTicketsPage>()
                .WithSecondGuest(email, fullName);
        }

        [When(@"my details are prefilled so I proceed to checkout")]
        public void WhenMyDetailsArePrefilledSoIProceedToCheckout()
        {
            var bookingTicketPage = _pageBrowser.Init<BookTicketsPage>();
            bookingTicketPage.WithPhone("0433 095 822");
            bookingTicketPage.Checkout();
        }

        [When(@"my details are prefilled so I proceed to checkout and payment")]
        public void WhenMyDetailsArePrefilledSoIProceedToPayment()
        {
            var bookingTicketPage = _pageBrowser.Init<BookTicketsPage>();
            bookingTicketPage.WithPhone("0433 095 822");
            bookingTicketPage.Checkout();
        }

        [Then(@"I should see a ticket purchased success page")]
        public void ThenIShouldSeeATicketPurchasedSuccessPage()
        {
            var eventBookedPage = _pageBrowser.Init<EventBookedPage>();
            Assert.That(eventBookedPage.GetSuccessText(), Is.EquivalentTo("You have tickets to The Opera"));
        }

        [When(@"credit card payment is complete")]
        public void WhenCreditCardPaymentIsComplete()
        {
            _pageBrowser.Init<MakeTicketPaymentPage>()
                .PayWithStripe();
        }
        
        [When(@"When selecting ""(.*)"" for guest ""(.*)""")]
        public void ThenWhenSelectingForGuest(string groupName, string guestFullName)
        {
            var eventBookedPage = _pageBrowser.Init<EventBookedPage>();
            eventBookedPage.AssignGroup(groupName, guestFullName);
        }
        
        [Then(@"the tickets should be booked with total cost ""(.*)"" and ticket count ""(.*)""")]
        public void ThenTheTicketsShouldBeBooked(int expectedCost, int expectedTicketCount)
        {
            var testContext = _contextData.Get();
            var eventBooking = _repository.GetEventBooking(testContext.EventId);
            var eventBookingTickets = _repository.GetPurchasedTickets(eventBooking.EventBookingId);

            Assert.That(eventBooking, Is.Not.Null);
            Assert.That(eventBooking.TotalCost, Is.EqualTo(expectedCost)); // 10 dollars - 5 bucks x 2 tickets
            Assert.That(eventBookingTickets.Count, Is.EqualTo(expectedTicketCount));
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

        [Then(@"the booking page should display total tickets ""(.*)"" total fees ""(.*)"" and sub total ""(.*)""")]
        public void ThenTheBookingPageShouldDisplayTotalTicketsTotalFeesAndSubTotal(decimal totalTickets, decimal totalFees, decimal subTotal)
        {
            var bookingPage = _pageBrowser.Init<BookTicketsPage>();

            var currentTotalTickets = bookingPage.GetTotalTicketsCost();
            Assert.That(currentTotalTickets, Is.EqualTo(totalTickets));

            var currentTotalFees = bookingPage.GetTotalFees();
            Assert.That(currentTotalFees, Is.EqualTo(totalFees));

            var currentSubTotal = bookingPage.GetSubTotal();
            Assert.That(currentSubTotal, Is.EqualTo(subTotal));

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
            _pageBrowser.Init<EventDashboardPage>(query: contextData.AdId).ManageGuests();
            _pageBrowser.Init<ManageGuestsPage>(contextData.AdId, contextData.EventId).AddGuest();
            _pageBrowser.Init<AddGuestPage>(contextData.AdId, contextData.EventId)
                .WithGuest(fullName, email, ticketType)
                .Add();
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

        [When(@"I go to the event dashboard for the current ad")]
        public void WhenIGoToTheEventDashboardForTheCurrentAd()
        {
            _pageBrowser.NavigateTo<EventDashboardPage>(_contextData.Get().AdId);
        }

        [When(@"I select to manage groups")]
        public void WhenISelectToManageGroups()
        {
            _pageBrowser.Init<EventDashboardPage>(_contextData.Get().AdId)
                .ManageGroups();
        }

        [When(@"I select to manage tickets")]
        public void WhenISelectToManageTickets()
        {
            _pageBrowser.Init<EventDashboardPage>(_contextData.Get().AdId)
                .ManageTickets();
        }

        [When(@"I go to edit the guest ""(.*)"" from the dashboard")]
        public void WhenIEditTheGuestFromTheDashboard(string guestEmail)
        {

            var eventAdContext = _contextData.Get();

            _pageBrowser.Init<EventDashboardPage>(eventAdContext.AdId)
                .ManageGuests();


            _pageBrowser.Init<ManageGuestsPage>(
                eventAdContext.AdId,
                eventAdContext.EventId).EditGuest(guestEmail);
        }

        [When(@"I go to edit event details")]
        public void WhenIGoToEditEventDetails(Table table)
        {
            var title = table.Rows.First().Values.ElementAt(0);
            var description = table.Rows.First().Values.ElementAt(1);

            var eventAdContext = _contextData.Get();

            _pageBrowser.Init<EventDashboardPage>(eventAdContext.AdId)
                .EditEventDetails();

            _pageBrowser.Init<EventEditDetailsPage>(eventAdContext.AdId)
                .WithTitle(title)
                .WithDescription(description)
                .SaveAndSendNotifications();
        }


        [When(@"I update the guest name to ""(.*)"" and email to ""(.*)""")]
        public void WhenIUpdateTheGuestNameToAndEmailTo(string newGuestName, string newGuestEmail)
        {
            _pageBrowser.Init<EditGuestPage>(ensureUrl: false)
                .WaitToInit()
                .WithName(newGuestName)
                .WithEmail(newGuestEmail)
                .Update()
                .WaitForToaster();
        }

        [When(@"I remove the guest from the event")]
        public void WhenIRemoveTheGuestFromTheEvent()
        {
            _pageBrowser.Init<EditGuestPage>(ensureUrl: false)
                .WaitToInit()
                .RemoveGuest();
        }

        [Then(@"the guest email ""(.*)"" should be ""(.*)"" for the current event")]
        public void ThenTheGuestEmailShouldBeForTheCurrentEvent(string guestEmail, string activeOrNot)
        {
            var expectedStatus = activeOrNot == "active";
            bool isActive = _repository.GetEventBookingTicketStatus(_contextData.Get().EventId, guestEmail);
            Assert.That(isActive, Is.EqualTo(expectedStatus));
        }


        [Then(@"the guest email ""(.*)"" event booking should not be active")]
        public void ThenTheGuestEmailEventBookingShouldNotBeActive(string guestEmail)
        {
            var status = _repository.GetEventBookingStatus(_contextData.Get().EventId, guestEmail);
            Assert.That(status, Is.EqualTo("Cancelled"));
        }


        [Then(@"I should see the remove guest success message ""(.*)""")]
        public void ThenIShouldSeeTheRemoveGuestSuccessMessage(string expectedMessage)
        {
            var removePage = _pageBrowser.Init<RemoveGuestCompletePage>();
            Assert.That(removePage.GetSuccessMessage(), Is.EqualTo(expectedMessage));
        }

        [When(@"I click Event Dashboard button")]
        public void WhenIClickEventDashboardButton()
        {
            _pageBrowser.Init<RemoveGuestCompletePage>().ClickBackToDashboard();
        }

        [Then(@"I should be back to event dashboard page")]
        public void ThenIShouldBeBackToEventDashboardPage()
        {
            var page = _pageBrowser.Init<EventDashboardPage>(_contextData.Get().AdId);

            Assert.That(page, Is.Not.Null);
        }

        [Then(@"the guest count should be (.*) and (.*) guests are in search results")]
        public void ThenTheGuestCountShouldBeAndGuestsAreInSearchResults(int expectedSoldQuantity, int expectedGuestsInSearch)
        {
            var dashboardPage = _pageBrowser.Init<EventDashboardPage>(_contextData.Get().AdId);

            Assert.That(dashboardPage.GetTotalSoldQty(), Is.EqualTo(expectedSoldQuantity));
        }

        [When(@"I choose to resend ticket for the guest")]
        public void WhenIChooseToResendTicketForTheGuest()
        {
            var page = _pageBrowser.Init<EditGuestPage>(ensureUrl: false);
            page.ResentTicket();
        }

        [Then(@"the ticket should be sent successfully")]
        public void ThenTicketShouldBeSentSuccessfullyInTicketEditingScreen()
        {
            var page = _pageBrowser.Init<EditGuestPage>(ensureUrl: false);
            var msg = page.GetToastSuccessMsg();

            Assert.That(msg, Is.EqualTo("Email has been sent successfully."));
        }

        [Then(@"the event dashboard for ""(.*)"" should display")]
        public void ThenTheEventDashboardForShouldDisplay(string title)
        {
            var eventDashboardPage = _pageBrowser.Init<EventDashboardPage>(ensureUrl: false);

            var eventTitle = eventDashboardPage.GetEventTitle();

            Assert.That(eventTitle, Is.EqualTo(title));
        }

    }
}
