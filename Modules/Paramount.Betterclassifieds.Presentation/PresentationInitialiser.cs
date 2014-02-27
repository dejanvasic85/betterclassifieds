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


            // Routes
            routes.MapRoute(
                "default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", module = Name, id = UrlParameter.Optional }, new[] { GetType().Namespace });

            //routes.MapRoute(
            //   "adsByCategory",
            //   "nextgen/listings/{title}",
            //   new { controller = "Ad", action = "Category", module = Name, title = UrlParameter.Optional }, new[] { GetType().Namespace });
        }

        public override void RegisterTypes(IUnityContainer container)
        {
            // container.RegisterType<LegacyIntegration.OnlineSearchParameter>(new SessionLifetimeManager<LegacyIntegration.OnlineSearchParameter>());
        }
    }
}