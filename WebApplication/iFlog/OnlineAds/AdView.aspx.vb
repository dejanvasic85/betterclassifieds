Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified.UI.CategorySelectorSupport

Partial Public Class AdView
    Inherits System.Web.UI.Page

    Dim _onlineAdId As Integer
    Dim _adDesignId As Integer
    Dim _bookReference As String    
    Dim _isPreview As Boolean
    Dim _type As String

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
        ''Fix issue with ie 8 caching page, when user clicks back
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        _type = Request.QueryString("type")
        _isPreview = Request.QueryString("preview")

        If Not Page.IsPostBack Then

            Dim onlineAd As BusinessEntities.OnlineAdEntity

            ' check the type of query for the page
            If _type = "ref" Then

                ' going by the booking reference ID (iFlogID)
                _bookReference = Server.UrlDecode(Request.QueryString("id"))
                onlineAd = AdController.GetOnlineAdEntityByReference(_bookReference, _isPreview)

            ElseIf _type = "dsId" Then

                Integer.TryParse(Request.QueryString("id"), _adDesignId)
                onlineAd = AdController.GetOnlineAdEntityByDesign(_adDesignId, _isPreview)

            Else

                ' the ID passed in the query is the straight Online Ad ID
                _onlineAdId = Request.QueryString("id")
                onlineAd = AdController.GetOnlineAdEntityById(_onlineAdId, _isPreview)

            End If

            ' perform databind
            If onlineAd IsNot Nothing Then
                ' increase the number of views for this ad if this is not a preview only.
                'If _isPreview = False Then
                '    AdController.IncreaseOnlineAdViews(onlineAd.OnlineAdId)
                'End If
                ' set the title of the page to be the heading
                Me.Title = onlineAd.Heading
                ' get the required information to bind the online ad.
                ucxOnlineAd.BindOnlineAd(onlineAd)
            Else
                Me.Title = "iFlog ID does not exist or has Expired"
                ucxOnlineAd.Visible = False
                pnlResult.Visible = True
                If _type = "dsId" Then
                    lblIFlogID.Text = _adDesignId.ToString
                End If
            End If
        End If
    End Sub
End Class