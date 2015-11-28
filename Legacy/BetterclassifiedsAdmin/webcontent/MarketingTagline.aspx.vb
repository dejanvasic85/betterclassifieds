Imports BetterclassifiedsCore

Public Class MarketingTagline
    Inherits System.Web.UI.Page

    Private Const _pageId As String = "BundlePage3.aspx"
    Private Const _pageTitle As String = "Marketing Tagline"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadContentDetails()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            GeneralController.UpdateWebContent(_pageId, radEditor.Content, Membership.GetUser.UserName, _pageTitle)
            msgPanel.MessageText = "Web Content saved successfully."
            msgPanel.MessageType = Paramount.Common.UI.MessagePanelType.Success

            LoadContentDetails()
        Catch ex As Exception
            msgPanel.MessageText = "Web Content failed to save. Please contact Paramount IT for assistance."
            msgPanel.MessageType = Paramount.Common.UI.MessagePanelType.Error
        End Try
    End Sub

    Private Sub LoadContentDetails()
        Dim content = GeneralController.GetWebContentDetail(_pageId)
        If content IsNot Nothing Then
            lblUser.Text = content.LastModifiedUser
            lblDate.Text = String.Format("{0:dd-MMM-yyyy} {1}", content.LastModifiedDate, content.LastModifiedDate.Value.ToLongTimeString)
            radEditor.Content = content.WebContent
        End If
    End Sub
End Class