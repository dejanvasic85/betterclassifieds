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

        public static UrlBuilder ActionAbsolute(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues = null)
        {
            return new UrlBuilder(urlHelper, actionName, controllerName, routeValues).WithFullUrl();
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
            if (documentId.IsNullOrEmpty() || documentId.Equals("placeholder"))
            {
                return new UrlBuilder(urlHelper).WithContent("~/Content/images/placeholder.png");
            }
            return new UrlBuilder(urlHelper, "Render", "Image", new { documentId, height, width });
        }
        
        public static UrlBuilder Booking(this UrlHelper urlHelper, int bookingStep, string adType = "")
        {
            return new UrlBuilder(urlHelper, "Step" + bookingStep, "Booking", new { adType });
        }

        public static UrlBuilder ImageUpload(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "UploadCropImage", "Image");
        }

        public static UrlBuilder CropImage(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "CropImage", "Image");
        }

        public static UrlBuilder CancelCrop(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "CancelCrop", "Image");
        }

        public static UrlBuilder BookingInvoice(this UrlHelper urlHelper, int? bookingId = null)
        {
            return new UrlBuilder(urlHelper, "Booking", "Invoice", new { bookingId });
        }

        public static UrlBuilder ConfirmRegistration(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "Confirmation", "Account");
        }

        public static UrlBuilder EventPaymentRequest(this UrlHelper urlHelper, int adId, int eventId)
        {
            return new UrlBuilder(urlHelper, "EventPaymentRequest", "EditAd", new { id = adId, eventId });
        }

        public static UrlBuilder EventDashboard(this UrlHelper urlHelper, int adId)
        {
            return new UrlBuilder(urlHelper, "EventDashboard", "EditAd", new { id = adId });
        }

        public static UrlBuilder UpdateEventDetails(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "EditAd", "UpdateEventDetails");
        }

    }
}
