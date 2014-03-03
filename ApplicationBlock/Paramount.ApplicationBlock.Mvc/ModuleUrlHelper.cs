using System.Web.Mvc;
using System.Web.Routing;

namespace Paramount.ApplicationBlock.Mvc
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
        public static string AdUrl(this UrlHelper urlHelper, string title, int id)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary
            {
                {"title", title},
                {"id", id}
            };

            VirtualPathData data = RouteTable.Routes.GetVirtualPath(null, "adRoute", dictionary);
            var path = data.VirtualPath;

            return path;
        }

        /// <summary>
        /// Generates outgoing URL for an Ad and particularly useful for the legacy integration piece
        /// </summary>
        public static string ListingsUrl(this UrlHelper urlHelper, string seoName)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary
            {
                {"seoName", seoName}
            };

            VirtualPathData data = RouteTable.Routes.GetVirtualPath(null, "seoName", dictionary);
            var path = data.VirtualPath;

            return path;
        }
    }
}
