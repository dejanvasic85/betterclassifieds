using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Paramount;

namespace Paramount.ApplicationBlock.Mvc
{
    public static class ModuleUrl
    {
        public static string ClientUrl(this UrlHelper urlHelper, string relativePath)
        {
            var routeData = urlHelper.RequestContext.RouteData;

            object moduleName;
            routeData.Values.TryGetValue("module", out moduleName);

            if (moduleName != null)
            {
                return relativePath.Replace("~/", string.Format("~/{0}/", moduleName));
            }
            
            return relativePath;
        }
    }
}
