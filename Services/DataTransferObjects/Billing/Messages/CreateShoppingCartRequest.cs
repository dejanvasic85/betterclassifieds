using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class CreateShoppingCartRequest: BaseRequest
    {
        //public ShoppingCartEntity ShoppingCart { get; set; }

        public string SessionId { get; set; }

        #region Overrides of BaseRequest

        public override string TransactionName
        {
            get { return "Billing.CreateShoppingCart"; }
        }

        public string Title { get; set; }

        #endregion
    }
}
