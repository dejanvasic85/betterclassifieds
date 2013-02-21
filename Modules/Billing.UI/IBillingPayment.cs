using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Billing.UI
{
    public  interface IBillingPayment
    {
        void InvoicePaid(string refrenceId, Decimal cost,string productTypeValue);
    }
}
