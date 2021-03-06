﻿using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Paramount.Betterclassifieds.Presentation
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, IUnityContainer container)
        {
            // Use the filter provider that uses unity container so it can resolve any dependencies
            var oldProvider = FilterProviders.Providers.Single(f => f is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(oldProvider);

            var provider = new UnityFilterAttributeFilterProvider(container);
            FilterProviders.Providers.Add(provider);

            filters.Add(new JsonHandlerAttribute());
            filters.Add(new LoggingFilter());
        }
    }
}