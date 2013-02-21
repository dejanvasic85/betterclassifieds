Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BundleBooking
Imports BetterClassified.UI.WebPage

Partial Public Class _Default2
    Inherits BaseBookingPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim action As String = Request.QueryString("action")

            If (action = "successful") Then

                lblBookingSuccess.Text = "Your booking has been successful."

            ElseIf (action = "fail") Then

                lblBookingSuccess.Text = "Please try again. Contact us if problem persists."
                pnlHeader.CssClass = "red"
                lblHeader.Text = "FAIL!"
                lblSubHeader.Text = "Booking failed."

            ElseIf (action = "cancel") Then

                lblBookingSuccess.Text = "Successfully cancelled your booking."
                lblSubHeader.Text = "Booking cancelled"
            Else
                'display a blank page
                lblHeader.Text = ""
                lblSubHeader.Text = ""
            End If

            ' clear any bookings
            BookingController.ClearAdBooking()
            BookingController.ClearSpecialBooking()
            BundleController.ClearBundleBooking()

        End If
    End Sub

End Class