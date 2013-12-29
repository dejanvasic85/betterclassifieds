﻿namespace Paramount.Betterclassifieds.Presentation
{
    using System.Web.Mvc;
    using ApplicationBlock.Mvc;
    
    public class HomePresentationInitialiser : ModuleRegistration
    {
        public override string Name
        {
            get { return "HomePresentation"; }
        }

        public override void CreateContextAndRegister(System.Web.Routing.RouteCollection routes, object state)
        {
            // Routes
            routes.MapRoute(
                "default",
                "nextgen/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { GetType().Namespace });
            
            // View engine ( for branding )
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new BrandRazorViewEngine());
        }
    }
}