Imports BetterclassifiedsCore

Partial Public Class Rates
    Inherits System.Web.UI.Page

    Dim _rateController As BetterClassified.UIController.RateController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _rateController = New BetterClassified.UIController.RateController()
        If Not Page.IsPostBack Then
            ' Load the categories
            DataBindMainCategories()
        End If
    End Sub

    Private Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearch.Click
        ' casual rates
        Dim rates = _rateController.GetRatesForCategory(ddlSubCategory.SelectedValue)
        'lstCasualRates.DataSource = GeneralController.RatecardsByCategoryId(ddlSubCategory.SelectedValue)
        lstCasualRates.DataSource = rates
        lstCasualRates.DataBind()
        pnlRates.Visible = rates.Count > 0
    End Sub

    Private Sub lstCasualRates_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles lstCasualRates.ItemCommand
        If e.CommandName = "Book" Then
            If e.CommandArgument.ToString.ToUpper.Trim = "ONLINE" Then
                General.StartBookingStep2(SystemAdType.ONLINE)
            Else
                'General.StartBookingStep2(SystemAdType.LINE)
                General.StartBundleBookingStep2() ' force to bundled
            End If
        End If
    End Sub

    Private Sub lstCasualRates_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles lstCasualRates.ItemDataBound
        'If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
        '    If e.Item.DataItem.Code.Trim() = SystemAdType.ONLINE.ToString Then
        '        Dim pnlRates As Panel = e.Item.FindControl("pnlRateDetails")
        '        pnlRates.Visible = False

        '        ' hide the main rate details
        '        TryCast(e.Item.FindControl("pnlRateDetails"), Panel).Visible = False
        '        ' show the online bundle price
        '        TryCast(e.Item.FindControl("pnlOnlineBundle"), Panel).Visible = True
        '    Else
        '        ' hide max charge if the value is at $0
        '        Dim pnlMaxCharge As Panel = TryCast(e.Item.FindControl("pnlMaxCharge"), Panel)
        '        If pnlMaxCharge IsNot Nothing Then
        '            If e.Item.DataItem.MaxCharge Is Nothing Then
        '                pnlMaxCharge.Visible = False
        '            ElseIf e.Item.DataItem.MaxCharge = 0 Then
        '                pnlMaxCharge.Visible = False
        '            End If
        '        End If
        '        ' do same for additional charge
        '        Dim pnlExtra As Panel = TryCast(e.Item.FindControl("pnlExtraCharge"), Panel)
        '        If pnlExtra IsNot Nothing Then
        '            If e.Item.DataItem.RatePerMeasureUnit Is Nothing Then
        '                pnlExtra.Visible = False
        '            ElseIf e.Item.DataItem.RatePerMeasureUnit = 0 Then
        '                pnlExtra.Visible = False
        '            End If
        '        End If

        '        ' show the online bundle price
        '        TryCast(e.Item.FindControl("pnlOnlineBundle"), Panel).Visible = False

        '    End If
        'End If
    End Sub

    Private Sub ddlMainCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMainCategory.SelectedIndexChanged
        ' Data Bind the sub categories
        DataBindSubCategories(ddlMainCategory.SelectedValue)
    End Sub

#Region "Category Binding"

    Public Sub DataBindMainCategories()
        ddlMainCategory.DataSource = CategoryController.GetMainParentCategories()
        ddlMainCategory.DataBind()
        If ddlMainCategory.Items.Count > 0 Then
            DataBindSubCategories(ddlMainCategory.Items(0).Value)
        End If
    End Sub

    Public Sub DataBindSubCategories(ByVal parentCategoryId As Integer)
        ddlSubCategory.DataSource = CategoryController.GetMainCategoriesByParent(parentCategoryId)
        ddlSubCategory.DataBind()
    End Sub

#End Region

End Class