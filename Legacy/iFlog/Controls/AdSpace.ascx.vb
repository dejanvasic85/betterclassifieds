Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.WebAdvertising
Imports Paramount.Utility
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl

Partial Public Class AdSpace
    Inherits System.Web.UI.UserControl

    Private _location As LocationCode
    Public Property Location() As LocationCode
        Get
            Return _location
        End Get
        Set(ByVal value As LocationCode)
            _location = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' read the app setting to check if AD Spaces are enabled
            If System.Configuration.ConfigurationManager.AppSettings("WebAdSpaces") Then
                Using db As New AdSpaceController
                    Dim query = db.GetActiveAdSpaces(_location)
                    rptAds.DataSource = query
                    rptAds.DataBind()
                End Using
            End If
        End If
    End Sub

    Private Sub rptAds_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptAds.ItemDataBound

        Dim lnk As HyperLink = e.Item.FindControl("lnkAd")
        If e.Item.DataItem.SpaceType = AdSpaceType.Image Then
            'lnk.ImageUrl = PageUrl.DSLAdSpace + e.Item.DataItem.ImageUrl
            Dim param As New DslQueryParam(Request.QueryString)
            param.DocumentId = e.Item.DataItem.ImageUrl
            param.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
            param.Resolution = BetterclassifiedSetting.DslDefaultResolution
            lnk.ImageUrl = param.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
        ElseIf e.Item.DataItem.SpaceType = AdSpaceType.Text Then
            lnk.Text = e.Item.DataItem.DisplayText
        End If
    End Sub

End Class