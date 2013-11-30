using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace BetterClassified.Models
{
    public class TheMusicHtmlScraper
    {
        private readonly string theMusicHtml;
        private readonly string homeUrl;

        public TheMusicHtmlScraper(string theMusicHtml, string homeUrl)
        {
            this.theMusicHtml = theMusicHtml;
            this.homeUrl = homeUrl;
        }

        public Dictionary<string, string> ParseMenuItems()
        {
            // Return empty dictionary if no html is available
            if (theMusicHtml.IsNullOrEmpty())
                return new Dictionary<string, string>();

            // Use html agility pack to parse the document
            var document = new HtmlDocument();
            document.LoadHtml(theMusicHtml);
            
            var listNode = document.DocumentNode.SelectSingleNode("//nav//ul");
            if (listNode == null)
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
    }
}