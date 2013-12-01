using System;
using System.Web;
using System.Web.Caching;

namespace Paramount
{
    /// <summary>
    /// Utility for storing and retrieving items from the web httpCache
    /// </summary>
    public static class HttpCacher
    {
        /// <summary>
        /// Returns required item T from the cache. If item is not cached, it uses the creator method and stores it for next time.
        /// </summary>
        public static T FetchOrCreate<T>(string key, Func<T> creator, int minutesToCache = 720, int seconds = 0) where T : class
        {
            var obj = HttpContext.Current.Cache.Get(key);
            if (obj != null)
            {
                // Force the cast to the required object
                return obj as T;
            }

            // Create the object and boom - create  
            var result = creator();
            HttpContext.Current.Cache.Insert(key, result, null, Cache.NoAbsoluteExpiration, new TimeSpan(hours:0, minutes:minutesToCache, seconds:seconds));
            return result;
        }
    }
}