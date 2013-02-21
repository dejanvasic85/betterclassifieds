namespace Paramount.Betterclassified.Utilities.CreditCardPayment
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Xml;

    public class CreditCardGatewayConfigurationHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            CCPaymentGatewaySettings settings = CCPaymentGatewaySettings.GetSection(section);
            return settings;
        }
    }
}
