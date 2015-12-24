using System;
using System.Collections.Generic;
using System.Security.Principal;
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
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    [TestFixture]
    internal class EventControllerTests : ControllerTest<EventController>
    {
        [Test]
        public void ViewEventAd_Get_ReturnsViewResult()
        {

            var mockAd = new AdSearchResultMockBuilder()
                .Default()
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

            // act
            var result = BuildController().ViewEventAd(mockAd.AdId);

            // assert
            result.IsTypeOf<ViewResult>();
            var eventViewModel = result.ViewResultModelIsTypeOf<EventViewDetailsModel>();
            eventViewModel.AdId.IsEqualTo(mockAd.AdId);
            eventViewModel.EventId.IsEqualTo(mockEventAd.EventId.GetValueOrDefault());
            eventViewModel.Title.IsEqualTo(mockAd.Heading);
            eventViewModel.Description.IsEqualTo(mockAd.Description);
            eventViewModel.EventPhoto.IsEqualTo(mockAd.PrimaryImage);
            eventViewModel.IsClosed.IsEqualTo(true);
            eventViewModel.EventStartDate.IsEqualTo(mockEventAd.EventStartDate.GetValueOrDefault().ToLongDateString());
            eventViewModel.EventStartTime.IsEqualTo(mockEventAd.EventStartDate.GetValueOrDefault().ToString("hh:mm tt"));
            eventViewModel.EventEndDate.IsEqualTo(mockEventAd.EventEndDate.GetValueOrDefault().ToLongDateString());

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
            var redirectResult = result.IsTypeOf<RedirectToRouteResult>();
            redirectResult.RedirectResultControllerIs("Error");
            redirectResult.RedirectResultActionIs("NotFound");
        }

        [Test]
        public void ReserveTickets_Post_NothingSelected_ReturnsModelError()
        {
            var controller = BuildController();
            var result = controller.ReserveTickets(null);
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

            // arrage services
            _httpContext.SetupWithVerification(call => call.Session.SessionID, "123");
            _eventManager.SetupWithVerification(call => call.ReserveTickets(It.IsAny<string>(), It.IsAny<IEnumerable<EventTicketReservation>>()));
            _eventTicketReservationFactory.SetupWithVerification(call => call.CreateReservations(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), mockReservations);

            // act
            var controller = BuildController();
            var result = controller.ReserveTickets(eventTicketRequests);

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
            result.IsTypeOf<RedirectToRouteResult>().RedirectResultIsNotFound();
        }

        [Test]
        public void BookTickets_Get_WithReservationsInSession_ReturnsView()
        {
            // Arrange
            var mockReservations = new List<EventTicketReservation>
            {
                new EventTicketReservation {EventTicket = new EventTicket {EventId = 10} }
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
            _mockUser.SetupIdentityCall();

            // act
            var result = BuildController(mockUser: _mockUser).BookTickets();

            // assert
            result.IsTypeOf<ViewResult>();
            result.ViewResultModelIsTypeOf<BookTicketsViewModel>();
        }

        [Test]
        public void BookTickets_Post_WithModelStateErrors_ReturnsJsonErrors()
        {
            var controller = BuildController();
            controller.ModelState.AddModelError("EventId", "EventId must be specified");

            var result = controller.BookTickets(null);

            var jsonResult = result.IsTypeOf<JsonResult>();
            jsonResult.JsonResultContainsErrors();
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
                }
            };

            var mockTicketReservations = new[] {
                new EventTicketReservationMockBuilder()
                .WithEventTicketReservationId(1)
                .Build()
            };

            var mockApplicationUser = new ApplicationUserMockBuilder().Default().Build();
            var mockPaymentResponse = new PaymentResponse { ApprovalUrl = "paypal.com/approveMe", PaymentId = "123", Status = PaymentStatus.ApprovalRequired };


            // arrange service calls
            _mockUser.SetupIdentityCall();
            _httpContext.SetupWithVerification(call => call.Session.SessionID, "session123");
            _eventManager.SetupWithVerification(call => call.GetTicketReservations(It.Is<string>(p => p == "session123")), mockTicketReservations);
            _eventManager.SetupWithVerification(call => call.CreateEventBooking(It.IsAny<int>(), It.IsAny<ApplicationUser>(), It.IsAny<IEnumerable<EventTicketReservation>>()), mockEventBooking);
            _eventManager.SetupWithVerification(call => call.SetPaymentReferenceForBooking(It.Is<int>(p => p == mockEventBookingId), It.Is<string>(p => p == mockPaymentResponse.PaymentId), It.Is<PaymentType>(p => p == PaymentType.PayPal)));
            _userManager.SetupWithVerification(call => call.GetUserByEmailOrUsername(It.IsAny<string>()), mockApplicationUser);
            _paymentService.SetupWithVerification(call => call.SubmitPayment(It.IsAny<PayPalPaymentRequest>()), mockPaymentResponse);

            // act
            var controller = BuildController(mockUser: _mockUser);
            var result = controller.BookTickets(mockBookTicketsRequestViewModel);

            // assert
            var jsonResult = result.IsTypeOf<JsonResult>();
            jsonResult.JsonResultNextUrlIs(mockPaymentResponse.ApprovalUrl);
        }

        private Mock<HttpContextBase> _httpContext;
        private Mock<EventBookingContext> _eventBookingContext;
        private Mock<ISearchService> _searchService;
        private Mock<IEventManager> _eventManager;
        private Mock<IClientConfig> _clientConfig;
        private Mock<IUserManager> _userManager;
        private Mock<IAuthManager> _authManager;
        private Mock<IPaymentService> _paymentService;
        private Mock<IBroadcastManager> _broadcastManager;
        private Mock<IBookingManager> _bookingManager;
        private Mock<IEventTicketReservationFactory> _eventTicketReservationFactory;
        private Mock<IPrincipal> _mockUser;

        [SetUp]
        public void SetupController()
        {
            _mockUser = CreateMockOf<IPrincipal>();
            _httpContext = CreateMockOf<HttpContextBase>();
            _eventBookingContext = CreateMockOf<EventBookingContext>();
            _searchService = CreateMockOf<ISearchService>();
            _eventManager = CreateMockOf<IEventManager>();
            _clientConfig = CreateMockOf<IClientConfig>();
            _userManager = CreateMockOf<IUserManager>();
            _authManager = CreateMockOf<IAuthManager>();
            _paymentService = CreateMockOf<IPaymentService>();
            _broadcastManager = CreateMockOf<IBroadcastManager>();
            _bookingManager = CreateMockOf<IBookingManager>();
            _eventTicketReservationFactory = CreateMockOf<IEventTicketReservationFactory>();
        }
    }
}