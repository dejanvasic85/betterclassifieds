using System.Collections.Generic;
using System.Net;
using Paramount;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Repository;

namespace BetterClassified.Repository
{
    public class TheMusicMenuRepository : IMenuRepository
    {
        private readonly IClientConfig _clientConfigSettings;
        private readonly TheMusicHtmlScraper htmlScraper;

        public TheMusicMenuRepository(IClientConfig _clientConfigSettings)
        {
            this._clientConfigSettings = _clientConfigSettings;

            // Attempt to fetch theMusic html
            WebClient client = new WebClient();
            var theMusicContent = client.DownloadString(_clientConfigSettings.PublisherHomeUrl);
            htmlScraper = new TheMusicHtmlScraper(theMusicContent, _clientConfigSettings.PublisherHomeUrl);
        }

        public IDictionary<string, string> GetMenuItemLinkNamePairs()
        {
            return HttpCacher.FetchOrCreate("TheMusicMenuCache", () =>
            {
                // Parse the items and add home link in case nothing comes back
                var parsedItems = htmlScraper.ParseMenuItems().AddOrUpdate("Home", _clientConfigSettings.PublisherHomeUrl);

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

    public class OfflineMenuRepository : IMenuRepository
    {
        public IDictionary<string, string> GetMenuItemLinkNamePairs()
        {
            return new Dictionary<string, string>
            {
                {"Home", "http://localhost/iFlog"}
            };
        }

        public string GetFooterContent()
        {
            return string.Empty;
        }
    }
}