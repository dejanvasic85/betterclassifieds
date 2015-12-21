namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventTicketFieldMockBuilder
    {
        public EventTicketFieldMockBuilder Default()
        {
            WithEventId(123);
            WithEventTicketFieldId(900);
            WithFieldName("Field 1");
            WithIsRequired(false);
            return this;
        }
    }
}