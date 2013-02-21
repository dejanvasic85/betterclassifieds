using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class UpdateInvoiceAddressDetailsRequest:BaseRequest
    {
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.UpdateInvoiceAddressDetails"; }
        }

        public Guid InvoiceId { get; set; }

        public AddressDetails BillingAddress { get; set; }

        public AddressDetails DeliveryAddress { get; set; }

        #endregion
    }
}
