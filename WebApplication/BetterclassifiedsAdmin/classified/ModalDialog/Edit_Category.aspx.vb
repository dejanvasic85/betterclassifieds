﻿Imports BetterclassifiedsCore

Partial Public Class Edit_Category
    Inherits System.Web.UI.Page

#Region "Form Variables"
    Private _categoryId As Integer
    Private _isParent As Boolean
#End Region

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _categoryId = Request.QueryString("categoryId")
        _isParent = Request.QueryString("isParent")
        lblCategoryMsg.Text = ""
    End Sub

#End Region

#Region "Databinding"

    Private Sub linqSourceCategory_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles linqSourceCategory.Selecting
        e.Result = CategoryController.GetMainCategoryById(_categoryId)
    End Sub

#End Region

#Region "Update"

    Private Sub btnUpdateCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateCategory.Click
        viewCategory.UpdateItem(True)
    End Sub

    Private Sub linqSourceCategory_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceUpdateEventArgs) Handles linqSourceCategory.Updating
        Try
            Dim category As DataModel.MainCategory = TryCast(e.NewObject, DataModel.MainCategory)
            If category IsNot Nothing Then
                CategoryController.UpdateCategoryDetails(category)
                lblCategoryMsg.Text = "Update Successful"
                lblCategoryMsg.ForeColor = Drawing.Color.Green
            Else
                lblCategoryMsg.Text = "Update Failed: " + e.Exception.Message
                lblCategoryMsg.ForeColor = Drawing.Color.Red
            End If
            ' cancel the original update by the linq data source

        Catch ex As Exception

            lblCategoryMsg.Text = "Update Failed: " + ex.Message
            lblCategoryMsg.ForeColor = Drawing.Color.Red

        Finally
            e.Cancel = True
        End Try
    End Sub

    Private Sub viewCategory_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdateEventArgs) Handles viewCategory.ItemUpdating
        ' get the template field values into the new values collection

        '' description
        Dim description As TextBox = viewCategory.FindControl("txtDescription")
        If description IsNot Nothing Then
            e.NewValues.Add("Description", description.Text)
        End If
    End Sub

#End Region

End Class