Imports BetterclassifiedsCore

Partial Public Class Categories
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData(Nothing)
        End If
    End Sub

#Region "Databinding"

    Private Sub BindData(ByVal parentIndex As Nullable(Of Integer))
        ' bind the controls to the database
        lstParentCategories.DataSource = CategoryController.GetMainParentCategories()
        lstParentCategories.DataBind()

        If lstParentCategories.Items.Count > 0 Then
            If parentIndex > 0 Then
                lstParentCategories.SelectedIndex = parentIndex
                lstSubCategories.DataSource = CategoryController.GetMainCategoriesByParent(lstParentCategories.Items(parentIndex).Value)
                lstSubCategories.DataBind()
                lnkEditMc.NavigateUrl = "~/classified/ModalDialog/Edit_Category.aspx?categoryId=" + lstParentCategories.Items(parentIndex).Value + "&isParent=True"

            Else
                lstParentCategories.SelectedIndex = 0
                lstSubCategories.DataSource = CategoryController.GetMainCategoriesByParent(lstParentCategories.Items(0).Value)
                lstSubCategories.DataBind()
                lnkEditMc.NavigateUrl = "~/classified/ModalDialog/Edit_Category.aspx?categoryId=" + lstParentCategories.Items(0).Value + "&isParent=True"
            End If

            If lstSubCategories.Items.Count > 0 Then
                lstSubCategories.SelectedIndex = 0
                lnkEditSc.NavigateUrl = "~/classified/ModalDialog/Edit_category.aspx?categoryId=" + lstSubCategories.Items(0).Value + "&isParent=False"
            End If
        End If

    End Sub

    Private Sub lstParentCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstParentCategories.SelectedIndexChanged
        ' bind the child categories
        lstSubCategories.DataSource = CategoryController.GetMainCategoriesByParent(lstParentCategories.SelectedValue)
        lstSubCategories.DataBind()
        lnkEditMc.NavigateUrl = "~/classified/ModalDialog/Edit_Category.aspx?categoryId=" + lstParentCategories.SelectedValue + "&isParent=True"
    End Sub

    Private Sub lstSubCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSubCategories.SelectedIndexChanged
        lnkEditSc.NavigateUrl = "~/classified/ModalDialog/Edit_Category.aspx?categoryId=" + lstSubCategories.SelectedValue + "&isParent=False"
    End Sub

#End Region

#Region "Add"

    Private Sub btnAddMc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddMc.Click
        ' first check if the category with that name exists
        Try
            If CategoryController.Exists(txtMainCategoryTitle.Text) Then
                lblParentConfirm.Text = "Add Failed: " + txtMainCategoryTitle.Text + " already exists."
                lblParentConfirm.ForeColor = Drawing.Color.Red
            ElseIf txtMainCategoryTitle.Text = String.Empty Then
                lblParentConfirm.Text = "Add Failed: You didn't provide the Title for Category"
                lblParentConfirm.ForeColor = Drawing.Color.Red
            Else
                ' attempt to save the new category
                If CategoryController.CreateNewCategory(txtMainCategoryTitle.Text, Nothing) Then
                    lblParentConfirm.Text = "Successfully added '" + txtMainCategoryTitle.Text + "'"
                    lblParentConfirm.ForeColor = Drawing.Color.Green
                    BindData(Nothing)
                Else
                    lblParentConfirm.Text = "Add Failed: Please contact administrator."
                    lblParentConfirm.ForeColor = Drawing.Color.Red
                End If
            End If

        Catch ex As Exception
            lblParentConfirm.Text = "Add Failed: " + ex.Message
            lblParentConfirm.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Private Sub btnAddSc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddSc.Click
        ' first check if the category with that name exists
        Try
            If lstParentCategories.SelectedIndex >= 0 Then
                Dim index As Integer = lstParentCategories.SelectedIndex
                If CategoryController.Exists(txtSubCategoryTitle.Text, lstParentCategories.SelectedValue) Then
                    lblSubCategoryConfirm.Text = "Add Failed: " + txtSubCategoryTitle.Text + " already exists."
                    lblSubCategoryConfirm.ForeColor = Drawing.Color.Red
                ElseIf txtSubCategoryTitle.Text = String.Empty Then
                    Me.lblSubCategoryConfirm.Text = "Add Failed: Add Failed: You didn't provide the Title for Category."
                    Me.lblSubCategoryConfirm.ForeColor = Drawing.Color.Red
                Else
                    ' attempt to save the new category
                    If CategoryController.CreateNewCategory(txtSubCategoryTitle.Text, lstParentCategories.SelectedValue) Then
                        Me.lblSubCategoryConfirm.Text = "Successfully added '" + txtSubCategoryTitle.Text + "' to '" + lstParentCategories.SelectedItem.Text + "'"
                        Me.lblSubCategoryConfirm.ForeColor = Drawing.Color.Green
                        BindData(index)
                    Else
                        Me.lblSubCategoryConfirm.Text = "Add Failed: Please contact administrator."
                        Me.lblSubCategoryConfirm.ForeColor = Drawing.Color.Red
                    End If
                End If
            Else
                lblSubCategoryConfirm.Text = "Please select parent category first."
                Me.lblSubCategoryConfirm.ForeColor = Drawing.Color.Red
            End If
        Catch ex As Exception
            Me.lblSubCategoryConfirm.Text = "Add Failed: " + ex.Message
            Me.lblSubCategoryConfirm.ForeColor = Drawing.Color.Red
        End Try
    End Sub

#End Region

#Region "Deleting Data"

    Private Sub btnDeleteMc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteMc.Click

        If lstParentCategories.SelectedIndex >= 0 Then
            Dim title As String = lstParentCategories.SelectedItem.Text
            If CategoryController.DeleteParentCategory(lstParentCategories.SelectedValue) Then
                lblParentConfirm.Text = "Successfully deleted '" + title + "'."
                lblParentConfirm.ForeColor = Drawing.Color.Green
                BindData(Nothing)
            Else
                lblParentConfirm.Text = "Delete failed: please contact administrator."
                lblParentConfirm.ForeColor = Drawing.Color.Red
            End If
        Else
            lblParentConfirm.Text = "Please select parent category first."
            lblParentConfirm.ForeColor = Drawing.Color.Red
        End If

    End Sub

    Private Sub btnDeleteSc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteSc.Click
        Try
            If lstSubCategories.SelectedIndex >= 0 Then
                Dim title As String = lstSubCategories.SelectedItem.Text
                If CategoryController.DeleteSubCategory(lstSubCategories.SelectedValue) Then
                    lblSubCategoryConfirm.Text = "Successfully deleted '" + title + "'"
                    lblSubCategoryConfirm.ForeColor = Drawing.Color.Green
                    BindData(lstParentCategories.SelectedIndex)
                Else
                    lblSubCategoryConfirm.Text = "Delete failed: please contact administrator."
                    lblSubCategoryConfirm.ForeColor = Drawing.Color.Red
                End If
            End If
        Catch ex As Exception
            lblSubCategoryConfirm.Text = "Please select the category to delete first."
            lblSubCategoryConfirm.ForeColor = Drawing.Color.Red
        End Try
    End Sub

#End Region

    Private Sub lnkRefreshParents_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRefreshParents.Click
        lstParentCategories.DataSource = CategoryController.GetMainParentCategories()
        lstParentCategories.DataBind()
    End Sub

    Private Sub lnkRefreshSubCategories_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRefreshSubCategories.Click
        If lstParentCategories.SelectedIndex > -1 Then
            lstSubCategories.DataSource = CategoryController.GetMainCategoriesByParent(lstParentCategories.SelectedValue)
            lstSubCategories.DataBind()
        End If
    End Sub
End Class