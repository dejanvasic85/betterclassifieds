﻿using System.Collections.Generic;
using System.IO;
using BetterClassified.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Paramount.Betterclassifieds.Tests.BusinessModel
{
    [TestClass]
    public class TheMusicHtmlScraperTests
    {
        public const string ClassiesMenuText = "Classies";

        // This is the item we always expect to be in the list! Otherwise some has been naughty and changed theMusic website menu
        [TestMethod]
        [DeploymentItem("Artefacts\\TheMusicValidHtml.html")]
        public void ParseMenuItems_ValidHtml_ReturnsDictionary()
        {
            string html = File.ReadAllText("TheMusicValidHtml.html");
            const string publisherUrl = "http://themusic.com.au/";

            // Create the parser and inject!
            var theMusicMenu = new TheMusicHtmlScraper(html, publisherUrl);

            Dictionary<string, string> result = theMusicMenu.ParseMenuItems();

            result.IsNotNull();
            result.Count.IsEqualTo(13);
            result.AreAllTrue(kv => kv.Value.StartsWith("http"));
        }

        [TestMethod]
        [DeploymentItem("Artefacts\\TheMusicBadHtml.html")]
        public void ParseMenuItems_BadHtml_ReturnsEmptyDictionary()
        {
            string html = File.ReadAllText("TheMusicBadHtml.html");
            const string publisherUrl = "http://themusic.com.au/";

            // Create the parser and inject!
            var theMusicMenu = new TheMusicHtmlScraper(html, publisherUrl);

            Dictionary<string, string> result = theMusicMenu.ParseMenuItems();

            result.IsNotNull();
            result.Count.IsEqualTo(0);
        }

        [TestMethod]
        public void ParseMenuItems_EmptyHtml_ReturnsEmptyDictionary()
        {
            // Create the parser and inject!
            var theMusicMenu = new TheMusicHtmlScraper(string.Empty, string.Empty);

            Dictionary<string, string> result = theMusicMenu.ParseMenuItems();

            result.IsNotNull();
            result.Count.IsEqualTo(0);
        }

        [TestMethod]
        [DeploymentItem("Artefacts\\TheMusicValidHtml.html")]
        public void ParseFooterHtml_ValidHtml_ReturnsString()
        {
            string html = File.ReadAllText("TheMusicValidHtml.html");
            const string publisherUrl = "http://themusic.com.au/";

            // Create the parser and inject!
            var theMusicMenu = new TheMusicHtmlScraper(html, publisherUrl);

            var result = theMusicMenu.ParseFooterHtml();

            result.IsNotNull();
            result.Contains("<section class=\"submenu\">").IsTrue();
            result.Contains("href=\"http://").IsTrue();
            result.Contains("href=\"/").IsFalse(); // make sure no links have href="/meta", they should all be href="http://" (outgoing)
        }

        [TestMethod]
        [DeploymentItem("Artefacts\\TheMusicBadHtml.html")]
        public void ParseFooterHtml_BadHtml_ReturnsEmptyString()
        {
            string html = File.ReadAllText("TheMusicBadHtml.html");
            const string publisherUrl = "http://themusic.com.au/";

            // Create the parser and inject!
            var theMusicMenu = new TheMusicHtmlScraper(html, publisherUrl);

            var result = theMusicMenu.ParseFooterHtml();

            string.IsNullOrEmpty(result).IsTrue();
        }
    }
}