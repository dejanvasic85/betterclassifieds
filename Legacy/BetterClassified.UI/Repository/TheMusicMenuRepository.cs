namespace BetterClassified.Repository
{
    using System.Collections.Generic;
    using System.Net;
    using Paramount;
    using Paramount.Betterclassifieds.Business;
    using Paramount.Betterclassifieds.Business.Repository;
    
    public class TheMusicMenuRepository : IMenuRepository
    {
        private readonly IClientConfig _clientConfigSettings;

        public TheMusicMenuRepository(IClientConfig _clientConfigSettings)
        {
            this._clientConfigSettings = _clientConfigSettings;
        }

        public IDictionary<string, string> GetMenuItemLinkNamePairs()
        {
            return HttpCacher.FetchOrCreate("TheMusicMenuCache", () =>
            {
                var htmlScraper = GetHtmlScraper();

                // Parse the items and add home link in case nothing comes back
                var parsedItems = htmlScraper.ParseMenuItems().AddOrUpdate("Home", _clientConfigSettings.PublisherHomeUrl);

                // Remove the classies link
                if (parsedItems.ContainsKey("Classies"))
                    parsedItems.Remove("Classies");

                return parsedItems;
            }, minutesToCache: 1440);
        }

        public string GetFooterContent()
        {
            return HttpCacher.FetchOrCreate("TheMusicFooter", () => GetHtmlScraper().ParseFooterHtml(), 1440);
        }

        private TheMusicHtmlScraper GetHtmlScraper()
        {
            return new TheMusicHtmlScraper(GetTheMusicContent(), _clientConfigSettings.PublisherHomeUrl);
        }

        private string _theMusicContent;
        private string GetTheMusicContent()
        {
            if (_theMusicContent.HasValue())
                return _theMusicContent;

            WebClient client = new WebClient();
            _theMusicContent = client.DownloadString(_clientConfigSettings.PublisherHomeUrl);
            return _theMusicContent;
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