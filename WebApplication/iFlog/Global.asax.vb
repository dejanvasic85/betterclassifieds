Imports BetterclassifiedsCore
Imports Paramount.Broadcast.Components

Imports Microsoft.Practices.Unity
Imports BetterClassified.Repository
Imports System.Web.Routing
Imports System.Web.Http

Public Delegate Sub OnPayment(ByVal ref As String)

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Public Shared OnPayment As OnPayment
    Public Shared TransactionManager As BetterClassified.UI.Presenters.TransactionManager

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        Application.Add("validApplication", True)

        ' Unity Container Registrations
        BetterClassified.Unity.DefaultContainer _
            .RegisterType(Of IBookingRepository, BookingRepository)() _
            .RegisterType(Of IPublicationRepository, PublicationRepository)() _
            .RegisterType(Of IConfigSettings, ConfigSettings)() _
            .RegisterType(Of IRateRepository, RateRepository)() _
            .RegisterType(Of IUserRepository, UserRepository)() _
            .RegisterType(Of IPaymentsRepository, PaymentsRepository)() _
            .RegisterType(Of IAdRepository, AdRepository)() _
            .RegisterType(Of ILookupRepository, LookupRepository)()

        RouteTable.Routes.MapHttpRoute(
           name:="DefaultApi",
           routeTemplate:="api/{controller}/{id}",
           defaults:=New With {Key .id = System.Web.Http.RouteParameter.[Optional]})

    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        If OnPayment Is Nothing Then
            OnPayment = New OnPayment(AddressOf SucessfulPayment)
        End If

        ' check the request length and direct the user to an appropriate error message.
        If Request.ContentLength > 4194304 Then
            Response.Redirect(Utilities.Constants.CONST_ERROR_DEFAULT_URL + "?type=" + Utilities.Constants.CONST_ERROR_REQUEST_SIZE)
        End If

    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

    Sub SucessfulPayment(ByVal bookRef As String)
        Dim content = BookingController.GetBookingStringContentByRef(bookRef)
        Dim emailString As String = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_SYSTEM, Utilities.Constants.CONST_KEY_System_AdminEmails)
        Dim emailCollection = emailString.Split(";")
        Dim email As New AfterAdBookingNotification(emailCollection, String.Empty, content)
        email.Send()
    End Sub

End Class