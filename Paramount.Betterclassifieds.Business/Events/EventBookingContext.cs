namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventBookingContext
    {
        int? EventId { get; set; }
        int? EventBookingId { get; set; }
        string EventBookingPaymentReference { get; set; }
        string Purchaser { get; set; }
        long? EventInvitationId { get; set; }
        bool EventBookingComplete { get; set; }
        void Clear();
    }

    /// <summary>
    /// Object used for session storage when booking tickets to an event
    /// </summary>
    public class EventBookingContext : IEventBookingContext
    {
        public int? EventId { get; set; }
        public int? EventBookingId { get; set; }
        public string EventBookingPaymentReference { get; set; }
        public string Purchaser { get; set; }
        public long? EventInvitationId { get; set; }
        public bool EventBookingComplete { get; set; }

        public void Clear()
        {
            EventId = null;
            EventBookingId = null;
            EventBookingPaymentReference = null;
            EventBookingComplete = false;
            Purchaser = null;
        }
    }
}
