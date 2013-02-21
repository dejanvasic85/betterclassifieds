using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class InvoicePaidRequest : BaseRequest
    {
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.InvoicePaid"; }
        }

        public Guid InvoiceId { get; set; }

        public string SessionId { get; set; }

        public decimal TotalAmount { get; set; }

        #endregion
    }
}
