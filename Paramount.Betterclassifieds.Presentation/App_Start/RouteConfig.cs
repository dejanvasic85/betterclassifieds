﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Paramount.Betterclassifieds.Presentation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes, bool includeAttributeRoutes)
        {
            // Ignored routes
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.Ignore("{resource}.aspx/{*pathInfo}");
            routes.Ignore("{resources}.ashx/{*pathInfo}");

            if (includeAttributeRoutes)
                routes.MapMvcAttributeRoutes();

            // Images   
            routes.MapRoute("imageRoute", "img/{documentId}/{width}/{height}", new { controller = "Image", action = "Render", width = UrlParameter.Optional, height = UrlParameter.Optional });

            // Ad route
            routes.MapRoute("adRoute", "Ad/{title}/{id}", new { controller = "Listings", action = "ViewAd" });
            routes.MapRoute("EventBarcode", "Event/Barcode/{barcode}", new { controller = "Event", action = "ValidateBarcode" }); // Need to be Pascal to suit the database record!
            routes.MapRoute("EventInvite", "invite/{*token}", new { controller = "Event", action = "Invite" });
            routes.MapRoute("Event", "Event/{title}/{id}", new { controller = "Event", action = "ViewEventAd" }); // Need to be Pascal to suit the database record!

            // Seo route
            routes.MapRoute("seoName", "{seoName}/listings", new { controller = "Listings", action = "SeoAds" });

            // Booking step routes
            routes.MapRoute("bookingRoute1", "Booking/Step/1", new { controller = "Booking", action = "Step1" });
            routes.MapRoute("bookingRoute2", "Booking/Step/2/{adType}", new { controller = "Booking", action = "Step2", adType = UrlParameter.Optional });
            routes.MapRoute("bookingRoute3", "Booking/Step/3", new { controller = "Booking", action = "Step3" });

            // Default
            routes.MapRoute("defaultRoute", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}