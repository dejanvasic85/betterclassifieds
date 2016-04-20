namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventBookingMockBuilder
    {
        public EventBookingMockBuilder Default()
        {
            WithEventId(111);
            WithEventBookingId(999);
            WithEmail("foo@bar.com");
            return this;
        }
    }
}