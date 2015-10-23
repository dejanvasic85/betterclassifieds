namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventTicketPrintViewModel
    {
        public int TicketNumber { get; set; }
        public string EventName { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string StartDateTime { get; set; }
        public string TicketName { get; set; }
        public decimal Price { get; set; }
        public string ContactNumber { get; set; }
    }
}