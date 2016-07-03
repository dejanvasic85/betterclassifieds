namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventGroupMockBuilder
    {
        public EventGroupMockBuilder Default()
        {
            WithEventGroupId(543);
            WithEventId(123);
            WithGroupName("Table 1");
            WithAvailableToAllTickets(false);
            WithIsDisabled(false);
            WithGuestCount(5);
            WithMaxGuests(10);
            WithCreatedBy("foo bar");

            return this;
        }
    }
}