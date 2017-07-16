using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Controllers;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    [TestFixture]
    public class EventPromoControllerTests : ControllerTest<EventPromoController>
    {
        private Mock<ISearchService> _mockSearchService;
        private Mock<IEventManager> _mockEventManager;
        private Mock<IEventPromoService> _mockEventPromoService;

        [SetUp]
        public void Setup()
        {
            _mockSearchService = CreateMockOf<ISearchService>();
            _mockEventManager = CreateMockOf<IEventManager>();
            _mockEventPromoService = CreateMockOf<IEventPromoService>();
        }

        [Test]
        public void ManagePromoCodes_Get_ReturnsViewModel()
        {
            var mockEventId = 100;
            var mockEvent = new EventModelMockBuilder().Default().WithEventId(mockEventId).Build();
            var mockSearchResult = new AdSearchResultMockBuilder().Default().Build();
            var mockAddress = new AddressMockBuilder().Default().Build();
            var mockPromos = new[]
            {
                new EventPromoCode {PromoCode = "PROMO1", DiscountPercent = 10}
            };
            var mockBooking = new EventBookingMockBuilder().Default().WithPromoCode("PROMO1").Build();

            _mockSearchService.SetupWithVerification(call => call.GetEvent(
                It.Is<int>(e => e == mockEventId)),
                new EventSearchResult(mockSearchResult, mockEvent, mockAddress));

            _mockEventPromoService.SetupWithVerification(call => call.GetEventPromoCodes(
                It.Is<int>(e => e == mockEventId)),
                mockPromos);

            _mockEventManager.SetupWithVerification(call => call.GetEventBookingsForEvent(
                    It.Is<int>(e => e == mockEventId)),
                new[] { mockBooking });

            var controller = BuildController();
            var result = controller.ManagePromoCodes(mockEventId);

            var viewResult = result.IsTypeOf<ViewResult>();
            var viewModel = viewResult.ViewResultModelIsTypeOf<ManageEventPromoViewModel>();

            viewModel.EventId.IsEqualTo(mockEventId);
            viewModel.AdId.IsEqualTo(mockSearchResult.AdId);
            var promoResult = viewModel.PromoCodes.Single();
            promoResult.PromoCode.IsEqualTo("PROMO1");
            promoResult.DiscountPercent.IsEqualTo(10);
        }

        [Test]
        public void CreatePromoCode_Post_ReturnsJson()
        {
            var mockEventId = 100;
            var mockEventPromoCode = new EventPromoCode();
            var mockPromoViewModel = new EventPromoViewModel
            {
                EventId = mockEventId,
                PromoCode = "PROMO2",
                DiscountPercent = 10
            };

            _mockEventPromoService.SetupWithVerification(call => call.CreateEventPromoCode(
                It.Is<int>(e => e == mockEventId),
                It.Is<string>(p => p == "PROMO2"),
                It.Is<decimal?>(d => d == 10)),
                mockEventPromoCode);

            var controller = BuildController();
            var result = controller.CreatePromoCode(
                mockEventId,
                mockPromoViewModel);

            result.IsTypeOf<JsonResult>();
        }
    }
}