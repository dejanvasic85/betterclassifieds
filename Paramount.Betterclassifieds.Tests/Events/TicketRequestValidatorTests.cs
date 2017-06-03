using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Events.Reservations;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class TicketRequestValidatorTests
    {
        [Test]
        public void IsSufficientTicketsAvailableForRequest_NullRequests_ThrowsArgException()
        {
            var mockEventManager = new Mock<IEventManager>();
            var validator = new TicketRequestValidator(mockEventManager.Object);

            Assert.Throws<ArgumentNullException>(() => validator.IsSufficientTicketsAvailableForRequest(null));
        }

        [Test]
        public void IsSufficientTicketsAvailableForRequest_AllRulesPass_ReturnsTrue()
        {
            var eventTicketId = 1;
            var eventGroupId = 100;
            var desiredQty = 1;

            var mockGroup = new EventGroupMockBuilder().Default().WithEventGroupId(eventGroupId).Build();
            var mockTicket = new EventTicketMockBuilder().Default().WithEventTicketId(eventTicketId).Build();

            var mockRequests = new[]
            {
                new TicketReservationRequest(eventTicketId, eventGroupId, desiredQty)
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
                .Returns(100); // Plenty left

            var validator = new TicketRequestValidator(mockEventManager.Object);
            var result = validator.IsSufficientTicketsAvailableForRequest(mockRequests);

            result.IsTrue();
        }

        [Test]
        public void IsSufficientTicketsAvailableForRequest_GroupsRuleFails_ReturnsFalse()
        {
            var eventTicketId = 1;
            var eventGroupId = 100;
            var desiredQty = 1;

            var mockGroup = new EventGroupMockBuilder().Default().WithEventGroupId(eventGroupId)
                .WithMaxGuests(2).WithGuestCount(2).WithEventGroupId(100).Build();
            var mockTicket = new EventTicketMockBuilder().Default().WithEventTicketId(eventTicketId).Build();

            var mockRequests = new[]
            {
                new TicketReservationRequest(eventTicketId, eventGroupId, desiredQty)
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
                .Returns(100); // Plenty left

            var validator = new TicketRequestValidator(mockEventManager.Object);
            var result = validator.IsSufficientTicketsAvailableForRequest(mockRequests);

            result.IsFalse();
        }

        [Test]
        public void IsSufficientTicketsAvailableForRequest_TicketRuleFails_ReturnsFalse()
        {
            var eventTicketId = 1;
            var eventGroupId = 100;
            var desiredQty = 1;

            var mockGroup = new EventGroupMockBuilder().Default().WithEventGroupId(eventGroupId).Build();
            var mockTicket = new EventTicketMockBuilder().Default().WithEventTicketId(eventTicketId).Build();

            var mockRequests = new[]
            {
                new TicketReservationRequest(eventTicketId, eventGroupId, desiredQty)
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

            var validator = new TicketRequestValidator(mockEventManager.Object);
            var result = validator.IsSufficientTicketsAvailableForRequest(mockRequests);

            result.IsFalse();
        }
    }
}