Imports System.IO
Imports BetterclassifiedsCore
Imports System.Globalization
Imports BetterclassifiedsCore.BundleBooking
Imports BetterclassifiedsCore.ParameterAccess
Imports Paramount.Betterclassified.Utilities.CreditCardPayment

Namespace payment
    Partial Public Class cc
        Inherits System.Web.UI.Page
        Public Shared ReadOnly Property Settings() As CCPaymentGatewaySettings
            Get
                Return ConfigurationManager.GetSection("ccPaymentGateway")
            End Get
        End Property

        Public Shared ReadOnly Property Cost() As String
            Get
                Dim ci As CultureInfo = New CultureInfo("en-au")
                Select Case BookingController.BookingType
                    Case Booking.BookingAction.NormalBooking
                        Return BookingController.AdBookCart.TotalPrice.Value.ToString("00.00")
                    Case Booking.BookingAction.Reschedule
                        Return BookingController.AdBookCart.TotalPrice.Value.ToString("00.00")
                    Case Booking.BookingAction.SpecialBooking
                        Return BookingController.SpecialBookCart.TotalPrice.Value.ToString("00.00")
                    Case Booking.BookingAction.BundledBooking
                        Return BundleBooking.BundleController.BundleCart.TotalPrice.ToString("00.00")
                    Case Booking.BookingAction.Extension
                        Return ParameterAccess.ExtensionContext.TotalCost.ToString("00.00")
                End Select
                Return Nothing
            End Get
        End Property

        Public Shared ReadOnly Property ItemName() As String
            Get
                If BookingController.BookingType = Booking.BookingAction.NormalBooking Then
                    Return BookingController.AdBookCart.BookReference
                ElseIf BookingController.BookingType = Booking.BookingAction.Reschedule Then
                    Return BookingController.AdBookCart.BookReference
                ElseIf BookingController.BookingType = Booking.BookingAction.SpecialBooking Then
                    Return BookingController.SpecialBookCart.BookReference
                ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                    Return BundleBooking.BundleController.BundleCart.BookReference
                ElseIf BookingController.BookingType = Booking.BookingAction.Extension Then
                    Return ParameterAccess.ExtensionContext.BookingReference
                End If
                Return Nothing
            End Get
        End Property

        Private ReadOnly Property this() As cc
            Get
                Return Me
            End Get
        End Property

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            SaveTempRecord()
        End Sub

        Private Sub SaveTempRecord()
            If BookingController.BookingType = Booking.BookingAction.NormalBooking Then
                BookingController.SaveTempAdRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, User.Identity.Name, BookingController.AdBookCart, TransactionType.CREDIT)
            ElseIf BookingController.BookingType = Booking.BookingAction.Reschedule Then
                BookingController.SaveTempAdRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, User.Identity.Name, BookingController.AdBookCart, TransactionType.CREDIT)
            ElseIf BookingController.BookingType = Booking.BookingAction.SpecialBooking Then
                BookingController.SaveTempAdSpecialRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, User.Identity.Name, BookingController.SpecialBookCart, TransactionType.CREDIT)
            ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                BundleBooking.BundleController.SaveTempAdBundleRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, BundleController.BundleCart.Username, BundleController.BundleCart, TransactionType.CREDIT)
            End If
        End Sub

        Private ReadOnly Property ReturnUrl As String
            Get
                If BookingController.BookingType = Booking.BookingAction.Extension Then
                    ' Override the return URL
                    Dim url = Replace(Settings.ReturnUrl, "/Booking/Success.aspx", "/MemberAccount/Bookings.aspx?extension=true")
                    Return url
                End If
                Return String.Format("{0}?id={1}&", Settings.ReturnUrl, BookingProcess.PaymentReferenceId)
            End Get
        End Property

        Protected Overrides Sub CreateChildControls()
            'Dim info As New AuditLog
            Dim sb As New StringBuilder
            sb.AppendFormat("Customer Email:{0},", Membership.GetUser.Email)
            sb.AppendFormat("Vendor Name: {0},", Settings.VendorName)
            sb.AppendFormat("Refund Policy Url: {0},", Settings.RefundPolicyUrl)
            sb.AppendFormat("Payment Alert Email: {0},", Settings.PaymentAlertEmail)
            sb.AppendFormat("Information Fields: {0},", Cost.ToString())
            sb.AppendFormat("Payment Reference: {0},", ItemName)
            sb.AppendFormat("Gst Rate: {0},", Settings.GstAdded)
            sb.AppendFormat("Gst Included: {0},", Settings.GstAdded)
            sb.AppendFormat("Return Url: {0},", Me.ReturnUrl)
            sb.AppendFormat("ReturnUrlText: {0},", Settings.VendorName)

            Dim contr As New SubmitInputControl

            With contr
                .CustomerEmailAddress = Membership.GetUser.Email
                .VendorName = Settings.VendorName
                .RefundPolicyUrl = Settings.RefundPolicyUrl
                .PaymentAlertEmail = Settings.PaymentAlertEmail
                .InformationFields.Add(Cost.ToString())
                .PaymentReference = ItemName
                .GstRate = Settings.GstRate
                .GstIncluded = Settings.GstAdded
                .ReturnUrl = Me.ReturnUrl
                .ReturnUrlText = Settings.ReturnUrlText
                .NotifyUrl = String.Format("{0}?sessionid={1}&id={2}&tt={3}&totalCost={4}&", Settings.NotifyUrl, Session.SessionID, Me.Id, Common.Constants.PaymentOption.CreditCard, Cost)
            End With

            contr.AddProduct(ItemName, Cost)
            payForm.Controls.Add(contr)

            Dim scriptText As String = String.Format("<script language ='javascript'> document.forms[0].action='{0}'; document.forms[0].submit(); </script>", Settings.GatewayUrl)
            ClientScript.RegisterStartupScript(this.GetType(), "paymentForm", scriptText)
            this.Controls.Add(payForm)
        End Sub

        Private ReadOnly Property Id As String
            Get
                Select Case BookingController.BookingType
                    Case Booking.BookingAction.Extension
                        Return ExtensionContext.ExtensionId
                    Case Else
                        Return BookingProcess.PaymentReferenceId
                End Select
            End Get
        End Property

    End Class
End Namespace