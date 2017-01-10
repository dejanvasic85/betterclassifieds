using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class ManageTicketsViewModel
    {
        public int Id { get; set; } // AdId
        public int EventId { get; set; }
        public List<EventTicketViewModel> Tickets { get; set; }
        public bool? IncludeTransactionFee { get; set; }
        
    }
}