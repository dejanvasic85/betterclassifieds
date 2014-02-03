Imports BetterClassified.Repository
Imports Paramount.Betterclassifieds.Business.Repository
Imports Paramount.Betterclassifieds.Business.Managers
Imports Microsoft.Practices.Unity
Imports Paramount.Betterclassifieds.Business.Bookings

Public Class ContainerConfig
    Public Shared Sub RegisterIocContainer(ByVal container As IUnityContainer)

        ' Repositories
        container.RegisterType(Of IMenuRepository, TheMusicMenuRepository)()
        
        ' Managers
        container.RegisterType(Of IBookingManager, BookingManager)() _
            .RegisterType(Of IEditionManager, EditionManager)()


    End Sub
End Class
