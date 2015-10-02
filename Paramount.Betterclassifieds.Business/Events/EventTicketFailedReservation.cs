namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketFailedReservation
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; }
        public int Quantity { get; set; }
        public EventTicketReservationStatus Status { get; set; }
    }
}