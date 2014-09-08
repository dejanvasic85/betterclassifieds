using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.ApplicationBlock.Mvc;
using Paramount.Betterclassifieds.Presentation.Framework;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using Paramount.Betterclassifieds.Presentation.ViewModels.Booking;

namespace Paramount.Betterclassifieds.Presentation
{
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
            routes.MapHttpRoute(name: "API Default", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });

            // Images
            routes.MapRoute("imageRoute", "Image/{documentId}/{width}/{height}", new { controller = "Image", action = "Render", width = UrlParameter.Optional, height = UrlParameter.Optional });

            // Ad route
            routes.MapRoute("adRoute", "Ad/{title}/{id}", new { controller = "Listings", action = "ViewAd", module = Name }, new[] { GetType().Namespace });

            // Seo route
            routes.MapRoute("seoName", "{seoName}/listings", new { controller = "Listings", action = "SeoAds", module = Name }, new[] { GetType().Namespace });

            // Booking step routes
            routes.MapRoute("bookingRoute1", "Booking/Step/1", new { controller = "Booking", action = "Step1", module = Name }, new[] { GetType().Namespace });
            routes.MapRoute("bookingRoute2", "Booking/Step/2", new { controller = "Booking", action = "Step2", module = Name }, new[] { GetType().Namespace });
            routes.MapRoute("bookingRoute3", "Booking/Step/3", new { controller = "Booking", action = "Step3", module = Name }, new[] { GetType().Namespace });
            routes.MapRoute("bookingRoute4", "Booking/Step/4", new { controller = "Booking", action = "Step4", module = Name }, new[] { GetType().Namespace });
            
            // Default
            routes.MapRoute("defaultRoute", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", module = Name, id = UrlParameter.Optional }, new[] { GetType().Namespace });
        }

        public override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<SearchFilters>(new SessionLifetimeManager<SearchFilters>());
            container.RegisterInstance(new BookingCartSessionManager());
        }
    }
}