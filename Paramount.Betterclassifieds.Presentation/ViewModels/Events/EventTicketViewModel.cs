namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventTicketViewModel
    {
        public int? EventTicketId { get; set; }
        public int? EventId { get; set; }
        public string TicketName { get; set; }
        public int AvailableQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public decimal Price { get; set; }
    }
}