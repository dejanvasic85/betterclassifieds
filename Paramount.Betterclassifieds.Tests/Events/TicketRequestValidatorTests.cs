using System;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Events.Reservations;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class TicketQuantityRequestRuleTests
    {
        [Test]
        public void IsSatisfied_RequestLargerThanRemaining_ReturnsFalse()
        {
            var mockTicket = new EventTicketMockBuilder()
                .WithAvailableQuantity(10)
                .WithRemainingQuantity(10)
                .Build();

            var mockEventManager = new Mock<IEventManager>();

            mockEventManager.SetupWithVerification(call => call.GetEventTicketAndReservations(It.IsAny<int>()), mockTicket);
            mockEventManager.SetupWithVerification(call => call.GetRemainingTicketCount(It.IsAny<EventTicket>()), 10);

            var rule = new TicketQuantityRequestRule(mockEventManager.Object);
            var result = rule.IsSatisfiedBy(new TicketQuantityRequest(mockTicket, 15));

            // Assert
            result.IsSatisfied.IsFalse();
            result.Result.IsEqualTo(EventTicketReservationStatus.RequestTooLarge);
        }

        [Test]
        public void IsSatisfied_TicketsSoldOut_ReturnsFalse()
        {
            var mockTicket = new EventTicketMockBuilder()
                .WithAvailableQuantity(10)
                .WithRemainingQuantity(0)
                .Build();

            var mockEventManager = new Mock<IEventManager>();

            mockEventManager.SetupWithVerification(call => call.GetEventTicketAndReservations(It.IsAny<int>()), mockTicket);
            mockEventManager.SetupWithVerification(call => call.GetRemainingTicketCount(It.IsAny<EventTicket>()), 5);

            var rule = new TicketQuantityRequestRule(mockEventManager.Object);
            var result = rule.IsSatisfiedBy(new TicketQuantityRequest(mockTicket, 15));

            // Assert
            result.IsSatisfied.IsFalse();
            result.Result.IsEqualTo(EventTicketReservationStatus.SoldOut);
        }

        [Test]
        public void IsSatisfied_TicketsAvailable_ReturnsTrue()
        {
            var mockTicket = new EventTicketMockBuilder()
                .WithAvailableQuantity(10)
                .WithRemainingQuantity(10)
                .Build();

            var mockEventManager = new Mock<IEventManager>();

            mockEventManager.SetupWithVerification(call => call.GetEventTicketAndReservations(It.IsAny<int>()), mockTicket);
            mockEventManager.SetupWithVerification(call => call.GetRemainingTicketCount(It.IsAny<EventTicket>()), 10);

            var rule = new TicketQuantityRequestRule(mockEventManager.Object);
            var result = rule.IsSatisfiedBy(new TicketQuantityRequest(mockTicket, 1));

            // Assert
            result.IsSatisfied.IsTrue();
            result.Result.IsEqualTo(EventTicketReservationStatus.Reserved);
        }
    }
}
