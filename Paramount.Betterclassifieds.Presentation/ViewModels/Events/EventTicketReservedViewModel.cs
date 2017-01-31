using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    /// <summary>
    /// Used for displaying currently reserved individual reserved event ticket
    /// </summary>
    public class EventTicketReservedViewModel
    {
        public EventTicketReservedViewModel()
        {
            TicketFields = new List<EventTicketFieldViewModel>();
        }
        public int EventTicketId { get; set; }
        public int EventTicketReservationId { get; set; }
        public string TicketName { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public string GuestFullName { get; set; }
        public string GuestEmail { get; set; }
        public List<EventTicketFieldViewModel> TicketFields { get; set; }
        public int? EventGroupId { get; set; }
        public bool IsPublic { get; set; }
    }
}