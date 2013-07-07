Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.Controller
Imports BetterclassifiedsCore.ParameterAccess
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility

Namespace Controls.Search
    Partial Public Class RecentOnlineAdList
        Inherits System.Web.UI.UserControl

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not Page.IsPostBack Then
                Dim value = GeneralController.GetRecentlyAddedAds(BookingStatus.BOOKED)
                listRecentlyListed.DataSource = value
                listRecentlyListed.DataBind()
            End If
        End Sub

        Private Sub listRecentlyListed_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listRecentlyListed.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim i As Image = e.Item.FindControl("imgDocument")
                If Not i Is Nothing Then
                    If e.Item.DataItem.DocumentId Is Nothing Then
                        i.ImageUrl = "~/OnlineAds/ad_placeholder.jpg"
                    Else
                        Dim param As New DslQueryParam(Request.QueryString)
                        param.DocumentId = e.Item.DataItem.DocumentID
                        param.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
                        param.Resolution = BetterclassifiedSetting.DslDefaultResolution
                        param.Width = BetterclassifiedSetting.DslThumbWidth
                        param.Height = BetterclassifiedSetting.DslThumbHeight
                        i.ImageUrl = param.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
                    End If
                End If
            End If
        End Sub

        Protected Sub CategoryClick(ByVal sender As Object, ByVal e As CommandEventArgs)
            If e Is Nothing Then
                Return
            End If
            OnlineSearchParameter.Clear()
            OnlineSearchParameter.Category = CInt(e.CommandArgument)
            Response.Redirect(PageUrl.OnlineAdSearch)

        End Sub
    End Class
End Namespace