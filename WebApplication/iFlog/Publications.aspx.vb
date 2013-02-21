Imports BetterclassifiedsCore
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility

Partial Public Class Publications
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            listPublications.DataSource = PublicationController.EditionDeadlineList()
            listPublications.DataBind()
        End If
    End Sub

    Private Sub listPublications_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listPublications.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim pubImage As Image = DirectCast(e.Item.FindControl("imgPublication"), Image)
            If pubImage IsNot Nothing Then
                Dim param As New DslQueryParam(Request.QueryString) With {.DocumentId = e.Item.DataItem.ImageUrl, _
                                                                          .Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode), _
                                                                          .Resolution = BetterclassifiedSetting.DslDefaultResolution}
                pubImage.ImageUrl = param.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
                pubImage.ToolTip = e.Item.DataItem.Title
                pubImage.AlternateText = e.Item.DataItem.Title
            End If
        End If
    End Sub
End Class