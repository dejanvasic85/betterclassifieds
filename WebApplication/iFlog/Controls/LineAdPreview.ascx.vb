Imports BetterclassifiedsCore
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility


Partial Public Class LineAdPreview
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub BindLineAd(ByVal lineAd As DataModel.LineAd, ByVal imageId As String)
        If Not String.IsNullOrEmpty(imageId) Then
            divImage.Visible = True
            ' imgLineAd.ImageUrl = "~/dsl/document.ashx?id=" + imageId
            ' Width="150px" Height="142px"
            Dim query As New DslQueryParam(Request.QueryString)
            query.DocumentId = imageId
            query.Height = 142
            query.Width = 150
            query.Resolution = BetterclassifiedSetting.DslDefaultResolution
            query.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
            imgLineAd.ImageUrl = query.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
        Else
            divImage.Visible = False
        End If

        If lineAd.UseBoldHeader Then
            divHeader.Visible = True
            lblHeading.Text = lineAd.AdHeader.ToUpper
        Else
            divHeader.Visible = False
        End If

        lblAdText.Text = lineAd.AdText
    End Sub

End Class