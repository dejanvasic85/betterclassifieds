using System;
using System.Web;
using System.Web.Mvc;

namespace Paramount
{
    public static class UrlExtensions
    {
        public static string ContentAbsolute(this UrlHelper urlhelper, string relativeContentPath)
        {
            Uri contextUri = HttpContext.Current.Request.Url;

            var baseUri = string.Format("{0}://{1}{2}", contextUri.Scheme,
               contextUri.Host, contextUri.Port == 80 ? string.Empty : ":" + contextUri.Port);

            return string.Format("{0}{1}", baseUri, VirtualPathUtility.ToAbsolute(relativeContentPath));
        }

        public static string ActionAbsolute(this UrlHelper urlHelper, string actionName, string controllerName,
                                            object routeValues = null)
        {
            string scheme = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;

            return urlHelper.Action(actionName, controllerName, routeValues, scheme);
        }
    }
}
