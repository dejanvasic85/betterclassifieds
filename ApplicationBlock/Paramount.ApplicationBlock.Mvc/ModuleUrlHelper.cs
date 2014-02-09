using System.Web.Mvc;

namespace Paramount.ApplicationBlock.Mvc
{
    public static class ModuleUrlHelper
    {
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
    }
}
