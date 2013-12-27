Imports System.Net
Imports BetterclassifiedsCore
Imports BetterclassifiedsWeb.Payment
Imports Paramount.ApplicationBlock.Configuration
Imports Paramount.Betterclassified.Utilities.PayPal
Imports Microsoft.Practices.Unity
Imports BetterClassified.UI.Presenters
Imports Paramount.Betterclassifieds.Business.Models
Imports Paramount.Betterclassifieds.Business.Managers

Partial Public Class notify
    Inherits System.Web.UI.Page

    Public Shared ReadOnly Property Settings() As PayPalSettings
        Get
            Return ConfigurationManager.GetSection("paypal")
        End Get
    End Property

    ' This helper method encodes a string correctly for an HTTP POST
    Private Function Encode(ByVal oldValue As String) As String
        Dim newValue As String = oldValue.Replace("""", "'")
        newValue = System.Web.HttpUtility.UrlEncode(newValue)
        newValue = newValue.Replace("%2f", "/")
        Return newValue
    End Function

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim result As String = "Success"

        ' Credit card processing
        If Request.QueryString("tt") = Common.Constants.PaymentOption.CreditCard Then
            If BookingController.BookTempAdRecord(NotifyParameterAccess.ReferenceId, NotifyParameterAccess.SessionId, NotifyParameterAccess.Cost) Then
                'redirect to book successful page
                Dim ref = BookingController.GetBookingRefByTempRec(NotifyParameterAccess.ReferenceId)
                If Not String.IsNullOrEmpty(ref) Then
                    Global_asax.OnPayment.BeginInvoke(ref, Nothing, Nothing)
                End If
                Me.Response.Redirect("~/Booking/Default.aspx?action=successful")
            Else
                ' This is an extension booking
                Dim manager = BetterClassified.Unity.DefaultContainer.Resolve(Of BookingManager)()
                Dim extension = manager.GetExtension(NotifyParameterAccess.ReferenceId)
                If extension IsNot Nothing Then
                    manager.Extend(extension, PaymentType.CreditCard)
                    Me.Response.Redirect("~/MemberAccount/Bookings.aspx?extension=true")
                End If
            End If
            Return
        End If

        ' Paypal processing
        Dim formPostData As String = "cmd = _notify-validate"
        For Each postKey As [String] In Request.Form
            Dim postValue As String = Encode(Request.Form(postKey))
            formPostData += String.Format("&{0}={1}", postKey, postValue)
        Next

        Dim client As New WebClient()
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
        Dim postByteArray As Byte() = Encoding.ASCII.GetBytes(formPostData)
        Dim responseArray As Byte() = client.UploadData(Settings.PayPalUrl, "POST", postByteArray)
        Dim response As String = Encoding.ASCII.GetString(responseArray)

        Select Case response
            Case "VERIFIED"
                If BookingController.BookTempAdRecord(NotifyParameterAccess.ReferenceId, NotifyParameterAccess.SessionId, NotifyParameterAccess.Cost) Then
                    Dim ref = BookingController.GetBookingRefByTempRec(NotifyParameterAccess.ReferenceId)
                    If Not String.IsNullOrEmpty(ref) Then
                        Global_asax.OnPayment.BeginInvoke(ref, Nothing, Nothing)
                    End If
                    'redirect to book successful page
                    Me.Response.Redirect("~/Booking/Default.aspx?action=successful")
                Else
                    Dim manager = BetterClassified.Unity.DefaultContainer.Resolve(Of BookingManager)()
                    Dim extension = manager.GetExtension(NotifyParameterAccess.ReferenceId)
                    If extension IsNot Nothing Then
                        manager.Extend(extension, PaymentType.PayPal)
                        Me.Response.Redirect("~/MemberAccount/Bookings.aspx?extension=true")
                    End If
                End If
            Case Else
                ' Possible fraud. Log for investigation.
                Dim secError = "Possible Fraud occurance using paypal system." + Environment.NewLine + _
                                                                 "Paypal User information" + Environment.NewLine + _
                                                                 "-----------------------" + Environment.NewLine + _
                                                                 "Username: " + Request("payer_email") + Environment.NewLine + _
                                                                 "First Name: " + Environment.NewLine + _
                                                                 "Last Name: " + Environment.NewLine + _
                                                                 Environment.NewLine + Environment.NewLine + _
                                                                 "Betterclassifieds User Information " + Environment.NewLine + _
                                                                 "---------------------------------- " + Environment.NewLine + _
                                                                 "Username: " + Membership.GetUser().UserName
                Throw New Exception(secError)
                Exit Select
        End Select
    End Sub
End Class