namespace Paramount.ApplicationBlock.Configuration
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using Utility;

    public class ConfigurationSectionHandler : ConfigurationDictionary, IConfigurationSectionHandler  
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            this.Clear();
            var loaded = XDocument.Parse(section.OuterXml);
            
            if(loaded.Descendants("milieu").Count() > 0)
            {
                var q = GetSettingsFromXDocument(loaded);
                
                if (q == null && section.Attributes["source"] != null)
                {
                    var masterconfig = section.Attributes["source"].Value;
                    if(  System.Web.HttpContext.Current != null)
                    {
                        masterconfig = System.Web.HttpContext.Current.Server.MapPath(masterconfig);
                    }
                    var loadParent = XDocument.Load(masterconfig);
                    q = GetSettingsFromXDocument(loadParent);
                }
            
               if (q !=null)
                {
                    foreach (var item in q)
                    {
                        AddElement(item);
                    }
                }
            }
            //see if there's a default
            if (loaded.Descendants("add").Count() >0)
            {
                var t = from c in loaded.Descendants("add") where c.Parent.Name != "milieu" select c;
                foreach(var item in t)
                {
                    if(string.IsNullOrEmpty(Get((string) item.Attribute("key"))))
                    {
                        AddElement(item);
                    }
                    
                }
            }
            return this;
        }

        private  void AddElement(XElement item )
        {
            if (item != null)
            {
                var credential = (string) item.Attribute("credential");
                var key = (string) item.Attribute("key");
                var itemValue = new ConfigurationItem
                                    {
                                        Value = (string)item.Attribute("value"), 
                                        Credential = (credential ==null)?string.Empty:credential.EndsWith("=") ? GetCredential(credential) : credential
                                    };
                if (key != null ) this[key] = itemValue;
            }
        }

        private static IEnumerable<XElement> GetSettingsFromXDocument(XDocument document)
        {
            return
                    (document.Descendants("milieu").Where(c => (string)c.Attribute("context") == ConfigManager.ConfigurationContext).Select(
                    c => c.Elements("add"))).FirstOrDefault();
        }

        private static string GetCredential(string encryptedData)
        {
            return CryptoHelper.Decrypt(encryptedData);
        }
    }
}
