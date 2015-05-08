using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.ApplicationBlock.Mvc.Unity;

namespace Paramount.Betterclassifieds.Presentation
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, IUnityContainer container)
        {
            // A custom error filter is not really needed because elmah does all the logging for us
            //filters.Add(new CustomErrorFilter());

            // Use the filter provider that uses unity container so it can resolve any dependencies
            var oldProvider = FilterProviders.Providers.Single(f => f is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(oldProvider);

            var provider = new UnityFilterAttributeFilterProvider(container);
            FilterProviders.Providers.Add(provider);
        }
    }
}