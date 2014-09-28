using System;

namespace Paramount.Utility.Dsl
{
    using System.Collections.Specialized;

    [Obsolete]
    public class DslQueryParam
    {
        private NameValueCollection collectionData;
        const int DefaultResolution = 90;
        private const string documentIdKey = "docId";
        private const string entityKey = "entity";
        private const string resolutionKey = "res";
        private const string heightKey = "height";
        private const string widthKey = "width";
        private string url = "?";

        public DslQueryParam(NameValueCollection collectionParams)
        {
            collectionData = new NameValueCollection(collectionParams);
        }

        public string DocumentId
        {
            get
            {
                return collectionData[documentIdKey];
            }
            set
            {
                collectionData.Add(documentIdKey, value);
                AddQueryStringValue(documentIdKey, value);
            }
        }

        public string Entity
        {
            get
            {
                return collectionData[entityKey];
            }
            set
            {
                collectionData.Add(entityKey, value);
                AddQueryStringValue(entityKey, value);
            }
        }

        public int Resolution
        {
            get
            {
                return string.IsNullOrEmpty(collectionData[resolutionKey]) ? DefaultResolution : int.Parse(collectionData[resolutionKey]);
            }
            set
            {
                collectionData.Add(resolutionKey, value.ToString());
                AddQueryStringValue(resolutionKey, value.ToString());
            }
        }

        public decimal? Height
        {
            get
            {
                decimal outValue;
                decimal? temp = null;
                return decimal.TryParse(collectionData[heightKey], out outValue) ? outValue : temp;
            }
            set
            {
                collectionData.Add(heightKey, value.ToString());
                AddQueryStringValue(heightKey, value.ToString());
            }
        }

        public decimal? Width
        {
            get
            {
                decimal outValue;
                decimal? temp = null;
                return decimal.TryParse(collectionData[widthKey], out outValue) ? outValue : temp;
            }
            set
            {
                collectionData.Add(widthKey, value.ToString());
                AddQueryStringValue(widthKey, value.ToString());
            }
        }

        private void AddQueryStringValue(string name, string value)
        {
            url += string.Format("{0}={1}&", name, value);
        }

        public string GenerateUrl()
        {
            return url;
        }

        public string GenerateUrl(string serverName)
        {
            return string.Format("{0}{1}", serverName, url);
        }
    }
}
