﻿using System.Collections.Generic;
using System.Net;
using Paramount;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Repository;

namespace BetterClassified.Repository
{
    public class TheMusicMenuRepository : IMenuRepository
    {
        private readonly IConfigManager configSettings;
        private readonly TheMusicHtmlScraper htmlScraper;

        public TheMusicMenuRepository(IConfigManager configSettings)
        {
            this.configSettings = configSettings;

            // Attempt to fetch theMusic html
            WebClient client = new WebClient();
            var theMusicContent = client.DownloadString(configSettings.PublisherHomeUrl);
            htmlScraper = new TheMusicHtmlScraper(theMusicContent, configSettings.PublisherHomeUrl);
        }

        public IDictionary<string, string> GetMenuItemLinkNamePairs()
        {
            return HttpCacher.FetchOrCreate("TheMusicMenuCache", () =>
            {
                // Parse the items and add home link in case nothing comes back
                var parsedItems = htmlScraper.ParseMenuItems().AddOrUpdate("Home", configSettings.PublisherHomeUrl);

                // Remove the classies link
                if (parsedItems.ContainsKey("Classies"))
                    parsedItems.Remove("Classies");

                return parsedItems;
            });
        }

        public string GetFooterContent()
        {
            return HttpCacher.FetchOrCreate("TheMusicFooter", () => htmlScraper.ParseFooterHtml(), minutesToCache: 0, seconds: 5);
        }
    }
}