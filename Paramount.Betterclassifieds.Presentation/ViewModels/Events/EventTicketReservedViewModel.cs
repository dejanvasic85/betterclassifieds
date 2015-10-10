namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    /// <summary>
    /// Used for displaying currently reserved individual reserved event ticket
    /// </summary>
    public class EventTicketReservedViewModel
    {
        public string TicketName { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
    }
}