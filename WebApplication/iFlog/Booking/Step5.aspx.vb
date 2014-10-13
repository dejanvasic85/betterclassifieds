Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified.UI.WebPage

Partial Public Class Step5
    Inherits BaseOnlineBookingPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.RedirectPermanent("~/Booking/Step/1")

        If BookingController.IsZeroValueTransaction Then
            Me.paymentPanel.Visible = False
        End If
        If Not Page.IsPostBack Then

            radOnlineWindow.OpenerElementID = btnPreviewOnline.ClientID

            ' perform the databinding on all the selections
            lstBookingSummary.DataSource = BookingController.GetBookSummary
            Me.DataBind()
        End If
    End Sub

    Private Sub ucxNavButtons_NextStep(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucxNavButtons.NextStep

        Dim pageValid As Boolean = True
        Dim errorList As New List(Of String)

        If chkConditions.Checked = False Then
            pageValid = False
            errorList.Add("You need to confirm the details before proceeding.")
        End If

        If chkConfirm.Checked = False Then
            pageValid = False
            errorList.Add("You need to confirm that you have read the terms and conditions before proceeding.")
        End If


        If pageValid Then
            'Zero value transaction
            If BookingController.IsZeroValueTransaction Then
                If BookingController.BookingType = Booking.BookingAction.NormalBooking Or BookingController.BookingType = Booking.BookingAction.Reschedule Then

                    BookingController.AdBookCart.BookingStatus = Controller.BookingStatus.BOOKED
                    If BookingController.PlaceAd(TransactionType.FREEAD) Then
                        Global_asax.OnPayment.BeginInvoke(BookingController.AdBookCart.BookReference, Nothing, Nothing)

                        'redirect to book successful page
                        Me.Response.Redirect("~/Booking/Default.aspx?action=successful")
                    End If
                End If
            End If

            ' set the booking process reference
            ParameterAccess.BookingProcess.PaymentReferenceId = Guid.NewGuid.ToString
            If paymentOption.SelectedValue = Common.Constants.PaymentOption.CreditCard Then
                BookingProcess.PaymentOption = Common.Constants.PaymentOption.CreditCard
                Response.Redirect(PageUrl.CreditCardPaymentPage)
            Else
                BookingProcess.PaymentOption = Common.Constants.PaymentOption.PayPal
                Response.Redirect(PageUrl.PaypalPaymentPage)
            End If
        Else
            ucxErrorList.ShowErrors(errorList)
        End If
    End Sub

End Class