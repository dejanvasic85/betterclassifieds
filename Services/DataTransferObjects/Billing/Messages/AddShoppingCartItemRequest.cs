using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class AddShoppingCartItemRequest : BaseRequest
    {

        public ShoppingCartItemEntity ShoppingCartItem { get; set; }
        
        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.AddShoppingCart"; }
        }

        #endregion
    }
}
