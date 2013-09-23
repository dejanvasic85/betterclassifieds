Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified.UI.WebPage

Partial Public Class Step5
    Inherits BaseBookingPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If BookingController.IsZeroValueTransaction Then
            Me.paymentPanel.Visible = False
        End If
        If Not Page.IsPostBack Then
            If BookingController.AdBookCart Is Nothing Then
                Response.Redirect(PageUrl.BookingStep_1 + "?action=expired")
            End If

            If (AdController.TempRecordExist(BookingController.AdBookCart.BookReference)) Then
                Response.Redirect(PageUrl.BookingStep_1 + "?action=expired")
            End If

            Dim adType As String = BookingController.AdBookCart.MainAdType.Code

            If adType = SystemAdType.LINE.ToString Then
                Me.AdType = SystemAdType.LINE
                ' perform databinding on the edition list control
                Dim editionList = PublicationController.PublicationEditionList(BookingController.AdBookCart.PublicationList, _
                                                                               BookingController.AdBookCart.Insertions, _
                                                                               BookingController.AdBookCart.StartDate)

                ucxEditionDates.BindPaperEditions(editionList)

                '' preview the line ad.
                Dim image As String = ""
                If BookingController.AdBookCart.Ad.AdDesigns(0).LineAds(0).UsePhoto Then
                    image = BookingController.AdBookCart.Ad.AdDesigns(0).AdGraphics(0).DocumentID
                End If
                radLineWindow.OpenerElementID = btnPreviewLine.ClientID

            ElseIf adType = SystemAdType.ONLINE.ToString Then
                Me.AdType = SystemAdType.ONLINE
                radOnlineWindow.OpenerElementID = btnPreviewOnline.ClientID
            End If

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

            ' set the booking pcoess reference
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

    Public Property AdType() As SystemAdType
        Get
            Return ViewState("adType")
        End Get
        Set(ByVal value As SystemAdType)
            ViewState("adType") = value
        End Set
    End Property

    Private Sub ucxEditionDates_GridPageIndexChanged() Handles ucxEditionDates.GridPageIndexChanged
        Dim editionList = PublicationController.PublicationEditionList(BookingController.AdBookCart.PublicationList, _
                                                                              BookingController.AdBookCart.Insertions, _
                                                                              BookingController.AdBookCart.StartDate)

        ucxEditionDates.BindPaperEditions(editionList)
    End Sub
End Class