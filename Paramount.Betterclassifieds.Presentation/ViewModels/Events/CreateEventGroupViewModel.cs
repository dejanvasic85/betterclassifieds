using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class CreateEventGroupViewModel
    {
        public CreateEventGroupViewModel()
        {
            AvailableTickets = new List<CreateEventGroupSelectedTicket>();
        }
        public int EventId { get; set; }
        public string GroupName { get; set; }
        public int? MaxGuests { get; set; }
        public List<CreateEventGroupSelectedTicket> AvailableTickets { get; set; }
    }

    public class CreateEventGroupSelectedTicket
    {
        public string TicketName { get; set; }
        public int EventTicketId { get; set; }
    }
}