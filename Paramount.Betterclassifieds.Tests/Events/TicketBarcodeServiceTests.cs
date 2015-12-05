using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    internal class TicketBarcodeServiceTests
    {
        [Test]
        public void Generate_Returns_String()
        {
            var eventId = 1000;
            var eventModelMock = new EventModelMockBuilder().WithEventId(eventId).Build();
            var eventTicketId = 2099;
            var eventBookingTicketId = 293433;
            var eventBookingTicketMock = new EventBookingTicketMockBuilder().WithEventTicketId(eventTicketId).WithEventBookingTicketId(eventBookingTicketId).Build();

            var service= new TicketBarcodeService();
            var result = service.Generate(eventModelMock, eventBookingTicketMock);

            Assert.That(result, Is.EqualTo("1000-2099-293433"));
        }
    }
}