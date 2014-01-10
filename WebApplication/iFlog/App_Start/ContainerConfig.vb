Imports BetterClassified.Repository
Imports Paramount.Betterclassifieds.Business.Repository
Imports Paramount.Betterclassifieds.Business.Managers
Imports Microsoft.Practices.Unity
Imports Paramount.Betterclassifieds.Business.Bookings

Public Class ContainerConfig
    Public Shared Sub RegisterIocContainer(ByVal container As IUnityContainer)

        ' Repositories
        container.RegisterType(Of IBookingRepository, BookingRepository)() _
            .RegisterType(Of IPublicationRepository, PublicationRepository)() _
            .RegisterType(Of IRateRepository, RateRepository)() _
            .RegisterType(Of IUserRepository, UserRepository)() _
            .RegisterType(Of IPaymentsRepository, PaymentsRepository)() _
            .RegisterType(Of IAdRepository, Paramount.Betterclassifieds.DataService.Repository.AdRepository)() _
            .RegisterType(Of ILookupRepository, LookupRepository)() _
            .RegisterType(Of IMenuRepository, TheMusicMenuRepository)()

        ' Managers
        container.RegisterType(Of IClientConfig, ClientConfig)() _
            .RegisterType(Of IApplicationConfig, AppConfig)() _
            .RegisterType(Of IBookingManager, BookingManager)() _
            .RegisterType(Of IEditionManager, EditionManager)()


    End Sub
End Class
