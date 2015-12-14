using System.ComponentModel.DataAnnotations;
using Paramount.Betterclassifieds.Mvc.Validators;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventPaymentRequestViewModel
    {
        [Required]
        public int? EventId { get; set; }
        [MustBeOneOf("PayPal", "DirectDebit")]
        public string PaymentMethod { get; set; }
        [Required]
        public decimal? RequestedAmount { get; set; }
    }
}