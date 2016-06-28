using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class ManageGroupsViewModel
    {
        public int Id { get; set; } // AdId
        public int EventId { get; set; }
        public List<EventGroupViewModel> EventGroups { get; set; }
        public List<EventTicketViewModel> Tickets { get; set; }
    }
}