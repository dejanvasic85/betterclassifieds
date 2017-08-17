namespace Paramount.Betterclassifieds.Presentation
{
    using Business;
    using Business.Booking;
    using Business.DocumentStorage;
    using Business.Payment;
    using Business.Print;
    using Business.Repository;
    using Business.Search;
    using Business.Events;
    using Business.Events.Reservations;
    using Business.Events.Organisers;
    using Business.Location;
    using DataService;
    using DataService.Repository;
    using Microsoft.Practices.Unity;
    using Security;
    using System.Web;
    using System.Web.Mvc;
    using Payments.pp;
    using Payments.Stripe;
    using Services;
    using Services.Seo;
    using Services.Mail;
    using ViewModels;
    using Unity.Mvc5;


    public class UnityConfig
    {

        public static IUnityContainer Initialise()
        {
            var container = new UnityContainer();

            // Web MVC Dependency resolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            // Web API Dependency resolver
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);


            // Repositories
            container
                .RegisterType<IDbContextFactory, DbContextFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IBookingRepository, BookingRepository>()
                .RegisterType<IPublicationRepository, PublicationRepository>()
                .RegisterType<IRateRepository, RateRepository>()
                .RegisterType<IUserRepository, UserRepository>()
                .RegisterType<IPaymentsRepository, PaymentsRepository>()
                .RegisterType<ISearchService, SearchService>()
                .RegisterType<IDocumentRepository, DocumentRepository>()
                .RegisterType<IAdRepository, AdRepository>()
                .RegisterType<IBookCartRepository, BookCartDocumentRepository>()
                .RegisterType<IEditionRepository, EditionRepository>()
                .RegisterType<IInvoiceRepository, InvoiceRepository>()
                .RegisterType<ICategoryAdFactory, CategoryFactory>()

                // Events
                .RegisterType(typeof(ICategoryAdRepository<ICategoryAd>), typeof(Paramount.Betterclassifieds.DataService.Events.EventRepository), CategoryAdType.Event)
                .RegisterType(typeof(ICategoryAdAuthoriser), typeof(EventAccess), CategoryAdType.Event)
                .RegisterType<Business.Events.IEventRepository, DataService.Events.EventRepository>()
                .RegisterType<IEventManager, EventManager>()
                .RegisterType<IEventSeatingService, EventSeatingService>()
                .RegisterType<IEventBarcodeValidator, EventBarcodeValidator>()
                .RegisterType<IBarcodeGenerator, BarcodeGenerator>()
                .RegisterType<Business.Events.IEventBookingContext, Business.Events.EventBookingContext>(
                    new SessionLifetimeManager<Business.Events.EventBookingContext>())
                .RegisterType<ITicketRequestValidator, TicketRequestValidator>()
                .RegisterType<IEventOrganiserService, EventOrganiserService>()
                .RegisterType<IEventGuestService, EventGuestService>()
                .RegisterType<IEventPromoService, EventPromoService>()

                // Managers and Config
                .RegisterType<IClientConfig, ClientConfig>()
                .RegisterType<IApplicationConfig, AppConfig>()
                .RegisterType<IBookingManager, BookingManager>()
                .RegisterType<IEditionManager, EditionManager>()
                .RegisterType<IUserManager, UserHttpManager>()
                .RegisterType<IAuthManager, AuthManager>()
                .RegisterType<IRateCalculator, RateCalculator>()
                .RegisterType<IBookingContext, BookingContextInCookie>()
                .RegisterType<IPayPalService, PayPalPayPalService>()
                .RegisterType<ICreditCardService, StripeApi>()
                .RegisterType<IInvoiceService, InvoiceService>()
                .RegisterType<SearchFilters>(new SessionLifetimeManager<SearchFilters>())
                .RegisterType<IAdFactory, AdFactory>()
                .RegisterType<IEventTicketReservationFactory, EventTicketReservationFactory>()
                .RegisterType<ILogService, LogService>()

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
                .RegisterType<ISitemapFactory, SitemapFactory>()
                .RegisterType<IEventBookingManager, EventBookingManager>()
                .RegisterType<IUrl, UrlService>()
                .RegisterType<IMailService, MailService>()
                .RegisterType<IMailSender, MailgunSender>()
                .RegisterType<IPdfGenerator, PdfGenerator>()
                .RegisterType<IRobotVerifier, RobotVerifier>()
                ;


#if (DEBUG)
            container.RegisterType<ILocationService, DebugLocationService>();
#else
            container.RegisterType<ILocationService, LocationService>();
#endif


            return container;
        }
    }
}