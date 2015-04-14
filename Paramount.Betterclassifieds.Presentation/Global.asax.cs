namespace Paramount.Betterclassifieds.Presentation
{
    using ApplicationBlock.Mvc;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Globalization;
    using System.Threading;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        // This global.asax will only be hit if the application is not a module

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            UnityConfig.Initialise();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBindingConfig.Register(ModelBinders.Binders);

            // View engine ( for branding )
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ClientViewEngine());
        }
    }
}