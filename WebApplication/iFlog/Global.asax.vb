Imports BetterclassifiedsCore
Imports Paramount.Broadcast.Components
Imports Paramount.Modules.Logging.UIController
Imports Microsoft.Practices.Unity
Imports BetterClassified.UI.Repository

Public Delegate Sub OnPayment(ByVal ref As String)
Public Class Global_asax
    Inherits System.Web.HttpApplication
    Public Shared OnPayment As OnPayment

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' todo - Get the encryption configuration to check the MAC Address (secure app to server)
        ' Application.Add("validApplication", GeneralRoutine.CheckMachineMacAddress(paramountKey))
        Application.Add("validApplication", True)

        ' Unity Registrations
        BetterClassified.Unity.DefaultContainer _
            .RegisterType(Of IBookingRepository, BookingRepository)() _
            .RegisterType(Of IPublicationRepository, PublicationRepository)() _
            .RegisterType(Of IConfigSettings, ConfigSettings)() _
            .RegisterType(Of IRateRepository, RateRepository)()
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        If OnPayment Is Nothing Then
            OnPayment = New OnPayment(AddressOf SucessfulPayment)
        End If

        Dim environment As String = System.Configuration.ConfigurationManager.AppSettings("ConfigurationContext")

        If (environment.ToUpper = "LIVE" And Application("validApplication") = False) Then

            ' print message to user
            ' and don't allow any other requests to come in.
            Response.Write("Cannot process your request at this time. Please try again later or if problem persists contact our support staff.")
            Response.End()

        Else

            ' check the request length and direct the user to an appropriate error message.
            If Request.ContentLength > Utilities.Constants.CONST_SYSTEM_MaxRequest_Length Then
                Response.Redirect(Utilities.Constants.CONST_ERROR_DEFAULT_URL + "?type=" + Utilities.Constants.CONST_ERROR_REQUEST_SIZE)
            End If

        End If

    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim ex As Exception = Server.GetLastError().GetBaseException
        'EventLogManager.Log(ex)
        ExceptionLogController(Of Exception).AuditException(ex)
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