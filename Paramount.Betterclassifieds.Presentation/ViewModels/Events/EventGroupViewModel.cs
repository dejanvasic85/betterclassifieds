using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventGroupViewModel
    {
        public int? EventGroupId { get; set; }
        public int? EventId { get; set; }
        public string GroupName { get; set; }
        public int? MaxGuests { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? CreatedDateTimeUtc { get; set; }
        public string CreatedBy { get; set; }
        public int GuestCount { get; set; }
        public bool? AvailableToAllTickets { get; set; }
        public List<EventTicketViewModel> AvailableTickets { get; set; }
    }
}