
using System;
using System.ComponentModel;

namespace Paramount.Common.DataTransferObjects.Billing
{
    public partial class InvoiceEntity
    {
        #region Properties

        [DisplayName("Invoice Number")]
        public System.Int64 InvoiceNumber { get; set; }

        [DisplayName("Invoice Id")]
        public System.Guid InvoiceId { get; set; }

        [DisplayName("User Id")]
        public string UserId { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Date Time Created")]
        public System.DateTime DateTimeCreated { get; set; }

        [DisplayName("Date Time Updated")]
        public System.DateTime DateTimeUpdated { get; set; }

        [DisplayName("Session Id")]
        public string SessionId { get; set; }

        [DisplayName("Reference Number")]
        public long ReferenceNumber { get; set; }

        [DisplayName("Billing Address")]
        public AddressDetails BillingAddress { get; set; }

        [DisplayName("Delivery Name")]
        public AddressDetails DeliveryAddress { get; set; }

        [DisplayName("Payment Type")]
        public string PaymentType { get; set; }

        [DisplayName("Payment Reference")]
        public string PaymentReference { get; set; }

        [DisplayName("Client Code")]
        public string ClientCode { get; set; }

        /* start new fields */
         [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Gst Included")]
        public bool GstIncluded { get; set; }

        /*end new fields */


        #endregion


    }
}
