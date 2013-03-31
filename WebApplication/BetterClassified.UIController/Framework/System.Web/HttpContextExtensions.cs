using System;
using System.Web;

namespace BetterClassified
{
    public static class HttpContextExtensions
    {
        public static T ReadQueryString<T>(this HttpContext context, string key, bool required = true)
        {
            if (context == null)
                throw new ArgumentNullException("context", "HttpContext does not exist or is null");
                
            var value = context.Request.QueryString.Get(key);
            if (string.IsNullOrEmpty(value) && required)
                throw new ApplicationException(string.Format("Query string value is missing [{0}].", key));

            return (T) Convert.ChangeType(value, typeof (T));
        }
    }
}
