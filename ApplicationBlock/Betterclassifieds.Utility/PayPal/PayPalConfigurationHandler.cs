using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace Paramount.Betterclassified.Utilities.PayPal
{
    public class PayPalConfigurationHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            PayPalSettings settings = PayPalSettings.GetSection(section);
            return settings;
        }
    }
}
