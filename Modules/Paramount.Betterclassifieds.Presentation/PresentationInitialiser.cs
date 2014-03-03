﻿using System.Web.Http;
using System.Web.Routing;

namespace Paramount.Betterclassifieds.Presentation
{
    using System.Web.Mvc;
    using ApplicationBlock.Mvc;
    using Microsoft.Practices.Unity;

    public class PresentationInitialiser : ModuleRegistration
    {
        public override string Name
        {
            get { return "Presentation"; }
        }

        public override void CreateContextAndRegister(System.Web.Routing.RouteCollection routes, object state)
        {
            // Ignored routes
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.Ignore("{resource}.aspx/{*pathInfo}");
            routes.Ignore("{resources}.ashx/{*pathInfo}");
            routes.Ignore("Image/View.ashx");

            // Api
            routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            // Images
            routes.MapRoute(
                "imageRoute",
                "Image/{documentId}/{width}/{height}",
                new { controller = "Image", action = "Render", width = UrlParameter.Optional, height = UrlParameter.Optional });

            // Ad (temporary)
            routes.MapPageRoute("adRoute", "Ad/{title}/{id}", "~/OnlineAds/AdView.aspx", checkPhysicalUrlAccess: false);

            // Listing search (seo friendly)
            routes.MapPageRoute("seoName", "listings/{seoName}", "~/OnlineAds/Default.aspx");

            // Default
            routes.MapRoute(
                "defaultRoute",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", module = Name, id = UrlParameter.Optional},
                new[] { GetType().Namespace });
        }

        public override void RegisterTypes(IUnityContainer container)
        {
        }
    }
}