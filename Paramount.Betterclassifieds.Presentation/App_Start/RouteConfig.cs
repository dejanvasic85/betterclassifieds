using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Paramount.Betterclassifieds.Presentation.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
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

            routes.MapRoute(
              "seoName",
              "{seoName}/listings",
              new { controller = "listings", action = "SeoAds" },
              new[] { "Temp.Module" });

            // Default
            routes.MapRoute(
                "defaultRoute",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Temp.Module" });
        }
    }
}