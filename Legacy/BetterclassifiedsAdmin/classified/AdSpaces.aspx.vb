Imports BetterclassifiedsCore.WebAdvertising
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility

Partial Public Class AdSpaces
    Inherits System.Web.UI.Page

    Private _settindId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _settindId = Request.QueryString("settingId")

        If Not Page.IsPostBack Then
            If System.Configuration.ConfigurationManager.AppSettings("WebAdSpaces") = False Then
                pnlNotAvailable.Visible = True
                pnlNavigationDiv.Visible = False
                pnlConfiguration.Visible = False
            Else
                ' todo - first check that this setting is available for this client
                pnlNotAvailable.Visible = False
                pnlNavigationDiv.Visible = (_settindId = 0)
                pnlConfiguration.Visible = (_settindId > 0)

                If (_settindId > 0) Then
                    ' call method to databind the ads for display
                    DataBindAds()
                Else
                    Using ctrl As New AdSpaceController
                        rptNavigation.DataSource = ctrl.GetAdSpaceSettings
                        rptNavigation.DataBind()
                    End Using
                End If
                ' set the "New" button navigation url
                lnkCreate.NavigateUrl = "~/classified/ModalDialog/SetupAdSpace.aspx?mode=1&spaceId=0&settingId=" + _settindId.ToString
            End If
        End If
    End Sub

    Private Sub DataBindAds()
        Using ctrl As New AdSpaceController
            Dim adSpaces = ctrl.GetAdSpaces(_settindId)
            grdAds.DataSource = adSpaces
            grdAds.DataBind()
        End Using
    End Sub

    Private Sub grdAds_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAds.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.DataItem.SpaceType = AdSpaceType.Image Then
                Dim documentId As String = e.Row.DataItem.ImageUrl
                Dim query As New DslQueryParam(Request.QueryString)
                query.DocumentId = documentId
                query.Height = BetterclassifiedSetting.DslThumbHeight
                query.Width = BetterclassifiedSetting.DslThumbWidth
                query.Resolution = BetterclassifiedSetting.DslDefaultResolution
                query.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
                Dim img As New Image
                img.ImageUrl = query.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
                e.Row.Cells(5).Controls.Add(img)
            ElseIf e.Row.DataItem.SpaceType = AdSpaceType.Text Then
                e.Row.Cells(5).Text = e.Row.DataItem.DisplayText
            End If

            ' active/disabled
            Dim aImg As New Image
            If e.Row.DataItem.Active = True Then
                aImg.ImageUrl = "~/App_Themes/blue/Images/webdev-ok.png"
                aImg.ToolTip = "Enabled"
            ElseIf e.Row.DataItem.Active = False Then
                aImg.ImageUrl = "~/App_Themes/blue/Images/webdev-remove.png"
                aImg.ToolTip = "Disabled"
            End If
            aImg.Height = 20
            aImg.Width = 20
            e.Row.Cells(7).Controls.Add(aImg)

        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        DataBindAds()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        For Each row As GridViewRow In grdAds.Rows
            If DirectCast(row.FindControl("chkRows"), CheckBox).Checked Then
                Dim id As Integer = DirectCast(row.FindControl("hdnId"), HiddenField).Value
                Using db As New AdSpaceController
                    db.DeleteWebSpace(id)
                End Using
            End If
        Next
        DataBindAds()
    End Sub

    Private Sub btnDisable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisable.Click
        For Each row As GridViewRow In grdAds.Rows
            If DirectCast(row.FindControl("chkRows"), CheckBox).Checked Then
                Dim id As Integer = DirectCast(row.FindControl("hdnId"), HiddenField).Value
                Using db As New AdSpaceController
                    db.UpdateStatus(id, False)
                End Using
            End If
        Next
        DataBindAds()
    End Sub

    Private Sub btnEnable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnable.Click
        For Each row As GridViewRow In grdAds.Rows
            If DirectCast(row.FindControl("chkRows"), CheckBox).Checked Then
                Dim id As Integer = DirectCast(row.FindControl("hdnId"), HiddenField).Value
                Using db As New AdSpaceController
                    db.UpdateStatus(id, True)
                End Using
            End If
        Next
        DataBindAds()
    End Sub
End Class