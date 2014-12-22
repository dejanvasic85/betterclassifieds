Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BundleBooking
Imports BetterclassifiedsCore.BusinessEntities
Imports BetterClassified.UI.WebPage

Partial Public Class BundlePage2
    Inherits BaseBundlePage

    Private _bundleController As BundleController
    Private _queryAction As String

#Region "Page Load / Pre render"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _bundleController = New BundleController

        ' load query string values
        _queryAction = Request.QueryString("action")

        If Not Page.IsPostBack Then
            ' check the query action if they are coming from the home page
            If _queryAction = "home" Then
                ' check that the user is logged in
                If BundleController.BundleCart.Username = String.Empty Then
                    ' also check that the username is not nothing - then we set it from the membership
                    BundleController.BundleCart.Username = Membership.GetUser().UserName
                End If
            End If

            ' databind all the UI controls (papers and categories)
            DataBindUI()
            ' load current session data
            LoadCurrentSessionData()
        End If
    End Sub

#End Region

#Region "DataBinding"

    Private Sub DataBindUI()
        ' bind the papers
        ucxPaperList.LoadPapers(PublicationController.GetPublications(True))
        ' bind the categories
        DataBindMainCategories()
    End Sub

    Private Sub DataBindMainCategories()
        ' first bind the main parent categories
        Me.ddlMainCategory.DataSource = CategoryController.GetMainParentCategories()
        Me.ddlMainCategory.DataBind()

        ' then bind the child categories
        If ddlMainCategory.Items.Count > 0 Then
            ddlMainCategory.SelectedIndex = 0
            DataBindSubCategories(ddlMainCategory.SelectedValue)
        End If
    End Sub

    Private Sub DataBindSubCategories(ByVal parentId As Integer)
        ' bind the child categories by the parent id
        Me.ddlSubCategory.DataSource = CategoryController.GetMainCategoriesByParent(parentId)
        Me.ddlSubCategory.DataBind()
    End Sub

    Private Sub ddlMainCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMainCategory.SelectedIndexChanged
        ' bind the child categories passing the id of selected value
        DataBindSubCategories(ddlMainCategory.SelectedValue)
    End Sub

    Private Sub LoadCurrentSessionData()
        ' first check if the bundle cart is not null
        If BundleController.BundleCart IsNot Nothing Then
            ' check if any publication has been set in the first place
            If BundleController.BundleCart.PublicationList IsNot Nothing Then

                Dim publicationIdList As New List(Of Integer) ' create the list required to pass to method below

                ' loop through each item in the session and add to the list
                For Each publication As DataModel.Publication In BundleController.BundleCart.PublicationList
                    publicationIdList.Add(publication.PublicationId)
                Next
                ' call method in the user control to make the current selections for papers
                ucxPaperList.SelecPublications(publicationIdList)
            End If

            ' check if any categories have been stored in session
            If BundleController.BundleCart.MainParentCategory IsNot Nothing Then
                ddlMainCategory.SelectedValue = BundleController.BundleCart.MainParentCategory.MainCategoryId
                DataBindSubCategories(BundleController.BundleCart.MainParentCategory.MainCategoryId)
            End If

            ' check if any sub categories have been stored in session
            If BundleController.BundleCart.MainSubCategory IsNot Nothing Then
                ddlSubCategory.SelectedValue = BundleController.BundleCart.MainSubCategory.MainCategoryId
            End If
        End If
    End Sub

#End Region

#Region "Navigation Buttons"

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
        ' call the validation page method that will display any missing information to the user
        If ValidatePage() Then

            ' get and set the publication list selected by the user from the UI
            _bundleController.SetPublication(ucxPaperList.GetSelectedPapers(SystemAdType.LINE), True)
            ' get and set the main categories selected by the user from the UI
            _bundleController.SetCategory(ddlMainCategory.SelectedValue, ddlSubCategory.SelectedValue)
            ' set the booking reference by the sub category id
            _bundleController.SetBookingReference(ddlSubCategory.SelectedValue)

            ' Set the pricing information based on the type of paper selected
            _bundleController.SetBookingOrderPrices(ddlSubCategory.SelectedValue, getBookingPublications)

            ' redirect to the 3rd step of the bundled booking
            Response.Redirect(PageUrl.BookingBundle_3)
        End If
    End Sub

    Protected Sub btnPrevious_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrevious.Click
        ' redirect back to step 1
        Response.Redirect(PageUrl.BookingStep_1 + "?action=back")
    End Sub

#End Region

#Region "Validation"

    Private Function ValidatePage() As Boolean
        Dim errorList As New List(Of String)

        ' perform validation to ensure user selected at least one publication
        If (ucxPaperList.GetSelectedPapers(SystemAdType.LINE).Count > 0) = False Then
            errorList.Add("You need to select at least one publication from the list.")
        End If

        ' ensure that a sub category has been selected
        If ddlSubCategory.SelectedValue < 0 Then
            errorList.Add("Please select a sub category for your booking.")
        End If

        ' call the user control to display any errors found
        ucxPageError.ShowErrors(errorList)

        ' return true (validation ok) if the error list contains 0 elements
        Return errorList.Count = 0
    End Function

#End Region

#Region "Helper Methods"

    Private Function getBookingPublications() As List(Of BookingPublicationEntity)
        ' Fetches the publications set in the booking session and 
        Dim bookingPublications As New List(Of BookingPublicationEntity)
        For Each publication In BundleController.BundleCart.PublicationList

            ' In this type of booking only Line ad and Online Ad types can be booked
            Dim publicationAdTypes = _bundleController.GetPublicationAdTypes(publication.PublicationId)
            Dim publicationAdType = publicationAdTypes.Where(Function(pat) pat.AdTypeId = BookingAdType.LineAd Or pat.AdTypeId = BookingAdType.OnlineAd).FirstOrDefault

            bookingPublications.Add(New BookingPublicationEntity() With {.AdType = publicationAdType.AdTypeId, _
                                                                         .PublicationId = publication.PublicationId})
        Next
        Return bookingPublications
    End Function

#End Region

End Class