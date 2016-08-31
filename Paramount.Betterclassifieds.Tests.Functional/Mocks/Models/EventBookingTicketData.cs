namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.Models
{
    internal class EventBookingTicketData
    {
        public int EventBookingTicketId { get; set; }
        public decimal Price { get; set; }
        public string GuestEmail { get; set; }
        public string GuestFullName { get; set; }
        public string TicketName { get; set; }
        public int? EventGroupId { get; set; }
    }
}