Imports BetterClassified.Repository
Imports Paramount.Betterclassifieds.Repository
Imports Microsoft.Practices.Unity

Public Class ContainerConfig
    Public Shared Sub RegisterIocContainer(ByVal container As IUnityContainer)

        container.RegisterType(Of IBookingRepository, BookingRepository)() _
            .RegisterType(Of IPublicationRepository, PublicationRepository)() _
            .RegisterType(Of IConfigManager, ConfigSettings)() _
            .RegisterType(Of IRateRepository, RateRepository)() _
            .RegisterType(Of IUserRepository, UserRepository)() _
            .RegisterType(Of IPaymentsRepository, PaymentsRepository)() _
            .RegisterType(Of IAdRepository, AdRepository)() _
            .RegisterType(Of ILookupRepository, LookupRepository)() _
            .RegisterType(Of IMenuRepository, TheMusicMenuRepository)()

    End Sub
End Class
