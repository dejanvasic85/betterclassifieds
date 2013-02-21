using System;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GetInvoiceDetailRequest : BaseRequest
    {
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.GetInvoiceDetailRequest"; }
        }

        public Guid InvoiceId { get; set; }

       
        #endregion


    }
}