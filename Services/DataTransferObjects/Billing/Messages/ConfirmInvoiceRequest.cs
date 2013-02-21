using System;
using Paramount.Common.DataTransferObjects.Enums;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class ConfirmInvoiceRequest : BaseRequest
    {
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.ConfirmInvoice"; }
        }

        public Guid InvoiceId { get; set; }

        public PaymentType PaymentType { get; set; }

        #endregion
    }
}