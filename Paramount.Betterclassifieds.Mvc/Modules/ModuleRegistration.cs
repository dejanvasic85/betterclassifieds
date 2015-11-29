using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace Paramount.Betterclassifieds.Mvc.Modules
{
    public abstract class ModuleRegistration
    {
        public abstract string Name { get; }

        public virtual void CreateContextAndRegister(RouteCollection routes, object state)
        {

        }

        public virtual void RegisterTypes(IUnityContainer container)
        {

        }

        public virtual void RegisterBundles(BundleCollection bundles)
        {
            
        }


        public virtual void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            
        }

        public virtual void Register(HttpConfiguration config)
        {
            
        }
    }
}