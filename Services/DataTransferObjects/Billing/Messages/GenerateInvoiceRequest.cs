using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GenerateInvoiceRequest : BaseRequest
    {
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.GenerateInvoice"; }
        }

        public Guid ShoppingCartId { get; set; }

        #endregion
    }
}
