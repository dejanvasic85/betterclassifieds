Imports BetterclassifiedsCore
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility

Partial Public Class Publications
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataBindPublications()
        End If
    End Sub

#Region "Binding"

    Private Sub grdPublications_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPublications.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            ' adjust the description so it doesn't give us the entire data in the grid.
            If e.Row.DataItem.Description IsNot Nothing Then
                Dim desc As Label = e.Row.FindControl("lblDescription")
                If e.Row.DataItem.Description.ToString.Length > 25 Then
                    desc.Text = e.Row.DataItem.Description.ToString.Substring(0, 25) + "..."
                End If
            End If

            If e.Row.DataItem.PublicationTypeId IsNot Nothing Then
                Dim type As Label = e.Row.FindControl("lblPublicationType")
                type.Text = PublicationController.GetPublicationTypeById(e.Row.DataItem.PublicationTypeId).Title
            End If

            ' Display the image using the Dsl Query
            Dim imgPublication As Image = TryCast(e.Row.FindControl("imgPublication"), Image)
            If imgPublication IsNot Nothing Then
                Dim documentId As String = e.Row.DataItem.ImageUrl
                If Not documentId = String.Empty Then
                    Dim query As New DslQueryParam(Request.QueryString)
                    query.DocumentId = documentId
                    query.Height = BetterclassifiedSetting.DslThumbHeight
                    query.Width = BetterclassifiedSetting.DslThumbWidth
                    query.Resolution = BetterclassifiedSetting.DslDefaultResolution
                    query.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
                    imgPublication.ImageUrl = query.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
                Else
                    imgPublication.Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub DataBindPublications()
        grdPublications.DataSource = PublicationController.GetAllPapers()
        grdPublications.DataBind()
    End Sub

    Private Sub btnRefreshList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshList.Click
        DataBindPublications()
    End Sub

#End Region

    Private Sub grdPublications_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdPublications.RowDeleting
        ' handles the deleting action
        Dim hdnField As HiddenField = grdPublications.Rows(e.RowIndex).FindControl("hdnPublicationId")
        Dim title As HiddenField = grdPublications.Rows(e.RowIndex).FindControl("hdnTitle")

        If hdnField IsNot Nothing Then
            Try
                If PublicationController.DeletePublication(hdnField.Value) Then
                    lblDeleteSuccess.Text = "Successfully deleted <b>" + title.Value + "</b>"
                    lblDeleteSuccess.ForeColor = Drawing.Color.Green
                    DataBindPublications()
                End If
            Catch ex As Exception
                Me.lblDeleteSuccess.Text = "An error occurred when trying to delete. Error: " + ex.Message
                lblDeleteSuccess.ForeColor = Drawing.Color.Red
            End Try
        End If
    End Sub
End Class