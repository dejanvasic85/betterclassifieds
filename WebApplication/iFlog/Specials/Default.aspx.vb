Imports BetterclassifiedsCore

Partial Public Class _Default6
    Inherits System.Web.UI.Page

    Private _action As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _action = Request.QueryString("action")

        ' display the session issue 
        sessionLabel.Visible = (_action = "expired")

        If Not Page.IsPostBack Then
            DataBindCategories()
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        listSpecials.DataSource = GeneralController.GetCurrentSpecials(ddlSubCategory.SelectedValue)
        listSpecials.DataBind()
    End Sub

#Region "Category Binding"

    Private Sub DataBindCategories()
        ddlMainCategory.DataSource = CategoryController.GetMainParentCategories()
        ddlMainCategory.DataBind()
        If ddlMainCategory.Items.Count > 0 Then
            DataBindSubCategories(ddlMainCategory.Items(0).Value)
        End If
    End Sub

    Private Sub DataBindSubCategories(ByVal parentId As Integer)
        ddlSubCategory.DataSource = CategoryController.GetMainCategoriesByParent(parentId)
        ddlSubCategory.DataBind()
    End Sub

    Private Sub ddlMainCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMainCategory.SelectedIndexChanged
        DataBindSubCategories(ddlMainCategory.SelectedValue)
    End Sub

#End Region

End Class