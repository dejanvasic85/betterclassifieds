using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class ClientConfigService
    {
        private static IClientConfig _instance;

        private ClientConfigService() { }

        public static IClientConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = DependencyResolver.Current.GetService<IClientConfig>();
                }
                return _instance;
            }
        }
    }
}