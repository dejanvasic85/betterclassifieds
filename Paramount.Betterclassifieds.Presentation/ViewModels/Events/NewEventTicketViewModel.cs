using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class NewEventTicketViewModel
    {
        [Required]
        public int? EventId { get; set; }

        [Required]
        public string TicketName { get; set; }

        public decimal? Price { get; set; }

        public int AvailableQuantity { get; set; }

        public EventTicketFieldViewModel[] Fields { get; set; }
    }
}