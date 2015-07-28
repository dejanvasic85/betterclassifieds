namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventTicket
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public EventModel EventModel { get; set; }
        public string TicketName { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
    }
}