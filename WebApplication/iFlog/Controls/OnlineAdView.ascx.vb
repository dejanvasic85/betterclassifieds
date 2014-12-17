Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified
Imports Microsoft.Practices.Unity
Imports Paramount.Betterclassifieds.Business

Partial Public Class OnlineAdView
    Inherits System.Web.UI.UserControl

    Const MainCategoryViewState = "MainCategoryViewState"
    Const SubCategoryViewState = "SubCategoryViewState"

    Dim configSettings As IClientConfig

    Public Sub OnlineAdView()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If _preview = True Then
            mainHeaderItemPageIDHits.Visible = False
            divSitemap.Visible = False
        End If
    End Sub

    Private _preview As Boolean
    Public Property PreviewOnly() As Boolean
        Get
            Return _preview
        End Get
        Set(ByVal value As Boolean)
            _preview = value
        End Set
    End Property

    Public Property MainCategoryId() As Integer
        Get
            Return CInt(ViewState(MainCategoryViewState))
        End Get
        Private Set(ByVal value As Integer)
            ViewState(MainCategoryViewState) = value
        End Set
    End Property

    Public Property SubCategoryId() As Integer
        Get
            Return CInt(ViewState(SubCategoryViewState))
        End Get
        Private Set(ByVal value As Integer)
            ViewState(SubCategoryViewState) = value
        End Set
    End Property

    Public Sub BindOnlineAd(ByVal onlineAd As BusinessEntities.OnlineAdEntity)

        With onlineAd
            lblHeading.Text = .Heading
            ' html text
            If onlineAd.OnlineAdId > 0 Then
                onlineAdFrame.Src = ResolveUrl(String.Format("~/OnlineAds/BodyView.aspx?type=db&id={0}", onlineAd.OnlineAdId))
            Else
                onlineAdFrame.Src = ResolveUrl("~/OnlineAds/BodyView.aspx?type=session")
            End If
            lblLocation.Text = .LocationValue
            lblArea.Text = .AreaValue
            lblNumOfViews.Text = .NumOfViews.ToString
            lblDatePosted.Text = String.Format("{0:D}", .DatePosted)
            If .AdBookingId.HasValue Then
                lblIFlogID.Text = .AdBookingId
                ' set up the sitemap for this ad
                lblID.Text = .AdBookingId
            End If

            If .ContactName = "" Or .ContactName = "Private" Then
                objContactName.Visible = False
            Else
                lblContactName.Text = .ContactName
            End If


            litContactEmail.Text = .ContactEmail
            litContactPhone.Text = .ContactPhone

            objContactPhone.Visible = Not String.IsNullOrEmpty(.ContactPhone)
            objContactEmail.Visible = Not String.IsNullOrEmpty(.ContactEmail)

            objPrice.Visible = onlineAd.Price > 0
            lblPrice.Text = String.Format("{0:C}", .Price)

            MainCategoryId = .ParentCategory.MainCategoryId
            lnkCategory.Text = .ParentCategory.Title

            SubCategoryId = .SubCategory.MainCategoryId.ToString
            lnkSubCategory.Text = .SubCategory.Title

            paramountGallery.ImageList = onlineAd.ImageList

            ' Inject meta tags for facebook integration
            configSettings = BetterClassified.Unity.DefaultContainer.Resolve(Of IClientConfig)()
            Me.Page.Header.AddMetaTag("fb:app_id", configSettings.FacebookAppId)
            Dim pageAbsUrl = PageUrl.AdViewItem(Me.Page, onlineAd.Heading, onlineAd.AdBookingId).ToAbsoluteUrl(Me.Request)
            Me.Page.Header.AddMetaTag("og:url", pageAbsUrl)
            Me.Page.Header.AddMetaTag("og:title", onlineAd.Heading)
            Me.Page.Header.AddMetaTag("og:locale", "en-AU")
            Me.Page.Header.AddMetaTag("og:type", "OnlineClassified")
            Me.Page.Header.AddMetaTag("og:site_name", "TheMusic")
            If onlineAd.ImageList.Any Then
                Me.Page.Header.AddMetaTag("og:image", PageUrl.AdImageUrl(onlineAd.ImageList.First(), maxWidth:=800, maxHeight:=800).ToAbsoluteUrl(Me.Request))
            End If
            Me.Page.Header.AddMetaTag("og:description", onlineAd.Description)

            Me.facebook.Attributes.Add("data-href", pageAbsUrl)
            Me.twitter.Attributes.Add("data-text", String.Format("Check out '{0}' - see it at", onlineAd.Heading))
        End With
    End Sub

    Private Sub lnkCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCategory.Click
        OnlineSearchParameter.Clear()
        OnlineSearchParameter.Category = Me.MainCategoryId
        Response.Redirect(PageUrl.OnlineAdSearch)
    End Sub

    Private Sub lnkSubCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSubCategory.Click
        OnlineSearchParameter.Clear()
        OnlineSearchParameter.Category = Me.MainCategoryId
        OnlineSearchParameter.SubCategory = Me.SubCategoryId
        Response.Redirect(PageUrl.OnlineAdSearch)
    End Sub


End Class