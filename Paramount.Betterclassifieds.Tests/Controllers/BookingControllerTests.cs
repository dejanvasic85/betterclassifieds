using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.DocumentStorage;
using Paramount.Betterclassifieds.Business.Location;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Presentation.ViewModels.Booking;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    public class BookingControllerTests : ControllerTest<BookingController>
    {
        [Test]
        public void EventTickets_Get_WithoutEventDetails_ReturnsRedirectResult()
        {
            var mockBookingCart = CreateMockOf<IBookingCart>();
            mockBookingCart.SetupWithVerification(call => call.Event, null);
            mockBookingCart.SetupWithVerification(call => call.CategoryAdType, "Event");

            var controller = BuildController();
            var result = controller.EventTickets(mockBookingCart.Object);

            // assert
            result.IsTypeOf<RedirectResult>();
            result.IsRedirectingTo("/Booking/Step/2/Event");
        }

        [Test]
        public void EventTickets_Get_WithEventDetails_ReturnsViewResult()
        {
            var mockStartDate = DateTime.Now;

            var mockField = new EventTicketFieldMockBuilder().Default().Build();
            var mockEventTicket = new EventTicketMockBuilder()
                .Default().WithEventTicketFields(new[] { mockField }).Build();
            var mockEvent = new EventModelMockBuilder()
                .WithFutureClosedDate()
                .WithTickets(new[] { mockEventTicket })
                .Build();

            var mockBookingCart = CreateMockOf<IBookingCart>();
            mockBookingCart.SetupWithVerification(call => call.Event, mockEvent);
            //mockBookingCart.SetupWithVerification(call => call.StartDate, mockStartDate);
            _clientConfigMock.SetupWithVerification(call => call.EventTicketFeePercentage, 5);
            _clientConfigMock.SetupWithVerification(call => call.EventTicketFeeCents, 30);

            var controller = BuildController();
            var result = controller.EventTickets(mockBookingCart.Object);

            // assert
            result.IsTypeOf<ViewResult>();
            var model = result.ViewResultModelIsTypeOf<BookingEventTicketSetupViewModel>();

            model.Tickets.Count.IsEqualTo(1);
            model.Tickets.First().EventTicketFields.Count.IsEqualTo(1);
            model.EventTicketFee.IsEqualTo(5);
        }

        [Test]
        public void EventTickets_Post_ReturnsJsonResult()
        {
            var mockEvent = new EventModelMockBuilder().Build();

            var mockBookingCart = CreateMockOf<IBookingCart>();
            mockBookingCart.SetupWithVerification(call => call.Event, mockEvent);

            var mockViewModel = new BookingEventTicketSetupViewModel
            {
                Tickets = new List<BookingEventTicketViewModel> { new BookingEventTicketViewModel { TicketName = "Adult", AvailableQuantity = 10, Price = 0 } }
            };

            _cartRepositoryMock.SetupWithVerification(call => call.Save(It.Is<IBookingCart>(p => p == mockBookingCart.Object)), mockBookingCart.Object);

            // act
            var result = BuildController().EventTickets(mockBookingCart.Object, mockViewModel);

            // assert
            result.IsTypeOf<JsonResult>();
            result.JsonResultContains("{ nextUrl = /Booking/Step/3 }");
        }

        [Test]
        public void Success_ReturnsView()
        {
            var mockAppUser = new ApplicationUserMockBuilder().Default().Build();

            var mockBookingCart = new BookingCart
            {
                UserId = mockAppUser.Username,
                CategoryAdType = CategoryAdType.Event,
                OnlineAdModel = new OnlineAdModel
                {
                    Heading = "Sample Listing"
                }
            };
            var mockBookingOrder = new BookingOrderResult();

            var mockAdId = 123;

            // Service calls
            _bookingContextMock.SetupWithVerification(call => call.Current(), result: (BookingCart)mockBookingCart);

            _bookingContextMock.SetupWithVerification(call => call.Clear());

            _rateCalculatorMock
                .Setup(call => call.Calculate(It.IsAny<IAdRateContext>(), It.IsAny<int?>()))
                .Returns(mockBookingOrder);

            _userManagerMock.SetupWithVerification(call => call.GetCurrentUser(), result: mockAppUser);


            _userManagerMock.SetupWithVerification(call => call.GetUserNetworksForUserId(
                It.Is<string>(userId => userId == mockAppUser.Username)),
                new List<UserNetworkModel>());


            _bookingManagerMock.SetupWithVerification(call => call.CreateBooking(
                It.Is<BookingCart>(cart => cart == mockBookingCart),
                It.Is<BookingOrderResult>(order => order == mockBookingOrder)),
                result: mockAdId);

            _cartRepositoryMock.SetupWithVerification(call => call.Save(
                It.IsAny<BookingCart>()), mockBookingCart);


            _mailService.SetupWithVerification(call => call.SendListingCompleteEmail(
                It.Is<ApplicationUser>(user => user == mockAppUser),
                It.Is<int>(id => id == mockAdId),
                It.Is<IBookingCart>(cart => cart == mockBookingCart)));

            _clientConfigMock.SetupWithVerification(call => call.RestrictedOnlineDaysCount, 10);


            var controller = BuildController();
            var result = controller.Success();

            var viewModel = result.ViewResultModelIsTypeOf<SuccessView>();
            viewModel.AdUrl.IsEqualTo("http://dummy-localhost/Event/sample-listing/123");
            viewModel.AdId.IsEqualTo(mockAdId);
            viewModel.IsBookingActive = true;
        }


        private Mock<ISearchService> _searchServiceMock;
        private Mock<IBookCartRepository> _cartRepositoryMock;
        private Mock<IBookingContext> _bookingContextMock;
        private Mock<IClientConfig> _clientConfigMock;
        private Mock<IDocumentRepository> _documentRepositoryMock;
        private Mock<IUserManager> _userManagerMock;
        private Mock<IRateCalculator> _rateCalculatorMock;
        private Mock<IMailService> _mailService;
        private Mock<IApplicationConfig> _applicationConfigMock;
        private Mock<IBookingManager> _bookingManagerMock;
        private Mock<IPayPalService> _paymentServiceMock;
        private Mock<IEditionManager> _editionManagerMock;
        private Mock<IDateService> _dateServiceMock;
        private Mock<ILocationService> _locationServiceMock;
        private Mock<ILogService> _logServiceMock;

        [SetUp]
        public void SetupController()
        {
            _searchServiceMock = CreateMockOf<ISearchService>();
            _cartRepositoryMock = CreateMockOf<IBookCartRepository>();
            _bookingContextMock = CreateMockOf<IBookingContext>();
            _clientConfigMock = CreateMockOf<IClientConfig>();
            _documentRepositoryMock = CreateMockOf<IDocumentRepository>();
            _userManagerMock = CreateMockOf<IUserManager>();
            _rateCalculatorMock = CreateMockOf<IRateCalculator>();
            _applicationConfigMock = CreateMockOf<IApplicationConfig>();
            _bookingManagerMock = CreateMockOf<IBookingManager>();
            _paymentServiceMock = CreateMockOf<IPayPalService>();
            _editionManagerMock = CreateMockOf<IEditionManager>();
            _dateServiceMock = CreateMockOf<IDateService>();
            _locationServiceMock = CreateMockOf<ILocationService>();
            _logServiceMock = CreateMockOf<ILogService>();

            _mailService = CreateMockOf<IMailService>();
            _mailService.Setup(call => call.Initialise(It.IsAny<Controller>())).Returns(_mailService.Object);
        }
    }
}