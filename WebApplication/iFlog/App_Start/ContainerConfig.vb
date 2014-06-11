﻿Imports BetterClassified.Repository
Imports Paramount.Betterclassifieds.Business.Repository
Imports Paramount.Betterclassifieds.Business.Managers
Imports Paramount.Betterclassifieds.Business
Imports Microsoft.Practices.Unity
Imports Paramount.Betterclassifieds.Security
Imports Paramount.Betterclassifieds.DataService.Repository
Imports Paramount.Betterclassifieds.Business.Broadcast

Public Class ContainerConfig
    Public Shared Sub RegisterIocContainer(ByVal container As IUnityContainer)

        ' Repositories
        container.RegisterType(Of IBookingRepository, BookingRepository)() _
            .RegisterType(Of IPublicationRepository, PublicationRepository)() _
            .RegisterType(Of IRateRepository, RateRepository)() _
            .RegisterType(Of IUserRepository, UserRepository)() _
            .RegisterType(Of IPaymentsRepository, PaymentsRepository)() _
            .RegisterType(Of ILookupRepository, LookupRepository)() _
            .RegisterType(Of IMenuRepository, OfflineMenuRepository)()

        ' Managers
        container.RegisterType(Of IClientConfig, ClientConfig)() _
            .RegisterType(Of IApplicationConfig, AppConfig)() _
            .RegisterType(Of IBookingManager, BookingManager)() _
            .RegisterType(Of IEditionManager, EditionManager)() _
            .RegisterType(Of IUserManager, UserManager)() _
            .RegisterType(Of IAuthManager, AuthenticationService)() _
            .RegisterType(Of IBroadcastManager, BroadcastManager)() _
            .RegisterType(Of INotificationProcessor, EmailProcessor)("emailProcessingEngine") _
            .RegisterType(Of ISmtpMailer, DefaultMailer)()


    End Sub
End Class
