namespace Paramount.Betterclassifieds.Business.Events
{
    /// <summary>
    /// Object used for session storage when booking tickets to an event
    /// </summary>
    public class EventBookingContext
    {
        public int? EventId { get; set; }
        public int? EventBookingId { get; set; }
        public string EventBookingPaymentReference { get; set; }
        public string[] EmailGuestList { get; set; }
        public string Purchaser { get; set; }

        public void Clear()
        {
            EventId = null;
            EventBookingId = null;
            EventBookingPaymentReference = null;
            EmailGuestList = null;
            Purchaser = null;
        }
    }
}
