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
            'Dim info = New AuditLog With {.TransactionName = "Request.SaveTempBookingRecord", .Data = String.Format("Ref:{0}, Cost:{1}, Booking Type:{2}", BookingProcess.PaymentReferenceId, Cost, BookingController.BookingType)}
            'AuditLogManager.Log(info)


            If BookingController.BookingType = Booking.BookingAction.NormalBooking Then
                BookingController.SaveTempAdRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, User.Identity.Name, BookingController.AdBookCart, TransactionType.CREDIT)
            ElseIf BookingController.BookingType = Booking.BookingAction.Reschedule Then
                BookingController.SaveTempAdRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, User.Identity.Name, BookingController.AdBookCart, TransactionType.CREDIT)
            ElseIf BookingController.BookingType = Booking.BookingAction.SpecialBooking Then
                BookingController.SaveTempAdSpecialRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, User.Identity.Name, BookingController.SpecialBookCart, TransactionType.CREDIT)
            ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                BundleBooking.BundleController.SaveTempAdBundleRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, BundleController.BundleCart.Username, BundleController.BundleCart, TransactionType.CREDIT)
            End If
            'AuditLogManager.Log(New AuditLog With {.TransactionName = "Request.SaveTempBookingRecord", .Data = "Success"})
        End Sub

        Protected Overrides Sub CreateChildControls()
            'Dim info As New AuditLog
            Dim sb As New StringBuilder
            sb.AppendFormat("Customer Email:{0},", Membership.GetUser.Email)
            sb.AppendFormat("Vendor Name: {0},", Settings.VendorName)
            sb.AppendFormat("Refund Policy Url: {0},", Settings.RefundPolicyUrl)
            sb.AppendFormat("Payment Alert Email: {0},", Settings.PaymentAlertEmail)
            sb.AppendFormat("Information Fields: {0},", GetPriceSummary)
            sb.AppendFormat("Payment Reference: {0},", ItemName)
            sb.AppendFormat("Gst Rate: {0},", Settings.GstAdded)
            sb.AppendFormat("Gst Included: {0},", Settings.GstAdded)
            sb.AppendFormat("Return Url: {0},", String.Format("{0}?id={1}&", Settings.ReturnUrl, BookingProcess.PaymentReferenceId))
            sb.AppendFormat("ReturnUrlText: {0},", Settings.VendorName)

            'info.TransactionName = "Request.SubmitPaymentInfo"
            'info.Data = String.Format(sb.ToString)
            'info.SecondaryData = String.Format("{0}?sessionid={1}&id={2}&tt={3}&totalCost={4}&", Settings.NotifyUrl, Session.SessionID, BookingProcess.PaymentReferenceId, Common.Constants.PaymentOption.CreditCard, Cost)
            'AuditLogManager.Log(info)
            Dim contr As New SubmitInputControl

            With contr
                .CustomerEmailAddress = Membership.GetUser.Email
                .VendorName = Settings.VendorName
                .RefundPolicyUrl = Settings.RefundPolicyUrl
                .PaymentAlertEmail = Settings.PaymentAlertEmail
                .InformationFields.Add(GetPriceSummary)
                .PaymentReference = ItemName
                .GstRate = Settings.GstRate
                .GstIncluded = Settings.GstAdded
                .ReturnUrl = String.Format("{0}?id={1}&", Settings.ReturnUrl, BookingProcess.PaymentReferenceId)
                .ReturnUrlText = Settings.ReturnUrlText
                .NotifyUrl = String.Format("{0}?sessionid={1}&id={2}&tt={3}&totalCost={4}&", Settings.NotifyUrl, Session.SessionID, BookingProcess.PaymentReferenceId, Common.Constants.PaymentOption.CreditCard, Cost)
            End With

            contr.AddProduct(ItemName, Cost)
            payForm.Controls.Add(contr)


            Dim scriptText As String = String.Format("<script language ='javascript'> document.payForm.action='{0}'; document.payForm.submit(); </script>", Settings.GatewayUrl)
            ClientScript.RegisterStartupScript(this.GetType(), "paymentForm", scriptText)
            this.Controls.Add(payForm)
            'AuditLogManager.Log(New AuditLog With {.TransactionName = "Response.SubmitPaymentInfo", .Data = "Success"})
        End Sub

        Private Function GetPriceSummary() As String
            Dim tempPage As New Page
            Dim sw As New StringWriter()
            Dim control = this.LoadControl("~/Controls/Booking/PriceSummary.ascx")
            Page.Controls.Add(control)
            HttpContext.Current.Server.Execute(tempPage, sw, False)
            Dim contentString = sw.ToString()
            sw.Close()
            Return contentString
        End Function
    End Class
End Namespace