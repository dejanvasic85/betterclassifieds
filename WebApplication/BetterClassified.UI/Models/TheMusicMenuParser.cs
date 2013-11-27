using System;
using System.Collections.Generic;
using BetterClassified.Repository;

namespace BetterClassified.Models
{
    public class TheMusicMenuParser
    {
        private readonly IConfigSettings config;

        public TheMusicMenuParser(string html, Repository.IConfigSettings config)
        {
            this.config = config;

            if (html.IsNullOrEmpty())
                throw new ArgumentException("html is required to parse the menu");

            this.Html = html;
        }

        public string Html { get; private set; }

        public Dictionary<string, string> GetMenuItemPairs()
        {
            throw new NotImplementedException();
        }
    }
}