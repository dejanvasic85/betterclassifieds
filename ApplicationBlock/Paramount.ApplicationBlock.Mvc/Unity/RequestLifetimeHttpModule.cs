using System;
using System.Web;

namespace Paramount.ApplicationBlock.Mvc.Unity
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