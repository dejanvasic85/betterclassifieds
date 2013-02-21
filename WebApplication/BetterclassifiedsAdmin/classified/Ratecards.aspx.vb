Imports BetterClassified.UIController
Imports Paramount.Modules.Logging.UIController

Partial Public Class Ratecards
    Inherits AdminBasePage

    Private _rateController As RateController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _rateController = New RateController
        UserMessageControl.ClearMessage()
    End Sub

    'Private Sub grdCasualRates_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdCasualRates.RowDeleting
    '    ' handles the deleting action
    '    Dim hdnField As HiddenField = grdCasualRates.Rows(e.RowIndex).FindControl("hdnRatecardId")
    '    If hdnField IsNot Nothing Then
    '        Try
    '            _rateController.DeleteRateCard(hdnField.Value)
    '            Me.UserMessageControl.PrintUserMessage("Rate Card has been removed successfully")
    '        Catch ex As Exception
    '            Me.UserMessageControl.PrintException(ex)
    '        End Try
    '    End If
    'End Sub

    Private Sub btnCreateNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateNew.Click
        Response.Redirect(PageUrl.ClassifiedRates_SetupRatecard)
    End Sub

    Private Sub grdPriceList_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdPriceList.ItemCommand
        Try
            If e.CommandName = "Remove" Then
                Dim rateCardId = Convert.ToInt32(e.CommandArgument)
                _rateController.DeleteRateCard(rateCardId)
                UserMessageControl.PrintUserMessage("Rate Card has been removed successfully.", True)
                grdPriceList.Rebind()
            End If
        Catch ex As Exception
            UserMessageControl.PrintException(ex)
            ExceptionLogController(Of Exception).AuditException(ex)
        End Try
    End Sub

    Private Sub grdPriceList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdPriceList.NeedDataSource
        grdPriceList.DataSource = _rateController.SearchRateCards()
    End Sub
End Class