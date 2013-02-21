Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.UI
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.ApplicationBlock.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility

Partial Public Class _Default1
    Inherits BaseMasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim papers = PublicationController.GetPublications(True)
            rptPublicationsTop.DataSource = papers.OrderByDescending(Function(i) i.SortOrder)
            rptPublicationsTop.DataBind()
            rptPublicationsBottom.DataSource = papers
            rptPublicationsBottom.DataBind()
            lnkFAQ.Visible = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_SYSTEM, Utilities.Constants.CONST_KEY_System_EnableFAQPage)

            Dim environment = ConfigSettingReader.GetConfigurationContext()
            If Not environment = "LIVE" Then
                lblVersion.Text = String.Format(" - Version : {0}", BetterclassifiedSetting.Version)
            End If
        End If
    End Sub

    Private Sub rptPublicationsTop_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptPublicationsTop.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim paperImg As Image = DirectCast(e.Item.FindControl("imgPublication"), Image)
            paperImg.ToolTip = e.Item.DataItem.Title
            If Not String.IsNullOrEmpty(e.Item.DataItem.ImageUrl) Then
                ' Render image with the DSL url handler
                Dim param As New DslQueryParam(Request.QueryString)

                param.DocumentId = e.Item.DataItem.ImageUrl
                param.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
                param.Resolution = BetterclassifiedSetting.DslDefaultResolution
                param.Height = 18 ' max height 18
                param.Width = 80
                paperImg.ImageUrl = param.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
            End If
        End If
    End Sub
End Class