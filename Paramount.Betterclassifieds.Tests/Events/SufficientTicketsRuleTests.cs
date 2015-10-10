using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class SufficientTicketsRuleTests
    {
        [Test]
        public void With_NoRemainingTickets_ReturnsFalse()
        {
            var data = new RemainingTicketsWithRequestInfo(10, 0);

            var rule = new SufficientTicketsRule();
            var result = rule.IsSatisfiedBy(data);

            Assert.That(result.IsSatisfied, Is.False);
            Assert.That(result.Result, Is.EqualTo(EventTicketReservationStatus.SoldOut));
        }

        [Test]
        public void With_RemainingTicketsLess_ThanRequested_ReturnsFalse()
        {
            var data = new RemainingTicketsWithRequestInfo(10, 2);

            var rule = new SufficientTicketsRule();
            var result = rule.IsSatisfiedBy(data);

            Assert.That(result.IsSatisfied, Is.False);
            Assert.That(result.Result, Is.EqualTo(EventTicketReservationStatus.RequestTooLarge));
        }

        [Test]
        public void With_Request_SameAs_Remaining_ReturnsTrue()
        {
            var data = new RemainingTicketsWithRequestInfo(10, 10);

            var rule = new SufficientTicketsRule();
            var result = rule.IsSatisfiedBy(data);

            Assert.That(result.IsSatisfied, Is.True);
            Assert.That(result.Result, Is.EqualTo(EventTicketReservationStatus.Reserved));
        }

        [Test]
        public void With_Request_LessThan_Remaining_ReturnsTrue()
        {
            var data = new RemainingTicketsWithRequestInfo(5, 10);

            var rule = new SufficientTicketsRule();
            var result = rule.IsSatisfiedBy(data);

            Assert.That(result.IsSatisfied, Is.True);
            Assert.That(result.Result, Is.EqualTo(EventTicketReservationStatus.Reserved));
        }
    }
}
