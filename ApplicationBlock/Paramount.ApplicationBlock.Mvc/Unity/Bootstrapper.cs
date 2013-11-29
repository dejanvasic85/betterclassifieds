using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.ApplicationBlock.Mvc.Unity;
namespace Paramount.ApplicationBlock.Mvc
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