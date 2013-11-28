using System;
using System.Collections.Generic;
using System.Linq;
using BetterClassified.Repository;
using HtmlAgilityPack;

namespace BetterClassified.Models
{
    public class TheMusicHtmlScraper
    {
        private readonly IConfigSettings config;

        public TheMusicHtmlScraper(string html, IConfigSettings config)
        {
            this.config = config;

            if (html.IsNullOrEmpty())
                throw new ArgumentException("html is required to parse the menu");

            this.Html = html;
        }

        public string Html { get; private set; }

        public Dictionary<string, string> GetMenuItemPairs()
        {
            // Use html agility pack to parse the document
            var document = new HtmlDocument();
            document.LoadHtml(Html);

            // Current structure is that top navigation comes under NAV tag
            var navigationListItems = document.DocumentNode
                .SelectSingleNode("//nav//ul")
                .ChildNodes
                .Where(n => n.NodeType == HtmlNodeType.Element);

            var dictionary = navigationListItems.ToDictionary(
                item => GetNextLink(item.FirstChild).InnerText, // Link Name
                item => // Href ( but ensure to fix it )
                {
                    var href = GetNextLink(item.FirstChild).GetAttributeValue("href", "");

                    // Ensure that all outgoing links contain complete URL's back to home 
                    if (!href.StartsWith("http"))
                    {
                        // Assume that those links are 
                        return href.TrimStart('/').Prefix(config.PublisherHomeUrl);
                    }

                    return href;
                });

            // Ensure that the dictionary contains "Classies"
            if (!dictionary.ContainsKey("Classies"))
            {
                dictionary.Add("Classies", config.BaseUrl);
            }

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