namespace Paramount.Betterclassifieds.Presentation
{
    using System.Web.Mvc;
    using System.Linq;
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
        }
    }
}