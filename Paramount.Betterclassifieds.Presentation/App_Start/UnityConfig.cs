namespace Paramount.Betterclassifieds.Presentation
{
    using Business;
    using Business.Booking;
    using Business.Broadcast;
    using Business.Payment;
    using Business.Print;
    using Business.Repository;
    using Business.Search;
    using DataService;
    using DataService.Repository;
    using DataService.Broadcast;
    using Microsoft.Practices.Unity;
    using Security;
    using System.Web.Mvc;
    using Unity.Mvc4;

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
                .RegisterType<IRateCalculator, RateCalculator>();
            
            
            return container;
        }
    }
}