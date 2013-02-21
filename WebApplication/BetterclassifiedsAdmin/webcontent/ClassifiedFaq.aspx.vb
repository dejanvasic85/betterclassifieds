Imports BetterclassifiedsCore

Partial Public Class ClassifiedFaq
    Inherits System.Web.UI.Page

    Private Const _pageId As String = "FAQ.aspx"
    Private Const _pageTitle As String = "FAQ"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataBindDetails()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ' save the content
        Try
            GeneralController.UpdateWebContent(_pageId, radEditor.Content, Membership.GetUser.UserName, _pageTitle)
            msgPanel.MessageText = "Successfully saved FAQ page content."
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DataBindDetails()
        Dim content = GeneralController.GetWebContentDetail(_pageId)
        If content IsNot Nothing Then
            lblUser.Text = content.LastModifiedUser
            lblDate.Text = String.Format("{0:dd-MMM-yyyy} {1}", content.LastModifiedDate, content.LastModifiedDate.Value.ToLongTimeString)
            radEditor.Content = content.WebContent
            lblEnabled.Text = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_SYSTEM, Utilities.Constants.CONST_KEY_System_EnableFAQPage).ToString
        End If
    End Sub
End Class