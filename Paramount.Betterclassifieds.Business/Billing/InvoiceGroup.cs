using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public class InvoiceGroup
    {
        public InvoiceGroup()
        {
            InvoiceLineItems = new List<InvoiceLineItem>();
        }
        public int AdBookingId { get; set; }
        public decimal OrderTotal { get; set; }
        public int? PublicationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public List<InvoiceLineItem> InvoiceLineItems { get; set; }
    }
}