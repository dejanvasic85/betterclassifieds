using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventTicketReservationMockBuilder
    {
        public EventTicketReservationMockBuilder Default()
        {
            WithEventTicketReservationId(1000);
            WithStatus(EventTicketReservationStatus.Reserved);
            return this;
        }
    }
}