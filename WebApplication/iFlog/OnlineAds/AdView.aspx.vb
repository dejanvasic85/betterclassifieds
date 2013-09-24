Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified.UI.CategorySelectorSupport
Imports BetterClassified.UI.Repository
Imports BetterClassified
Imports Microsoft.Practices.Unity
Imports BetterClassified.UI.Models

Partial Public Class AdView
    Inherits System.Web.UI.Page

    Dim _bookReference As String
    Dim _id As String
    Dim _isPreview As Boolean
    Dim _type As String
    Dim _adRepository As BetterClassified.UI.Repository.IAdRepository

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        _adRepository = BetterClassified.Unity.DefaultContainer.Resolve(Of IAdRepository)()
        _type = Request.QueryStringValue(Of String)("type").Default(String.Empty)
        _isPreview = Request.QueryStringValue(Of Boolean)("preview")
        _id = Request.QueryString("id")
        Dim adId As Integer

        If Not Page.IsPostBack Then

            Dim onlineAd As BusinessEntities.OnlineAdEntity = Nothing

            If _type.EqualTo("ref") Then
                _bookReference = Server.UrlDecode(_id)
                onlineAd = AdController.GetOnlineAdEntityByReference(_bookReference, _isPreview)
            ElseIf _type.EqualTo("dsId") And Integer.TryParse(Request.QueryString("id"), adId) Then
                onlineAd = AdController.GetOnlineAdEntityByDesign(adId, _isPreview)
            ElseIf _type.EqualTo("bkid") And Integer.TryParse(Request.QueryString("id"), adId) Then
                onlineAd = AdController.GetOnlineAdEntityByBookingId(adId, _isPreview)
            ElseIf Integer.TryParse(Request.QueryString("id"), adId) Then
                onlineAd = AdController.GetOnlineAdEntityById(adId, _isPreview)
            End If

            ' perform databind
            If onlineAd IsNot Nothing Then
                Me.Title = onlineAd.Heading
                ucxOnlineAdDetailView.BindOnlineAd(onlineAd)

                If (onlineAd.OnlineAdTag.HasValue) Then
                    ' bind the specific ad type - locate the control first
                    Dim adTypeControl = pnlAdDetails.FindControl(Of IOnlineAdView)("ucx" + onlineAd.OnlineAdTag)

                    ' todo - use a factory here to determine which ad to fetch
                    adTypeControl.DatabindAd(_adRepository.GetTutorAd(onlineAd.OnlineAdId))
                    DirectCast(adTypeControl, Control).Visible = True
                End If
            Else
                Me.Title = "Ad does not exist or has Expired"
                ucxOnlineAdDetailView.Visible = False
                pnlResult.Visible = True
                lblIFlogID.Text = _id
            End If
        End If
    End Sub
End Class