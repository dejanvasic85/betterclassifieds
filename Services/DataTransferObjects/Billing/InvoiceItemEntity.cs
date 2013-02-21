using System;
using System.ComponentModel;

namespace Paramount.Common.DataTransferObjects.Billing
{
    public partial class InvoiceItemEntity
    {
        #region Properties

        [DisplayName("Invoice Item Id")]
        public System.Guid InvoiceItemId { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Summary")]
        public string Summary { get; set; }

        [DisplayName("Quantity")]
        public decimal Quantity { get; set; }

        [DisplayName("Price")]
        public decimal Price { get; set; }

        [DisplayName("Invoice Id")]
        public Guid? InvoiceId { get; set; }

        [DisplayName("Reference Id")]
        public string ReferenceId { get; set; }

        [DisplayName("Product Type")]
        public string ProductType { get; set; }

        [DisplayName("Sub Total")]
        public decimal SubTotal 
        { 
            get { return this.Price * this.Quantity; }

        }


        #endregion


    }
}
