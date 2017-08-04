using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Events.Reservations;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class TicketRequestValidatorTests
    {
        [Test]
        public void IsSufficientTicketsAvailableForRequest_NullRequests_ThrowsArgException()
        {
            var mockEventManager = new Mock<IEventManager>();
            var mockEventSeatingService = new Mock<IEventSeatingService>();
            var validator = new TicketRequestValidator(mockEventManager.Object, mockEventSeatingService.Object);

            Assert.Throws<ArgumentNullException>(() => validator.IsSufficientTicketsAvailableForRequest(null, null));
        }

        [Test]
        public void IsSufficientTicketsAvailableForRequest_SeatedEvent_AllRulesPass_ReturnsTrue()
        {
            var eventId = 100;
            var eventTicketId = 1;
            var eventGroupId = 100;
            var desiredQty = 1;
            var seatNumber = "A1";
            var orderRequestId = "123";

            var mockGroup = new EventGroupMockBuilder().Default().WithEventGroupId(eventGroupId).Build();
            var mockTicket = new EventTicketMockBuilder().Default().WithEventTicketId(eventTicketId).Build();
            var mockSeat = new EventSeatMockBuilder()
                .WithSeatNumber(seatNumber)
                .WithEventTicketId(eventTicketId)
                .WithEventTicket(mockTicket)
                .Build();

            var mockRequests = new[]
            {
                new TicketReservationRequest(eventTicketId, eventGroupId, desiredQty, orderRequestId, seatNumber)
            };

            var mockEventModel = new EventModelMockBuilder()
                .WithEventId(eventId)
                .WithTickets(new[]
                {
                    mockTicket
                })
                .WithIsSeatedEvent(true)
                .Build();

            var mockEventManager = new Mock<IEventManager>(MockBehavior.Strict);
            mockEventManager
                .Setup(call => call.GetEventGroup(It.Is<int>(g => g == eventGroupId)))
                .Returns(Task.FromResult(mockGroup));
            
            mockEventManager
                .Setup(call => call.GetRemainingTicketCount(It.Is<EventTicket>(t => t == mockTicket)))
                .Returns(100); // Plenty left

            var mockEventSeatingService = new Mock<IEventSeatingService>(MockBehavior.Strict);
            mockEventSeatingService
                .Setup(call => call.GetSeat(
                    It.Is<int>(e => e == eventId),
                    It.Is<string>(s => s == seatNumber),
                    It.Is<string>(o => o == orderRequestId)))
                .Returns(mockSeat);

            var validator = new TicketRequestValidator(mockEventManager.Object, mockEventSeatingService.Object);
            var result = validator.IsSufficientTicketsAvailableForRequest(mockEventModel, mockRequests);

            result.IsTrue();
            mockEventManager.Verify(call => call.GetEventGroup(It.IsAny<int>()));
            mockEventSeatingService.Verify(call => call.GetSeat(It.Is<int>(e => e == eventId), It.Is<string>(s => s == seatNumber), It.Is<string>(o => o == orderRequestId)));
            mockEventManager.Verify(call => call.GetRemainingTicketCount(It.IsAny<EventTicket>()));
        }

        [Test]
        public void IsSufficientTicketsAvailableForRequest_GroupsRuleFails_ReturnsFalse()
        {
            var eventTicketId = 1;
            var eventGroupId = 100;
            var desiredQty = 1;
            var orderRequestId = "123";

            var mockGroup = new EventGroupMockBuilder().Default().WithEventGroupId(eventGroupId)
                .WithMaxGuests(2).WithGuestCount(2).WithEventGroupId(100).Build();
            var mockTicket = new EventTicketMockBuilder().Default().WithEventTicketId(eventTicketId).Build();

            var mockRequests = new[]
            {
                new TicketReservationRequest(eventTicketId, eventGroupId, desiredQty, orderRequestId, string.Empty)
            };

            var mockEvent = new EventModelMockBuilder().Default().Build();

            var mockEventManager = new Mock<IEventManager>();
            mockEventManager
                .Setup(call => call.GetEventGroup(It.Is<int>(g => g == eventGroupId)))
                .Returns(Task.FromResult(mockGroup));

            mockEventManager
                .Setup(call => call.GetEventTicketAndReservations(It.Is<int>(t => t == eventTicketId)))
                .Returns(mockTicket);

            mockEventManager
                .Setup(call => call.GetRemainingTicketCount(It.Is<EventTicket>(t => t == mockTicket)))
                .Returns(100); // Plenty left

            
            var mockEventSeatingService = new Mock<IEventSeatingService>();
            var validator = new TicketRequestValidator(mockEventManager.Object, mockEventSeatingService.Object);
            var result = validator.IsSufficientTicketsAvailableForRequest(mockEvent, mockRequests);

            result.IsFalse();
        }

        [Test]
        public void IsSufficientTicketsAvailableForRequest_TicketRuleFails_ReturnsFalse()
        {
            var eventTicketId = 1;
            var eventGroupId = 100;
            var desiredQty = 1;
            var orderRequestId = "123";

            var mockGroup = new EventGroupMockBuilder().Default().WithEventGroupId(eventGroupId).Build();
            var mockTicket = new EventTicketMockBuilder().Default().WithEventTicketId(eventTicketId).Build();

            var mockRequests = new[]
            {
                new TicketReservationRequest(eventTicketId, eventGroupId, desiredQty, orderRequestId, seatNumber: string.Empty)
            };

            var mockEventManager = new Mock<IEventManager>();
            mockEventManager
                .Setup(call => call.GetEventGroup(It.Is<int>(g => g == eventGroupId)))
                .Returns(Task.FromResult(mockGroup));

            mockEventManager
                .Setup(call => call.GetEventTicketAndReservations(It.Is<int>(t => t == eventTicketId)))
                .Returns(mockTicket);

            mockEventManager
                .Setup(call => call.GetRemainingTicketCount(It.Is<EventTicket>(t => t == mockTicket)))
                .Returns(0); // None left

            var mockEvent = new EventModelMockBuilder().Default()
                .WithTickets(new[]
                {
                    mockTicket
                }).Build();

            var mockEventSeatingService = new Mock<IEventSeatingService>();
            var validator = new TicketRequestValidator(mockEventManager.Object, mockEventSeatingService.Object);
            var result = validator.IsSufficientTicketsAvailableForRequest(mockEvent, mockRequests);

            result.IsFalse();
        }


        [Test]
        public void IsSufficientTicketsAvailableForRequest_SeatAvailabilityRuleFails_ReturnsFalse()
        {
            var eventTicketId = 1;
            var eventGroupId = 100;
            var desiredQty = 1;
            var seatNumber = "A1";
            var orderRequestId = "123";

            var mockGroup = new EventGroupMockBuilder().Default().WithEventGroupId(eventGroupId).Build();
            var mockTicket = new EventTicketMockBuilder().Default().WithEventTicketId(eventTicketId).Build();
            var mockSeat = new EventSeatMockBuilder()
                .WithSeatNumber("A2")
                .WithEventTicketId(eventTicketId)
                .WithEventTicket(mockTicket)
                .Build();

            var mockRequests = new[]
            {
                new TicketReservationRequest(eventTicketId, eventGroupId, desiredQty, orderRequestId, seatNumber)
            };

            var mockEventManager = new Mock<IEventManager>();
            mockEventManager
                .Setup(call => call.GetEventGroup(It.Is<int>(g => g == eventGroupId)))
                .Returns(Task.FromResult(mockGroup));
            
            mockEventManager
                .Setup(call => call.GetRemainingTicketCount(It.Is<EventTicket>(t => t == mockTicket)))
                .Returns(100); // Plenty left


            var mockEvent = new EventModelMockBuilder()
                .Default()
                .WithIsSeatedEvent(true)
                .WithTickets(new []
                {
                    mockTicket
                })
                .Build();

            var mockEventSeatingService = new Mock<IEventSeatingService>(MockBehavior.Strict);
            mockEventSeatingService
                .Setup(call => call.GetSeat(
                    It.Is<int>(e => e == mockEvent.EventId),
                    It.Is<string>(s => s == seatNumber),
                    It.Is<string>(o => o == orderRequestId)))
                .Returns(mockSeat);


            var validator = new TicketRequestValidator(mockEventManager.Object, mockEventSeatingService.Object);
            var result = validator.IsSufficientTicketsAvailableForRequest(mockEvent, mockRequests);

            result.IsFalse();
            mockEventManager.Verify(call => call.GetEventGroup(It.IsAny<int>()));
            mockEventManager.Verify(call => call.GetRemainingTicketCount(It.IsAny<EventTicket>()));
        }
    }
}