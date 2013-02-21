Imports BetterclassifiedsCore.DataModel
Imports System.Drawing
Imports BetterClassified.UIController
Imports Telerik.Web.UI

Public Class ColourPalette
    Inherits System.Web.UI.Page

    Dim _controller As LineAdController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _controller = New LineAdController()
    End Sub

    Private Sub grdLineAdColours_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdLineAdColours.ItemCommand
        If (e.CommandName = "Delete") Then
            Dim lineColourId As Integer = Integer.Parse(e.CommandArgument.ToString)
            _controller.DisableLineAdColourCode(lineColourId)
        End If
    End Sub

    Private Sub grdLineAdColours_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdLineAdColours.ItemDataBound
        If (e.Item.ItemType = Telerik.Web.UI.GridItemType.Item Or
            e.Item.ItemType = Telerik.Web.UI.GridItemType.AlternatingItem) Then

            Dim div = TryCast(e.Item.FindControl("pnlColourDisplay"), Panel)
            If div IsNot Nothing Then
                div.BackColor = ColorTranslator.FromHtml(e.Item.DataItem.ColourCode)
            End If

        End If
    End Sub

    Protected Sub grdLineAdColours_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdLineAdColours.NeedDataSource
        grdLineAdColours.DataSource = _controller.GetLineAdColourCodes()
    End Sub

    Private Sub btnUpdateOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateOrder.Click
        ' Loop through each item and update it
        Dim lineAdColourOrders = New Dictionary(Of Integer, Integer)

        For Each item In grdLineAdColours.MasterTableView.Items
            If item.ItemType = GridItemType.Item Or item.ItemType = GridItemType.AlternatingItem Then
                Dim sortOrder As Integer = DirectCast(item.FindControl("txtSortOrder"), RadNumericTextBox).Value
                Dim lineAdColourId As Integer = Integer.Parse(DirectCast(item.FindControl("lblLineAdColourId"), Literal).Text)
                lineAdColourOrders.Add(lineAdColourId, sortOrder)
            End If
        Next

        ' Update all the colour sort order
        _controller.UpdateLineAdColourOrder(lineAdColourOrders)

        ' Bind the grid
        grdLineAdColours.Rebind()
    End Sub
End Class