namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicketFactory
    {
        public EventTicket Create(int availableQty, int eventId, string ticketName, decimal price, string colourCode, bool isActive,
            string ticketImage)
        {
            return new EventTicket
            {
                AvailableQuantity = availableQty,
                RemainingQuantity = availableQty,
                EventId = eventId,
                TicketName = ticketName,
                Price = price,
                ColourCode = colourCode,
                IsActive = isActive,
                TicketImage = ticketImage
            };
        }
    }
}