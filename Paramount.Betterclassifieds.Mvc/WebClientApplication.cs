using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Mvc.Configuration;
using Paramount.Betterclassifieds.Mvc.Modules;
using Paramount.Betterclassifieds.Mvc.Unity;

namespace Paramount.Betterclassifieds.Mvc
{
    public abstract class WebClientApplication : System.Web.HttpApplication
    {
        private static readonly Dictionary<string, string> modules = new Dictionary<string, string>();
        private static Dictionary<string, object> moduleData;

        protected static Dictionary<string, object> ModuleData
        {
            get { return moduleData ?? (moduleData = new Dictionary<string, object>()); }
        }


        public abstract IUnityContainer DefaultContainer
        {
            get;
        }


        void LoadModules()
        {
            var section = ConfigurationManager.GetSection("modulesRegistration") as ModuleRegistrationSection;
            if (section == null) return;
            
            // Remove all engines and register the client view engine
            ViewEngines.Engines.Clear();
            var clientViewEngine = new ClientViewEngine();

            foreach (IModuleInfo module in section.Modules)
            {
                modules.Add(module.Name, module.Namespace);
                if (!module.RegisterView) continue;
                ViewModulesVirtualPathProvider.RegisterViewModule(module);
                clientViewEngine.AddViewPath(module.VirtualPath);
            }

            //register viewengine
            ViewEngines.Engines.Add(clientViewEngine);
        }

        protected virtual void ApplicationStart()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new ViewModulesVirtualPathProvider());
            LoadModules();

            RegisterAllModules(Bootstrapper.Initialise(DefaultContainer), RouteTable.Routes, ModuleData);
        }

        private static void RegisterAllModules(IUnityContainer container, RouteCollection routes, object state)
        {
            var list = MyTypeUtil.GetFilteredTypesFromAssemblies(
                modules.Select(a => a.Value).ToList(), 
                IsModuleRegistrationType).ToList();

            foreach (var module in list.Select(type => (ModuleRegistration)Activator.CreateInstance(type)))
            {
                module.RegisterTypes(container);
                module.RegisterGlobalFilters(GlobalFilters.Filters);
                module.RegisterBundles(BundleTable.Bundles);
                module.Register(GlobalConfiguration.Configuration);
                
                module.CreateContextAndRegister(routes, state);
            }
        }

        public static bool IsModuleRegistrationType(Type type)
        {
            if (typeof(ModuleRegistration).IsAssignableFrom(type))
                return type.GetConstructor(Type.EmptyTypes) != null;
            return false;
        }
    }

}


