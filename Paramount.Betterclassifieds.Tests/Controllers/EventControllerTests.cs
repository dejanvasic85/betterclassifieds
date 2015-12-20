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
            // arrange
            var mockAdId = 458745;
            var mockOnlineAdId = 232;
            var mockEventId = 777;
            var mockPhotoId = "photo.jpg";
            var mockTitle = "Lord of the rings";
            var mockDescription = "very average movie!";

            var mockAd = new AdSearchResultMockBuilder()
                .WithAdId(mockAdId)
                .WithHeading(mockTitle)
                .WithDescription(mockDescription)
                .WithOnlineAdId(mockOnlineAdId)
                .WithImageUrls(new [] { mockPhotoId })
                .Build();

            var mockEventAd = new EventModelMockBuilder()
                .WithEventId(mockEventId)
                .WithPastClosedDate()
                .Build();

            // arrange services
            _searchService.SetupWithVerification(call => call.GetByAdId(It.Is<int>(p => p == mockAdId)), mockAd);
            _bookingManager.SetupWithVerification(call => call.IncrementHits(It.Is<int>(p => p == mockAdId)));
            _eventManager.SetupWithVerification(call => call.GetEventDetailsForOnlineAdId(It.Is<int>(p => p == mockOnlineAdId), It.Is<bool>(p => p == false)), mockEventAd);
            _clientConfig.SetupWithVerification(call => call.EventMaxTicketsPerBooking, 10);

            // act
            var result = BuildController().ViewEventAd(mockAdId);

            // assert
            result.IsTypeOf<ViewResult>();
            var eventViewModel = result.ViewResultModelIsTypeOf<EventViewDetailsModel>();
            eventViewModel.AdId.IsEqualTo(mockAdId);
            eventViewModel.EventId.IsEqualTo(mockEventId);
            eventViewModel.Title.IsEqualTo(mockTitle);
            eventViewModel.Description.IsEqualTo(mockDescription);
            eventViewModel.EventPhoto.IsEqualTo(mockPhotoId);
            eventViewModel.IsClosed.IsEqualTo(true);
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

        [SetUp]
        public void SetupController()
        {
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