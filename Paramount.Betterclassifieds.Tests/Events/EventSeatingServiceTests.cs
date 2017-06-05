using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventSeatingServiceTests : TestContext<EventSeatingService>
    {

        private Mock<IEventRepository> _mockEventRepository;

        [SetUp]
        public void SetupDependencies()
        {
            _mockEventRepository = CreateMockOf<IEventRepository>();
        }

        [Test]
        public void BookSeat_GetsSeats_UpdatesBookingId_SavesToRepository()
        {
            var eventId = 1;
            var eventBookingTicketId = 200;
            var seatNumber = "A1";
            var seatingService = BuildTargetObject();
            
            seatingService.BookSeat(eventId, eventBookingTicketId, seatNumber);
        }
    }
}
