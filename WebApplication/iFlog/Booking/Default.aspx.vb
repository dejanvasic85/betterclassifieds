﻿Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BundleBooking
Imports BetterClassified.UI.WebPage

Partial Public Class _Default2
    Inherits BaseBookingPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.RedirectPermanent("~/Booking/Step/1")
        If Not Page.IsPostBack Then


            Dim bookingId As Integer = BookingController.AdBookCart.AdId

            ' clear any bookings
            BookingController.ClearAdBooking()
            BundleController.ClearBundleBooking()

            Dim action As String = Request.QueryString("action")

            If (action = "successful") Then

                ' lblBookingSuccess.Text = "Your booking has been successful."
                ' Todo - Find out how to fetch the current ID of the completed booking
                ' Todo - Append ID to the route /{}

                Response.RedirectPermanent(String.Format("~/Booking/Success/{0}", bookingId))


            ElseIf (action = "fail") Then

                lblBookingSuccess.Text = "Please try again. Contact us if problem persists."
                pnlHeader.CssClass = "red"
                lblHeader.Text = "FAIL!"

            ElseIf (action = "cancel") Then

                lblBookingSuccess.Text = "Successfully cancelled your booking."
            Else
                'display a blank page
                lblHeader.Text = ""
            End If



        End If
    End Sub

End Class