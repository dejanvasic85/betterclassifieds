namespace Paramount.Common.DataTransferObjects.Broadcast
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Collections.Generic;

    public class TemplateItemCollection:List<TemplateItem>
    {
        public string this[string name]
        {
            get
            {
                foreach(var item in this)
                {
                    if(item.Name.ToLower(CultureInfo.InvariantCulture) == name.ToLower(CultureInfo.InvariantCulture))
                    {
                        return item.Value;
                    }
                }
                return string.Empty;
            }
            set
            {
                this[IndexOf(GetItem(name))].Value = value;
            }
        }

        public bool Contains(string name)
        {
            foreach (var item in this)
            {
                if (item.Name.ToLower(CultureInfo.InvariantCulture) != name.ToLower(CultureInfo.InvariantCulture))
                    continue;
                return true;
            }
            return false;
        }

        public TemplateItem GetItem(string name)
        { 
            foreach( var item in this)
            {
                if (item.Name.ToLower(CultureInfo.InvariantCulture) != name.ToLower(CultureInfo.InvariantCulture))
                    continue;
                return item;
            }   
            return null;
        }

        public void Add(string name, string value)
        {
            if(Contains(name))
            {
                throw new Exception("Template name already exist");
            }
            this.Add(new TemplateItem{Name =name ,Value =value});
        }
    }
}