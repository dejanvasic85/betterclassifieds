using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EditGuestViewModel
    {
        public int AdId { get; set; }

        [Required]
        public int EventBookingTicketId { get; set; }

        [Required]
        public int EventBookingId { get; set; }

        [Required]
        public int EventTicketId { get; set; }

        [MaxLength(100)]
        public string GuestFullName { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string GuestEmail { get; set; }
    }
}