using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Paramount.ApplicationBlock.Configuration
{
    public class XmlConfigurator : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            if (section == null)
                throw new ArgumentNullException("section", "Invalid or missing configuration section " + "provided to XmlConfigurator"); 
            XPathNavigator xNav = section.CreateNavigator(); 
            if (xNav == null)                 
                throw new ApplicationException("Unable to create XPath Navigator"); 
            Type sectionType = Type.GetType((string)(xNav).Evaluate("string(@configType)")); 
            XmlSerializer xs = new XmlSerializer(sectionType); 
            return xs.Deserialize(new XmlNodeReader(section));
        }
    }
    [XmlRoot("ConnectionConfig")]
    public class ConnectionConfig
    {
        private ConnCompanys comps;    
        [XmlArrayItem(ElementName = "ConnCompany")]    
        public ConnCompanys ConnCompanys
        {
            get { return comps; }        
            set { comps = value; }
        }    
        
        public ConnApp this[string CompanyName, string AppName]
        {
            get { return ConnCompanys[CompanyName][AppName]; }
        }    
        
        public ConnSpec this[string CompanyName, string AppName, APPENV env]
        {
            get
            {
                return ConnCompanys[CompanyName][AppName, env];
            }
        }
    }
    
    public class ConnCompanys : List<ConnCompany>
    {
        public ConnCompany this[string companyName]
        {
            get
            {
                foreach (ConnCompany comp in this)                
                    if (comp.CompanyName == companyName)                    
                        return comp;            
                return null;
            }
        }    
        
        public bool Contains(string companyName)
        {
            foreach (ConnCompany comp in this)            
                if (comp.CompanyName == companyName)                
                    return true;        
            return false;
        }
    }

    public class ConnCompany
    {
        #region private state fields
        private string compNm;
        private ConnApps apps;
        #endregion private state fields

        #region public properties
        [XmlAttribute(DataType = "string", AttributeName = "companyName")]
        public string CompanyName
        {
            get { return compNm; }
            set { compNm = value; }
        }

        [XmlArrayItem(ElementName = "ConnApp")]
        public ConnApps ConnApps
        {
            get { return apps; }
            set { apps = value; }
        }

        #endregion public properties
        #region indexers
        public ConnApp this[string applicationName]
        {
            get { return ConnApps[applicationName]; }
        }
        public ConnSpec this[string applicationName, APPENV environment]
        {
            get
            {
                foreach (ConnSpec con in this[applicationName].ConnSpecs)
                    if (con.Environment == environment)
                        return con;
                return null;
            }
        }
        #endregion indexers}
    }
}
