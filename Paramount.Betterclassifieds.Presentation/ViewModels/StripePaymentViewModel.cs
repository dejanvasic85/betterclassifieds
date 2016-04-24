using System;
namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class StripePaymentViewModel
    {
        public string StripeToken { get; set; }
        public string StripeTokenType { get; set; }
        public string StripeEmail { get; set; }
    }
}