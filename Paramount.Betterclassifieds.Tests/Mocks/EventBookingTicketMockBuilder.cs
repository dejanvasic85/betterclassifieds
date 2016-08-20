using System;

namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventBookingTicketMockBuilder
    {
        public EventBookingTicketMockBuilder Default()
        {
            WithEventBookingTicketId(1);
            WithGuestFullName("Foo Bar");
            WithGuestEmail("foo@bar.com");
            WithCreatedDateTime(DateTime.Now);
            WithCreatedDateTimeUtc(DateTime.UtcNow);
            WithEventBookingId(999);
            WithEventTicketId(1000);
            return this;
        }
    }
}