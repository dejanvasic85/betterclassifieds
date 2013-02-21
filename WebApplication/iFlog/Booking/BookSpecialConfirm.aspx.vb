Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified.UI.WebPage


Partial Public Class BookSpecialConfirm
    Inherits BaseBookingPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' complete check if the session object exists
        If BookingController.SpecialBookCart Is Nothing Then
            Response.Redirect(PageUrl.BookingStep_1)
        End If

        If (AdController.TempRecordExist(BookingController.SpecialBookCart.BookReference)) Then
            Response.Redirect(PageUrl.BookingStep_1)
        End If

        If BookingController.SpecialBookCart.TotalPrice = 0 Then
            Me.paymentPanel.Visible = False
        End If

        If Not Page.IsPostBack Then
            listSpecialBookingSummary.DataSource = BookingController.SpecialBookCartSummary
            listSpecialBookingSummary.DataBind()

            ' show the ad designs if they exist.
            If BookingController.SpecialBookCart.IsLineAd Then

                Dim image As String = ""
                If Not BookingController.SpecialBookCart.LineAdImage Is Nothing Then
                    image = BookingController.SpecialBookCart.LineAdImage.DocumentID
                End If
                radLineWindow.OpenerElementID = btnPreviewLine.ClientID
            End If

            ' Check if Special contains online Ad 
            If BookingController.SpecialBookCart.IsOnlineAd Then
                radOnlineWindow.OpenerElementID = btnPreviewOnline.ClientID
            End If


            ' Check if special booking contains Line Ad
            If (BookingController.SpecialBookCart.IsLineAd) Then
                Dim maxInserts As Integer = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ADBOOKING, _
                                                                         Utilities.Constants.CONST_KEY_Maximum_Insertions)
                ucxEditionDates.BindPaperEditions(PublicationController.PublicationEditionList(BookingController.SpecialBookCart.Publications, _
                                                                                               BookingController.SpecialBookCart.Insertions, _
                                                                                               BookingController.SpecialBookCart.StartDate))

                ucxEditionDates.Visible = True
            End If

        End If
    End Sub

    Private Sub btnPlaceAd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPlaceAd.Click

        ' complete check if the session object exists
        If BookingController.SpecialBookCart Is Nothing Then
            Response.Redirect(PageUrl.BookingStep_1)
        End If

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

            '' Code that will go straight to place the ad into the Records

            If BookingController.SpecialBookCart.TotalPrice = 0 Then
                BookingController.SpecialBookCart.BookingStatus = Controller.BookingStatus.BOOKED
                If BookingController.PlaceSpecialAd(BookingController.SpecialBookCart, TransactionType.FREEAD) Then
                    Global_asax.OnPayment.BeginInvoke(BookingController.SpecialBookCart.BookReference, Nothing, Nothing)
                    Response.Redirect("~/Booking/Default.aspx?action=successful")
                Else
                    Response.Redirect("~/Booking/Default.aspx?action=fail")
                End If
                Return
            End If

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

    Private Sub ucxEditionDates_GridPageIndexChanged() Handles ucxEditionDates.GridPageIndexChanged
        ucxEditionDates.BindPaperEditions(PublicationController.PublicationEditionList(BookingController.SpecialBookCart.Publications, _
                                                                                               BookingController.SpecialBookCart.Insertions, _
                                                                                               BookingController.SpecialBookCart.StartDate))
    End Sub
End Class