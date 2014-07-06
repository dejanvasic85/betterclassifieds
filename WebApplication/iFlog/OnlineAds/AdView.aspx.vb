Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified.UI.CategorySelectorSupport
Imports BetterClassified
Imports Microsoft.Practices.Unity

Imports Paramount
Imports Paramount.Betterclassifieds.Business.Repository

Partial Public Class AdView
    Inherits System.Web.UI.Page

    Dim _bookReference As String
    Dim _queryid As String
    Dim _isPreview As Boolean
    Dim _type As String
    Dim _adRepository As IAdRepository

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        AddHandler categorySelector.OnCategoryClick, AddressOf CategoryClicked
    End Sub

    Private Sub CategoryClicked(ByVal sender As Object, ByVal e As EventArgs)
        '' redirect them to search values
        Dim control = CType(sender, CategoryItem)
        OnlineSearchParameter.Clear()
        If control.CategoryId.HasValue Then
            OnlineSearchParameter.Category = control.CategoryId.Value
            Dim subCategory = TryCast(sender, BetterClassified.UI.CategorySelectorSupport.SubCategoryTemplateControl)
            If Not subCategory Is Nothing Then
                OnlineSearchParameter.SubCategory = subCategory.CategoryId
                OnlineSearchParameter.Category = subCategory.ParentCategoryId
            End If
            Response.Redirect(PageUrl.SearchCategoryResults)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' We are going to MVC, so this page is redundant
        ' Just go to the home page
        Response.RedirectPermanent("~")

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        _adRepository = BetterClassified.Unity.DefaultContainer.Resolve(Of IAdRepository)()
        _type = Request.QueryStringValue(Of String)("type").Default("bkid")
        _isPreview = Request.QueryStringValue(Of Boolean)("preview")
        _queryid = Request.QueryString("id").Default(RouteData.Values("id"))
        Dim adId As Integer

        If Not Page.IsPostBack Then

            Dim onlineAd As BusinessEntities.OnlineAdEntity = Nothing

            If _type.EqualTo("ref") Then
                _bookReference = Server.UrlDecode(_queryid)
                onlineAd = AdController.GetOnlineAdEntityByReference(_bookReference, _isPreview)
            ElseIf _type.EqualTo("dsId") And Integer.TryParse(_queryid, adId) Then
                onlineAd = AdController.GetOnlineAdEntityByDesign(adId, _isPreview)
            ElseIf _type.EqualTo("bkid") And Integer.TryParse(_queryid, adId) Then
                onlineAd = AdController.GetOnlineAdEntityByBookingId(adId, _isPreview)
            ElseIf Integer.TryParse(_queryid, adId) Then
                onlineAd = AdController.GetOnlineAdEntityById(adId, _isPreview)
            End If

            ' perform databind
            If onlineAd IsNot Nothing Then
                Me.Title = onlineAd.Heading
                ucxOnlineAdDetailView.BindOnlineAd(onlineAd)

                If (onlineAd.OnlineAdTag.HasValue) Then
                    ' bind the specific ad type - locate the control first
                    Dim adTypeControl = pnlAdDetails.FindControl(Of OnlineAdViewControl)("ucx" + onlineAd.OnlineAdTag)

                    ' todo - use a factory here to determine which ad to fetch
                    adTypeControl.DatabindAd(_adRepository.GetTutorAd(onlineAd.OnlineAdId))
                    adTypeControl.Visible = True
                End If
            Else
                Me.Title = "Ad does not exist or has Expired"
                ucxOnlineAdDetailView.Visible = False
                pnlResult.Visible = True
                lblIFlogID.Text = _queryid
            End If
        End If
    End Sub
End Class