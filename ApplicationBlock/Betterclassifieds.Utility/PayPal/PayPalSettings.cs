using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Paramount.Betterclassified.Utilities.PayPal
{
    [Serializable]
    public class PayPalSettings
    {
        private static PayPalSettings instance;

        public string BusinessEmail
        {
            get; set;
        }

        public string SuccessUrl
        {
            get; set;
        }

        public string CancelPurchaseUrl
        { 
            get;set;
        }

        public string CurrencyCode
        {
            get; set;
        }

        public string NotifyUrl
        { get; set; }

        public bool SendToReturnURL
        {
            get; set;
        }

        public string PayPalUrl
        {
            get; set;
        }

        public static PayPalSettings GetSection(XmlNode node)
        {
            if(instance == null)
            {
                instance = new PayPalSettings(node);
            }
            return instance;
        }

        private PayPalSettings(XmlNode section)
        {
            XmlNode configContent = section.SelectSingleNode("/paypal");
            if (configContent != null)
            {
                BusinessEmail = configContent.Attributes["BusinessEmail"].InnerText;
                SuccessUrl = configContent.Attributes["SuccessUrl"].InnerText;
                NotifyUrl = configContent.Attributes["NotifyUrl"].InnerText;
                SendToReturnURL = Convert.ToBoolean(configContent.Attributes["SendToReturnURL"].InnerText);
                CurrencyCode = configContent.Attributes["CurrencyCode"].InnerText;
                CancelPurchaseUrl = configContent.Attributes["CancelPurchaseUrl"].InnerText;
                PayPalUrl = configContent.Attributes["PayPalUrl"].InnerText;
            }
        }



    }
}
