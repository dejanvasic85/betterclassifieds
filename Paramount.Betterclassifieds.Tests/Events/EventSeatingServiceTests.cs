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
        }

        [Test]
        public void BookSeat_UpdatesBookingId_SavesToRepository()
        {
            var eventTicketId = 1;
            var eventBookingTicketId = 200;
            var seatNumber = "A1";
            var seatingService = BuildTargetObject();
            var mockEventSeatBooking = new EventSeatBookingMockBuilder()
                .WithEventTicketId(eventTicketId)
                .WithSeatNumber(seatNumber)
                .WithEventBookingTicketId(null)
                .Build();
            
            // Setup service calls
            _mockLogService.SetupWithVerification(call => call.Info(It.IsAny<string>()));

            _mockEventRepository.SetupWithVerification(call => call.GetEventSeat(
                    It.Is<int>(t => t == eventTicketId),
                    It.Is<string>(s => s == seatNumber)),
                result: mockEventSeatBooking);

            _mockEventRepository.SetupWithVerification(call => call.UpdateEventSeat(
                It.Is<EventSeatBooking>(s => s == mockEventSeatBooking)));


            // Act
            seatingService.BookSeat(eventTicketId, eventBookingTicketId, seatNumber);

            // Assert
            mockEventSeatBooking.EventBookingTicketId.IsEqualTo(eventBookingTicketId);
        }

        [Test]
        public void BookSeat_SeatIsNull_ThrowsException()
        {
            var eventTicketId = 1;
            var eventBookingTicketId = 200;
            var seatNumber = "A1";
            var seatingService = BuildTargetObject();

            // Setup service calls
            _mockLogService.SetupWithVerification(call => call.Info(It.IsAny<string>()));

            _mockEventRepository.SetupWithVerification(call => call.GetEventSeat(
                    It.Is<int>(t => t == eventTicketId),
                    It.Is<string>(s => s == seatNumber)),
                result: null);

            
            // Act
            Assert.Throws<NullReferenceException>(() =>  seatingService.BookSeat(eventTicketId, eventBookingTicketId, seatNumber));
        }
    }
}
