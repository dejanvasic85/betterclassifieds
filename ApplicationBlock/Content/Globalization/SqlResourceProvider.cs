namespace Paramount.ApplicationBlock.Content.Globalization
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Resources;
    using System.Web.Compilation;
    using Data;

    sealed class SqlResourceProvider : IResourceProvider
    {
        private readonly string _virtualPath;
        private readonly string _className;
        private IDictionary _resourceCache;
        private static readonly object CultureNeutralKey = new object();

        public SqlResourceProvider(string virtualPath, string className)
        {
            _virtualPath = virtualPath;
            _className = className;
        } 

        object IResourceProvider.GetObject(string resourceKey, CultureInfo culture)
        {
            var cultureName = culture != null ? culture.Name : CultureInfo.CurrentUICulture.Name;

            object value = GetResourceCache(cultureName)[resourceKey];
            if (value == null)
            {
                // resource is missing for current culture, use default
                SqlResourceHelper.AddResource(resourceKey,
                        _virtualPath, _className, cultureName);
                value = GetResourceCache(null)[resourceKey];
            }
            if (value == null)
            {
                // the resource is really missing, no default exists
                SqlResourceHelper.AddResource(resourceKey,
                     _virtualPath, _className, string.Empty);
            }
            return value;
        }


        IResourceReader IResourceProvider.ResourceReader
        {
            get
            {
                return new SqlResourceReader(GetResourceCache(null));
            }
        }


        private IDictionary GetResourceCache(string cultureName)
        {
            var cultureKey = cultureName ?? CultureNeutralKey;

            if (_resourceCache == null)
            {
                _resourceCache = new ListDictionary();
            }

            var resourceDict = _resourceCache[cultureKey] as IDictionary;
            if (resourceDict == null)
            {
                resourceDict = SqlResourceHelper.GetResources(_virtualPath,
                              _className, cultureName, false, null);
                _resourceCache[cultureKey] = resourceDict;
            }
            return resourceDict;
        }

    }
}