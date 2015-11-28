Imports BetterClassified.UIController

Public Class ManageThemes
    Inherits System.Web.UI.Page

    Private _controller As LineAdController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _controller = New LineAdController()
        ucxMessage.ClearMessage()

        If Not Page.IsPostBack Then
            dataBindThemes()
        End If

        If Not String.IsNullOrEmpty(Request.QueryString("msg")) Then
            Dim msg As UserMessageType = Request.QueryString("msg")
            ucxMessage.PrintUserMessage(msg)
        End If
    End Sub

    Private Sub dataBindThemes()
        lstThemes.DataSource = _controller.GetLineAdThemes()
        lstThemes.DataBind()
    End Sub

    Private Sub btnCreateNewTheme_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateNewTheme.Click
        Response.Redirect(PageUrl.ClassifiedThemes_CreateLineAdTheme)
    End Sub

    Private Sub lstThemes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles lstThemes.ItemCommand
        Dim lineAdThemeId As Integer = Integer.Parse(e.CommandArgument.ToString)
        If e.CommandName = "Disable" Then
            _controller.DisableLineAdTheme(lineAdThemeId)
            Response.Redirect(String.Format("{0}?msg={1}", PageUrl.ClassifiedThemes_ManageThemes, Integer.Parse(UserMessageType.ItemDeletedSuccessfully)))
        ElseIf e.CommandName = "Edit" Then
            Response.Redirect(String.Format("{0}?mode=edit&lineAdThemeId={1}", PageUrl.ClassifiedThemes_CreateLineAdTheme, lineAdThemeId))
        End If
    End Sub
End Class