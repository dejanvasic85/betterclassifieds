using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class InvoiceView
    {
        public InvoiceView()
        {
            InvoiceGroups = new List<InvoiceGroupView>();
        }

        public string BookingReference { get; set; }
        public DateTime BookingStartDate { get; set; }
        public string PaymentReference { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessPhone { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceGroupView> InvoiceGroups { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public bool IsLineAd { get; set; }
    }

    public class InvoiceGroupView
    {
        public int AdBookingId { get; set; }
        public decimal OrderTotal { get; set; }
        public int? PublicationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public List<InvoiceLineItemView> InvoiceLineItems { get; set; }
    }

    public class InvoiceLineItemView
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal ItemTotal { get; set; }
        public int? Editions { get; set; }
    }
}