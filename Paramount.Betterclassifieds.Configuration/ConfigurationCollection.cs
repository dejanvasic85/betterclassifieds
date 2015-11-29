using System;
using System.Collections;
using System.Collections.Generic;

namespace Paramount.ApplicationBlock.Configuration
{
    public class ConfigurationCollection:IDictionary<string,string>
    {
        private readonly Dictionary<string, ConfigurationItem> _configurationList;

        
        private Dictionary<string,string> GetItems()
        {
            var list = new Dictionary<string,string>();
            foreach (var item in _configurationList)
            {
                list.Add(item.Key, item.Value.Value);
            }

            return list ;
        }

        public ConfigurationCollection()
        {
            _configurationList = new Dictionary<string, ConfigurationItem>();
        }

        public string Get(string key)
        {
            return this[key];
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            
          return GetItems().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, string> item)
        {
            _configurationList.Add(item.Key,new ConfigurationItem{Value =item.Value});
        }


        public void Add(KeyValuePair<string, string> item, ConfigurationItem value)
        {
            _configurationList.Add(item.Key, value);

        }

        public void Clear()
        {
            _configurationList.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return _configurationList.ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return _configurationList.Remove(item.Key);
        }

        public int Count
        {
            get { return _configurationList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool ContainsKey(string key)
        {
            return _configurationList.ContainsKey(key);
        }

        public void Add(string key, string value)
        {
            _configurationList.Add(key, new ConfigurationItem { Value = value});
        }

        public bool Remove(string key)
        {
            return _configurationList.Remove(key);
        }

        public bool TryGetValue(string key, out string value)
        {
            value = this[key];
            return ContainsKey(key);
        }

        public string this[string key]
        {
            get { return this.ContainsKey(key)? _configurationList[key].Value:string.Empty; }
            set { _configurationList[key]=new ConfigurationItem{Key=key, Value =value }; }
        }

        public ICollection<string> Keys
        {
            get { return _configurationList.Keys; }
        }

        public ICollection<string> Values
        {
            get { return GetItems().Values; }
        }
    }
}