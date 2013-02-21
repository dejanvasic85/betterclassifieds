using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class GetShoppingCartDetailResponse
    {
        public ShoppingCartEntity ShoppingCart { get; set; }

        public IEnumerable<ShoppingCartItemEntity> Items { get; set; }
    }
}
