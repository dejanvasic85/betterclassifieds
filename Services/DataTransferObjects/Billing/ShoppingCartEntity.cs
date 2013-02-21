
using System.ComponentModel;

namespace Paramount.Common.DataTransferObjects.Billing
{
    public partial class ShoppingCartEntity
    {
        #region Properties

        [DisplayName("Shopping Cart Id")]
        public System.Guid ShoppingCartId { get; set; }

        [DisplayName("User Id")]
        public string UserId { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Date Time Created")]
        public System.DateTime DateTimeCreated { get; set; }

        [DisplayName("Date Time Modified")]
        public System.DateTime DateTimeModified { get; set; }

        [DisplayName("Session Id")]
        public string SessionId { get; set; }

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
