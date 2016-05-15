namespace Paramount.Betterclassifieds.Presentation
{
    using Business;
    using Business.Booking;
    using Business.Broadcast;
    using Business.DocumentStorage;
    using Business.Payment;
    using Business.Print;
    using Business.Repository;
    using Business.Search;
    using Business.Events;
    using Business.Location;
    using DataService;
    using DataService.Broadcast;
    using DataService.Repository;
    using Microsoft.Practices.Unity;
    using Security;
    using System.Web;
    using System.Web.Mvc;
    using Unity.Mvc4;
    using Payments.pp;
    using Payments.Stripe;
    using Services;
    using ViewModels;


    public class UnityConfig
    {
        public static IUnityContainer Initialise()
        {
            var container = new UnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

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
                .RegisterType<IBookingContext, BookingContextInCookie>()
                .RegisterType<IPayPalService, PayPalPayPalService>()
                .RegisterType<ICreditCardService, StripeApi>()
                .RegisterType<IInvoiceService, InvoiceService>()
                .RegisterType<SearchFilters>(new SessionLifetimeManager<SearchFilters>())
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
                .RegisterType<HttpContextBase>(new InjectionFactory(c => new HttpContextWrapper(HttpContext.Current)))

                // UI Services
                .RegisterType<ITemplatingService, TemplatingService>()
                .RegisterType<ILocationService, LocationService>()
                ;

            return container;
        }
    }
}