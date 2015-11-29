namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicketField
    {
        public int EventBookingTicketFieldId { get; set; }
        public int EventBookingTicketId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
}