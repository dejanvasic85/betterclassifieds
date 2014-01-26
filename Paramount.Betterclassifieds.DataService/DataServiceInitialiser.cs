using Microsoft.Practices.Unity;
using Paramount.ApplicationBlock.Mvc;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.DataSources;
using Paramount.Betterclassifieds.DataService.Repository;
using Paramount.Betterclassifieds.DataService.SeoSettings;

namespace Paramount.Betterclassifieds.DataService
{
    public class DataServiceInitialiser : ModuleRegistration
    {
        public override string Name
        {
            get { return "DataService"; }
        }

        public override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IAdRepository, AdRepository>();
            container.RegisterType<ISeoNameMappingDataSource, SeoNameMappingDataSource>();
            container.RegisterType<ISeoMappingRepository, SeoMappingRepository>();
        }
    }
}