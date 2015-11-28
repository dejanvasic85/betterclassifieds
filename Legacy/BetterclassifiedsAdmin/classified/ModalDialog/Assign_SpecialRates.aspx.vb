Imports Telerik.Web.UI
Imports BetterclassifiedsCore

Partial Public Class AssignSpecialRatesToCategories
    Inherits System.Web.UI.Page

    Private _specialRateId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _specialRateId = Integer.Parse(Request.QueryString("specialRateId"))
    End Sub

    Private Sub CategoryTree_NodeCheck(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles CategoryTree.NodeCheck
        ' todo - select all child nodes if parent was checked
        For Each n As RadTreeNode In e.Node.Nodes
            n.Checked = e.Node.Checked
        Next
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            ' Get the selected Publications
            Dim publications As New List(Of Integer)
            For Each pubItem As ListItem In chkList.Items
                If pubItem.Selected Then
                    publications.Add(Integer.Parse(pubItem.Value))
                End If
            Next

            Dim categories As New List(Of Integer)
            For Each n As RadTreeNode In CategoryTree.CheckedNodes
                If n.Level = 1 And n.Checked Then
                    categories.Add(n.Value)
                End If
            Next

            PublicationController.AssignSpecialRate(publications, categories, _specialRateId, chkRemoveCurrentSpecials.Checked)

            ' Print success message
            lblMessage.Text = "Successfully assigned special rate."
            lblMessage.CssClass = "message-success"
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.CssClass = "message-fail"
        End Try
    End Sub

    Private Sub lnqSourcePublications_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles lnqSourcePublications.Selecting
        e.Result = PublicationController.GetAllPapers
    End Sub

    Private Sub lnqSourceCategories_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles lnqSourceCategories.Selecting
        e.Result = CategoryController.GetAllMainCategories
    End Sub

End Class