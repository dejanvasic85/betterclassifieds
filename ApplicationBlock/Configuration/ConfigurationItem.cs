namespace Paramount.ApplicationBlock.Configuration
{
    public class ConfigurationItem
    {
        public string Value { get; set; }

        public string Credential { get; set; }

        public string Wsdl { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}