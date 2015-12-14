namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketFactory
    {
        public EventTicket Create(int remainingQuantity, int eventId, string ticketName, decimal price)
        {
            return new EventTicket
            {
                AvailableQuantity = remainingQuantity,
                RemainingQuantity = remainingQuantity,
                EventId = eventId,
                TicketName = ticketName,
                Price = price
            };
        }
    }
}