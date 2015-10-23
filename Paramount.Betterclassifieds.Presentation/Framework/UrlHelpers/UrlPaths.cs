using System;
using System.Web;
using System.Web.Mvc;

namespace Paramount
{
    public static class UrlPaths
    {
        public static string ContentAbsolute(this UrlHelper urlhelper, string relativeContentPath)
        {
            Uri contextUri = urlhelper.RequestContext.HttpContext.Request.Url;

            var baseUri = string.Format("{0}://{1}{2}", contextUri.Scheme,
               contextUri.Host, contextUri.Port == 80 ? string.Empty : ":" + contextUri.Port);

            return string.Format("{0}{1}", baseUri, VirtualPathUtility.ToAbsolute(relativeContentPath));
        }

        public static string ActionAbsolute(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues = null)
        {
            return new UrlBuilder(urlHelper, actionName, controllerName, routeValues).WithFullUrl().Build();
        }

        public static UrlBuilder Home(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "Index", "Home");
        }

        public static UrlBuilder LocationOptions(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "GetLocationOptions", "Location");
        }

        public static UrlBuilder LocationAreaOptions(this UrlHelper urlHelper, int? locationId)
        {
            return new UrlBuilder(urlHelper, "GetLocationAreas", "Location", new { locationId });
        }

        public static UrlBuilder LocationAreaOptions(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "GetLocationAreas", "Location");
        }

        public static UrlBuilder Image(this UrlHelper urlHelper, string documentId, int height = 100, int width = 100)
        {
            return new UrlBuilder(urlHelper, "Render", "Image", new { documentId, height, width });
        }

        public static UrlBuilder ImageUpload(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "UploadCropImage", "Image");
        }

        public static UrlBuilder SetLineAdImageId(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "SetLineAdImage", "Booking");
        }

        public static UrlBuilder RemoveLineAdImage(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "RemoveLineAdImage", "Booking");
        }

        public static UrlBuilder RemoveOnlineImage(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "RemoveOnlineImage", "Booking");
        }

        public static UrlBuilder CropImage(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "CropImage", "Image");
        }

        public static UrlBuilder CancelCrop(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "CancelCrop", "Image");
        }

        public static UrlBuilder RenderCropImage(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "RenderCropImage", "Image");
        }

        public static UrlBuilder UploadOnlineImage(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "UploadOnlineImage", "Image");
        }
        
        public static UrlBuilder UpdateBookingRates(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "GetRate", "Booking");
        }

        public static UrlBuilder PreviewBookingEditions(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "PreviewEditions", "Booking");
        }

        public static UrlBuilder BookingInvoice(this UrlHelper urlHelper, int? bookingId = null)
        {
            return new UrlBuilder(urlHelper, "Booking", "Invoice", new { bookingId });
        }

        public static UrlBuilder ConfirmRegistration(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "Confirmation", "Account");
        }
        
    }
}
