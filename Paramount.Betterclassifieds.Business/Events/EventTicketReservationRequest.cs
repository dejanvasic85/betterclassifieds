namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketReservationRequest
    {
        public int SelectedQuantity { get; set; }
        public string SessionId { get; set; }
        public EventTicket EventTicket { get; set; }
    }
}