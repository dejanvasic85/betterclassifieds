using System;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GetInvoiceItemsRequest : BaseRequest
    {
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.GetInvoiceItemsRequest"; }
        }

        public Guid InvoiceId { get; set; }

        #endregion
    }
}