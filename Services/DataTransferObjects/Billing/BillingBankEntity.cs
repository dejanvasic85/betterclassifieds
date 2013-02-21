using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing
{
    public class BillingBankEntity
    {
        public bool AllowOverride { get; set; }

        public Guid BankId { get; set; }

        public string BankName { get; set; }

        public string Description { get; set; }

        public string GatewayUrl { get; set; }

        public decimal GSTRate { get; set; }

        public string RefundPolicyUrl { get; set; }

        public string ReturnLinkText { get; set; }

        public string ReturnLinkUrl { get; set; }

        public string VendorCode { get; set; }
    }
}
