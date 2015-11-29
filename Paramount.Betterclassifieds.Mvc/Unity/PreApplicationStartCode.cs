using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Paramount.Betterclassifieds.Mvc.Unity;

[assembly: PreApplicationStartMethod(
  typeof(PreApplicationStartCode), "PreStart")]
namespace Paramount.Betterclassifieds.Mvc.Unity
{
    
    public class PreApplicationStartCode
    {
        private static bool _isStarting;

        public static void PreStart()
        {
            if (PreApplicationStartCode._isStarting)
                return;
            PreApplicationStartCode._isStarting = true;
            DynamicModuleUtility.RegisterModule(typeof(RequestLifetimeHttpModule));
        }
    }
}