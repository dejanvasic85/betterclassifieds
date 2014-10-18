using System.Configuration;

namespace Paramount.ApplicationBlock.Configuration
{
    public class PaymentProvidersSection : ConfigurationSection
    {
        [ConfigurationProperty("mockProvider", IsRequired = false)]
        public MockProviderElement MockProvider
        {
            get { return (MockProviderElement) this["mockProvider"]; }
            set { this["mockProvider"] = value; }
        }

        [ConfigurationProperty("payPalProvider", IsRequired = false)]
        public PayPalProviderElement PayPalProvider
        {
            get { return (PayPalProviderElement) this["payPalProvider"]; }
            set { this["payPalProvider"] = value; }
        }
    }

    public class MockProviderElement : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string FriendlyName
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
    }

    public class PayPalProviderElement : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string FriendlyName
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
    }
}