using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class ReserveTicketsViewModel
    {
        public int EventId { get; set; }
        public long? EventInvitationId { get; set; }
        public List<EventTicketRequestViewModel> Tickets { get; set; }
    }
}