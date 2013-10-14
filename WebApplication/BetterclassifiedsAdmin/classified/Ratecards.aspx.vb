Imports BetterClassified.UIController

Partial Public Class Ratecards
    Inherits AdminBasePage

    Private _rateController As RateController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _rateController = New RateController
        UserMessageControl.ClearMessage()
    End Sub

    Private Sub btnCreateNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateNew.Click
        Response.Redirect(PageUrl.ClassifiedRates_SetupRatecard)
    End Sub

    Private Sub grdPriceList_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdPriceList.ItemCommand
        If e.CommandName = "Remove" Then
            Dim rateCardId = Convert.ToInt32(e.CommandArgument)
            _rateController.DeleteRateCard(rateCardId)
            UserMessageControl.PrintUserMessage("Rate Card has been removed successfully.", True)
            grdPriceList.Rebind()
        End If
    End Sub

    Private Sub grdPriceList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdPriceList.NeedDataSource
        grdPriceList.DataSource = _rateController.SearchRateCards()
    End Sub
End Class