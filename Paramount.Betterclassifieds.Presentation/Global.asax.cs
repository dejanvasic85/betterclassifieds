using System;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.Presentation
{
    using DataService;
    using ApplicationBlock.Mvc;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Optimization;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        // This global.asax will only be hit if the application is not a module

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DocumentDataConfig.RegisterMappings();
            var container = UnityConfig.Initialise(); 
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, container);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBindingConfig.Register(ModelBinders.Binders);
            BundleConfig.Register(BundleTable.Bundles);
            BundleConfig.RegisterStyles(BundleTable.Bundles);

            // Do this on another thread
            Task.Factory.StartNew(DataServiceInitialiser.InitializeContexts);
            
            // View engine ( for branding )
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ClientViewEngine());
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            Session["init"] = 0;
        }
    }
}