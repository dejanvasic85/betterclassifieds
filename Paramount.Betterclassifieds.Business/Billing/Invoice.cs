using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceGroups = new List<InvoiceGroup>();
        }

        public List<InvoiceGroup> InvoiceGroups { get; set; }
        public DateTime BookingStartDate { get; set; }
        public string BookingReference { get; set; }
        public string PaymentReference { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessPhone { get; set; }
        public decimal Total { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedDateUtc { get; set; }

        public bool IsLineAd
        {
            get { return InvoiceGroups.Any(g => g.PublicationId.HasValue); }
        }
    }
}