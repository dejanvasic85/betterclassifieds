Imports BetterclassifiedsCore

Partial Public Class PublicationCategories
    Inherits System.Web.UI.Page

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' databind the lists
            ddlPublications.DataSource = PublicationController.GetAllPapers()
            ddlPublications.DataBind()

            If ddlPublications.Items.Count > 0 Then
                DatabindCategories(ddlPublications.Items(0).Value)
            End If
        End If
        lblParentConfirm.Text = ""
        lblParentConfirm.ForeColor = Drawing.Color.Red
        lblSubCategoryConfirm.Text = ""
        lblSubCategoryConfirm.ForeColor = Drawing.Color.Red
    End Sub

#End Region

#Region "Databinding"

    Private Sub DatabindCategories(ByVal publicationId As Integer)
        ' bind the parent categories
        lstParentCategories.DataSource = PublicationController.GetPublicationCategories(publicationId, Nothing)
        lstParentCategories.DataBind()

        If lstParentCategories.Items.Count > 0 Then
            lstSubCategories.DataSource = PublicationController.GetPublicationCategories(publicationId, _
                                                                                         lstParentCategories.Items(0).Value)
        End If

        Me.DataBind()
    End Sub

    Private Sub lstParentCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstParentCategories.SelectedIndexChanged
        lstSubCategories.DataSource = PublicationController.GetPublicationCategories(ddlPublications.SelectedValue, _
                                                                                     lstParentCategories.SelectedValue)

        lnkCreateSubCategory.NavigateUrl = String.Format("~/classified/ModalDialog/Create_PublicationCategory.aspx?isParent={0}&parentId={1}&publicationId={2}", _
                                                         "false", lstParentCategories.SelectedValue, ddlPublications.SelectedValue)

        ' set the url for the edit parent category
        lnkEdit.NavigateUrl = String.Format("~/classified/ModalDialog/Edit_PublicationCategory.aspx?id={0}&isParent={1}", _
                                            lstParentCategories.SelectedValue, "true")
        Me.DataBind()
    End Sub

    Private Sub ddlPublications_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPublications.SelectedIndexChanged
        DatabindCategories(ddlPublications.SelectedValue)
    End Sub

    Private Sub lnkRefreshParents_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRefreshParents.Click
        DatabindCategories(ddlPublications.SelectedValue)
    End Sub

    Private Sub lnkRefreshSubCategories_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRefreshSubCategories.Click
        If lstParentCategories.SelectedIndex > -1 Then
            lstSubCategories.DataSource = PublicationController.GetPublicationCategories(ddlPublications.SelectedValue, _
                                                                                                 lstParentCategories.SelectedValue)
            lstSubCategories.DataBind()
        End If
    End Sub

    Private Sub lnkCreate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCreate.Load
        Dim publicationId As Integer = ddlPublications.SelectedValue

        lnkCreate.NavigateUrl = String.Format("~/classified/ModalDialog/Create_PublicationCategory.aspx?publicationId={0}&isParent={1}", _
                                             publicationId.ToString(), "true")
    End Sub

#End Region

#Region "Delete"

    Private Sub btnDeleteSc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteSc.Click
        Try
            If lstSubCategories.SelectedIndex > -1 Then
                If PublicationController.DeletePublicationCategory(lstSubCategories.SelectedValue) Then
                    lblSubCategoryConfirm.Text = "Successfully Deleted Category."
                    lblSubCategoryConfirm.ForeColor = Drawing.Color.Green

                    DatabindCategories(ddlPublications.SelectedValue)
                End If
            Else
                lblSubCategoryConfirm.Text = "Please select a category to delete."
            End If
        Catch ex As Exception
            lblSubCategoryConfirm.Text = "Delete Failed: " + ex.Message
        End Try
    End Sub

    Private Sub btnDeleteMc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteMc.Click
        Try
            If lstParentCategories.SelectedIndex > -1 Then
                Dim title As String = lstParentCategories.SelectedItem.Text
                If PublicationController.DeletePublicationCategory(lstParentCategories.SelectedValue) Then
                    lblParentConfirm.Text = "Successfully deleted '" + title + "'."
                    lblParentConfirm.ForeColor = Drawing.Color.Green
                    DatabindCategories(ddlPublications.SelectedValue)
                Else
                    lblParentConfirm.Text = "Delete failed. Please contact system administrator."
                End If
            Else
                lblParentConfirm.Text = "Please select a category to delete."
            End If
        Catch ex As Exception
            lblParentConfirm.Text = "Delete Failed: " + ex.Message
        End Try
    End Sub

#End Region

    Private Sub lstSubCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSubCategories.SelectedIndexChanged
        lnkEditSub.NavigateUrl = String.Format("~/classified/ModalDialog/Edit_PublicationCategory.aspx?id={0}&isParent={1}", _
                                                        lstSubCategories.SelectedValue, "false")
    End Sub
End Class