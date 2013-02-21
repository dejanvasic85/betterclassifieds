using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GetShoppingCartDetailRequest:BaseRequest
    {
        public Guid ShoppingCartId { get; set; }
        
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.GetShoppingCartDetails"; }
        }

        #endregion
    }
}
