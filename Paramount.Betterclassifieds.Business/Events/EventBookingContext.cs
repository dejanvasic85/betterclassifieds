namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventBookingContext
    {
        int? EventId { get; set; }
        int? EventBookingId { get; set; }
        string EventBookingPaymentReference { get; set; }
        string[] EmailGuestList { get; set; }
        string Purchaser { get; set; }
        void Clear();
    }

    /// <summary>
    /// Object used for session storage when booking tickets to an event
    /// </summary>
    public class EventBookingContext : IEventBookingContext
    {
        public EventBookingContext()
        {
            EmailGuestList = new string[0];
        }

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
            EmailGuestList = new string[0];
            Purchaser = null;
        }
    }
}
