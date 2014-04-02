﻿Imports BetterclassifiedsCore
Imports Paramount.Betterclassifieds.Business.Broadcast
Imports Paramount.Betterclassifieds.Business.Managers
Imports BetterClassified
Imports Microsoft.Practices.Unity
Imports Paramount

Public Delegate Sub OnPayment(ByVal ref As String)

Public Class Global_asax
    Inherits Paramount.ApplicationBlock.Mvc.WebClientApplication

    Public Shared OnPayment As OnPayment
    'Public Shared TransactionManager As TransactionManager

    Public Overrides ReadOnly Property DefaultContainer As Microsoft.Practices.Unity.IUnityContainer
        Get
            Return BetterClassified.Unity.DefaultContainer
        End Get
    End Property

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        Application.Add("validApplication", True)

        ContainerConfig.RegisterIocContainer(BetterClassified.Unity.DefaultContainer)
        ApplicationStart()
        
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

    Sub SucessfulPayment(ByVal bookRef As String)
        Dim content = BookingController.GetBookingStringContentByRef(bookRef)
        Dim emailString As String = AppKeyReader(Of String).ReadFromStore(AppKey.AdminNotificationAccounts, defaultIfNotExists:="support@paramountit.com.au")

        Dim broadcastManager = DefaultContainer.Resolve(Of IBroadcastManager)()

        For Each recipient In emailString.Split(";")

            If recipient.HasValue() Then
                ' Always create a new email ( broadcast ) per recipient
                broadcastManager.SendEmail(Of NewBooking)(New NewBooking() With {.Content = content}, recipient)
            End If

        Next

    End Sub

End Class