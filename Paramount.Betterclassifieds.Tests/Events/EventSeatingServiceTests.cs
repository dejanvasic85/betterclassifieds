using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Tests.Mocks;
using System;
using System.Linq;

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
            Assert.Throws<NullReferenceException>(() => seatingService.BookSeat(eventTicketId, eventBookingTicketId, seatNumber));
        }

        [Test]
        public void GetSeatsForEvent_FiltersForRequestId_ReturnsSeatsWithReservationExpiry()
        {
            var eventId = 1;
            var orderRequestId = "order-123";
            var seatBuilder = new EventSeatBookingMockBuilder();
            var seatReservationExpiry = DateTime.Now.AddMinutes(10);

            var seats = new[]
            {
                seatBuilder.WithSeatNumber("Seat-1").Build(),
                seatBuilder.WithSeatNumber("Seat-2").Build(),
            };

            var reservationBuilder = new EventTicketReservationMockBuilder();
            var reservations = new[]
            {
                reservationBuilder
                    .WithSeatNumber("Seat-1") // This should match and add the exiry
                    .WithSessionId("123")
                    .WithExpiryDateUtc(seatReservationExpiry)
                    .Build(),

                reservationBuilder
                    .WithSeatNumber("Seat-2")
                    .WithSessionId(orderRequestId) // This should be filtered out
                    .WithExpiryDateUtc(seatReservationExpiry)
                    .Build()
            };

            _mockEventRepository.SetupWithVerification(call => call.GetEventSeats(It.Is<int>(e => e == eventId)), result: seats);
            _mockEventRepository.SetupWithVerification(call => call.GetCurrentReservationsForEvent(It.Is<int>(e => e == eventId)), result: reservations);

            var service = BuildTargetObject();

            var result = service.GetSeatsForEvent(eventId, orderRequestId).ToList();

            // Assert

            result.Count.IsEqualTo(2);

            var availableSeat = result.FirstOrDefault(s => s.IsAvailable());
            availableSeat.IsNotNull();
            availableSeat.SeatNumber.IsEqualTo("Seat-2");

            var reservedSeat = result.FirstOrDefault(s => !s.IsAvailable());
            reservedSeat.IsNotNull();
            reservedSeat.SeatNumber.IsEqualTo("Seat-1");
            reservedSeat.ReservationExpiryUtc.IsNotNull();
        }

        [Test]
        public void GetSeatsForTicket_FiltersForRequestId_ReturnsSeatsWithReservationExpiry()
        {
            var eventId = 1;
            var eventTicketId = 298;
            var orderRequestId = "order-123";
            var seatBuilder = new EventSeatBookingMockBuilder();
            var seatReservationExpiry = DateTime.Now.AddMinutes(10);

            var mockTicket = new EventTicketMockBuilder()
                .Default()
                .WithEventTicketId(eventTicketId)
                .WithEventId(eventId)
                .Build();

            var seats = new[]
            {
                seatBuilder.WithSeatNumber("Seat-1").Build(),
                seatBuilder.WithSeatNumber("Seat-2").Build(),
            };

            var reservationBuilder = new EventTicketReservationMockBuilder();
            var reservations = new[]
            {
                reservationBuilder
                    .WithEventTicketId(eventTicketId)
                    .WithSeatNumber("Seat-1") // This should match and add the exiry
                    .WithSessionId("123")
                    .WithExpiryDateUtc(seatReservationExpiry)
                    .Build(),

                reservationBuilder
                    .WithEventTicketId(eventTicketId)
                    .WithSeatNumber("Seat-2")
                    .WithSessionId(orderRequestId) // This should be filtered out
                    .WithExpiryDateUtc(seatReservationExpiry)
                    .Build()
            };

            _mockEventRepository.SetupWithVerification(call => call.GetEventSeatsForTicket(It.Is<int>(e => e == eventTicketId)), result: seats);
            _mockEventRepository.SetupWithVerification(call => call.GetCurrentReservationsForEvent(It.Is<int>(e => e == eventId)), result: reservations);

            var service = BuildTargetObject();

            var result = service.GetSeatsForTicket(mockTicket, orderRequestId).ToList();

            // Assert

            result.Count.IsEqualTo(2);

            var availableSeat = result.FirstOrDefault(s => s.IsAvailable());
            availableSeat.IsNotNull();
            availableSeat.SeatNumber.IsEqualTo("Seat-2");

            var reservedSeat = result.FirstOrDefault(s => !s.IsAvailable());
            reservedSeat.IsNotNull();
            reservedSeat.SeatNumber.IsEqualTo("Seat-1");
            reservedSeat.ReservationExpiryUtc.IsNotNull();
        }
    }
}
