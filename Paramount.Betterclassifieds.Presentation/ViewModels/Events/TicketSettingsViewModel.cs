using System;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class TicketSettingsViewModel
    {
        public bool IncludeTransactionFee { get; set; }

        public DateTime? TicketClosingDateTime { get; set; }
        public DateTime? TicketOpeningDateTime { get; set; }
    }
}