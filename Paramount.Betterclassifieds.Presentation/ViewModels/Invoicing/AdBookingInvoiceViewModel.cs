using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class AdBookingInvoiceViewModel
    {
        public AdBookingInvoiceViewModel()
        {
            InvoiceGroups = new List<AdBookingInvoiceGroupViewModel>();
        }

        public string BookingReference { get; set; }
        public DateTime BookingStartDate { get; set; }
        public string PaymentReference { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessPhone { get; set; }
        public decimal Total { get; set; }
        public List<AdBookingInvoiceGroupViewModel> InvoiceGroups { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public bool IsLineAd { get; set; }
    }
}