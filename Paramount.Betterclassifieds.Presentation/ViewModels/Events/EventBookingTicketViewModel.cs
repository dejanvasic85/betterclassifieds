namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventBookingTicketViewModel
    {
        public int EventBookingId { get; set; }
        public int EventTicketId { get; set; }
        public string TicketName { get; set; }
        public decimal? TotalPrice { get; set; }
        public string GuestFullName { get; set; }
        public string GuestEmail { get; set; }
    }
}