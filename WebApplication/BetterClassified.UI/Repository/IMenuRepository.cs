using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Caching;
using BetterClassified.Models;

namespace BetterClassified.Repository
{
    public interface IMenuRepository
    {
        Dictionary<string, string> GetMenuItemLinkNamePairs();
    }

    public class TheMusicMenuRepository : IMenuRepository
    {
        private readonly IConfigSettings configSettings;
        private const string cacheKey = "TheMusicMenuCache";

        public TheMusicMenuRepository(IConfigSettings configSettings)
        {
            this.configSettings = configSettings;
        }

        public Dictionary<string, string> GetMenuItemLinkNamePairs()
        {
            // We are going to fetch the menu items from TheMusic website and CACHE IT
            if (HttpContext.Current.Cache[cacheKey] == null)
            {
                // Make a WebClient call to get the full html
                WebClient client = new WebClient();
                var theMusicContent = client.DownloadString("http://themusic.com.au");

                Models.TheMusicMenuParser parser = new TheMusicMenuParser(theMusicContent, configSettings);
                var parsedItems = parser.GetMenuItemPairs();

                // Store in cache for up to 12 hours!
                HttpContext.Current.Cache.Insert(cacheKey, parsedItems, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 12, 0));
            }

            return HttpContext.Current.Cache[cacheKey] as Dictionary<string, string>;
        }
    }
}