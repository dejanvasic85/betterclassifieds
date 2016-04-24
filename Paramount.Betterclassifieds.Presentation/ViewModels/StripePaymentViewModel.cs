using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class StripePaymentViewModel
    {
        public string StripeToken { get; set; }
        public string StripeTokenType { get; set; }
        public string StrikeEmail { get; set; }
    }
}