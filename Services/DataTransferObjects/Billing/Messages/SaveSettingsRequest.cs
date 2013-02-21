using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing.Messages
{
    public class SaveSettingsRequest : BaseRequest
    {
        public override string TransactionName
        {
            get { return "Billing.SaveSettingsRequest"; }
        }

        public BillingSettingsEntity BillingSettings { get; set; }
    }
}
