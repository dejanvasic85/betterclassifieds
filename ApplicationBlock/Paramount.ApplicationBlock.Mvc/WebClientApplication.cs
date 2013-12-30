using System.Web.Http;
using System.Web.Optimization;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Paramount.ApplicationBlock.Configuration;
using Paramount.ApplicationBlock.Mvc.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;


namespace Paramount.ApplicationBlock.Mvc
{
    public abstract class WebClientApplication : System.Web.HttpApplication
    {
        private static readonly Dictionary<string, string> modules = new Dictionary<string, string>();
        private static Dictionary<string, object> moduleData;

        protected static Dictionary<string, object> ModuleData
        {
            get { return moduleData ?? (moduleData = new Dictionary<string, object>()); }
        }


        protected abstract IUnityContainer DefaultContainer
        {
            get;
        }


        void LoadModules()
        {
            var section = ConfigurationManager.GetSection("modulesRegistration") as ModuleRegistrationSection;
            if (section == null) return;
            foreach (IModuleInfo module in section.Modules)
            {
                modules.Add(module.Name, module.Namespace);
                if (module.RegisterView)
                {
                    ViewModulesVirtualPathProvider.RegisterViewModule(module);
                    RegisterModuleViewLocations(module);
                }

            }
        }

        protected virtual void ApplicationStart()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new ViewModulesVirtualPathProvider());
            LoadModules();

            //AreaRegistration.RegisterAllAreas();
            RegisterAllModules(Bootstrapper.Initialise(DefaultContainer), RouteTable.Routes, ModuleData);
            Register();
        }

        public virtual void Register()
        {
        }

        private static void RegisterAllModules(IUnityContainer container,
                         RouteCollection routes, object state)
        {
            var list = MyTypeUtil.GetFilteredTypesFromAssemblies(modules.Select(a => a.Value).ToList(),
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

        /// <summary>
        /// Registers the view engine module view locations.
        /// </summary>
        /// <param name="moduleInfo">The module info.</param>
        protected virtual void RegisterModuleViewLocations(IModuleInfo moduleInfo)
        {
            var brand = ConfigManager.ReadAppSetting("Brand");

            if (brand.HasValue())
            {
                this.RegisterViewLocationFormat(moduleInfo.VirtualPath + "/Views/{1}/{0}." + brand + ".cshtml");
            }

            this.RegisterViewLocationFormat(moduleInfo.VirtualPath + "/Views/{1}/{0}.cshtml");
         

            this.RegisterViewLocationFormat(moduleInfo.VirtualPath + "/Views/{0}.cshtml");
            this.RegisterViewLocationFormat(moduleInfo.VirtualPath + "/Views/Shared/{0}.cshtml");

            this.RegisterPartialViewLocationFormat(moduleInfo.VirtualPath + "/Views/{1}/{0}.cshtml");
            this.RegisterPartialViewLocationFormat(moduleInfo.VirtualPath + "/Views/{0}.cshtml");
            this.RegisterPartialViewLocationFormat(moduleInfo.VirtualPath + "/Views/Shared/{0}.cshtml");


            this.RegisterMasterLocationFormat(moduleInfo.VirtualPath + "/Views/Shared/{0}.cshtml");
            this.RegisterMasterLocationFormat(moduleInfo.VirtualPath + "/Views/{0}.cshtml");

        }

        protected void RegisterPartialViewLocationFormat(string locationFormat)
        {
            var currentViewEngine = ViewEngines.Engines.First(a => a is RazorViewEngine) as RazorViewEngine;
            this.RegisterPartialViewLocationFormat(locationFormat, currentViewEngine);
        }

        protected void RegisterMasterLocationFormat(string locationFormat)
        {
            var currentViewEngine = ViewEngines.Engines.First(a => a is RazorViewEngine) as RazorViewEngine;
            this.RegisterMasterLocationFormat(locationFormat, currentViewEngine);
        }


        /// <summary>
        /// Registers the view location format with the default view engine.
        /// </summary>
        /// <param name="locationFormat">The location format.</param>
        protected void RegisterViewLocationFormat(string locationFormat)
        {
            var currentViewEngine = ViewEngines.Engines.First(a => a is RazorViewEngine) as RazorViewEngine;
            this.RegisterViewLocationFormat(locationFormat, currentViewEngine);
        }

        /// <summary>
        /// Registers the view location format with the given view engine.
        /// </summary>
        /// <param name="locationFormat">The location format.</param>
        /// <param name="viewEngine">The view engine.</param>
        private void RegisterViewLocationFormat(string locationFormat, RazorViewEngine viewEngine)
        {
            if (viewEngine != null)
            {
                viewEngine.ViewLocationFormats = viewEngine.ViewLocationFormats.Union(new[] { locationFormat }).ToArray();
            }
        }

        /// <summary>
        /// Registers the view location format with the given view engine.
        /// </summary>
        /// <param name="locationFormat">The location format.</param>
        /// <param name="viewEngine">The view engine.</param>
        private void RegisterPartialViewLocationFormat(string locationFormat, RazorViewEngine viewEngine)
        {
            if (viewEngine != null)
            {
                viewEngine.PartialViewLocationFormats = viewEngine.ViewLocationFormats.Union(new[] { locationFormat }).ToArray();
            }
        }

        /// <summary>
        /// Registers the view location format with the given view engine.
        /// </summary>
        /// <param name="locationFormat">The location format.</param>
        /// <param name="viewEngine">The view engine.</param>
        private void RegisterMasterLocationFormat(string locationFormat, RazorViewEngine viewEngine)
        {
            if (viewEngine != null)
            {
                viewEngine.MasterLocationFormats = viewEngine.ViewLocationFormats.Union(new[] { locationFormat }).ToArray();
            }
        }

        /// <summary>
        /// Registers the view location format with the given view engine.
        /// </summary>
        /// <param name="locationFormat">The location format.</param>
        /// <param name="viewEngine">The view engine.</param>
        private void RegisterAreaViewLocationFormats(string locationFormat, RazorViewEngine viewEngine)
        {
            if (viewEngine != null)
            {
                viewEngine.AreaViewLocationFormats = viewEngine.ViewLocationFormats.Union(new[] { locationFormat }).ToArray();
            }
        }

        /// <summary>
        /// Registers the view location format with the default view engine.
        /// </summary>
        /// <param name="locationFormat">The location format.</param>
        protected void RegisterAreaViewLocationFormats(string locationFormat)
        {
            var currentViewEngine = ViewEngines.Engines.First(a => a is RazorViewEngine) as RazorViewEngine;
            this.RegisterAreaViewLocationFormats(locationFormat, currentViewEngine);
        }

    }

}


