using System.Configuration;
using Paramount.Betterclassifieds.Mvc.Modules;

namespace Paramount.Betterclassifieds.Mvc.Configuration
{
    public class ModuleRegistrationSection : ConfigurationSection
    {
        private static ModuleRegistrationSection settings = ConfigurationManager.GetSection("moduleregistration") as ModuleRegistrationSection;

        public static ModuleRegistrationSection Settings
        {
            get
            {
                return settings;
            }
        }

        [ConfigurationProperty("modules", IsDefaultCollection = false)]
        public ModuleCollection Modules
        {
            get { return (ModuleCollection)base["modules"]; }

        }
    }

    public class ModuleCollection : ConfigurationElementCollection
    {
        public ModuleCollection()
        {

        }

        public new ModuleConfigElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;

                return (ModuleConfigElement)BaseGet(name);
            }
        }

        public ModuleConfigElement this[int index]
        {
            get { return (ModuleConfigElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }

        }

        public void Add(ModuleConfigElement details)
        {
            BaseAdd(details);
        }
        //protected override void BaseAdd(ModuleConfigElement element)
        //{
        //    BaseAdd(element, false);
        //}

        public void Remove(ModuleConfigElement details)
        {
            if (BaseIndexOf(details) >= 0)
                BaseRemove(details.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }


        public int IndexOf(string name)
        {
            name = name.ToLower();

            for (int idx = 0; idx < base.Count; idx++)
            {
                if (this[idx].Name.ToLower() == name)
                    return idx;
            }
            return -1;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ModuleConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModuleConfigElement)element).Name;
        }

        protected override string ElementName
        {
            get { return "module"; }
        }

    }

    public class ModuleConfigElement : ConfigurationElement, IModuleInfo
    {
        public ModuleConfigElement()
        {

        }


        public ModuleConfigElement(string name, string @namespace, bool registerView)
        {
            this.Name = name;
            this.Namespace = @namespace;
            this.RegisterView = registerView;
        }

        [ConfigurationProperty("physicalpath", IsRequired = false, DefaultValue = "~/")]
        public string PhysicalPath
        {
            get { return (string)this["physicalpath"]; }
            set { this["physicalpath"] = value; }
        }

        [ConfigurationProperty("virtualpath", IsRequired = false, DefaultValue = "~/")]
        public string VirtualPath
        {
            get { return (string)this["virtualpath"]; }
            set { this["virtualpath"] = value; }
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }



        [ConfigurationProperty("namespace", IsRequired = true, DefaultValue = "")]
        public string Namespace
        {
            get { return (string)this["namespace"]; }
            set { this["namespace"] = value; }
        }

        [ConfigurationProperty("registerview", IsRequired = false, DefaultValue = false)]
        public bool RegisterView
        {
            get { return (bool)this["registerview"]; }
            set { this["registerview"] = value; }
        }

    }
}
