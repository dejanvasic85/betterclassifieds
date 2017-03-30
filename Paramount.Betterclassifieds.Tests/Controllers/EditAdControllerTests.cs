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
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Presentation.Services.Mail;
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

            var mockSearchResult = new AdSearchResultMockBuilder()
                .WithOnlineAdId(onlineAdId)
                .WithNumOfViews(7)
                .WithHeading("Business Research Event")
                .Build();
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

            _userManagerMock.SetupWithVerification(call => call.GetCurrentUser(), new ApplicationUserMockBuilder().Default().Build());

            // Act 
            var result = BuildController().EventDashboard(adId);

            // Assert
            var viewModel = result.ViewResultModelIsTypeOf<EventDashboardViewModel>();

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
            Assert.That(viewModel.EventName, Is.Not.Null);
        }

        [Test]
        public void EventGuestListDownloadPdf_Get_Returns_FileResult()
        {
            // arrange
            const int adId = 1;
            const int eventId = 2;
            const string mockPdfOutput = "<html><body>Sample Data</body></html>";
            const string expectedViewLocation = "~/Views/Templates/EventGuestList.cshtml";

            var builder = new EventGuestDetailsMockBuilder();
            var mockGuests = new[]
            {
                builder.WithGuestEmail("foo@bar.com").WithGuestFullName("Foo bar").Build(),
                builder.WithGuestEmail("foo@bar.com").WithGuestFullName("Foo bar").Build()
            };

            var mockSearchResult = new AdSearchResultMockBuilder().WithHeading("Testing").Build();

            _searchServiceMock.SetupWithVerification(call => call.GetByAdId(It.Is<int>(p => p == 1)), mockSearchResult);

            _templatingServiceMock.SetupWithVerification(call => call.Generate(It.IsAny<object>(), It.Is<string>(param => param == expectedViewLocation)), mockPdfOutput);
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
            var adId = 10;
            var eventId = 1;
            decimal requestedAmount = 100;
            var paymentMethod = "PayPal";
            var supportEmails = new[] { "support@email.com" };

            _eventManagerMock.SetupWithVerification(call => call.CreateEventPaymentRequest(
                It.Is<int>(p => p == eventId),
                It.Is<PaymentType>(p => p == PaymentType.PayPal),
                It.Is<decimal>(p => p == requestedAmount)));

            _searchServiceMock.SetupWithVerification(
                call => call.GetByAdId(It.IsAny<int>()), 
                new AdSearchResultMockBuilder().Default().WithAdId(adId).Build());

            _eventManagerMock.SetupWithVerification(
                call => call.GetEventDetails(It.IsAny<int>()), 
                new EventModelMockBuilder().Default().WithEventId(eventId).Build());

            _mailService.SetupWithVerification(call => call.SendEventPaymentRequest(
                It.IsAny<AdSearchResult>(),
                It.IsAny<EventModel>(),
                It.Is<string>(payment => payment == paymentMethod),
                It.Is<decimal>(amount => amount == requestedAmount)
            ));

            // act
            var controller = BuildController();

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
            var mockAd = new AdSearchResultMockBuilder().Default().Build();

            // Mock service calls
            _searchServiceMock.SetupWithVerification(call => call.GetByAdId(It.IsAny<int>()), mockAd);
            _eventManagerMock.SetupWithVerification(call => call.GetEventDetailsForOnlineAdId(It.IsAny<int>(), It.IsAny<bool>()), mockEvent);
            _eventManagerMock.Setup(call => call.GetEventGroupsAsync(It.IsAny<int>(), It.IsAny<int?>())).Returns(Task.FromResult(Enumerable.Empty<EventGroup>()));

            // Act
            var controller = BuildController();
            var result = controller.AddGuest(123).Result;

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
                call => call.CreateFreeReservation(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<EventTicket>()),
                mockEventTicketReservation);

            _eventManagerMock.SetupWithVerification(call => call.CreateEventBooking(It.IsAny<int>(),
                It.IsAny<ApplicationUser>(),
                It.IsAny<IEnumerable<EventTicketReservation>>(),
                It.IsAny<Func<string, string>>()), mockEventBooking);

            _httpContextBase.SetupWithVerification(call => call.Session.SessionID, "1234");

            _userManagerMock.SetupWithVerification(call => call.GetCurrentUser(), mockApplicationUser);
            
            _eventManagerMock.SetupWithVerification(call => call.AdjustRemainingQuantityAndCancelReservations(It.IsAny<string>(),
                It.IsAny<IList<EventBookingTicket>>()));

            _eventBookingManager
                .SetupWithVerification(call => call.WithEventBooking(It.IsAny<int?>()), result: _eventBookingManager.Object)
                .SetupWithVerification(call => call.SendTicketToGuest(It.IsAny<string>()), _eventBookingManager.Object);

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
                It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<EventTicket>()), mockEventTicketReservation);

            _httpContextBase.SetupWithVerification(call => call.Session.SessionID, "1234");

            var controller = BuildController();

            var result = controller.AddGuest(100, new AddEventGuestViewModel());
            result.IsTypeOf<JsonResult>();

            Assert.That(controller.ModelState.ContainsKey("SelectedTicket"), Is.True);
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

        [Test]
        public void ManageGroups_Get_Returns_ViewResult()
        {
            // Arrange
            var mockAdId = 1;
            var mockEventId = 123;
            var mockTicket = new EventTicketMockBuilder().Default().Build();
            var mockTicketsForGroup = new[] { mockTicket };
            var mockEvent = new EventModelMockBuilder().Default().WithGroupsRequired(true).WithCustomTicket(mockTicket).Build();
            var eventGroups = new[] { new EventGroupMockBuilder().Default().Build() };

            // Arrange service calls
            _eventManagerMock.SetupWithVerification(call => call.GetEventDetails(It.IsAny<int>()), mockEvent);

            _eventManagerMock
                .Setup(call => call.GetEventGroupsAsync(It.IsAny<int>(), It.IsAny<int?>()))
                .Returns(Task.FromResult(eventGroups.AsEnumerable()));

            _eventManagerMock
                .Setup(call => call.GetEventTicketsForGroup(It.IsAny<int>()))
                .Returns(Task.FromResult(mockTicketsForGroup.AsEnumerable()));

            // Act
            var controller = BuildController();
            var viewResult = controller.ManageGroups(mockAdId, mockEventId).Result;

            // Assert - ensure tha mapping
            var vm = viewResult.ViewResultModelIsTypeOf<ManageGroupsViewModel>();
            vm.Id.IsEqualTo(mockAdId);
            vm.EventId.IsEqualTo(mockEventId);
            vm.EventGroups.Count.IsEqualTo(1);
            vm.EventGroups.Single().AvailableTickets.Count.IsEqualTo(1);
            vm.GroupsRequired.IsTrue();
        }

        [Test]
        public void UpdateEventGroupSettings_Post_ReturnsJson()
        {
            _eventManagerMock.SetupWithVerification(call =>
                call.UpdateEventGroupSettings(It.Is<int>(v => v == 123), It.Is<bool>(v => v)));

            // Act 
            var controller = BuildController();
            var result = controller.UpdateEventGroupSettings(1, 123, true);

            // Assert
            result.IsTypeOf<JsonResult>();
        }

        [Test]
        public void ResendGuestEmail_SendsEmailToIndividualGuest()
        {
            var mockEventBookingTicket = new EventBookingTicketMockBuilder()
                .WithGuestEmail("guest@one.com")
                .Build();

            // arrange service calls
            _eventManagerMock.SetupWithVerification(call => call.GetEventBookingTicket(It.IsAny<int>()), mockEventBookingTicket);
            _eventBookingManager.SetupWithVerification(call => call.WithEventBooking(It.IsAny<int>()), _eventBookingManager.Object);
            _eventBookingManager.SetupWithVerification(call => call.SendTicketToGuest(It.IsAny<EventBookingTicket>()), _eventBookingManager.Object);

            var controller = BuildController();
            var result = controller.ResendGuestEmail(1, eventBookingTicketId: 100);

            result.IsTypeOf<JsonResult>();
        }


        [Test]
        public void EditTicket_Get_ReturnsView()
        {
            var mockTicket = new EventTicketMockBuilder().Default().Build();
            var guestBuilder = new EventGuestDetailsMockBuilder();
            var guestList = new[]
            {
                guestBuilder.WithGuestEmail("foo1@bar.com").WithTicketId(mockTicket.EventTicketId.Value).Build(),
                guestBuilder.WithGuestEmail("foo2@bar.com").WithTicketId(mockTicket.EventTicketId.Value).Build()
            };

            _eventManagerMock.SetupWithVerification(call => call.GetEventTicket(It.IsAny<int>()), mockTicket);
            _eventManagerMock.SetupWithVerification(call => call.BuildGuestList(It.IsAny<int>()), guestList);

            var result = BuildController().EditTicket(100, mockTicket.EventTicketId.Value);
            var viewModel = result.ViewResultModelIsTypeOf<EventTicketViewModel>();
            viewModel.IsNotNull();
            viewModel.SoldQty.IsEqualTo(2);
        }

        [Test]
        public void EditTicket_Get_NoTicket_Returns404()
        {
            _eventManagerMock.SetupWithVerification(call => call.GetEventTicket(It.IsAny<int>()), null);
            var result = BuildController().EditTicket(0, 0);
            result.IsRedirectingToNotFound();
        }

        [Test]
        public void EditTicket_Post_TicketsSoldDuringEditing_ReturnsJsonError()
        {
            var viewModelMock = new UpdateEventTicketViewModel()
            {
                EventTicket = new EventTicketViewModel
                {
                    EventTicketId = 100,
                    SoldQty = 0,
                    EventId = 1
                }
            };

            var guestBuilder = new EventGuestDetailsMockBuilder();
            var guestList = new[]
            {
                guestBuilder.WithGuestEmail("foo1@bar.com").WithTicketId(100).Build(),
                guestBuilder.WithGuestEmail("foo2@bar.com").WithTicketId(100).Build()
            };

            // mock service calls 
            _eventManagerMock.SetupWithVerification(call => call.BuildGuestList(It.IsAny<int>()), guestList);

            var result = BuildController().EditTicket(1, viewModelMock.EventTicket.EventTicketId.Value, viewModelMock);
            var jsonResult = result.IsTypeOf<JsonResult>();
        }

        [Test]
        public void EditTicketSettings_ReturnsJson()
        {
            var mockSettings = new TicketSettingsViewModel
            {
                IncludeTransactionFee = true
            };

            _eventManagerMock.SetupWithVerification(
                call => call.UpdateEventTicketSettings(
                    It.Is<int>(id => id == 10),
                    It.Is<bool>(val => val == mockSettings.IncludeTransactionFee),
                    It.Is<DateTime?>(val => val == mockSettings.ClosingDate),
                    It.Is<DateTime?>(val => val == mockSettings.OpeningDate)));

            var controller = BuildController();

            controller.EditTicketSettings(1, 10, mockSettings);
        }

        private Mock<ISearchService> _searchServiceMock;
        private Mock<IApplicationConfig> _applicationConfigMock;
        private Mock<IClientConfig> _clientConfigMock;
        private Mock<IBookingManager> _bookingManagerMock;
        private Mock<IEventManager> _eventManagerMock;
        private Mock<ITemplatingService> _templatingServiceMock;
        private Mock<IUserManager> _userManagerMock;
        private Mock<IDateService> _dateService;
        private Mock<IEventTicketReservationFactory> _eventTicketReservationFactory;
        private Mock<HttpContextBase> _httpContextBase;
        private Mock<IEventBarcodeValidator> _eventBarcodeValidator;
        private Mock<IEventBookingManager> _eventBookingManager;
        private Mock<IMailService> _mailService;


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
            _dateService = CreateMockOf<IDateService>();
            _eventTicketReservationFactory = CreateMockOf<IEventTicketReservationFactory>();
            _httpContextBase = CreateMockOf<HttpContextBase>();
            _eventBarcodeValidator = CreateMockOf<IEventBarcodeValidator>();
            _eventBookingManager = CreateMockOf<IEventBookingManager>();

            _mailService = CreateMockOf<IMailService>();
            _mailService.Setup(call => call.Initialise(It.IsAny<Controller>()))
                .Returns(_mailService.Object);

            _eventBookingManager
                .Setup(call => call.WithTemplateService(It.IsAny<ITemplatingService>()))
                .Returns(_eventBookingManager.Object);

            _eventBookingManager
               .Setup(call => call.WithMailService(It.IsAny<IMailService>()))
               .Returns(_eventBookingManager.Object);
        }
    }
}