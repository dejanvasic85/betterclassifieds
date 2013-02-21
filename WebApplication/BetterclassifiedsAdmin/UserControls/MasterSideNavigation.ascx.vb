Partial Public Class MasterSideNavigation
    Inherits System.Web.UI.UserControl
    Private _isActiveIndexSet As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Private Sub MyAccordion_ItemDataBound(ByVal sender As Object, ByVal e As AjaxControlToolkit.AccordionItemEventArgs) Handles MyAccordion.ItemDataBound
        If (_isActiveIndexSet) Then Return
        Dim item = CType(e.Item, SiteMapNode)
        If item IsNot Nothing And item.Provider.CurrentNode IsNot Nothing Then
            If item.Provider.CurrentNode.HasChildNodes Then
                MyAccordion.SelectedIndex = e.AccordionItem.DisplayIndex
                _isActiveIndexSet = True
            End If
        End If
    End Sub
End Class