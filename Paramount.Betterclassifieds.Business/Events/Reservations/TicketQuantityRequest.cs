namespace Paramount.Betterclassifieds.Business.Events.Reservations
{
    public class TicketQuantityRequest
    {
        public EventTicket EventTicket { get; }
        public int Quantity { get; }

        public TicketQuantityRequest(EventTicket eventTicket, int quantity)
        {
            EventTicket = eventTicket;
            Quantity = quantity;
        }
    }
}