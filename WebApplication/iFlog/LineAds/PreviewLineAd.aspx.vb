Imports BetterclassifiedsCore
Imports Paramount.Utility.Dsl
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility

Partial Public Class PreviewLineAd
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ad As New DataModel.LineAd
        Dim adGraphicId As String = String.Empty
        Dim gr As List(Of DataModel.AdGraphic)

        Select Case Request.QueryString("viewType").ToString.ToLower
            Case "db"
                ' then load the ad details from the database by the ad design
                ad = AdController.GetLineAd(Request.QueryString("id"))
                gr = AdController.GetAdGraphics(ad.AdDesignId)
                If gr.Count > 0 Then
                    adGraphicId = gr(0).DocumentID
                End If
            Case "session"
                If BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                    ' grab the session values from the booking cart
                    ad = BundleBooking.BundleController.BundleCart.LineAd
                    If BundleBooking.BundleController.BundleCart.LineAdGraphic IsNot Nothing Then
                        adGraphicId = BundleBooking.BundleController.BundleCart.LineAdGraphic.DocumentID
                    End If
                Else
                    ' otherwise the booking details should be just in the ad book cart
                    If BookingController.AdBookCart.Ad.AdDesigns(0).LineAds(0) IsNot Nothing Then
                        ad = BookingController.AdBookCart.Ad.AdDesigns(0).LineAds(0)
                        If ad.AdDesign.AdGraphics.Count > 0 Then

                        End If
                    End If
                End If

        End Select

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
        If String.IsNullOrEmpty(adGraphicId) Then
            lineAdView.IsImageVisible = False
        Else
            lineAdView.IsImageVisible = True
            Dim query As New DslQueryParam(Request.QueryString)
            query.DocumentId = adGraphicId
            query.Height = 142
            query.Width = 150
            query.Resolution = BetterclassifiedSetting.DslDefaultResolution
            query.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
            lineAdView.ImageUrl = query.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
        End If

    End Sub

End Class