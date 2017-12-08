using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventTicketViewModel
    {
        public EventTicketViewModel()
        {
            this.EventTicketFields = new List<EventTicketFieldViewModel>();
        }

        public int? EventTicketId { get; set; }
        [Required]
        public int? EventId { get; set; }
        [Required]
        public string TicketName { get; set; }
        public int AvailableQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public decimal Price { get; set; }
        public int SoldQty { get; set; }
        public bool IsActive { get; set; }
        public string ColourCode { get; set; }
        public string TicketImageId { get; set; }

        public List<EventTicketFieldViewModel> EventTicketFields { get; set; }

    }
}