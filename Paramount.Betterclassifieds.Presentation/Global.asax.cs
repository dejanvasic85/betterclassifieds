﻿using System.Globalization;
using System.Threading;
using System.Web.Optimization;

namespace Paramount.Betterclassifieds.Presentation
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Linq;

    using App_Start;
    using ApplicationBlock.Mvc;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        // This global.asax will only be hit if the application is not a module

        protected void Application_Start()
        {
            // Force the UI Culture for now
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // View engine ( for branding )
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ClientViewEngine());
        }
    }
}