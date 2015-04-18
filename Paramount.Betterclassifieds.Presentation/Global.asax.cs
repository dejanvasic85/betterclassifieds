using System.Web.Optimization;
using Paramount.Betterclassifieds.Presentation.App_Start;

namespace Paramount.Betterclassifieds.Presentation
{
    using DataService;
    using ApplicationBlock.Mvc;
    using System.Web.Mvc;
    using System.Web.Routing;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        // This global.asax will only be hit if the application is not a module

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            DocumentDataConfig.RegisterMappings();
            var container = UnityConfig.Initialise(); 
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, container);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBindingConfig.Register(ModelBinders.Binders);
            BundleConfig.Register(BundleTable.Bundles);

            // View engine ( for branding )
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ClientViewEngine());
        }
    }
}