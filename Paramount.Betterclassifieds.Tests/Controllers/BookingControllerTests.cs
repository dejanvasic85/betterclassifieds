using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.DocumentStorage;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
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

            var mockEvent = new EventModelMockBuilder()
                .WithFutureClosedDate()
                .WithTickets(new[] { new EventTicket() })
                .WithTicketFields(new[] { new EventTicketField() })
                .Build();
            var mockBookingCart = CreateMockOf<IBookingCart>();

            mockBookingCart.SetupWithVerification(call => call.Event, mockEvent);
            mockBookingCart.SetupWithVerification(call => call.StartDate, mockStartDate);

            var controller = BuildController();
            var result = controller.EventTickets(mockBookingCart.Object);

            // assert
            result.IsTypeOf<ViewResult>();
            var model = result.ViewResultModelIsTypeOf<BookingEventTicketSetupViewModel>();
            model.AdStartDate.IsEqualTo(mockStartDate);
            model.ClosingDate.IsNotNull();
            model.Tickets.Count.IsEqualTo(1);
            model.TicketFields.Count.IsEqualTo(1);
        }

        private Mock<ISearchService> _searchServiceMock;
        private Mock<IBookCartRepository> _cartRepositoryMock;
        private Mock<IBookingContext> _bookingContextMock;
        private Mock<IClientConfig> _clientConfigMock;
        private Mock<IDocumentRepository> _documentRepositoryMock;
        private Mock<IUserManager> _userManagerMock;
        private Mock<IRateCalculator> _rateCalculatorMock;
        private Mock<IBroadcastManager> _broadcastManagerMock;
        private Mock<IApplicationConfig> _applicationConfigMock;
        private Mock<IBookingManager> _bookingManagerMock;
        private Mock<IPaymentService> _paymentServiceMock;
        private Mock<IEditionManager> _editionManagerMock;
        private Mock<IDateService> _dateServiceMock;

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
            _broadcastManagerMock = CreateMockOf<IBroadcastManager>();
            _applicationConfigMock = CreateMockOf<IApplicationConfig>();
            _bookingManagerMock = CreateMockOf<IBookingManager>();
            _paymentServiceMock = CreateMockOf<IPaymentService>();
            _editionManagerMock = CreateMockOf<IEditionManager>();
            _dateServiceMock = CreateMockOf<IDateService>();
        }
    }
}