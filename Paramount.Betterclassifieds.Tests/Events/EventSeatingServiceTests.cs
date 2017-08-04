using System;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventSeatingServiceTests : TestContext<EventSeatingService>
    {

        private Mock<IEventRepository> _mockEventRepository;
        private Mock<ILogService> _mockLogService;

        [SetUp]
        public void SetupDependencies()
        {
            _mockEventRepository = CreateMockOf<IEventRepository>();
            _mockLogService = CreateMockOf<ILogService>();
            _mockLogService.Setup(call => call.Info(It.IsAny<string>()));
            _mockLogService.Setup(call => call.Info(It.IsAny<string>(), It.IsAny<TimeSpan>()));
        }

        [Test]
        public void GetSeatsForEvent_CallsRepository()
        {
            var mockBuilder = new EventSeatMockBuilder();
            var mockSeats = new[]
            {
                mockBuilder.WithSeatNumber("1").Build(),
                mockBuilder.WithSeatNumber("2").Build(),
                mockBuilder.WithSeatNumber("3").Build(),
            };

            _mockEventRepository.SetupWithVerification(call => call.GetEventSeats(
                It.IsAny<int>(),
                It.IsAny<int?>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                mockSeats);

            var eventSeatingService = BuildTargetObject();
            eventSeatingService.GetSeatsForEvent(1, "order-123");
        }
    }
}
