Imports BetterclassifiedsCore
''' <summary>
''' User Control that displays a summary of pricing information in a neat Data List.
''' </summary>
''' <remarks>This control relies on a PriceSummary object to be passed to the BindPriceSummary method
''' in order to display the pricing details.</remarks>
Partial Public Class PriceSummary
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub BindPriceSummary(ByVal priceSummary As List(Of BetterclassifiedsCore.Booking.PriceSummary), _
                                ByVal subTotal As Decimal, ByVal showImage As Boolean, ByVal showHeader As Boolean, _
                                ByVal type As SystemAdType)
        _ShowImage = showImage
        _ShowHeader = showHeader
        _AdType = type
        lstPrices.DataSource = priceSummary
        lstPrices.DataBind()
        lblSummaryPrice.Text = String.Format("{0:C}", subTotal)
    End Sub

    Private Sub lstPrices_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles lstPrices.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            ' fix the label so we display the minimum charge properly if it's larger than 0 - otherwise we hide it.
            Dim pnlMinCharge As HtmlControl = TryCast(e.Item.FindControl("divMinCharge"), HtmlControl)
            If e.Item.DataItem.MinimumCharge > 0 Then
                Dim lblCharge As Label = TryCast(e.Item.FindControl("lblMinimumCharge"), Label)
                lblCharge.Text = String.Format("{0:C}", e.Item.DataItem.MinimumCharge)
                If pnlMinCharge IsNot Nothing Then
                    If e.Item.DataItem.RegularWordCount > 0 Then
                        lblCharge.Text += " (includes " + e.Item.DataItem.RegularWordCount.ToString + " free words)"
                    End If
                End If
            Else
                pnlMinCharge.Visible = False
            End If

            ' add information about the maximum charge if needed
            If e.Item.DataItem.MaximumCharge > 0 Then
                Dim maxLabel As Literal = TryCast(e.Item.FindControl("lblMaxCharge"), Literal)
                If maxLabel IsNot Nothing Then
                    maxLabel.Text = "Paper Price is capped at " + String.Format("{0:C}", e.Item.DataItem.MaximumCharge)
                End If
            End If
        End If
    End Sub

#Region "Properties"

    Private _ShowImage As Boolean
    Private _ShowHeader As Boolean
    Private _AdType As SystemAdType

    Public Property ShowImage() As Boolean
        Get
            Return _ShowImage
        End Get
        Set(ByVal value As Boolean)
            _ShowImage = True
        End Set
    End Property

    Public Property ShowHeader() As Boolean
        Get
            Return _ShowHeader
        End Get
        Set(ByVal value As Boolean)
            _ShowHeader = value
        End Set
    End Property

    Public Property AdType() As SystemAdType
        Get
            Return _AdType
        End Get
        Set(ByVal value As SystemAdType)
            _AdType = value
        End Set
    End Property

#End Region

End Class