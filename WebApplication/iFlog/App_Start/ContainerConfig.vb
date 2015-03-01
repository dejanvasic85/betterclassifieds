Imports BetterClassified.Repository
Imports Paramount.Betterclassifieds.Business.Repository
Imports Paramount.Betterclassifieds.Business
Imports Microsoft.Practices.Unity
Imports Paramount.Betterclassifieds.Security
Imports Paramount.Betterclassifieds.DataService.Repository
Imports Paramount.Betterclassifieds.Business.Broadcast
Imports Paramount.Betterclassifieds.Business.Booking
Imports Paramount.Betterclassifieds.Business.Print
Imports Paramount.Betterclassifieds.Business.Payment

Public Class ContainerConfig
    Public Shared Sub RegisterIocContainer(ByVal container As IUnityContainer)

        ' Repositories
        container.RegisterType(Of IBookingRepository, BookingRepository)() _
            .RegisterType(Of IPublicationRepository, PublicationRepository)() _
            .RegisterType(Of IRateRepository, RateRepository)() _
            .RegisterType(Of IUserRepository, UserRepository)() _
            .RegisterType(Of IPaymentsRepository, PaymentsRepository)() _
            .RegisterType(Of IMenuRepository, TheMusicMenuRepository)() _
            .RegisterType(Of IEnquiryRepository, EnquiryRepository)()

        ' Managers
        container.RegisterType(Of IClientConfig, ClientConfig)() _
            .RegisterType(Of IApplicationConfig, AppConfig)() _
            .RegisterType(Of IBookingManager, BookingManager)() _
            .RegisterType(Of IEditionManager, EditionManager)() _
            .RegisterType(Of IUserManager, UserManager)() _
            .RegisterType(Of IAuthManager, AuthenticationService)() _
            .RegisterType(Of IBroadcastManager, BroadcastManager)() _
            .RegisterType(Of INotificationProcessor, EmailProcessor)("emailProcessingEngine") _
            .RegisterType(Of ISmtpMailer, DefaultMailer)() _
            .RegisterType(Of IEnquiryManager, EnquiryManager)() _
            .RegisterType(Of IRateCalculator, RateCalculator)() _
            .RegisterType(Of IRateCalculator, RateCalculator)()

        ' Online charges
        container.RegisterType(Of IOnlineChargeableItem, OnlineBasePriceCharge)("BasePrice")

        ' Print charges
        container.RegisterType(Of IPrintChargeableItem, PrintPhotoCharge)("PrintPhoto") _
            .RegisterType(Of IPrintChargeableItem, PrintHeadingCharge)("PrintHeadingCharge") _
            .RegisterType(Of IPrintChargeableItem, PrintSuperBoldHeadingCharge)("SuperBoldHeadingCharge") _
            .RegisterType(Of IPrintChargeableItem, PrintWordCharge)("PrintWordCharge")


    End Sub
End Class
