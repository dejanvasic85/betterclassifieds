using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Caching;
using BetterClassified.Models;
using Paramount;

namespace BetterClassified.Repository
{
    public interface IMenuRepository
    {
        IDictionary<string, string> GetMenuItemLinkNamePairs();
    }

    public class TheMusicMenuRepository : IMenuRepository
    {
        private readonly IConfigSettings configSettings;
        private const string CacheKey = "TheMusicMenuCache";

        public TheMusicMenuRepository(IConfigSettings configSettings)
        {
            this.configSettings = configSettings;
        }

        public IDictionary<string, string> GetMenuItemLinkNamePairs()
        {
            // We are going to fetch the menu items from TheMusic website and CACHE IT
            if (HttpContext.Current.Cache[CacheKey] == null)
            {
                // Make a WebClient call to get the full html
                WebClient client = new WebClient();
                var theMusicContent = client.DownloadString(configSettings.PublisherHomeUrl);

                Models.TheMusicHtmlScraper parser = new TheMusicHtmlScraper(theMusicContent, configSettings.PublisherHomeUrl);

                // Parse the items and home in case nothing comes back
                var parsedItems = parser.ParseMenuItems().AddOrUpdate("Home", configSettings.PublisherHomeUrl);

                // Remove the classies link
                if (parsedItems.ContainsKey("Classies"))
                    parsedItems.Remove("Classies");

                // Store in cache for up to 12 hours!
                HttpContext.Current.Cache.Insert(CacheKey, parsedItems, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 12, 0));

                return parsedItems;
            }

            return HttpContext.Current.Cache[CacheKey] as Dictionary<string, string>;
        }
    }
}