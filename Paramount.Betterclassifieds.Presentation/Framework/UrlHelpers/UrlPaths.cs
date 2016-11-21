using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Paramount.Betterclassifieds.Business;

namespace Paramount
{
    public static class UrlPaths
    {
        public static string ContentAbsolute(this UrlHelper urlhelper, string relativeContentPath)
        {
            var contextUri = urlhelper.RequestContext.HttpContext.Request.Url;

            var baseUri =
                $"{contextUri.Scheme}://{contextUri.Host}{(contextUri.Port == 80 ? string.Empty : ":" + contextUri.Port)}";

            return $"{baseUri}{VirtualPathUtility.ToAbsolute(relativeContentPath)}";
        }

        public static string ContentLogo(this UrlHelper urlHelper)
        {
            var brand = DependencyResolver.Current.GetService<IApplicationConfig>().Brand;
            return urlHelper.Content($"~/Content/{brand}/img/logo.png");
        }

        public static string ContentForBrand(this UrlHelper urlHelper, string contentUrl)
        {
            var brand = DependencyResolver.Current.GetService<IApplicationConfig>().Brand;
            return urlHelper.Content($"~/Content/{brand}{contentUrl}");
        }

        public static UrlBuilder AdUrl(this UrlHelper urlHelper, string titleSlug, int id, string categoryAdType = "")
        {
            var routeName = "adRoute";
            if (categoryAdType.HasValue())
                routeName = categoryAdType;

            var builder = new UrlBuilder(urlHelper)
                .WithRouteName(routeName)
                .WithRouteValues(new
                {
                    title = titleSlug,
                    id = id
                });

            return builder;
        }


        public static UrlBuilder ActionAbsolute(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues = null)
        {
            return new UrlBuilder(urlHelper, actionName, controllerName, routeValues).WithFullUrl();
        }

        public static UrlBuilder Home(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "index", "home");
        }

        public static UrlBuilder NotFound(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper).WithAction("notFound", "error");
        }

        public static UrlBuilder ContactUs(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "ContactUs", "Home");
        }

        public static UrlBuilder HowItWorks(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "how-it-works", "Help");
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

        public static UrlBuilder ImageOriginal(this UrlHelper urlhelper, string documentId)
        {
            return new UrlBuilder(urlhelper, "Render", "Image", new { documentId });
        }

        public static UrlBuilder Booking(this UrlHelper urlHelper, int bookingStep = 1, string adType = "")
        {
            return new UrlBuilder(urlHelper, "Step" + bookingStep, "Booking", new { adType });
        }

        public static UrlBuilder Booking(this UrlHelper urlHelper, string seoName)
        {
            return new UrlBuilder(urlHelper, "SeoAds", "Listings", new { seoName });
        }

        public static UrlBuilder Search(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "Find", "Listings");
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
            return new UrlBuilder(urlHelper, "eventDashboard", "editAd", new { id = adId });
        }

        public static UrlBuilder EditEventDetails(this UrlHelper urlHelper, int adId)
        {
            return new UrlBuilder(urlHelper, "eventDetails", "editAd")
                .WithRouteValues(new { id = adId });
        }

        public static UrlBuilder EventTicketManagement(this UrlHelper urlHelper, int adId, int eventId)
        {
            return new UrlBuilder(urlHelper)
                .WithAction("event-ticketing", "editad")
                .WithRouteValues(new { id = adId, eventId });
        }

        public static UrlBuilder UpdateEventDetails(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "UpdateEventDetails", "EditAd");
        }

        public static UrlBuilder ValidateBarcode(this UrlHelper urlHelper, string barcode)
        {
            return new UrlBuilder(urlHelper)
                .WithRouteName("EventBarcode")
                .WithRouteValues(new { barcode });
        }

        public static UrlBuilder EventTicketingMakePayment(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper).WithAction("MakePayment", "Event");
        }

        public static UrlBuilder EventBooked(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper).WithAction("EventBooked", "Event");
        }

        public static UrlBuilder EventPaymentAuthorisePayPal(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper).WithAction("AuthorisePayPal", "Event");
        }

        public static UrlBuilder EventBookTickets(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper).WithAction("BookTickets", "Event");
        }

        public static UrlBuilder EventPayWithStripe(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper).WithAction("PayWithCreditCard", "Event");
        }

        public static UrlBuilder Terms(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "Terms", "Home");
        }

        public static UrlBuilder EventPricing(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper).WithAction("event-pricing", "help");
        }

        public static UrlBuilder BarcodeValidationHelp(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper).WithAction("barcode-validation", "help");
        }

        public static UrlBuilder DashboardHelp(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper).WithAction("realtime-data", "help");
        }

        public static UrlBuilder AddEventGuest(this UrlHelper urlHelper, int id, int eventId)
        {
            return new UrlBuilder(urlHelper).WithAction("add-guest", "editad").WithRouteValues(new { id, eventId });
        }

        public static UrlBuilder DownloadGuestListPdf(this UrlHelper urlHelper, int id, int eventId)
        {
            return new UrlBuilder(urlHelper).WithAction("DownloadGuestListPdf", "EditAd").WithRouteValues(new { id, eventId });
        }

        public static UrlBuilder DownloadGuestListCsv(this UrlHelper urlHelper, int id, int eventId)
        {
            return new UrlBuilder(urlHelper).WithAction("DownloadGuestListCsv", "EditAd").WithRouteValues(new { id, eventId });
        }

        public static UrlBuilder DownloadGuestListExcel(this UrlHelper urlHelper, int id, int eventId)
        {
            return new UrlBuilder(urlHelper).WithAction("DownloadGuestListExcel", "EditAd").WithRouteValues(new { id, eventId });
        }

        public static UrlBuilder ManageGroups(this UrlHelper urlHelper, int id, int eventId)
        {
            return new UrlBuilder(urlHelper).WithAction("manage-groups", "editad").WithRouteValues(new { id, eventId });
        }

        public static UrlBuilder CategorySeoView(this UrlHelper urlHelper, string seoName)
        {
            return new UrlBuilder(urlHelper).WithAction("SeoAds", "Listings").WithRouteValues(new { seoName });
        }

        public static UrlBuilder AdEnquiry(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper, "adEnquiry", "listings");
        }

        public static UrlBuilder RemoveGuestComplete(this UrlHelper urlHelper, int id)
        {
            return new UrlBuilder(urlHelper)
                .WithAction("remove-guest-complete", "EditAd")
                .WithRouteValues(new { id });
        }

        public static UrlBuilder ConfirmEventOrganiser(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper).WithAction("confirm-event-organiser", "account");
        }

        public static UrlBuilder EventBookingSessionExpired(this UrlHelper urlHelper)
        {
            return new UrlBuilder(urlHelper).WithAction("session-expired", "event");
        }
    }
}
