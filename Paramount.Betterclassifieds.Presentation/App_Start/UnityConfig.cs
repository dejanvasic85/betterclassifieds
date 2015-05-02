namespace Paramount.Betterclassifieds.Presentation
{
    using ApplicationBlock.Mvc;
    using Business;
    using Business.Booking;
    using Business.Broadcast;
    using Business.DocumentStorage;
    using Business.Payment;
    using Business.Print;
    using Business.Repository;
    using Business.Search;
    using DataService;
    using DataService.Broadcast;
    using DataService.Repository;
    using Microsoft.Practices.Unity;
    using Security;
    using System.Web.Mvc;
    using Unity.Mvc4;
    using Payments.pp;
    using ViewModels;

    public class UnityConfig
    {
        public static IUnityContainer Initialise()
        {
            var container = new UnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // Repositories
            container.RegisterType<IBookingRepository, BookingRepository>()
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
                ;

            // Managers and Config
            container.RegisterType<IClientConfig, ClientConfig>()
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
                .RegisterType<IPaymentService, PayPalPaymentService>()
                .RegisterType<IInvoiceService, InvoiceService>()
                .RegisterType<SearchFilters>(new SessionLifetimeManager<SearchFilters>())
                ;
            
            // Rates/ prices (chargeable items)
            container.RegisterType<IPrintChargeableItem, PrintHeadingCharge>("PrintHeadingCharge")
                .RegisterType<IPrintChargeableItem, PrintPhotoCharge>("PrintPhotoCharge")
                .RegisterType<IPrintChargeableItem, PrintSuperBoldHeadingCharge>("PrintSuperBoldHeadingCharge")
                .RegisterType<IPrintChargeableItem, PrintWordCharge>("PrintWordCharge")
                .RegisterType<IOnlineChargeableItem, OnlineBasePriceCharge>("OnlineBasePriceCharge")
                ;

            return container;
        }
    }
}