namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketReservationRequest
    {
        public int Quantity { get; set; }
        public EventTicket EventTicket { get; set; }
    }
}