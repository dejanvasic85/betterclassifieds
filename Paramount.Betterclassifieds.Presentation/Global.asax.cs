using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.DataService;
using WebGrease.Configuration;

namespace Paramount.Betterclassifieds.Presentation
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        // This global.asax will only be hit if the application is not a module

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var container = UnityConfig.Initialise();
            var logService = container.Resolve<ILogService>();

            LoggingConfig.Register(logService);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, container);
            RouteConfig.RegisterRoutes(RouteTable.Routes, includeAttributeRoutes: true);
            ModelBindingConfig.Register(ModelBinders.Binders);
            BundleConfig.Register(BundleTable.Bundles);
            BundleConfig.RegisterStyles(BundleTable.Bundles);
            BookingWorkflowConfig.Register(BookingWorkflowTable.Workflows);


            // Do this on another thread
            Task.Factory.StartNew(DataServiceInitialiser.InitializeContexts);

            // View engine ( for branding )
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ClientViewEngine());

            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_End()
        {
            var logService = DependencyResolver.Current.GetService<ILogService>();
            logService.Info("Application shutting down");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["init"] = 0; // This line is here so that a new session object is created on the server!
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception == null)
                return;

            try
            {
                var logService = DependencyResolver.Current.GetService<ILogService>();
                logService.Error(exception);
            }
            catch { }
        }
    }
}