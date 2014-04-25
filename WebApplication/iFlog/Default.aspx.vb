Imports BetterClassified.UI.CategorySelectorSupport
Imports BetterclassifiedsCore.ParameterAccess
Imports Paramount.Betterclassifieds.Business.Repository
Imports Microsoft.Practices.Unity
Partial Public Class _Default4
    Inherits System.Web.UI.Page

    Public Sub New()
        categorySelector = New BetterClassified.UI.CategorySelector()
    End Sub
    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        AddHandler categorySelector.OnCategoryClick, AddressOf CategoryClicked
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            OnlineSearchParameter.Clear()
        End If
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

            If Not String.IsNullOrEmpty(control.SeoName) Then
                Response.Redirect(String.Format(PageUrl.SearchSeoCategoryResults, control.SeoName, control.CategoryId.Value))
            Else
                Response.Redirect(PageUrl.SearchCategoryResults)
            End If
        End If
    End Sub

End Class