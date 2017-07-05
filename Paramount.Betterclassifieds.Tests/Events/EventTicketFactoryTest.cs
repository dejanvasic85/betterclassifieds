using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventTicketFactoryTests
    {
        [Test]
        public void Create_ReturnsNew_EventTicket()
        {
            var factory = new EventTicketFactory();
            var result = factory.Create(10, 1, "Adult", 100, "#blue", isActive: true);

            Assert.That(result, Is.TypeOf<EventTicket>());
            Assert.That(result.EventId, Is.EqualTo(1));
            Assert.That(result.RemainingQuantity, Is.EqualTo(10));
            Assert.That(result.AvailableQuantity, Is.EqualTo(10));
            Assert.That(result.Price, Is.EqualTo(100));
            Assert.That(result.TicketName, Is.EqualTo("Adult"));
            Assert.That(result.IsActive, Is.True);
            Assert.That(result.EventTicketReservations, Is.Not.Null);
            Assert.That(result.EventTicketReservations.Count, Is.EqualTo(0));
            Assert.That(result.EventBookingTickets, Is.Not.Null);
            Assert.That(result.EventBookingTickets.Count, Is.EqualTo(0));
            Assert.That(result.ColourCode, Is.EqualTo("#blue"));

        }
    }
}