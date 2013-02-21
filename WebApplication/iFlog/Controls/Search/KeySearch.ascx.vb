Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess

Namespace Controls.Search
    Partial Public Class KeySearch1
        Inherits System.Web.UI.UserControl

        Public Event Search As EventHandler
        ' Page Constants
        Private _searchKeyword As String = "search keywords..."
        Private _iFlogID As String = "iFlog ID"
        Private _AnyCategory As String = " Any Category "
        Private _AnySubCategory As String = " Any Sub-Category "

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            If Not Page.IsPostBack Then
                ' set the keyword text box
                txtKeyword.Text = _searchKeyword
                txtIflogId.Text = _iFlogID
                DataBindSearchControls()
                LoadSearchCriteria()
            End If

        End Sub
        Public ReadOnly Property KeyWord() As String
            Get
                If txtKeyword.Text.Contains(_searchKeyword) Then
                    Return String.Empty
                End If
                Return Me.txtKeyword.Text
            End Get
        End Property

        Public ReadOnly Property Category() As Integer?
            Get
                If (ddlMainCategory.SelectedIndex > 0) Then
                    Return Me.ddlMainCategory.SelectedValue
                End If
            End Get
        End Property

        Public ReadOnly Property SubCategory() As Integer?
            Get
                If (ddlSubCategory.SelectedIndex > 0) Then
                    Return ddlSubCategory.SelectedValue
                End If
            End Get
        End Property

        Public ReadOnly Property Area() As Integer?
            Get
                If (ddlLocationArea.SelectedIndex > 0) Then
                    Return ddlLocationArea.SelectedValue
                End If
            End Get
        End Property

        Public ReadOnly Property Location() As Integer?
            Get
                If (ddlLocation.SelectedIndex > 0) Then
                    Return ddlLocation.SelectedValue
                End If
            End Get
        End Property
        Public Sub DataBindSearchControls()

            ddlMainCategory.DataSource = CategoryController.GetMainParentCategories()
            ddlLocation.DataSource = GeneralController.GetLocations()
            ddlLocationArea.DataSource = GeneralController.GetLocationAreas(0)

            ddlMainCategory.DataBind()
            ddlSubCategory.DataBind()
            ddlLocation.DataBind()
            ddlLocationArea.DataBind()

            Dim mainItem As New ListItem(_AnyCategory, 0)
            ddlMainCategory.Items.Insert(0, mainItem)

            Dim firstItem As New ListItem(_AnySubCategory, 0)
            ddlSubCategory.Items.Insert(0, firstItem)
        End Sub

        Public Sub LoadArea()
            If (ddlLocation.SelectedIndex > 0) Then
                ddlLocationArea.DataSource = GeneralController.GetLocationAreas(ddlLocation.SelectedValue)
            Else
                ddlLocationArea.DataSource = GeneralController.GetLocationAreas(0)
            End If
            ddlLocationArea.DataBind()
        End Sub

        Private Sub LoadSearchCriteria()
            Me.txtKeyword.Text = IIf(String.IsNullOrEmpty(OnlineSearchParameter.KeyWord), Me._searchKeyword, OnlineSearchParameter.KeyWord)

            If OnlineSearchParameter.Location.HasValue Then
                Me.ddlLocation.SelectedValue = OnlineSearchParameter.Location.Value
                LoadArea()
            End If

            If OnlineSearchParameter.Area.HasValue Then
                Me.ddlLocationArea.SelectedValue = OnlineSearchParameter.Area.Value
            End If

            If OnlineSearchParameter.Category.HasValue Then
                Me.ddlMainCategory.SelectedValue = OnlineSearchParameter.Category.Value
                DataBindSubCategories(OnlineSearchParameter.Category.Value)
            End If
            If OnlineSearchParameter.SubCategory.HasValue Then
                Me.ddlSubCategory.SelectedValue = OnlineSearchParameter.SubCategory.Value
            End If

        End Sub

        Private Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            OnlineSearchParameter.Clear()
            '' perform the main search if they didn't provide the iFlog ID
            If txtIflogId.Text = String.Empty Or txtIflogId.Text = _iFlogID Then

                ' default text will contain "search keywords..." so if we find this then remove the keyword
                If txtKeyword.Text.Contains(_searchKeyword) Then
                    OnlineSearchParameter.KeyWord = String.Empty
                Else
                    OnlineSearchParameter.KeyWord = txtKeyword.Text
                End If

                ' store the items into the context (safer and less complex than query string)
                If (ddlMainCategory.SelectedIndex > 0) Then
                    OnlineSearchParameter.Category = ddlMainCategory.SelectedValue
                End If

                If (ddlSubCategory.SelectedIndex > 0) Then
                    OnlineSearchParameter.SubCategory = ddlSubCategory.SelectedValue
                End If

                If (ddlLocation.SelectedIndex > 0) Then
                    OnlineSearchParameter.Location = ddlLocation.SelectedValue
                End If

                If (ddlLocationArea.SelectedIndex > 0) Then
                    OnlineSearchParameter.Area = ddlLocationArea.SelectedValue
                End If
                Response.Redirect(PageUrl.OnlineAdSearch)
            Else
                ' get the online ad details for the reference user has provided and direct to item page
                Response.Redirect(PageUrl.AdViewItem + "?preview=false&type=dsId&id=" + txtIflogId.Text)

            End If

        End Sub

        Private Sub ddlMainCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMainCategory.SelectedIndexChanged
            ' if they select the first index then load all subcategories
            DataBindSubCategories(ddlMainCategory.SelectedValue)
        End Sub

        Private Sub DataBindSubCategories(ByVal categoryId As Integer)
            ddlSubCategory.Items.Clear()
            ddlSubCategory.DataSource = CategoryController.GetMainCategoriesByParent(categoryId)
            ddlSubCategory.DataBind()
            Dim firstItem As New ListItem(_AnySubCategory, 0)
            ddlSubCategory.Items.Insert(0, firstItem)
        End Sub

        Private Sub ddlLocaton_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
            LoadArea()
        End Sub

        Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

            Dim sb As New StringBuilder
            sb.AppendLine("<script language='javascript' type='text/javascript'>")
            sb.AppendLine("function removeText(txt){")
            sb.AppendLine("     document.getElementById(txt).value = '';")
            sb.AppendLine("}")
            sb.AppendLine("</script>")

            Page.ClientScript.RegisterClientScriptBlock(GetType(KeySearch1), "textScript", sb.ToString())

            txtKeyword.Attributes.Add("onClick", "removeText('" + txtKeyword.ClientID + "')")
            txtIflogId.Attributes.Add("onClick", "removeText('" + txtIflogId.ClientID + "')")

        End Sub

    End Class
End Namespace