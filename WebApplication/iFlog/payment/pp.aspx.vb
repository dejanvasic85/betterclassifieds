Imports BetterclassifiedsCore.ParameterAccess
Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BundleBooking
Imports Paramount.Betterclassified.Utilities.PayPal

Partial Public Class pp
    Inherits System.Web.UI.Page
    Public Shared ReadOnly Property Settings() As PayPalSettings
        Get
            Return ConfigurationManager.GetSection("paypal-classic")
        End Get
    End Property

    Public Shared ReadOnly Property SuccessUrl() As String
        Get
            If BookingController.BookingType = Booking.BookingAction.Extension Then
                Dim returnUrl = Replace(Settings.SuccessUrl, "/Booking/Success.aspx", "/MemberAccount/Bookings.aspx?extension=true")
                Return returnUrl
            Else
                Return String.Format("{0}?id={1}&", Settings.SuccessUrl, BookingProcess.PaymentReferenceId)
            End If
        End Get
    End Property

    Public Shared ReadOnly Property NotifyUrl() As String
        Get
            Dim reference = BookingProcess.PaymentReferenceId
            If BookingController.BookingType = Booking.BookingAction.Extension Then
                reference = ExtensionContext.ExtensionId
            End If

            Return String.Format("{0}?sessionid={1}&id={2}&tt={3}&totalCost={4}&", _
                                 Settings.NotifyUrl, _
                                 HttpContext.Current.Session.SessionID, _
                                 reference, _
                                 Common.Constants.PaymentOption.PayPal, _
                                 Cost)
        End Get
    End Property

    Public Shared ReadOnly Property Cost() As String
        Get
            If BookingController.BookingType = Booking.BookingAction.NormalBooking Then
                If (BookingController.AdBookCart.TotalPrice.HasValue) Then
                    Return BookingController.AdBookCart.TotalPrice.Value.ToString("00.00")
                End If
            ElseIf BookingController.BookingType = Booking.BookingAction.Reschedule Then
                If (BookingController.AdBookCart.TotalPrice.HasValue) Then
                    Return BookingController.AdBookCart.TotalPrice.Value.ToString("00.00")
                End If
            ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                Return BundleController.BundleCart.TotalPrice.ToString("00.00")
            ElseIf BookingController.BookingType = Booking.BookingAction.Extension Then
                Return ParameterAccess.ExtensionContext.TotalCost.ToString("00.00")
            End If
            Return 0
        End Get
    End Property

    Public Shared ReadOnly Property ItemName() As String
        Get
            If BookingController.BookingType = Booking.BookingAction.NormalBooking Then
                Return String.Format("Booking Reference: {0}", BookingController.AdBookCart.BookReference)
            ElseIf BookingController.BookingType = Booking.BookingAction.Reschedule Then
                Return String.Format("Booking Reference: {0}", BookingController.AdBookCart.BookReference)
            ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                Return String.Format("Booking Reference: {0}", BundleController.BundleCart.BookReference)
            ElseIf BookingController.BookingType = Booking.BookingAction.Extension Then
                Return String.Format("Booking Reference: {0}", ParameterAccess.ExtensionContext.BookingReference)
            End If
            Return ""
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        payForm.Attributes.Add("action", Settings.PayPalUrl)
        SaveTempRecord()
    End Sub

    Private Sub SaveTempRecord()

        If BookingController.BookingType = Booking.BookingAction.NormalBooking Then
            ' normal booking 
            BookingController.SaveTempAdRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, User.Identity.Name, BookingController.AdBookCart, TransactionType.PAYPAL)
        ElseIf BookingController.BookingType = Booking.BookingAction.Reschedule Then
            ' reschedule
            BookingController.SaveTempAdRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, User.Identity.Name, BookingController.AdBookCart, TransactionType.PAYPAL)
        ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
            ' bundled
            BundleController.SaveTempAdBundleRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, BundleController.BundleCart.Username, BundleController.BundleCart, TransactionType.PAYPAL)
        End If
    End Sub

End Class