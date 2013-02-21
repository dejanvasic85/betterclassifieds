Imports BetterclassifiedsCore
Imports BetterClassified.UI.WebPage

Partial Public Class BookSpecialCategories
    Inherits BaseBookingPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            DataBindMainCategories()
        End If
    End Sub

    Private Sub btnCreateAd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateAd.Click
        Dim specialRates = GeneralController.GetCurrentSpecials(ddlSubCategory.SelectedValue)

        If specialRates.Count > 0 Then
            ' Construct query string to redirect to special booking
            Dim redirectString = String.Format("{0}?specialId={1}&mainCategoryId={2}", PageUrl.BookSpecialMain, specialRates.FirstOrDefault.SpecialRateId, ddlSubCategory.SelectedValue)
            Response.Redirect(redirectString)
        Else
            ' Display error to user that no special is available and alert administrator
            Dim errors As New List(Of String)
            errors.Add("No Special rate is available for the selected category. Please contact iFlog Administrators for more information.")

            Dim errorDetails = String.Format("Category ID: {0} contains no special rates. Users unable to make special booking.", ddlSubCategory.SelectedValue)
            'EventLogManager.Log(New EventLog(errorDetails))
        End If

    End Sub

#Region "Category Binding"

    Private Sub DataBindMainCategories()
        Me.ddlMainCategory.DataSource = CategoryController.GetMainParentCategories()
        Me.ddlMainCategory.DataBind()

        If ddlMainCategory.Items.Count > 0 Then
            ddlMainCategory.SelectedIndex = 0
            DataBindSubCategories(ddlMainCategory.SelectedValue)
        End If
    End Sub

    Private Sub DataBindSubCategories(ByVal parentId As Integer)
        Me.ddlSubCategory.DataSource = CategoryController.GetMainCategoriesByParent(parentId)
        Me.ddlSubCategory.DataBind()
    End Sub

    Private Sub ddlMainCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMainCategory.SelectedIndexChanged
        DataBindSubCategories(ddlMainCategory.SelectedValue)
    End Sub

#End Region

    Private Sub btnUpgrade_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpgrade.Click
        General.StartBundleBookingStep2()
    End Sub
End Class