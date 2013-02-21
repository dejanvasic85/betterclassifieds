namespace Paramount.ApplicationBlock.Configuration
{
    using System.Collections.Generic;

    public class ConfigurationDictionary:Dictionary<string ,ConfigurationItem>
    {
        public string this[string key,bool returnEmptyIfNull]
        {
            get { return this.ContainsKey(key)? this[key].Value:string.Empty; }
            set { this[key] = new ConfigurationItem {  Value = value }; }
        }

        public string Get(string key)
        {
            return this.ContainsKey(key) ? this[key].Value : string.Empty; 
        }

        public void Add(string key, string value)
        {
            this.Add(key, new ConfigurationItem {Value = value});
        }
    }
}