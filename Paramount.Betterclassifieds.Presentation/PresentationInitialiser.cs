using Microsoft.Practices.Unity;
using Paramount.ApplicationBlock.Mvc;
using Paramount.ApplicationBlock.Mvc.ModelBinders;
using Paramount.ApplicationBlock.Mvc.Unity;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.DataService;
using Paramount.Betterclassifieds.Presentation.ViewModels;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Paramount.Betterclassifieds.Presentation
{
    public class PresentationInitialiser : ModuleRegistration
    {
        public override string Name
        {
            get { return "Presentation"; }
        }

        public override void CreateContextAndRegister(RouteCollection routes, object state)
        {
            RegisterRoutes(routes);

            RegisterModelBinders();

            DocumentDataConfig.RegisterMappings();
        }

        private void RegisterModelBinders()
        {
            ModelBinders.Binders.Add(typeof(DateTime?), new ModelSpecificDateModelBinder());
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute), typeof(RequiredAttributeAdapter));
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            // Ignored routes
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.Ignore("{resource}.aspx/{*pathInfo}");
            routes.Ignore("{resources}.ashx/{*pathInfo}");
            routes.Ignore("Booking/Step1.aspx");
            routes.Ignore("Image/View.ashx");

            // Api
            routes.MapHttpRoute(name: "API Default", routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            // Images
            routes.MapRoute("imageRoute", "img/{documentId}/{width}/{height}",
                new { controller = "Image", action = "Render", width = UrlParameter.Optional, height = UrlParameter.Optional });


            // Ad route
            routes.MapRoute("adRoute", "Ad/{title}/{id}", new { controller = "Listings", action = "ViewAd", module = Name },
                new[] { GetType().Namespace });

            // Seo route
            routes.MapRoute("seoName", "{seoName}/listings", new { controller = "Listings", action = "SeoAds", module = Name },
                new[] { GetType().Namespace });

            // Booking step routes
            routes.MapRoute("bookingRoute1", "Booking/Step/1", new { controller = "Booking", action = "Step1", module = Name },
                new[] { GetType().Namespace });
            routes.MapRoute("bookingRoute2", "Booking/Step/2", new { controller = "Booking", action = "Step2", module = Name },
                new[] { GetType().Namespace });
            routes.MapRoute("bookingRoute3", "Booking/Step/3", new { controller = "Booking", action = "Step3", module = Name },
                new[] { GetType().Namespace });

            // Default
            routes.MapRoute("defaultRoute", "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", module = Name, id = UrlParameter.Optional },
                new[] { GetType().Namespace });
        }

        public override void RegisterTypes(IUnityContainer container)
        {
            // Use the filter provider that uses unity container so it can resolve any dependencies
            var oldProvider = FilterProviders.Providers.Single(f => f is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(oldProvider);

            var provider = new UnityFilterAttributeFilterProvider(container);
            FilterProviders.Providers.Add(provider);

            // Searching throughout the website allows to save in to the session ( at the moment )
            container.RegisterType<SearchFilters>(new SessionLifetimeManager<SearchFilters>());
            container.RegisterType<IBookingContext, BookingContextInCookie>();
        }
    }

}