using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class PricingFactorsView
    {
        public string LineAdText { get; set; }
        public string LineAdHeader { get; set; }
        public bool IsSuperBoldHeader { get; set; }

        public int Editions { get; set; }
    }
}