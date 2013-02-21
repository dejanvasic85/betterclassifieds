using System;
using System.Collections.Specialized;
using System.Web;
using System.Linq;

namespace Paramount.Utility
{
    
    public class CustomQueryString : NameValueCollection
    {
        public CustomQueryString(NameValueCollection col) : base(col)
        {
            
        }

       

        public  void AddOrOverWrite(string key,string value)
        {
            if (AllKeys.Contains(key))
            {
                this[key] = value;
            }
            else
            {
                this.Add(key,value);
            }
        }
        
        public string ToQueryString()
        {
            return "?" + string.Join("&", Array.ConvertAll(this.AllKeys, key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(this[key]))));
        }

        public string CompleteUrl(Uri uri)
        {
            return uri.GetLeftPart(UriPartial.Path) + this.ToQueryString();
        }
    }
}
