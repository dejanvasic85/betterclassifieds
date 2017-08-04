using System;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Events.Reservations;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class SeatAvailabilityRuleTests
    {
        [Test]
        public void IsSatisfiedBy_ChecksNullValues()
        {
            var rule = new SeatAvailabilityRule();
            Assert.Throws<ArgumentNullException>(() => rule.IsSatisfiedBy(null));
            Assert.Throws<ArgumentNullException>(() => rule.IsSatisfiedBy(new SeatRequest(null, null, null)));
        }
        
        [Test]
        public void IsSatisfiedBy_SeatIsNull_ReturnsInvalidRequest()
        {
            var rule = new SeatAvailabilityRule();

            var desiredSeat = "A1";

            var request = new SeatRequest("order1", desiredSeat, null);

            var ruleResult = rule.IsSatisfiedBy(request);

            ruleResult.IsSatisfied.IsFalse();
            ruleResult.Result.IsEqualTo(EventTicketReservationStatus.InvalidRequest);
        }

        [Test]
        public void IsSatisfiedBy_SeatNotAvailableToPublic_ReturnsSoldOut()
        {
            var rule = new SeatAvailabilityRule();

            var desiredSeat = "A1";
            var seatsForDesiredTicket = new EventSeatMockBuilder().WithSeatNumber("A1").WithNotAvailableToPublic(true).Build();

            var request = new SeatRequest("order1", desiredSeat, seatsForDesiredTicket);

            var ruleResult = rule.IsSatisfiedBy(request);

            ruleResult.IsSatisfied.IsFalse();
            ruleResult.Result.IsEqualTo(EventTicketReservationStatus.SoldOut);
        }

        [Test]
        public void IsSatisfiedBy_SeatIsBooked_ReturnsSoldOut()
        {
            var rule = new SeatAvailabilityRule();

            var desiredSeat = "A1";
            var seatsForDesiredTicket = new EventSeatMockBuilder().WithSeatNumber("A1").WithIsBooked(true).Build();

            var request = new SeatRequest("order1", desiredSeat, seatsForDesiredTicket);

            var ruleResult = rule.IsSatisfiedBy(request);

            ruleResult.IsSatisfied.IsFalse();
            ruleResult.Result.IsEqualTo(EventTicketReservationStatus.SoldOut);
        }

        [Test]
        public void IsSatisfiedBy_SeatAvailable_ReservesSeat()
        {
            var rule = new SeatAvailabilityRule();

            var desiredSeat = "A1";
            var seat = new EventSeatMockBuilder().WithSeatNumber("A1")
                .WithNotAvailableToPublic(false)
                .WithIsBooked(false)
                .Build();

            var request = new SeatRequest("order", desiredSeat, seat);

            var ruleResult = rule.IsSatisfiedBy(request);

            ruleResult.IsSatisfied.IsTrue();
            ruleResult.Result.IsEqualTo(EventTicketReservationStatus.Reserved);
        }
    }
}