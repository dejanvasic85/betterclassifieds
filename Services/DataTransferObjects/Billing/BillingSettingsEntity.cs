using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paramount.Common.DataTransferObjects.Billing
{
    public class BillingSettingsEntity
    {
        public string ClientCode { get; set; }

        public Guid BankId { get; set; }

        public string BankName { get; set; }

        public string Description { get; set; }

        public string GatewayUrl { get; set; }

        public string ReturnLinkText { get; set; }
        
        public decimal? GSTRate { get; set; }

        public string RefundPolicyUrl { get; set; }
        

        public bool CollectAddressDetails { get; set; }

        public Guid InvoiceBannerImageId { get; set; }
        
        public string ReferencePrefix { get; set; }

        public string VendorCode { get; set; }
        
        public string PaypalBusinessEmail { get; set; }

        public string PaypalCurrencyCode { get; set; }

        //public string PaypalCancelUrl { get; set; }

        //public string PaypalSuccessUrl { get; set; }


        /* new fields */

        //public string ReturnLinkUrl { get; set; }

       // public string NotifyUrl { get; set; }

       // public string SendToReturn { get; set; }

        //public string ReplyLinkUrl { get; set; }

        public string GstIncluded { get; set; }

        public string PaymentAlertEmail { get; set; }
    }
}
