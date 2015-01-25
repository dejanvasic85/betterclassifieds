namespace Paramount.Betterclassifieds.DataService
{
    using Microsoft.Practices.Unity;
    using ApplicationBlock.Mvc;
    using Business;
    using Business.Booking;
    using Business.Print;
    using Business.Repository;
    using Managers;
    using Repository;
    using Business.DocumentStorage;
    using Business.Payment;

    
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
                     .RegisterType<IBookCartRepository, BookCartRepository>(new ContainerControlledLifetimeManager())
                     .RegisterType<Business.Search.ISearchService, SearchService>()
                     .RegisterType<IBookCartRepository, BookCartRepository>()
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