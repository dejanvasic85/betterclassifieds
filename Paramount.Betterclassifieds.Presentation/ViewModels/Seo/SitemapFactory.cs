using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Seo
{
    public class SitemapFactory
    {
        private readonly HttpContextBase _httpContext;
        private readonly ISearchService _searchService;

        public SitemapFactory(HttpContextBase httpContext, ISearchService searchService)
        {
            _httpContext = httpContext;
            _searchService = searchService;
        }


        public string Create()
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (var sitemapNode in GetNodes())
            {
                XElement urlElement = new XElement(
            xmlns + "url",
            new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url.ToLower())),
            sitemapNode.LastModified == null ? null : new XElement(
                xmlns + "lastmod",
                sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
            sitemapNode.Frequency == null ? null : new XElement(
                xmlns + "changefreq",
                sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
            sitemapNode.Priority == null ? null : new XElement(
                xmlns + "priority",
                sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);

                //var node = new XElement("sitemap");
                //root.Add(node);
                //var urlElement = new XElement(xmlns + "url", new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),

                //    sitemapNode.LastModified == null
                //        ? null
                //        : new XElement(xmlns + "lastmod", sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),

                //    sitemapNode.Frequency == null
                //        ? null
                //        : new XElement(xmlns + "changefreq", sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),

                //    sitemapNode.Priority == null
                //        ? null
                //        : new XElement(xmlns + "priority", sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }

        private IReadOnlyCollection<SitemapNode> GetNodes()
        {
            var urlHelper = new UrlHelper(_httpContext.Request.RequestContext);
            var currentAds = _searchService.GetLatestAds();

            var adSitemaps = currentAds.Select(a => new SitemapNode
            {
                Frequency = SitemapFrequency.Yearly,
                Url = urlHelper.AdUrl(a.HeadingSlug, a.AdId,
                    includeSchemeAndProtocol: true,
                    routeName: a.CategoryAdType)
            });

            return new List<SitemapNode>
            {
                new SitemapNode {Priority = 1, Url = urlHelper.Home().WithFullUrl()},
                new SitemapNode {Priority = 0.9, Url = urlHelper.HowItWorks().WithFullUrl()},
                new SitemapNode {Priority = 0.9, Url = urlHelper.ContactUs().WithFullUrl()},
                new SitemapNode {Priority = 0.9, Url = urlHelper.DashboardHelp().WithFullUrl()},
                new SitemapNode {Priority = 0.9, Url = urlHelper.BarcodeValidationHelp().WithFullUrl()},
            }
                .Union(adSitemaps)
                .ToList();
        }


    }
}