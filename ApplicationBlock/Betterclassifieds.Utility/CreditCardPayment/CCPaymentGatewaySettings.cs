using System;
using System.Xml;

namespace Paramount.Betterclassified.Utilities.CreditCardPayment
{
    public class CCPaymentGatewaySettings
    {
        private static CCPaymentGatewaySettings instance;

        public static CCPaymentGatewaySettings GetSection(XmlNode node)
        {
            if(instance == null)
            {
                instance = new CCPaymentGatewaySettings(node);
            }
            return instance;
        }

       public string RefundPolicyUrl
        {
            get; set;
        }

        public string VendorName
        {
            get;
            set;
        }

        public string PaymentAlertEmail
        { 
            get;
            set;
        }

        public decimal GstRate
        {
            get; set;
        }

        public bool GstAdded
        {
            get; set;
        }

        public string PaymentReference
        {
            get; set;
        }


        public string CurrencyCode
        {
            get;
            set;
        }

        public string ReturnUrl
        { get; set; }

        public string NotifyUrl
        { get; set; }


        public string GatewayUrl
        {
            get; set;
        }

        public string HiddenFields
        {
            get; set;
        }

        public string ReturnUrlText
        { get; set; }

        private CCPaymentGatewaySettings(XmlNode section)
        {
            XmlNode configContent = section.SelectSingleNode("/ccPaymentGateway");
            if (configContent != null)
            {
                this.RefundPolicyUrl = configContent.Attributes["refundPolicyUrl"].InnerText;
                this.VendorName = configContent.Attributes["vendorName"].InnerText;
                this.PaymentAlertEmail = configContent.Attributes["paymentAlertEmail"].InnerText;
                this.PaymentReference = configContent.Attributes["paymentReference"].InnerText;
                this.GstRate = Convert.ToDecimal(  configContent.Attributes["gstRate"].InnerText);
                this.GstAdded = Convert.ToBoolean(configContent.Attributes["gstIncluded"].InnerText);
                NotifyUrl = configContent.Attributes["replyLinkUrl"].InnerText;
                this.ReturnUrl = configContent.Attributes["returnLinkUrl"].InnerText;
                this.ReturnUrlText = configContent.Attributes["returnLinkText"].InnerText;
                GatewayUrl = configContent.Attributes["gatewayUrl"].InnerText;
            }
        }
    }
}
