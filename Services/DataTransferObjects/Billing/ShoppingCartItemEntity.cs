
using System;
using System.ComponentModel;

namespace Paramount.Common.DataTransferObjects.Billing
{
    public partial class ShoppingCartItemEntity
    {
        #region Properties

        [DisplayName("Shopping Cart Item Id")]
        public System.Guid ShoppingCartItemId { get; set; }

        [DisplayName("Reference Id")]
        public string ReferenceId { get; set; }

        [DisplayName("Quantity")]
        public decimal Quantity { get; set; }

        [DisplayName("Price")]
        public decimal Price { get; set; }

        [DisplayName("Product Type")]
        public string ProductType { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Summary")]
        public string Summary { get; set; }

        [DisplayName("Shopping Cart Id")]
        public Guid ShoppingCartId { get; set; }


        #endregion


    }
}
