﻿using Microsoft.Practices.Unity;
using Paramount.ApplicationBlock.Mvc;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.DataSources;
using Paramount.Betterclassifieds.DataService.Managers;
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
            container.RegisterType<IAdRepository, AdRepository>()
                     .RegisterType<ICategoryRepository, CategoryRepository>()
                     .RegisterType<ISeoNameMappingDataSource, SeoNameMappingDataSource>()
                     .RegisterType<ISeoMappingRepository, SeoMappingRepository>()
                     .RegisterType<IBookingRepository, BookingRepository>()
                     .RegisterType<IPublicationRepository, PublicationRepository>()
                     .RegisterType<IRateRepository, RateRepository>()
                     .RegisterType<IUserRepository, UserRepository>()
                     .RegisterType<IDocumentRepository, DocumentRepository>()
                     .RegisterType<IPaymentsRepository, PaymentsRepository>()
                     .RegisterType<IClientConfig, ClientConfig>()
                     .RegisterType<IApplicationConfig, AppConfig>()
                     .RegisterType<IClientIdentifierManager, CookiesManager>()
                     .RegisterType<IClientSideCacheRepository, ClientSideCacheRepository>()
                     .RegisterType<IClientSideCacheDataSource, ClientSideCacheDataSource>()
                     .RegisterType<Business.Broadcast.IBroadcastRepository, Broadcast.BroadcastRepository>();

        }
    }
}