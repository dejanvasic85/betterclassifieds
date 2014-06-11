using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Routing;
using Paramount.Betterclassifieds.Presentation.ViewModels;

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
        public static string AdUrl(this UrlHelper urlHelper, string titleSlug, int id)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary
            {
                {"title", titleSlug},
                {"id", id}
            };

            VirtualPathData data = RouteTable.Routes.GetVirtualPath(null, "adRoute", dictionary);
            var path = data.VirtualPath;

            return path;
        }

        /// <summary>
        /// Generates a nice SEO URL for a particular listings
        /// </summary>
        public static string SeoUrl(this UrlHelper urlHelper, string seoName)
        {
            return urlHelper.Action("SeoAds", "Listings", new { seoName });
        }
    }
}
