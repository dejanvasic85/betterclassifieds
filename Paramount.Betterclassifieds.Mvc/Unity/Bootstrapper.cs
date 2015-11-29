using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Paramount.Betterclassifieds.Mvc.Unity
{
  public static class Bootstrapper
  {
      public static IUnityContainer Initialise(IUnityContainer container)
      {
          DependencyResolver.SetResolver(new UnityDependencyResolver(container));
          
          return container;
    }

  }
}