Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.DataModel
Imports BetterClassified.UI.CategorySelectorSupport
Imports BetterClassified
Imports BetterclassifiedsCore.ParameterAccess
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility

Partial Public Class _Default5
    Inherits System.Web.UI.Page
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
        If Not Page.IsPostBack Then

            ' change the title for this page based on the user's search
            Me.Title = "Results for "
            ' append to the title depending on the seach

            If OnlineSearchParameter.Category.HasValue Then
                Me.Title += " - " + CategoryController.GetMainCategoryById(OnlineSearchParameter.Category.Value).Title
            End If
            If OnlineSearchParameter.SubCategory.HasValue Then
                Me.Title += " - " + CategoryController.GetMainCategoryById(OnlineSearchParameter.SubCategory).Title
            End If

            If Not OnlineSearchParameter.Category.HasValue Then
                Me.Title += " - " + "All Categories"
            End If

            GetDatasource()
        End If
    End Sub

    Private Sub grdSearchResults_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSearchResults.DataBound

        If OnlineSearchParameter.Category.HasValue Then
            Me.lblSearchDetails.Text += CategoryController.GetMainCategoryById(OnlineSearchParameter.Category.Value).Title
        End If
        If OnlineSearchParameter.SubCategory.HasValue Then
            Me.lblSearchDetails.Text += " - " + CategoryController.GetMainCategoryById(OnlineSearchParameter.SubCategory).Title
        End If
        If Not OnlineSearchParameter.Category.HasValue Then
            Me.lblSearchDetails.Text += "All Categories"
        End If
    End Sub

    Private Sub GetDatasource()
        Dim table = Search.SearchOnlineAds(OnlineSearchParameter.Category, OnlineSearchParameter.SubCategory, OnlineSearchParameter.Location, OnlineSearchParameter.Area, OnlineSearchParameter.KeyWord, grdSearchResults.PageIndex, grdSearchResults.PageSize)
        grdSearchResults.DataSource = table
        If table IsNot Nothing AndAlso table.Data.Rows.Count > 0 Then
            lblSearchDetails.Text = String.Format("Showing {0} to {1} of {2} ads :: ", ((grdSearchResults.PageIndex + 1 - 1) * grdSearchResults.PageSize + 1), ((grdSearchResults.PageIndex + 1) * grdSearchResults.PageSize) - (grdSearchResults.PageSize - table.Data.Rows.Count), grdSearchResults.VirtualItemCount)
        End If

        grdSearchResults.DataBind()
    End Sub

    Private Sub grdSearchResults_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSearchResults.PageIndexChanging
        grdSearchResults.PageIndex = e.NewPageIndex
        GetDatasource()
    End Sub

    Private Sub grdSearchResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSearchResults.RowDataBound
        ' method is used to display an automatic placeholder if image doesn't exist from datasource.
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim adBookingId = CInt(DataBinder.Eval(e.Row.DataItem, "AdBookingId"))
            Dim imageId = DataBinder.Eval(e.Row.DataItem, "DocumentId")
            Dim price = Val(DataBinder.Eval(e.Row.DataItem, "Price"))

            e.Row.FindControl(Of Label)("lblPrice").Text = If(price > 0, price.ToString("C"), String.Empty)
            e.Row.FindControl(Of HyperLink)("lnkAdLink").NavigateUrl = PageUrl.AdViewItem(adBookingId)
            e.Row.FindControl(Of HyperLink)("lnkHeadingLink").NavigateUrl = PageUrl.AdViewItem(adBookingId)

            Dim imageLink As HyperLink = e.Row.FindControl(Of HyperLink)("imgDocument")
            imageLink.NavigateUrl = PageUrl.AdViewItem(adBookingId)

            If imageId Is Nothing Or imageId Is DBNull.Value Then
                imageLink.ImageUrl = "ad_placeholder.jpg"
            Else
                ' Use the Paramount DSL image handler
                Dim param As New DslQueryParam(Request.QueryString) With {.DocumentId = imageId.ToString, _
                                                                          .Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode), _
                                                                          .Height = BetterclassifiedSetting.DslThumbHeight, _
                                                                          .Width = BetterclassifiedSetting.DslThumbWidth, _
                                                                          .Resolution = BetterclassifiedSetting.DslDefaultResolution}
                imageLink.ImageUrl = param.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
            End If
        End If
    End Sub

    Private Sub objSourceResults_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles objSourceResults.Selected
        Try
            ViewState("resultCount") = e.ReturnValue
            lblSearchResults.Text = e.ReturnValue.ToString + " results found "
        Catch ex As Exception

        End Try
    End Sub

    Private Sub StartBooking(ByVal type As SystemAdType)
        ' clear any current bookings
        BookingController.ClearAdBooking()

        ' start the new booking now with no user defined
        If Membership.GetUser Is Nothing Then
            BookingController.StartAdBooking("")
        Else
            BookingController.StartAdBooking(Membership.GetUser().UserName)
        End If

        BookingController.BookingType = Booking.BookingAction.NormalBooking

        ' set the ad type to be Print
        BookingController.SetAdType(AdController.GetAdTypeByCode(type).AdTypeId)

        ' specify that the user is coming from the home page which means they may need to store the user again
        Response.Redirect(PageUrl.BookingStep_2 + "?action=home")
    End Sub

    Private Sub grdSearchResults_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSearchResults.Sorting
        GetDatasource()
    End Sub

End Class