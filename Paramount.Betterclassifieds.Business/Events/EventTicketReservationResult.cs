namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketReservationResult
    {
        public EventTicket EventTicket { get; set; }
        public bool IsReserved { get; set; }
    }
}