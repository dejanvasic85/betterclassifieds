using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Features.Events
{
    [Binding]
    internal class BookEventTicketSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _repository;
        private readonly ContextData<EventAdContext> _contextData;

        public BookEventTicketSteps(PageBrowser pageBrowser, ITestDataRepository repository, ContextData<EventAdContext> contextData)
        {
            _pageBrowser = pageBrowser;
            _repository = repository;
            _contextData = contextData;
        }

        [Given(@"an event ad titled ""(.*)"" exists")]
        public void GivenAnEventAdTitledExists(string adTitle)
        {
            var eventAdContext = _contextData.Get();

            eventAdContext.AdId = _repository.DropCreateOnlineAd(adTitle, TestData.ParentEventCategory,
                TestData.SubEventCategory, TestData.DefaultUsername);

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
            _pageBrowser.Init<BookingTicketPage>()
                .WithSecondGuest(email, fullName);
        }

        [When(@"choose to email all guests")]
        public void WhenChooseToEmailAllGuests()
        {
            _pageBrowser.Init<BookingTicketPage>()
                .WithEmailGuests();
        }

        [When(@"my details are prefilled so I proceed to payment")]
        public void WhenMyDetailsArePrefilledSoIProceedToPayment()
        {
            var bookingTicketPage = _pageBrowser.Init<BookingTicketPage>();
            bookingTicketPage.WithPhone("0433 095 822");
            bookingTicketPage.ProceedToPayment();

            _pageBrowser.Init<MakeTicketPaymentPage>()
                .PayWithPayPal()
                .WaitForPayPal();
        }

        [Then(@"i should see a ticket purchased success page")]
        public void ThenIShouldSeeATicketPurchasedSuccessPage()
        {
            var eventBookedPage = _pageBrowser.Init<EventBookedPage>();
            Assert.That(eventBookedPage.GetSuccessText(), Is.EquivalentTo("You have tickets to The Opera"));
        }

        [Then(@"the tickets should be booked")]
        public void ThenTheTicketsShouldBeBooked()
        {
            var testContext = _contextData.Get();
            var eventBooking = _repository.GetEventBooking(testContext.EventId);
            var eventBookingTickets = _repository.GetEventBookingTickets(eventBooking.EventBookingId);

            Assert.That(eventBooking, Is.Not.Null);
            Assert.That(eventBooking.TotalCost, Is.EqualTo(10)); // 10 dollars - 5 bucks x 2 tickets
            Assert.That(eventBookingTickets.Count, Is.EqualTo(2));
        }

        [Then(@"the ticket ""(.*)"" price should be ""(.*)""")]
        public void ThenTheTicketPriceShouldBe(string ticketName, string expectedPrice)
        {
            var currentPrice = _pageBrowser.Init<EventDetailsPage>(ensureUrl: false)
                .GetPriceForTicket(ticketName);

            Assert.That(currentPrice, Is.EqualTo(expectedPrice));
        }

    }
}
