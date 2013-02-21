Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BundleBooking

Partial Public Class BundlePriceSummary
    Inherits System.Web.UI.UserControl

    Private _showImage As Boolean
    Private _showHeading As Boolean

    Public Sub DataBindSummary(ByVal summaryList As List(Of Booking.PriceSummary), ByVal subTotal As Decimal, ByVal showImage As Boolean, ByVal showHeader As Boolean)
        ' set the form variables
        _showHeading = showHeader
        _showImage = showImage
        lblSummaryPrice.Text = String.Format("{0:C}", subTotal)
        lstPrices.DataSource = summaryList
        lstPrices.DataBind()
    End Sub

    Private Sub lstPrices_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles lstPrices.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Minimum Charge
            Dim rowMinCharge As HtmlControl = TryCast(e.Item.FindControl("trMinCharge"), HtmlControl)
            rowMinCharge.Visible = (e.Item.DataItem.MinimumCharge > 0)

            ' Regular Word Count (only show along with min charge
            If e.Item.DataItem.RegularWordCount > 0 Then
                ' get the label control and append the text onto it for the user
                Dim lblMinimumCharge As Label = TryCast(e.Item.FindControl("lblMinimumCharge"), Label)
                lblMinimumCharge.Text += " (includes " + e.Item.DataItem.RegularWordCount.ToString + " free words)"
            End If

            ' Line Ad
            Dim pnlLineAd As Panel = TryCast(e.Item.FindControl("pnlLineAd"), Panel)
            pnlLineAd.Visible = (e.Item.DataItem.AdType = SystemAdType.LINE)

            ' Online Ad
            ' Check if it's bundled with Line Ad Editions
            If e.Item.DataItem.AdType = SystemAdType.ONLINE Then
                If GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ONLINE_ADS, Utilities.Constants.CONST_KEY_Online_BundleWithPrint) = False Then
                    ' Do not show the entire Details
                    TryCast(e.Item.FindControl("divItemContainer"), HtmlControl).Visible = False
                    TryCast(e.Item.FindControl("lblOtherInfo"), Label).Visible = True
                End If
            End If

            ' Line Ad Show Heading
            TryCast(e.Item.FindControl("trShowHeading"), HtmlControl).Visible = _showHeading
            TryCast(e.Item.FindControl("trShowImage"), HtmlControl).Visible = _showImage

        End If

    End Sub
End Class