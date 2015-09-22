using Microsoft.Practices.Unity;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.DataService;
using Paramount.Betterclassifieds.DataService.Broadcast;
using Paramount.Betterclassifieds.DataService.Repository;
using Paramount.Betterclassifieds.Presentation;
using Paramount.Betterclassifieds.Security;

namespace Paramount.Betterclassifieds.Console
{
    public class UnityConfig
    {
        public static void Initialise(IUnityContainer container)
        {
            // Register all managers and other components
            container.RegisterType<IBookingManager, BookingManager>()
                .RegisterType<IEditionManager, EditionManager>()
                .RegisterType<IClientConfig, ClientConfig>()
                .RegisterType<IBroadcastManager, BroadcastManager>()
                .RegisterType<INotificationProcessor, Business.Broadcast.EmailProcessor>("EmailProcessor")
                .RegisterType<ISmtpMailer, DefaultMailer>()
                .RegisterType<IDateService, ServerDateService>()
                .RegisterType<IUserManager, UserManager>()
                .RegisterType<IAuthManager, AuthManager>()
                .RegisterType<IApplicationConfig, AppConfig>()
                .RegisterType<IConfirmationCodeGenerator, ConfirmationCodeGenerator>()
                .RegisterType<ICategoryAdRepositoryFactory, CategoryRepositoryFactory>()
                

            // Repositories
                .RegisterType<IBookingRepository, BookingRepository>()
                .RegisterType<IBroadcastRepository, BroadcastRepository>()
                .RegisterType<IEditionRepository, EditionRepository>()
                .RegisterType<IPublicationRepository, PublicationRepository>()
                .RegisterType<IPaymentsRepository, PaymentsRepository>()
                .RegisterType<IAdRepository, AdRepository>()
                .RegisterType<IUserRepository, UserRepository>()
                .RegisterType<IDbContextFactory, DbContextFactory>()


            // Console framework
                .RegisterType<ILogger, ConsoleLogger>()
                ;
            
        }
    }
}
