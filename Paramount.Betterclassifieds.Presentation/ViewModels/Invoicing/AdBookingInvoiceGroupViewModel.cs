using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class AdBookingInvoiceGroupViewModel
    {
        public int AdBookingId { get; set; }
        public decimal OrderTotal { get; set; }
        public int? PublicationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public List<AdBookingLineItemViewModel> InvoiceLineItems { get; set; }
    }
}