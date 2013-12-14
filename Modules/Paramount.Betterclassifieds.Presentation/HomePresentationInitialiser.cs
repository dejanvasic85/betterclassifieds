using System.Web.Mvc;
using Paramount.ApplicationBlock.Mvc;

namespace Paramount.Betterclassifieds.Presentation
{
    public class HomePresentationInitialiser : ModuleRegistration
    {
        public override string Name
        {
            get { return "HomePresentation"; }
        }

        public override void CreateContextAndRegister(System.Web.Routing.RouteCollection routes, object state)
        {
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            // BundleConfig.RegisterBundles(BundleTable.Bundles);

            routes.MapRoute(
                "default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { GetType().Namespace });

        }
    }
}