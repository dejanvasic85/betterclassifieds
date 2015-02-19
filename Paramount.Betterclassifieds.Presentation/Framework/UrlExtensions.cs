using System;
using System.Web;
using System.Web.Mvc;

namespace Paramount
{
    public static class UrlExtensions
    {
        public static string Home(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Index", "Home");
        }

        public static string LocationOptions(this UrlHelper urlHelper)
        {
            return urlHelper.Action("GetLocationOptions", "Location");
        }

        public static string LocationAreaOptions(this UrlHelper urlHelper, int? locationId)
        {
            return urlHelper.Action("GetLocationAreas", "Location", new { locationId });
        }

        public static string Image(this UrlHelper urlHelper, string documentId, int height = 100, int width = 100)
        {
            return urlHelper.Action("Render", "Image", new { documentId, height, width });
        }

        public static string ImageUpload(this UrlHelper urlHelper)
        {
            return urlHelper.Action("UploadCropImage", "Image");
        }

        public static string ContentAbsolute(this UrlHelper urlhelper, string relativeContentPath)
        {
            Uri contextUri = HttpContext.Current.Request.Url;

            var baseUri = string.Format("{0}://{1}{2}", contextUri.Scheme,
               contextUri.Host, contextUri.Port == 80 ? string.Empty : ":" + contextUri.Port);

            return string.Format("{0}{1}", baseUri, VirtualPathUtility.ToAbsolute(relativeContentPath));
        }

        public static string ActionAbsolute(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues = null)
        {
            string scheme = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;

            return urlHelper.Action(actionName, controllerName, routeValues, scheme);
        }
    }
}
