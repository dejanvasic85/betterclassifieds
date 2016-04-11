namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicketValidation
    {
        public EventBookingTicketValidation()
        {

        }
        public EventBookingTicketValidation(int eventBookingTicketId)
        {
            EventBookingTicketId = eventBookingTicketId;
            ValidationCount = 1;
        }

        public long EventBookingTicketValidationId { get; set; }
        public int EventBookingTicketId { get; set; }
        public int ValidationCount { get; set; }

        public void IncrementCount()
        {
            ValidationCount += 1;
        }
    }
}