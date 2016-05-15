using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.DocumentStorage;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Location;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.DataService;
using Paramount.Betterclassifieds.DataService.Broadcast;
using Paramount.Betterclassifieds.DataService.Repository;
using Paramount.Betterclassifieds.Presentation;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Security;

namespace Paramount.Betterclassifieds.Console
{
    public class UnityConfig
    {
        public static void Initialise(IUnityContainer container)
        {
            // Repositories
            container
                .RegisterType<IDbContextFactory, DbContextFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IBookingRepository, BookingRepository>()
                .RegisterType<IPublicationRepository, PublicationRepository>()
                .RegisterType<IRateRepository, RateRepository>()
                .RegisterType<IUserRepository, UserRepository>()
                .RegisterType<IPaymentsRepository, PaymentsRepository>()
                .RegisterType<IEnquiryRepository, EnquiryRepository>()
                .RegisterType<ISearchService, SearchService>()
                .RegisterType<IBroadcastRepository, BroadcastRepository>()
                .RegisterType<IDocumentRepository, DocumentRepository>()
                .RegisterType<IAdRepository, AdRepository>()
                .RegisterType<IBookCartRepository, BookCartRepository>()
                .RegisterType<IEditionRepository, EditionRepository>()
                .RegisterType<IInvoiceRepository, InvoiceRepository>()
                .RegisterType<ICategoryAdRepositoryFactory, CategoryRepositoryFactory>()

                // Events
                .RegisterType(typeof(ICategoryAdRepository<ICategoryAd>), typeof(Paramount.Betterclassifieds.DataService.Events.EventRepository), "Event")
                .RegisterType<Business.Events.IEventRepository, DataService.Events.EventRepository>()
                .RegisterType<IEventManager, EventManager>()
                .RegisterType<IEventBarcodeManager, EventBarcodeManager>()
                .RegisterType<Business.Events.IEventBookingContext, Business.Events.EventBookingContext>(new SessionLifetimeManager<Business.Events.EventBookingContext>())

                // Managers and Config
                .RegisterType<IClientConfig, ClientConfig>()
                .RegisterType<IApplicationConfig, AppConfig>()
                .RegisterType<IBookingManager, BookingManager>()
                .RegisterType<IEditionManager, EditionManager>()
                .RegisterType<IUserManager, UserManager>()
                .RegisterType<IAuthManager, AuthManager>()
                .RegisterType<IBroadcastManager, BroadcastManager>()
                .RegisterType<INotificationProcessor, EmailProcessor>("emailProcessingEngine")
                .RegisterType<ISmtpMailer, DefaultMailer>()
                .RegisterType<IEnquiryManager, EnquiryManager>()
                .RegisterType<IRateCalculator, RateCalculator>()
                .RegisterType<IInvoiceService, InvoiceService>()
                .RegisterType<IAdFactory, AdFactory>()
                .RegisterType<IEventTicketReservationFactory, EventTicketReservationFactory>()

                // Rates/ prices (chargeable items)
                .RegisterType<IPrintChargeableItem, PrintHeadingCharge>("PrintHeadingCharge")
                .RegisterType<IPrintChargeableItem, PrintPhotoCharge>("PrintPhotoCharge")
                .RegisterType<IPrintChargeableItem, PrintSuperBoldHeadingCharge>("PrintSuperBoldHeadingCharge")
                .RegisterType<IPrintChargeableItem, PrintWordCharge>("PrintWordCharge")
                .RegisterType<IOnlineChargeableItem, OnlineBasePriceCharge>("OnlineBasePriceCharge")

                // Infrastructure
                .RegisterType<IDateService, ServerDateService>()
                .RegisterType<IConfirmationCodeGenerator, ConfirmationCodeGenerator>()


                // UI Services
                .RegisterType<ITemplatingService, TemplatingService>()
                .RegisterType<ILocationService, LocationService>()

            // Console framework
                .RegisterType<ILogger, ConsoleLogger>()
                ;
            
        }
    }
}
