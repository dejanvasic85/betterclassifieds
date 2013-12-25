using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Paramount;

namespace BetterClassified.Models
{
    public class TheMusicHtmlScraper
    {
        private readonly string theMusicHtml;
        private readonly string homeUrl;
        private readonly HtmlDocument document;

        public TheMusicHtmlScraper(string theMusicHtml, string homeUrl)
        {
            this.theMusicHtml = theMusicHtml;
            this.homeUrl = homeUrl;

            // Ensure that the links are aligned
            if (!this.homeUrl.EndsWith("/"))
                this.homeUrl.Append("/");

            document = new HtmlDocument();
            document.LoadHtml(theMusicHtml);
        }

        public Dictionary<string, string> ParseMenuItems()
        {
            // Return empty dictionary if no html is available
            var listNode = document.DocumentNode.SelectSingleNode("//nav//ul");

            if (theMusicHtml.IsNullOrEmpty() || listNode == null)
                return new Dictionary<string, string>();

            var navigationListItems = listNode.ChildNodes.Where(n => n.NodeType == HtmlNodeType.Element);

            var dictionary = navigationListItems.ToDictionary(
                item => GetNextLink(item.FirstChild).InnerText, // Link Name
                item => // Href ( but ensure to fix it )
                {
                    var href = GetNextLink(item.FirstChild).GetAttributeValue("href", "");

                    // Ensure that all outgoing links contain complete URL's back to home 
                    if (!href.StartsWith("http"))
                    {
                        // Assume that those links are 
                        return href.TrimStart('/').Prefix(homeUrl);
                    }

                    return href;
                });
            return dictionary;
        }

        private HtmlNode GetNextLink(HtmlNode node)
        {
            if (node.Name != "a")
                return GetNextLink(node.NextSibling);
            return node;
        }

        public string ParseFooterHtml()
        {
            var footerNode = document.DocumentNode.SelectSingleNode("//footer");

            if (footerNode == null)
                return string.Empty;

            var links = footerNode.Descendants().Where(a => a.Name == "a");
            
            foreach (var link in links)
            {
                var href = link.Attributes["href"];

                if (href != null && !href.Value.StartsWith("http"))
                {
                    href.Value = href.Value.TrimStart('/').Prefix(homeUrl);
                }
            }

            // Sanitise the footer links to contain the full link back to the music
            return footerNode.InnerHtml;
        }
    }
}