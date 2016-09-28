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
    internal class EditAdControllerTests : ControllerTest<EditAdController>
    {

        [Test]
        public void EventDashboard_Get_Returns_ViewResult()
        {
            // Arrange 
            const int adId = 23;
            const int onlineAdId = 90;
            const int eventId = 10;

            var mockSearchResult = new AdSearchResultMockBuilder().WithOnlineAdId(onlineAdId).WithNumOfViews(7).Build();
            var mockGuestListBuilder = new EventGuestDetailsMockBuilder().WithGuestEmail("foo@bar.com").WithGuestFullName("Foo Bar").WithTicketNumber(123);
            var mockGuestList = new[] { mockGuestListBuilder.Build(), mockGuestListBuilder.Build() };
            var mockTicketBuilder = new EventTicketMockBuilder().WithRemainingQuantity(5).WithEventId(eventId);
            var mockEvent = new EventModelMockBuilder()
                .WithEventId(eventId)
                .WithTickets(new[] { mockTicketBuilder.Build() })
                .WithClosingDateUtc(DateTime.Now.AddDays(-1))
                .Build();
            var mockPaymentSummary = new EventPaymentSummaryMockBuilder()
                .WithEventOrganiserOwedAmount(90)
                .WithEventOrganiserFeesTotalFeesAmount(10)
                .WithTotalTicketSalesAmount(100).Build();

            _searchServiceMock.SetupWithVerification(call => call.GetByAdId(It.Is<int>(p => p == adId)), mockSearchResult);
            _eventManagerMock.SetupWithVerification(call => call.GetEventDetailsForOnlineAdId(It.Is<int>(p => p == onlineAdId), It.Is<bool>(p => true)), mockEvent);
            _eventManagerMock.SetupWithVerification(call => call.BuildGuestList(It.Is<int>(p => p == eventId)), mockGuestList);
            _eventManagerMock.SetupWithVerification(call => call.BuildPaymentSummary(It.Is<int>(p => p == eventId)), mockPaymentSummary);
            _eventManagerMock.SetupWithVerification(call => call.GetEventPaymentRequestStatus(It.Is<int>(p => p == eventId)), EventPaymentRequestStatus.Complete);

            // Act 
            var result = BuildController().EventDashboard(adId);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewModel = ((ViewResult)result).Model as EventDashboardViewModel;
            Assert.That(viewModel, Is.Not.Null);

            Assert.That(viewModel.PageViews, Is.EqualTo(7));
            Assert.That(viewModel.Tickets.Count, Is.EqualTo(1));
            Assert.That(viewModel.AdId, Is.EqualTo(adId));
            Assert.That(viewModel.EventId, Is.EqualTo(eventId));
            Assert.That(viewModel.Guests.Count, Is.EqualTo(2));
            Assert.That(viewModel.TotalRemainingQty, Is.EqualTo(5));
            Assert.That(viewModel.TotalTicketFees, Is.EqualTo("$10.00"));
            Assert.That(viewModel.EventOrganiserOwedAmount, Is.EqualTo(90));
            Assert.That(viewModel.TotalSoldAmount, Is.EqualTo(100));
            Assert.That(viewModel.IsClosed, Is.True);
        }

        [Test]
        public void EventGuestListDownloadPdf_Get_Returns_FileResult()
        {
            // arrange
            const int adId = 1;
            const int eventId = 2;
            const string mockPdfOutput = "<html><body>Sample Data</body></html>";


            var builder = new EventGuestDetailsMockBuilder();
            var mockGuests = new[]
            {
                builder.WithGuestEmail("foo@bar.com").WithGuestFullName("Foo bar").Build(),
                builder.WithGuestEmail("foo@bar.com").WithGuestFullName("Foo bar").Build()
            };

            var mockSearchResult = new AdSearchResultMockBuilder().WithHeading("Testing").Build();

            _searchServiceMock.SetupWithVerification(call => call.GetByAdId(It.Is<int>(p => p == 1)), mockSearchResult);
            _templatingServiceMock.SetupWithVerification(call => call.Generate(It.IsAny<object>(), It.Is<string>(param => param == "EventGuestList")), mockPdfOutput);
            _eventManagerMock.SetupWithVerification(call => call.BuildGuestList(It.Is<int?>(val => val == eventId)), mockGuests);


            // act
            var result = BuildController().DownloadGuestListPdf(adId, eventId);

            // assert
            Assert.That(result, Is.TypeOf<FileContentResult>());
            var fileContentResult = (FileContentResult)result;
            Assert.That(fileContentResult.FileDownloadName, Is.EqualTo(mockSearchResult.Heading + " - Guest List.pdf"));
            Assert.That(fileContentResult.ContentType, Is.EqualTo("application/pdf"));
        }

        [Test]
        public void EventPaymentRequest_Get_Returns_View()
        {
            // arrange
            int eventId = 1939;
            var mockEvent = new EventModelMockBuilder()
                .WithEventId(eventId)
                .WithPastClosedDate()
                .Build();

            var mockApplicationUser = new ApplicationUserMockBuilder().Default().Build();

            var mockPrincipal = CreateMockOf<IPrincipal>();
            var mockPaymentSummary = new EventPaymentSummaryMockBuilder()
                .WithEventOrganiserOwedAmount(90)
                .WithEventOrganiserFeesTotalFeesAmount(10)
                .WithTotalTicketSalesAmount(100)
                .Build();

            _userManagerMock.SetupWithVerification(call => call.GetCurrentUser(It.IsAny<IPrincipal>()), mockApplicationUser);
            _eventManagerMock.SetupWithVerification(call => call.BuildPaymentSummary(It.IsAny<int>()), mockPaymentSummary);
            _eventManagerMock.SetupWithVerification(call => call.GetEventDetails(It.Is<int>(p => p == eventId)), mockEvent);

            // act
            var result = this.BuildController(mockUser: mockPrincipal).EventPaymentRequest(100, eventId);

            // assert
            Assert.That(result, Is.TypeOf<ViewResult>());
            var viewModel = ((ViewResult)result).Model as EventPaymentSummaryViewModel;
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.AmountOwed, Is.EqualTo(90));
            Assert.That(viewModel.OurFees, Is.EqualTo(10));
            Assert.That(viewModel.TotalTicketSalesAmount, Is.EqualTo(100));
            Assert.That(viewModel.PreferredPaymentType, Is.EqualTo(mockApplicationUser.PreferredPaymentMethod.ToString()));
            Assert.That(viewModel.PayPalEmail, Is.EqualTo(mockApplicationUser.PayPalEmail));
            Assert.That(viewModel.DirectDebitDetails, Is.Not.Null);
            Assert.That(viewModel.DirectDebitDetails.BankName, Is.EqualTo(mockApplicationUser.BankName));
            Assert.That(viewModel.DirectDebitDetails.AccountName, Is.EqualTo(mockApplicationUser.BankAccountName));
            Assert.That(viewModel.DirectDebitDetails.AccountNumber, Is.EqualTo(mockApplicationUser.BankAccountNumber));
            Assert.That(viewModel.DirectDebitDetails.BSB, Is.EqualTo(mockApplicationUser.BankBsbNumber));
        }

        [Test]
        public void EventPaymentRequest_Get_WithNotClosedEvent_View()
        {
            // arrange
            int eventId = 1939;
            var mockEvent = new EventModelMockBuilder()
                .WithEventId(eventId)
                .WithFutureClosedDate()
                .Build();

            _eventManagerMock.SetupWithVerification(call => call.GetEventDetails(It.Is<int>(p => p == eventId)), mockEvent);

            // act
            var result = this.BuildController().EventPaymentRequest(2929, eventId);

            // assert
            result.IsTypeOf<RedirectResult>();
            result.IsRedirectingTo("/editAd/eventDashboard/2929");
        }

        [Test]
        public void EventPaymentRequest_Post_CallsManagerAndBroadcast_ReturnsJsonUrl()
        {
            // arrange
            var username = "fooBarr";
            var mockUser = CreateMockOf<IPrincipal>().SetupIdentityCall(username);
            var adId = 10;
            var eventId = 1;
            var requestedAmount = 100;
            var paymentMethod = "PayPal";
            var supportEmails = new[] { "support@email.com" };

            _clientConfigMock.SetupWithVerification(call => call.SupportEmailList, supportEmails);
            _eventManagerMock.SetupWithVerification(call => call.CreateEventPaymentRequest(
                It.Is<int>(p => p == eventId),
                It.Is<PaymentType>(p => p == PaymentType.PayPal),
                It.Is<decimal>(p => p == requestedAmount),
                It.Is<string>(p => p == username)));

            _broadcastManagerMock.SetupWithVerification(call => call.SendEmail(
                It.IsAny<Business.Broadcast.EventPaymentRequest>(),
                It.Is<string[]>(p => p == supportEmails)), new Notification(Guid.NewGuid()));

            // act
            var controller = BuildController(mockUser: mockUser);

            var result = controller.EventPaymentRequest(adId, new EventPaymentRequestViewModel
            {
                EventId = eventId,
                PaymentMethod = paymentMethod,
                RequestedAmount = requestedAmount
            });

            // assert
            result.IsTypeOf<JsonResult>();
            result.JsonResultContains("{ NextUrl = /editAd/eventDashboard/" + adId + " }");
        }

        [Test]
        public void CloseEvent_Post_CallsManager_ReturnsJson()
        {
            int eventId = 100;
            int adId = 1949;
            _eventManagerMock.SetupWithVerification(call => call.CloseEvent(It.Is<int>(p => p == eventId)));

            var result = BuildController().CloseEvent(adId, eventId);

            // assert
            result.IsTypeOf<JsonResult>();
        }

        [Test]
        public void AddGuest_Get_MapsToViewModel_ReturnsActionResult()
        {
            var mockTicket = new EventTicketMockBuilder().Default().Build();
            var mockEvent = new EventModelMockBuilder().Default()
                .WithTickets(new[] { mockTicket })
                .Build();

            // Mock service calls

            _eventManagerMock.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);
            var eventGroups = new List<EventGroup>();
            _eventManagerMock.Setup(call => call.GetEventGroups(It.IsAny<int>(), It.IsAny<int?>())).Returns(Task.FromResult(eventGroups.AsEnumerable()));

            // Act
            var controller = BuildController();
            var result = controller.AddGuest(123, mockEvent.EventId.GetValueOrDefault()).Result;

            result.IsTypeOf<ActionResult>();
            var vm = result.ViewResultModelIsTypeOf<AddEventGuestViewModel>();
            vm.EventId.IsEqualTo(mockEvent.EventId);
            vm.Id.IsEqualTo(123);
            vm.EventTickets.Count.IsEqualTo(1);
        }

        [Test]
        public void AddGuest_Post_ReturnsJsonResult()
        {
            // Mock objects
            var mockEvent = new EventModelMockBuilder().Default().Build();
            var mockEventTicket = new EventTicketMockBuilder().Default().Build();
            var mockTicketField = new EventTicketFieldMockBuilder().Default().Build();
            var mockEventTicketReservation = new EventTicketReservationMockBuilder().Build();
            var mockEventBooking = new EventBookingMockBuilder().Default().Build();
            var mockApplicationUser = new ApplicationUserMockBuilder().Default().Build();

            // arrange calls
            _eventManagerMock.SetupWithVerification(call => call.GetEventTicket(It.IsAny<int>()), mockEventTicket);
            _eventTicketReservationFactory.SetupWithVerification(
                call => call.CreateFreeReservation(It.IsAny<string>(), It.IsAny<EventTicket>()),
                mockEventTicketReservation);

            _eventManagerMock.SetupWithVerification(call => call.CreateEventBooking(It.IsAny<int>(),
                It.IsAny<ApplicationUser>(),
                It.IsAny<IEnumerable<EventTicketReservation>>()), mockEventBooking);

            _httpContextBase.SetupWithVerification(call => call.Session.SessionID, "1234");

            _userManagerMock.SetupWithVerification(call => call.GetCurrentUser(), mockApplicationUser);

            _eventManagerMock.SetupWithVerification(call => call.AdjustRemainingQuantityAndCancelReservations(It.IsAny<string>(),
                It.IsAny<IList<EventBookingTicket>>()));

            _eventNotificationBuilder
                .SetupWithVerification(call => call.WithEventBooking(It.IsAny<int?>()), result: _eventNotificationBuilder.Object)
                .SetupWithVerification(call => call.CreateTicketPurchaserNotification(), new EventTicketsBookedNotification())
                .SetupWithVerification(call => call.CreateEventGuestNotifications(), new [] {new EventGuestNotification() });
                
            _broadcastManagerMock.SetupWithVerification(call => call.Queue(It.IsAny<IDocType>(), It.IsAny<string>()), result: null);
            
            // Act
            var controller = BuildController(mockUser: new Mock<IPrincipal>());
            var mockRequest = new AddEventGuestViewModel
            {
                EventId = mockEvent.EventId,
                SelectedTicket = new EventTicketViewModel { EventTicketId = mockEventTicket.EventTicketId },
                TicketFields = new List<EventTicketFieldViewModel>
                {
                    new EventTicketFieldViewModel {FieldName = mockTicketField.FieldName, FieldValue = "123"}
                },
                GuestFullName = "Foo Bar",
                GuestEmail = "Foo@Bar.com",
                SendEmailToGuest = true,
                EventTickets = new List<EventTicketViewModel> { new EventTicketViewModel { TicketName = mockEventTicket.TicketName } }
            };

            var result = controller.AddGuest(123, mockRequest);


            // Assert
            result.JsonResultContains("true");
        }

        [Test]
        public void AddGuest_Post_ModelStateNotValid_ReturnsJsonErrors()
        {
            var controller = BuildController();
            controller.ModelState.AddModelError("err", "msg");

            var result = controller.AddGuest(100, new AddEventGuestViewModel());
            result.IsTypeOf<JsonResult>();
        }


        [Test]
        public void AddGuest_Post_TicketCannotBeReserved_ReturnsJsonErrors()
        {
            var mockEventTicketReservation = new EventTicketReservationMockBuilder().WithStatus(EventTicketReservationStatus.SoldOut).Build();

            _eventManagerMock.SetupWithVerification(call => call.GetEventTicket(It.IsAny<int>()), 
                new EventTicketMockBuilder().Default().Build());
            
            _eventTicketReservationFactory.SetupWithVerification(call => call.CreateFreeReservation(
                It.IsAny<string>(), It.IsAny<EventTicket>()), mockEventTicketReservation);

            _httpContextBase.SetupWithVerification(call => call.Session.SessionID, "1234");

            var controller = BuildController();
            
            var result = controller.AddGuest(100, new AddEventGuestViewModel());
            result.IsTypeOf<JsonResult>();
            
            Assert.That(controller.ModelState.ContainsKey("SelectedTicket") , Is.True);
        }

        [Test]
        public void EditGuest_Get_WithUnknownTicketId_Returns_404()
        {
            // arrange
            _eventManagerMock.SetupWithVerification(
                call => call.GetEventBookingTicket(It.IsAny<int>()), null);

            // act
            var controller = BuildController();
            var result = controller.EditGuest(id: 100, ticketNumber: 200);

            // assert
            result.IsRedirectingTo("error", "notfound");
        }

        private Mock<ISearchService> _searchServiceMock;
        private Mock<IApplicationConfig> _applicationConfigMock;
        private Mock<IClientConfig> _clientConfigMock;
        private Mock<IBookingManager> _bookingManagerMock;
        private Mock<IEventManager> _eventManagerMock;
        private Mock<ITemplatingService> _templatingServiceMock;
        private Mock<IUserManager> _userManagerMock;
        private Mock<IBroadcastManager> _broadcastManagerMock;
        private Mock<IDateService> _dateService;
        private Mock<IEventTicketReservationFactory> _eventTicketReservationFactory;
        private Mock<HttpContextBase> _httpContextBase;
        private Mock<IEventBarcodeManager> _eventBarcodeManager;
        private Mock<IEventNotificationBuilder> _eventNotificationBuilder;

        [SetUp]
        public void SetupDependencies()
        {
            _searchServiceMock = CreateMockOf<ISearchService>();
            _applicationConfigMock = CreateMockOf<IApplicationConfig>();
            _clientConfigMock = CreateMockOf<IClientConfig>();
            _bookingManagerMock = CreateMockOf<IBookingManager>();
            _eventManagerMock = CreateMockOf<IEventManager>();
            _templatingServiceMock = CreateMockOf<ITemplatingService>();
            _templatingServiceMock.Setup(call => call.Init(It.IsAny<Controller>())).Returns(_templatingServiceMock.Object);
            _userManagerMock = CreateMockOf<IUserManager>();
            _broadcastManagerMock = CreateMockOf<IBroadcastManager>();
            _dateService = CreateMockOf<IDateService>();
            _eventTicketReservationFactory = CreateMockOf<IEventTicketReservationFactory>();
            _httpContextBase = CreateMockOf<HttpContextBase>();
            _eventBarcodeManager = CreateMockOf<IEventBarcodeManager>();
            _eventNotificationBuilder = CreateMockOf<IEventNotificationBuilder>();

            _eventNotificationBuilder
                .Setup(call => call.WithTemplateService(It.IsAny<ITemplatingService>()))
                .Returns(_eventNotificationBuilder.Object);
        }
    }
}