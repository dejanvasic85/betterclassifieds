using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class EventTicketReservervationViewModel
    {
        [Required] 
        public int? TicketId { get; set; }
        [Required]
        public int? EventId { get; set; }
        [Required]
        public string TicketName { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int SelectedQuantity { get; set; }
    }
}