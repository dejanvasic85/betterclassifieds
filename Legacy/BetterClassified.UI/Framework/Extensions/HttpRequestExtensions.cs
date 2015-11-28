using System;
using System.Web;

namespace BetterClassified
{
    public static class HttpRequestExtensions
    {
        public static T QueryStringValue<T>(this HttpRequest request, string key)
        {
            var value = request.QueryString.Get(key);
            if (value == null)
                return default(T);

            return (T) Convert.ChangeType(value, typeof (T));
        }
    }
}