using Microsoft.Practices.Unity;
using Paramount.ApplicationBlock.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.Managers;
using Paramount.Betterclassifieds.DataService.Repository;

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
                     .RegisterType<IBookingRepository, BookingRepository>()
                     .RegisterType<IPublicationRepository, PublicationRepository>()
                     .RegisterType<IRateRepository, RateRepository>()
                     .RegisterType<IUserRepository, UserRepository>()
                     .RegisterType<IDocumentRepository, DocumentRepository>()
                     .RegisterType<IPaymentsRepository, PaymentsRepository>()
                     .RegisterType<IClientConfig, ClientConfig>()
                     .RegisterType<IApplicationConfig, AppConfig>()
                     .RegisterType<IClientIdentifierManager, CookiesManager>()
                     .RegisterType<Business.Broadcast.IBroadcastRepository, Broadcast.BroadcastRepository>()
                     .RegisterType<IBookingCartRepository, BookingCartRepository>(new ContainerControlledLifetimeManager())
                     .RegisterType<Business.Search.ISearchService, SearchService>()
                     .RegisterType<IBookingCartRepository, BookingCartRepository>()
                     ;

           
            InitializeContexts();
        }

        /// <summary>
        /// Entity Framework initializes on the first query and it's too slow. This will be forced on app startup instead now
        /// </summary>
        private static void InitializeContexts()
        {
            using (var context = DataContextFactory.CreateClassifiedEntitiesContext())
            {
                context.Database.Initialize(false);
            }
        }
    }
}