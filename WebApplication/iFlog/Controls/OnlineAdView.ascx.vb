Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess

Partial Public Class OnlineAdView
    Inherits System.Web.UI.UserControl

    Const MainCategoryViewState = "MainCategoryViewState"
    Const SubCategoryViewState = "SubCategoryViewState"
    Const contactAdvertiserUrl As String = "~/OnlineAdMessaging/default.aspx?AdNumber={0}"

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
                'onlineAdFrame.Attributes.Add("src", String.Format("BodyView.aspx?type=db&id={0}", onlineAd.OnlineAdId))
                onlineAdFrame.Src = ResolveUrl(String.Format("~/OnlineAds/BodyView.aspx?type=db&id={0}", onlineAd.OnlineAdId))
            Else
                onlineAdFrame.Src = ResolveUrl("~/OnlineAds/BodyView.aspx?type=session")
                'onlineAdFrame.Attributes.Add("src", String.Format("BodyView.aspx?type=session"))

            End If

            lblLocation.Text = .LocationValue
            lblArea.Text = .AreaValue
            lblNumOfViews.Text = .NumOfViews.ToString
            lblDatePosted.Text = String.Format("{0:D}", .DatePosted)
            If .AdDesignId > 0 Then
                lblIFlogID.Text = .AdDesignId
                ' set up the sitemap for this ad
                lblID.Text = .AdDesignId
            End If

            If .ContactName = "" Or .ContactName = "Private" Then
                objContactName.Visible = False
            Else
                lblContactName.Text = .ContactName
            End If

            If .ContactValue = "" Or .ContactName = "Private" Then
                objContactDetail.Visible = False
            Else
                If .ContactType.ToLower = "email" Then
                    Dim r As Regex = New Regex(Utilities.Constants.CONST_REGEX_Email, RegexOptions.IgnoreCase)
                    Dim m As Match = r.Match(.ContactValue)
                    If m.Success Then
                        litContactDetails.Text = String.Format("<a href=""mailto:{0}"">{1}</a>", .ContactValue, .ContactValue)
                    Else
                        litContactDetails.Text = .ContactValue
                    End If
                Else
                    litContactDetails.Text = .ContactValue
                End If
            End If

            objPrice.Visible = onlineAd.Price > 0
            lblPrice.Text = String.Format("{0:C}", .Price)

            MainCategoryId = .ParentCategory.MainCategoryId
            lnkCategory.Text = .ParentCategory.Title

            SubCategoryId = .SubCategory.MainCategoryId.ToString
            lnkSubCategory.Text = .SubCategory.Title

            paramountGallery.ImageList = onlineAd.ImageList
        End With
    End Sub

    Public Sub BindOnlineAd(ByVal onlineAd As DataModel.OnlineAd, ByVal imageList As List(Of String), ByVal datePosted As DateTime, ByVal parentCategoryID As Integer, ByVal subCategoryId As Integer)
        With onlineAd
            lblHeading.Text = .Heading
            lblLocation.Text = GeneralController.GetLocationById(.LocationId).Title
            lblArea.Text = GeneralController.GetLocationAreaById(.LocationAreaId).Title
            lblNumOfViews.Text = .NumOfViews.ToString
            lblDatePosted.Text = String.Format("{0:D}", datePosted)
            If .AdDesignId > 0 Then
                lblIFlogID.Text = .AdDesignId
                lblID.Text = .AdDesignId
            End If

            If .ContactName = "" Or .ContactName = "Private" Then
                objContactName.Visible = False
            Else
                lblContactName.Text = .ContactName
            End If

            If .ContactValue = "" Or .ContactName = "Private" Then
                objContactDetail.Visible = False
            Else
                If .ContactType.ToLower = "email" Then
                    litContactDetails.Text = String.Format("<a href='mailto:{0}'></a>", .ContactValue)
                Else
                    litContactDetails.Text = .ContactValue
                End If
            End If

            objPrice.Visible = onlineAd.Price > 0
            lblPrice.Text = String.Format("{0:C}", .Price)

            MainCategoryId = parentCategoryID
            lnkCategory.Text = CategoryController.GetMainCategoryById(parentCategoryID).Title

            subCategoryId = subCategoryId
            lnkSubCategory.Text = CategoryController.GetMainCategoryById(subCategoryId).Title

            paramountGallery.ImageList = imageList

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