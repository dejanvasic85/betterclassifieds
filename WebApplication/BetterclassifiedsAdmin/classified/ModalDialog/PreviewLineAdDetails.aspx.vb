Imports BetterclassifiedsCore
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.DSL.UIController
Imports Paramount.Utility

Public Class PreviewLineAdDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ad As New DataModel.LineAd

        ' then load the ad details from the database by the ad design
        ad = AdController.GetLineAdById(Request.QueryString("id"))
        Dim gr = AdController.GetAdGraphics(ad.AdDesignId).FirstOrDefault
        
        ' Bind the Properties to the Line Ad Preview
        lineAdView.AdText = ad.AdText
        lineAdView.HeaderText = ad.AdHeader
        lineAdView.IsHeaderVisible = Not String.IsNullOrEmpty(lineAdView.HeaderText)

        If ad.IsSuperBoldHeading Then
            lineAdView.IsHeadingSuperBold = ad.IsSuperBoldHeading
        End If

        If Not String.IsNullOrEmpty(ad.BoldHeadingColourCode) Then
            lineAdView.HeaderColourCode = ad.BoldHeadingColourCode
        End If

        If Not String.IsNullOrEmpty(ad.BorderColourCode) Then
            lineAdView.BorderColourCode = ad.BorderColourCode
        End If

        If Not String.IsNullOrEmpty(ad.BackgroundColourCode) Then
            lineAdView.BackgroundColourCode = ad.BackgroundColourCode
        End If

        ' Graphic
        If gr Is Nothing Then
            lineAdView.IsImageVisible = False
        Else
            lineAdView.IsImageVisible = True
            Dim query As New DslQueryParam(Request.QueryString)
            query.DocumentId = gr.DocumentID
            query.Height = 142
            query.Width = 150
            query.Resolution = BetterclassifiedSetting.DslDefaultResolution
            query.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
            lineAdView.ImageUrl = query.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
        End If

    End Sub

End Class