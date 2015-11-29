using System;
using System.Web;

namespace Paramount.Betterclassifieds.Mvc.Unity
{
    internal class RequestLifetimeHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.EndRequest += (EventHandler)((sender, e) => UnityDependencyResolver.DisposeOfChildContainer());
        }

        public void Dispose()
        {
        }
    }
}