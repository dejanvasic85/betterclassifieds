namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicketField
    {
        public long EventBookingTicketFieldId { get; set; }
        public int EventBookingTicketId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
}