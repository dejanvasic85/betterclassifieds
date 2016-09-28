using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    [TestFixture]
    internal class EventControllerTests : ControllerTest<EventController>
    {
        [Test]
        [Ignore("The session mock object needs to be mocked properly")]
        public void ViewEventAd_Get_ReturnsViewResult()
        {

            var mockAd = new AdSearchResultMockBuilder()
                .Default()
                .WithHtmlText("FirstLine<br />SecondLine")
                .Build();

            var mockEventAd = new EventModelMockBuilder()
                .Default()
                .WithOnlineAdId(mockAd.OnlineAdId)
                .WithPastClosedDate()
                .Build();

            // arrange services
            _searchService.SetupWithVerification(call => call.GetByAdId(It.Is<int>(p => p == mockAd.AdId)), mockAd);
            _bookingManager.SetupWithVerification(call => call.IncrementHits(It.Is<int>(p => p == mockAd.AdId)));
            _eventManager.SetupWithVerification(call => call.GetEventDetailsForOnlineAdId(It.Is<int>(p => p == mockEventAd.OnlineAdId), It.Is<bool>(p => p == false)), mockEventAd);
            _clientConfig.SetupWithVerification(call => call.EventMaxTicketsPerBooking, 10);
            _clientConfig.SetupWithVerification(call => call.FacebookAppId, "123");
            _httpContext.SetupWithVerification(call => call.Server.HtmlEncode(It.IsAny<string>()), "This looks good " + mockAd.Heading);

            // act
            var result = BuildController().ViewEventAd(mockAd.AdId);

            // assert
            result.IsTypeOf<ViewResult>();
            var eventViewModel = result.ViewResultModelIsTypeOf<EventViewDetailsModel>();
            eventViewModel.AdId.IsEqualTo(mockAd.AdId);
            eventViewModel.EventId.IsEqualTo(mockEventAd.EventId.GetValueOrDefault());
            eventViewModel.Title.IsEqualTo(mockAd.Heading);
            eventViewModel.HtmlText.IsEqualTo("FirstLine<br />SecondLine");
            eventViewModel.EventPhoto.IsEqualTo(mockAd.PrimaryImage);
            eventViewModel.IsClosed.IsEqualTo(true);
            eventViewModel.EventStartDateDisplay.IsEqualTo(mockEventAd.EventStartDate.GetValueOrDefault().ToLongDateString());
            eventViewModel.EventStartTime.IsEqualTo(mockEventAd.EventStartDate.GetValueOrDefault().ToString("hh:mm tt"));
            eventViewModel.EventEndDateDisplay.IsEqualTo(mockEventAd.EventEndDate.GetValueOrDefault().ToLongDateString());
        }

        [Test]
        public void ViewEventAd_Get_WithAdThatDoesNotExist_RedirectsTo404()
        {
            // arrange
            var mockAdId = 458745;
            _searchService.SetupWithVerification(call => call.GetByAdId(It.Is<int>(p => p == mockAdId)), null);

            // act
            var result = BuildController().ViewEventAd(mockAdId);

            // assert
            result.IsRedirectingTo("error", "notfound");
        }

        [Test]
        public void ReserveTickets_Post_NothingSelected_ReturnsModelError()
        {
            var controller = BuildController();
            var vm = new ReserveTicketsViewModel { EventId = 1, Tickets = null };
            var result = controller.ReserveTickets(vm);
            var jsonResult = result.IsTypeOf<JsonResult>();
            jsonResult.JsonResultContainsErrors();
        }

        [Test]
        public void ReserveTickets_Post_EventIsClosed_ReturnsModelError()
        {
            // arrange
            var eventTicketRequests = new List<EventTicketRequestViewModel>
            {
                new EventTicketRequestViewModel {TicketName = "Tick1", AvailableQuantity = 10, EventId = 999, Price = 10, SelectedQuantity = 1},
                new EventTicketRequestViewModel {TicketName = "Tick2", AvailableQuantity = 11, EventId = 999, Price = 60, SelectedQuantity = 2},
            };

            var mockEventModel = new EventModelMockBuilder()
                .WithPastClosedDate()
                .Build();

            _eventManager.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEventModel);

            var controller = BuildController();
            var vm = new ReserveTicketsViewModel { EventId = 1, Tickets = eventTicketRequests };
            var result = controller.ReserveTickets(vm);
            var jsonResult = result.IsTypeOf<JsonResult>();
            jsonResult.JsonResultContainsErrors();
        }

        [Test]
        public void ReserveTickets_Post_EventManager_SavesTheReservations_ReturnsJsonResult()
        {
            // arrange
            var eventTicketRequests = new List<EventTicketRequestViewModel>
            {
                new EventTicketRequestViewModel {TicketName = "Tick1", AvailableQuantity = 10, EventId = 999, Price = 10, SelectedQuantity = 1},
                new EventTicketRequestViewModel {TicketName = "Tick2", AvailableQuantity = 11, EventId = 999, Price = 60, SelectedQuantity = 2},
            };

            var mockReservations = new[] { new EventTicketReservationMockBuilder().Build() };
            var mockEventModel = new EventModelMockBuilder()
                .WithClosingDate(DateTime.Now.AddMonths(1))
                .WithClosingDateUtc(DateTime.UtcNow.AddMonths(1))
                .Default()
                .Build();

            var vm = new ReserveTicketsViewModel
            {
                EventId = 1,
                Tickets = eventTicketRequests,
                EventInvitationId = 1234
            };

            // arrange service calls
            _httpContext.SetupWithVerification(call => call.Session.SessionID, "123");
            _eventManager.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEventModel);
            _eventManager.SetupWithVerification(call => call.ReserveTickets(It.IsAny<string>(), It.IsAny<IEnumerable<EventTicketReservation>>()));
            _eventTicketReservationFactory.SetupWithVerification(call => call.CreateReservations(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), mockReservations);
            _eventBookingContext.SetupWithVerification(call => call.Clear());
            _eventBookingContext.SetupSet(p => p.EventInvitationId = It.Is<long>(s => s == vm.EventInvitationId));

            // act
            var controller = BuildController();
            var result = controller.ReserveTickets(vm);

            // assert
            result.IsTypeOf<JsonResult>().JsonResultPropertyEquals("NextUrl", "/Event/BookTickets");
        }

        [Test]
        public void BookTickets_Get_NothingInSession_RedirectsTo_NotFound()
        {
            // Arrange
            var mockSessionId = "session123";
            _httpContext.SetupWithVerification(call => call.Session.SessionID, mockSessionId);
            _eventManager.SetupWithVerification(call => call.GetTicketReservations(It.Is<string>(p => p == mockSessionId)), new List<EventTicketReservation>());

            var result = BuildController().BookTickets();

            result.IsRedirectingTo("error", "notfound");
        }

        [Test]
        public void BookTickets_Get_WithReservationsInSession_ReturnsView()
        {
            // Arrange
            var mockReservations = new List<EventTicketReservation>
            {
                new EventTicketReservation {EventTicket = new EventTicket {EventId = 10}, Price = 10, TransactionFee = (decimal)0.5}
            };
            var mockEvent = new EventModelMockBuilder().Default().Build();
            var mockAd = new AdSearchResultMockBuilder().Default().Build();
            var mockApplicationUser = new ApplicationUserMockBuilder().Default().Build();
            var mockSessionId = "session123";
            var secondsRemaining = 100;

            // arrange services
            _httpContext.SetupWithVerification(call => call.Session.SessionID, mockSessionId);
            _eventManager.SetupWithVerification(call => call.GetTicketReservations(It.Is<string>(p => p == mockSessionId)), mockReservations);
            _eventManager.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            _eventManager.SetupWithVerification(call => call.GetRemainingTimeForReservationCollection(It.IsAny<IEnumerable<EventTicketReservation>>()), TimeSpan.FromSeconds(secondsRemaining));
            _searchService.SetupWithVerification(call => call.GetByAdOnlineId(It.Is<int>(p => p == mockEvent.OnlineAdId)), mockAd);
            _userManager.SetupWithVerification(call => call.GetCurrentUser(It.IsAny<IPrincipal>()), mockApplicationUser);
            _clientConfig.SetupWithVerification(call => call.EventTicketReservationExpiryMinutes, 10);
            _appConfig.SetupWithVerification(call => call.Brand, "HelloBrand");
            _eventBookingContext.SetupWithVerification(call => call.EventInvitationId, null);

            // act
            var result = BuildController(mockUser: _mockUser).BookTickets();

            // assert
            result.IsTypeOf<ViewResult>();
            var model = result.ViewResultModelIsTypeOf<BookTicketsViewModel>();
            model.BrandName.IsEqualTo("HelloBrand");
            model.TotalCostCents.IsEqualTo(1050);
        }

        [Test]
        public void BookTickets_Post_UserIsAuthenticated_BookingIsActive_ReturnsJsonResult()
        {
            // arrange
            var mockEvent = new EventModelMockBuilder().Default().Build();
            var mockEventBooking = new EventBookingMockBuilder().WithEventBookingId(1).WithStatus(EventBookingStatus.Active).Build();
            var mockBookTicketsRequestViewModel = new BookTicketsRequestViewModel
            {
                EventId = mockEvent.EventId,
                SendEmailToGuests = true,
                Reservations = new List<EventTicketReservedViewModel>
                {
                    new EventTicketReservedViewModel
                    {
                        EventTicketReservationId = 1,
                        GuestEmail = "guest@email.com",
                        GuestFullName = "guest name",
                        TicketFields = new List<EventTicketFieldViewModel>{new EventTicketFieldViewModel { FieldName = "Age", FieldValue = "30", IsRequired = false}}
                    }
                }
            };

            var mockTicketReservations = new[] {
                new EventTicketReservationMockBuilder()
                .WithEventTicketReservationId(1)
                .Build()
            };

            var mockApplicationUser = new ApplicationUserMockBuilder().Default().Build();


            // arrange service calls
            _mockUser.SetupIdentityCall();
            _httpContext.SetupWithVerification(call => call.Session.SessionID, "session123");
            _eventManager.SetupWithVerification(call => call.GetTicketReservations(It.Is<string>(p => p == "session123")), mockTicketReservations);
            _eventManager.SetupWithVerification(call => call.CreateEventBooking(It.IsAny<int>(), It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<EventTicketReservation>>()), mockEventBooking);
            _userManager.SetupWithVerification(call => call.GetUserByEmailOrUsername(It.IsAny<string>()), mockApplicationUser);

            _eventBookingContext.SetupSet(p => p.EventId = It.IsAny<int?>());
            _eventBookingContext.SetupSet(p => p.EventBookingId = It.IsAny<int?>());
            _eventBookingContext.SetupSet(p => p.Purchaser = It.IsAny<string>());
            _eventBookingContext.SetupSet(p => p.SendEmailToGuests = It.Is<bool>(val => val));



            // act
            var controller = BuildController(mockUser: _mockUser);
            var result = controller.BookTickets(mockBookTicketsRequestViewModel);

            // assert
            var jsonResult = result.IsTypeOf<JsonResult>();
            jsonResult.JsonResultNextUrlIs("/Event/EventBooked");
        }

        [Test]
        public void BookTickets_Post_UserIsAuthenticated_SubmitsPayment_ReturnsJsonResult()
        {
            // arrange
            var mockEvent = new EventModelMockBuilder().Default().Build();
            var mockEventBookingId = 888;
            var mockEventBooking = new EventBookingMockBuilder().WithEventBookingId(mockEventBookingId)
                .WithStatus(EventBookingStatus.PaymentPending)
                .Build();

            var mockBookTicketsRequestViewModel = new BookTicketsRequestViewModel
            {
                EventId = mockEvent.EventId,
                Reservations = new List<EventTicketReservedViewModel>
                {
                    new EventTicketReservedViewModel
                    {
                        EventTicketReservationId = 1,
                        GuestEmail = "guest@email.com",
                        GuestFullName = "guest name",
                        TicketFields = new List<EventTicketFieldViewModel>{new EventTicketFieldViewModel { FieldName = "Age", FieldValue = "30", IsRequired = false}}
                    }
                },
                FirstName = "John",
                LastName = "Smith"
            };

            var mockTicketReservations = new[] {
                new EventTicketReservationMockBuilder()
                .WithEventTicketReservationId(1)
                .Build()
            };

            var mockApplicationUser = new ApplicationUserMockBuilder().Default().Build();
            //var mockPaymentResponse = new PaymentResponse { ApprovalUrl = "paypal.com/approveMe", PaymentId = "123", Status = PaymentStatus.ApprovalRequired };

            // arrange service calls
            _mockUser.SetupIdentityCall();
            _httpContext.SetupWithVerification(call => call.Session.SessionID, "session123");
            _eventManager.SetupWithVerification(call => call.GetTicketReservations(It.Is<string>(p => p == "session123")), mockTicketReservations);
            _eventManager.SetupWithVerification(call => call.CreateEventBooking(It.IsAny<int>(), It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<EventTicketReservation>>()), mockEventBooking);
            _userManager.SetupWithVerification(call => call.GetUserByEmailOrUsername(It.IsAny<string>()), mockApplicationUser);

            _eventBookingContext.SetupSet(p => p.EventId = It.IsAny<int?>());
            _eventBookingContext.SetupSet(p => p.EventBookingId = It.IsAny<int?>());
            _eventBookingContext.SetupSet(p => p.Purchaser = It.IsAny<string>());
            _eventBookingContext.SetupSet(p => p.EventBookingPaymentReference = It.IsAny<string>());
            _eventBookingContext.SetupSet(p => p.SendEmailToGuests = It.IsAny<bool>());

            // act
            var controller = BuildController(mockUser: _mockUser);
            var result = controller.BookTickets(mockBookTicketsRequestViewModel);

            // assert
            var jsonResult = result.IsTypeOf<JsonResult>();
            jsonResult.JsonResultNextUrlIs("/Event/MakePayment");
        }

        [Test]
        public void EventBooked_Get_CreatesInvoices_SendsNotifications()
        {
            // arrange
            var sessionMock = "session123";
            var adMock = new AdSearchResultMockBuilder()
                .Default()
                .Build();

            var eventMock = new EventModelMockBuilder()
                .Default()
                .WithOnlineAdId(adMock.OnlineAdId)
                .Build();

            var eventBookingMock = new EventBookingMockBuilder()
                .WithEventBookingId(987)
                .WithEvent(eventMock)
                .WithEmail("foo@bar.com")
                .WithTotalCost(10)
                .WithPaymentReference("pay123")
                .WithEventBookingTickets(new List<EventBookingTicket>
                {
                    new EventBookingTicketMockBuilder().WithPrice(10).WithTicketName("Boom").Default().Build()
                })
                .WithEventId(eventMock.EventId.GetValueOrDefault())
                .Build();

            var applicationUserMock = new ApplicationUserMockBuilder().Default().Build();

            // arrange service calls ( obviously theres a lot going on here and we should refactor this to use event sourcing)
            _eventBookingContext.SetupWithVerification(call => call.EventBookingId, eventBookingMock.EventBookingId);
            _eventBookingContext.SetupWithVerification(call => call.Purchaser, "George Clooney");
            _eventBookingContext.SetupWithVerification(call => call.SendEmailToGuests, true);
            _eventBookingContext.SetupSet<bool>(s => s.EventBookingComplete = true);

            _httpContext.SetupWithVerification(call => call.Session.SessionID, sessionMock);
            _httpContext.SetupWithVerification(call => call.Server.HtmlEncode(It.IsAny<string>()), "");
            _eventManager.SetupWithVerification(call => call.GetEventBooking(eventBookingMock.EventBookingId), eventBookingMock);
            _eventManager.SetupWithVerification(call => call.AdjustRemainingQuantityAndCancelReservations(sessionMock, eventBookingMock.EventBookingTickets));
            _eventManager.SetupWithVerification(call => call.CreateEventTicketsDocument(eventBookingMock.EventBookingId, It.IsAny<byte[]>(), It.IsAny<DateTime?>()), "Document123");
            _eventManager.SetupWithVerification(call => call.GetEventGroups(It.IsAny<int>(), It.IsAny<int?>()), Task.FromResult(Enumerable.Empty<EventGroup>()));
            _searchService.SetupWithVerification(call => call.GetByAdOnlineId(eventMock.OnlineAdId), adMock);
            _templatingService.SetupWithVerification(call => call.Generate(It.IsAny<object>(), "Tickets"), "<html><body>Output for PDF</body></html>");
            _templatingService.SetupWithVerification(call => call.Generate(It.IsAny<object>(), "Invoice"), "<html><body>Output for PDF Invoice</body></html>");
            _broadcastManager.Setup(call => call.Queue(It.IsAny<IDocType>(), It.IsAny<string[]>())).Returns(new Notification(Guid.NewGuid(), "BoomDoc"));
            _clientConfig.SetupWithVerification(call => call.ClientName, "A-Brand");
            _clientConfig.SetupWithVerification(call => call.ClientAddress, new Address() { StreetNumber = "1", StreetName = "Smith Street", State = "VIC" });
            _clientConfig.SetupWithVerification(call => call.ClientPhoneNumber, "9999 0000");
            _clientConfig.SetupWithVerification(call => call.FacebookAppId, "123");
            _userManager.SetupWithVerification(call => call.GetUserByEmailOrUsername("foo@bar.com"), applicationUserMock);
            _barcodeManager.SetupWithVerification(call => call.GenerateBarcodeData(It.IsAny<EventModel>(), It.IsAny<EventBookingTicket>()), "http://somewhere-cool/2020/20200");
            _barcodeManager.SetupWithVerification(call => call.GenerateBase64StringImageData(It.IsAny<EventModel>(), It.IsAny<EventBookingTicket>(), It.IsAny<int>(), It.IsAny<int>(), 0), result: "kdk34830498lkdjf0934");

            // act
            var result = BuildController().EventBooked();

            // assert
            result.IsTypeOf<ViewResult>();
            var viewModel = result.ViewResultModelIsTypeOf<EventBookedViewModel>();
            viewModel.EventHasGroups.IsEqualTo(false);

            _broadcastManager.Verify(call => call.Queue(It.IsAny<IDocType>(), It.IsAny<string[]>()), Times.Exactly(2)); // Sends the tickets and each guest a calendar invite!
        }

        [Test]
        public void AuthorisePayPal_CallsPaymentServiceAndEventManager()
        {
            // arrange 
            var mockEventBooking = new EventBookingMockBuilder()
                .WithUserId("user123")
                .WithTotalCost(100)
                .WithEventBookingId(1000)
                .Build();

            _eventBookingContext.SetupWithVerification(call => call.EventBookingId, 1000);
            _eventBookingContext.SetupWithVerification(call => call.EventBookingPaymentReference, "ref123");
            _eventBookingContext.SetupWithVerification(call => call.EventInvitationId, 123);

            _eventManager.SetupWithVerification(call => call.GetEventBooking(It.IsAny<int>()), mockEventBooking);
            _eventManager.SetupWithVerification(call => call.ActivateBooking(It.IsAny<int>(), It.IsAny<long>()));
            _paymentService.SetupWithVerification(call => call.CompletePayment(
                "ref123",
                "payer123",
                "user123",
                100,
                "1000",
                TransactionTypeName.EventBookingTickets
                ));

            // act
            var controller = BuildController();
            var result = controller.AuthorisePayPal("payer123");

            // assert
            result.IsRedirectingTo("Event", "EventBooked");
        }

        [Test]
        public void Invite_CannotBeFound_Returns404()
        {
            // arrange
            _eventManager.SetupWithVerification(call => call.GetEventInvitation(It.IsAny<long>()), null);

            // act
            var controller = BuildController();
            var result = controller.Invite(123);

            result.IsRedirectingTo("error", "notfound");
        }

        [Test]
        public void Invite_Returns_View()
        {
            // arrange
            var eventInvitation = new EventInvitationMockBuilder().Default().Build();
            var eventModel = new EventModelMockBuilder().Default().Build();
            var adSearchResult = new AdSearchResultMockBuilder().Default().Build();
            var userNetwork = new UserNetworkModelMockBuilder().Default();

            _eventManager.SetupWithVerification(call => call.GetEventInvitation(It.IsAny<long>()), eventInvitation);
            _eventManager.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), eventModel);
            _searchService.SetupWithVerification(call => call.GetByAdOnlineId(It.IsAny<int>()), adSearchResult);
            _userManager.SetupWithVerification(call => call.GetUserNetwork(It.IsAny<int>()), userNetwork);

            // act
            var controller = BuildController();
            var result = controller.Invite(123);

            result.IsTypeOf<ViewResult>();
        }

        private Mock<HttpContextBase> _httpContext;
        private Mock<IEventBookingContext> _eventBookingContext;
        private Mock<ISearchService> _searchService;
        private Mock<IEventManager> _eventManager;
        private Mock<IClientConfig> _clientConfig;
        private Mock<IUserManager> _userManager;
        private Mock<IPayPalService> _paymentService;
        private Mock<IBroadcastManager> _broadcastManager;
        private Mock<IBookingManager> _bookingManager;
        private Mock<IEventTicketReservationFactory> _eventTicketReservationFactory;
        private Mock<IPrincipal> _mockUser;
        private Mock<ITemplatingService> _templatingService;
        private Mock<IEventBarcodeManager> _barcodeManager;
        private Mock<IApplicationConfig> _appConfig;
        private Mock<ICreditCardService> _creditCardService;

        [SetUp]
        public void SetupController()
        {
            _mockUser = CreateMockOf<IPrincipal>();
            _httpContext = CreateMockOf<HttpContextBase>();
            _eventBookingContext = CreateMockOf<IEventBookingContext>();
            _searchService = CreateMockOf<ISearchService>();
            _eventManager = CreateMockOf<IEventManager>();
            _clientConfig = CreateMockOf<IClientConfig>();
            _userManager = CreateMockOf<IUserManager>();
            _paymentService = CreateMockOf<IPayPalService>();
            _broadcastManager = CreateMockOf<IBroadcastManager>();
            _bookingManager = CreateMockOf<IBookingManager>();
            _eventTicketReservationFactory = CreateMockOf<IEventTicketReservationFactory>();
            _templatingService = CreateMockOf<ITemplatingService>();
            _templatingService.Setup(call => call.Init(It.IsAny<Controller>())).Returns(_templatingService.Object);
            _barcodeManager = CreateMockOf<IEventBarcodeManager>();
            _appConfig = CreateMockOf<IApplicationConfig>();
            _creditCardService = CreateMockOf<ICreditCardService>();
        }
    }
}