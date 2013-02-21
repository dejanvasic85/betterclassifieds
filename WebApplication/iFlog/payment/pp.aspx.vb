﻿Imports BetterclassifiedsCore.ParameterAccess
Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BundleBooking
Imports Paramount.Betterclassified.Utilities.PayPal

Partial Public Class pp
    Inherits System.Web.UI.Page
    Public Shared ReadOnly Property Settings() As PayPalSettings
        Get
            Return ConfigurationManager.GetSection("paypal")
        End Get
    End Property

    Public Shared ReadOnly Property SuccessUrl() As String
        Get
            Return String.Format("{0}?id={1}&", Settings.SuccessUrl, BookingProcess.PaymentReferenceId)
        End Get
    End Property

    Public Shared ReadOnly Property NotifyUrl() As String
        Get
            Return String.Format("{0}?sessionid={1}&id={2}&tt={3}&totalCost={4}&", Settings.NotifyUrl, HttpContext.Current.Session.SessionID, BookingProcess.PaymentReferenceId, Common.Constants.PaymentOption.PayPal, Cost)
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
            ElseIf BookingController.BookingType = Booking.BookingAction.SpecialBooking Then
                If BookingController.SpecialBookCart.TotalPrice.HasValue Then
                    Return BookingController.SpecialBookCart.TotalPrice.Value.ToString("00.00")
                End If
            ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                Return BundleController.BundleCart.TotalPrice.ToString("00.00")
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
            ElseIf BookingController.BookingType = Booking.BookingAction.SpecialBooking Then
                Return String.Format("Booking Reference: {0}", BookingController.SpecialBookCart.BookReference)
            ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                Return String.Format("Booking Reference: {0}", BundleController.BundleCart.BookReference)
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
        ElseIf BookingController.BookingType = Booking.BookingAction.SpecialBooking Then
            ' special
            BookingController.SaveTempAdSpecialRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, User.Identity.Name, BookingController.SpecialBookCart, TransactionType.PAYPAL)
        ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
            ' bundled
            BundleController.SaveTempAdBundleRecord(BookingProcess.PaymentReferenceId, Cost, Session.SessionID, BundleController.BundleCart.Username, BundleController.BundleCart, TransactionType.PAYPAL)
        End If
    End Sub

End Class