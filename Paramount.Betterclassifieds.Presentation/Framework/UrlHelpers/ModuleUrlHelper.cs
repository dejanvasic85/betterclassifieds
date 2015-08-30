using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Paramount
{
    public static class ModuleUrlHelper
    {
        /// <summary>
        /// Generates outgoing URL for relative server path by extracting and using module Name from RouteData
        /// </summary>
        public static string ClientUrl(this UrlHelper urlOutHelper, string relativePath)
        {
            var routeData = urlOutHelper.RequestContext.RouteData;

            object moduleName;
            routeData.Values.TryGetValue("module", out moduleName);

            // Convention - Module URL's are a little different so ensure to generate outgoing URL based on module
            if (moduleName != null)
            {
                return relativePath.Replace("~/", string.Format("~/{0}/", moduleName));
            }

            return relativePath;
        }

        /// <summary>
        /// Generates outgoing URL for an Ad and particularly useful for the legacy integration piece
        /// </summary>
        public static string AdUrl(this UrlHelper urlHelper, string titleSlug, int id, bool includeSchemeAndProtocol = false, string routeName = "adRoute")
        {
            RouteValueDictionary dictionary = new RouteValueDictionary
            {
                {"title", titleSlug},
                {"id", id}
            };

            VirtualPathData data = RouteTable.Routes.GetVirtualPath(null, routeName, dictionary);
            var path = data.VirtualPath;

            if (!includeSchemeAndProtocol)
                return path;

            var contextUri = urlHelper.RequestContext.HttpContext.Request.Url;
            var baseUri = string.Format("{0}://{1}{2}", contextUri.Scheme,
              contextUri.Host, contextUri.Port == 80 ? string.Empty : ":" + contextUri.Port);

            return string.Format("{0}{1}", baseUri, path);
        }

        /// <summary>
        /// Generates a nice SEO URL for a particular listings
        /// </summary>
        public static string SeoUrl(this UrlHelper urlHelper, string seoName)
        {
            return urlHelper.Action("SeoAds", "Listings", new { seoName });
        }

        public static string ToEncodedUrl(this string url)
        {
            return HttpUtility.HtmlEncode(url);
        }
    }
}
