Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BundleBooking
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified.UI.WebPage

Partial Public Class BundlePage5
    Inherits BaseBookingPage

    Private _bundleController As BundleController

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' check if the bundle booking cart is expired
        If BundleController.BundleCart Is Nothing Then
            Response.Redirect(PageUrl.BookingStep_1 + "?action=expired")
        End If

        'make sure ad has not been saved in temp booking
        If (AdController.TempRecordExist(BundleController.BundleCart.BookReference)) Then
            Response.Redirect(PageUrl.BookingStep_1 + "?action=expired")
        End If
        ' initiate the global variables
        _bundleController = New BundleController()

        If Not Page.IsPostBack Then
            If BundleController.BundleCart IsNot Nothing Then
                ' bind the ad details
                radOnlineWindow.OpenerElementID = btnPreviewOnline.ClientID
                radLineWindow.OpenerElementID = btnPreviewLine.ClientID
                DataBindEditions()
                DataBindOrderDetails()
                ' hide the payment panel if there is nothing to pay
                paymentPanel.Visible = (BundleController.BundleCart.TotalPrice > 0)
            End If
        End If

    End Sub

#End Region

#Region "DataBinding"

    Private Sub DataBindEditions()
        ' generate the selected editions
        Dim pubEditions As List(Of Booking.EditionList) = _bundleController.GetPublicationEditions(BundleController.BundleCart.PublicationList, BundleController.BundleCart.Insertions, BundleController.BundleCart.FirstEdition)
        ' bind to the UI
        ucxEditionDates.BindPaperEditions(pubEditions)
    End Sub

    Private Sub DataBindOrderDetails()
        lstBookingSummary.DataSource = _bundleController.GetBindableBookCart(BundleController.BundleCart)
        lstBookingSummary.DataBind()
    End Sub

#End Region

#Region "Validation"

    Private Function ValidatePage() As Boolean
        Dim errors As New List(Of String)
        ' check their detail confirmation
        If chkConditions.Checked = False Then
            errors.Add("You need to confirm the details before proceeding.")
        End If
        ' check their condition confirmation
        If chkConditions.Checked = False Then
            errors.Add("You need to confirm that you have read the terms and conditions before proceeding.")
        End If
        ' show errors (if any)
        ucxErrorList.ShowErrors(errors)
        ' return true (valid page) if there's no errors
        Return errors.Count = 0
    End Function

#End Region

#Region "Navigation"

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If ValidatePage() Then
            ' zero value transaction
            If BundleController.BundleCart.TotalPrice = 0 Then
                If GeneralRoutine.PlaceBundledAd(BundleController.BundleCart, TransactionType.FREEAD, Controller.BookingStatus.BOOKED) Then
                    Global_asax.OnPayment.BeginInvoke(BundleController.BundleCart.BookReference, Nothing, Nothing)
                    Response.Redirect("~/Booking/Default.aspx?action=successful")
                End If
            End If
            ' otherwise we go through the payment process
            ParameterAccess.BookingProcess.PaymentReferenceId = Guid.NewGuid.ToString
            If paymentOption.SelectedValue = Common.Constants.PaymentOption.CreditCard Then
                BookingProcess.PaymentOption = Common.Constants.PaymentOption.CreditCard
                Response.Redirect(PageUrl.CreditCardPaymentPage)
            Else
                BookingProcess.PaymentOption = Common.Constants.PaymentOption.PayPal
                Response.Redirect(PageUrl.PaypalPaymentPage)
            End If
        End If
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        ' direct back to 4th path (scheduling)
        Response.Redirect(PageUrl.BookingBundle_4)
    End Sub

#End Region

End Class