using System;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventTicketReservationMockBuilder
    {
        public EventTicketReservationMockBuilder Default()
        {
            WithEventTicketReservationId(1000);
            WithStatus(EventTicketReservationStatus.Reserved);
            WithEventTicketId(1000);
            WithPrice(55);
            WithQuantity(1);
            WithSeatNumber("A1");
            WithCreatedDate(DateTime.Now);
            WithCreatedDateUtc(DateTime.UtcNow);
            WithExpiryDate(DateTime.Now.AddMinutes(20));
            WithExpiryDateUtc(DateTime.UtcNow.AddMinutes(20));
            return this;
        }
    }
}