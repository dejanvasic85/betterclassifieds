using System;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class TicketSettingsViewModel
    {
        public bool IncludeTransactionFee { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime? OpeningDate { get; set; }
    }
}