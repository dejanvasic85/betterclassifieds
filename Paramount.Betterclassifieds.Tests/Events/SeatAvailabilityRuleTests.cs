using System;
using System.Collections.Generic;
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
            Assert.Throws<ArgumentNullException>(() => rule.IsSatisfiedBy(new SeatRequest(null, null)));
            Assert.Throws<ArgumentNullException>(() => rule.IsSatisfiedBy(new SeatRequest("A1", null)));
        }

        [Test]
        public void IsSatisfiedBy_MissingSeat_ReturnsInvalidRequest()
        {
            var rule = new SeatAvailabilityRule();

            var desiredSeat = "A1";
            var seatsForDesiredTicket = new List<EventSeatBooking>()
            {
                new EventSeatBookingMockBuilder().WithSeatNumber("A2").Build()
            };

            var request = new SeatRequest(desiredSeat, seatsForDesiredTicket);

            var ruleResult = rule.IsSatisfiedBy(request);

            ruleResult.IsSatisfied.IsFalse();
            ruleResult.Result.IsEqualTo(EventTicketReservationStatus.InvalidRequest);
        }

        [Test]
        public void IsSatisfiedBy_SeatNotAvailable_ReturnsSoldOut()
        {
            var rule = new SeatAvailabilityRule();

            var desiredSeat = "A1";
            var seatsForDesiredTicket = new List<EventSeatBooking>
            {
                new EventSeatBookingMockBuilder().WithSeatNumber("A1").WithEventBookingTicketId(1).Build()
            };

            var request = new SeatRequest(desiredSeat, seatsForDesiredTicket);

            var ruleResult = rule.IsSatisfiedBy(request);

            ruleResult.IsSatisfied.IsFalse();
            ruleResult.Result.IsEqualTo(EventTicketReservationStatus.SoldOut);
        }

        [Test]
        public void IsSatisfiedBy_SeatNotAvailableToPublic_ReturnsSoldOut()
        {
            var rule = new SeatAvailabilityRule();

            var desiredSeat = "A1";
            var seatsForDesiredTicket = new List<EventSeatBooking>
            {
                new EventSeatBookingMockBuilder().WithSeatNumber("A1").WithNotAvailableToPublic(true).Build()
            };

            var request = new SeatRequest(desiredSeat, seatsForDesiredTicket);

            var ruleResult = rule.IsSatisfiedBy(request);

            ruleResult.IsSatisfied.IsFalse();
            ruleResult.Result.IsEqualTo(EventTicketReservationStatus.SoldOut);
        }

        [Test]
        public void IsSatisfiedBy_SeatAvailable_ReservesSeat()
        {
            var rule = new SeatAvailabilityRule();

            var desiredSeat = "A1";
            var seatsForDesiredTicket = new List<EventSeatBooking>
            {
                new EventSeatBookingMockBuilder().WithSeatNumber("A1")
                    .WithEventBookingTicketId(null)
                    .WithNotAvailableToPublic(false)
                    .Build()
            };

            var request = new SeatRequest(desiredSeat, seatsForDesiredTicket);

            var ruleResult = rule.IsSatisfiedBy(request);

            ruleResult.IsSatisfied.IsTrue();
            ruleResult.Result.IsEqualTo(EventTicketReservationStatus.Reserved);
        }
    }
}