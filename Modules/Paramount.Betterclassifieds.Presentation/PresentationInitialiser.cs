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
                "nextgen/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { GetType().Namespace });

        }

        public override void RegisterTypes(IUnityContainer container)
        {
            //container.RegisterType<Business.Repository.IAdRepository, AdRepository>();
        }
    }
}