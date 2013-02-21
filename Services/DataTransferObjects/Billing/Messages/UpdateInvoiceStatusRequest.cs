using System;
using Paramount.Common.DataTransferObjects.Enums;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class UpdateInvoiceStatusRequest : BaseRequest
    {
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.ConfirmInvoice"; }
        }

        public Guid InvoiceId { get; set; }

        public InvoiceStatus InvoiceStatus { get; set; }

        #endregion
    }
}