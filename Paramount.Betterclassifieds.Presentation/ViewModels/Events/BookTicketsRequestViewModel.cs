using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    /// <summary>
    /// Used to load the ticketing booking page
    /// </summary>
    public class BookTicketsRequestViewModel
    {
        [Required]
        public int? EventId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [RequiredIf("PaymentRequired", true)]
        public string PaymentMethod { get; set; }
        [Required]
        public decimal TotalCost { get; set; }

        public bool PaymentRequired
        {
            get { return this.TotalCost > 0; }
        }

        public List<EventTicketReservedViewModel> Reservations { get; set; }
    }
}