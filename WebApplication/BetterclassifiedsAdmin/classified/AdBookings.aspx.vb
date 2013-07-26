Imports BetterclassifiedsCore

Partial Public Class AdBookings
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            DataBindPublications()
            DataBindCategories()
        End If
    End Sub

    Private Sub DataBindPublications()
        ddlPublication.DataSource = PublicationController.GetAllPapers()
        ddlPublication.DataBind()
        ' insert extra item
        ddlPublication.Items.Insert(0, New ListItem("Any", 0))
    End Sub

    Private Sub DataBindCategories()
        ddlMainCategories.DataSource = CategoryController.GetMainParentCategories
        ddlMainCategories.DataBind()
        If ddlMainCategories.Items.Count > 0 Then
            DataBindSubCategories(ddlMainCategories.Items(0).Value)
        End If
        ddlMainCategories.Items.Insert(0, New ListItem("Any", 0))
    End Sub

    Private Sub DataBindSubCategories(ByVal parentId As Integer)
        ddlSubCategories.DataSource = CategoryController.GetMainCategoriesByParent(parentId)
        ddlSubCategories.DataBind()
        ' insert extra item
        ddlSubCategories.Items.Insert(0, New ListItem("Any", 0))
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click, grdBookingResults.PageSizeChanged
        grdBookingResults.Rebind()
    End Sub

    Private Sub ddlMainCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMainCategories.SelectedIndexChanged
        DataBindSubCategories(ddlMainCategories.SelectedValue)
    End Sub

    Private Sub bookingsDataSource_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles bookingsDataSource.Selecting
        e.Arguments.MaximumRows = grdBookingResults.PageSize
        e.InputParameters.Clear()
        If Not e.ExecutingSelectCount Then

            Dim adDesignId As Nullable(Of Integer)
            If Not txtAdId.Text = String.Empty Then
                adDesignId = txtAdId.Text
            End If

            Dim bookingStatus As Nullable(Of Integer)
            If ddlStatus.SelectedIndex > 0 Then
                bookingStatus = ddlStatus.SelectedValue
            End If

            Dim publicationId As Nullable(Of Integer)
            If ddlPublication.SelectedIndex > 0 Then
                publicationId = ddlPublication.SelectedValue
            End If

            Dim parentCategoryId As Nullable(Of Integer)
            If ddlMainCategories.SelectedIndex > 0 Then
                parentCategoryId = ddlMainCategories.SelectedValue
            End If

            Dim subCategoryId As Nullable(Of Integer)
            If ddlSubCategories.SelectedIndex > 0 Then
                subCategoryId = ddlSubCategories.SelectedValue
            End If

            e.InputParameters.Add("adDesignId", adDesignId)
            e.InputParameters.Add("bookReference", txtBookReference.Text)
            e.InputParameters.Add("userName", txtUsername.Text)
            e.InputParameters.Add("bookingStartDate", dtmFrom.SelectedDate)
            e.InputParameters.Add("bookingEndDate", dtmTo.SelectedDate)
            e.InputParameters.Add("bookingStatus", bookingStatus)
            e.InputParameters.Add("publicationId", publicationId)
            e.InputParameters.Add("parentCategoryId", parentCategoryId)
            e.InputParameters.Add("subCategoryId", subCategoryId)
            e.InputParameters.Add("editionStartDate", dtmEditionFrom.SelectedDate)
            e.InputParameters.Add("editionEndDate", dtmEditionTo.SelectedDate)
            e.InputParameters.Add("adSearchText", txtKeyword.Text)
            e.InputParameters.Add("sortBy", getSortExpression)

        End If
        e.InputParameters.Add("e", e)
    End Sub

    Private Sub grdBookingResults_SortCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles grdBookingResults.SortCommand

        If SortDirection = "DESC" Then
            SortDirection = "ASC"
        Else
            SortDirection = "DESC"
        End If

        SortBy = e.SortExpression

        e.Canceled = True
        grdBookingResults.Rebind()
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ' clear the form by setting all the values to be empty
        txtAdDesignID.Text = String.Empty
        txtBookReference.Text = String.Empty
        txtUsername.Text = String.Empty
        dtmEditionFrom.Clear()
        dtmEditionTo.Clear()
        dtmFrom.Clear()
        dtmTo.Clear()
        ddlStatus.SelectedIndex = 0
        ddlPublication.SelectedIndex = 0
        ddlMainCategories.SelectedIndex = 0
        ddlSubCategories.SelectedIndex = 0
        txtKeyword.Text = String.Empty

        grdBookingResults.Rebind()
    End Sub

    Private Function getSortExpression() As String
        Dim sortExpression As String = Me.SortBy

        ' If we don't have an explicitly set sort expression, set one
        If String.IsNullOrEmpty(sortExpression) Then
            sortExpression = "AdBookingId"
        End If

        Return String.Format("{0} {1}", sortExpression, Me.SortDirection)
    End Function


    Private Property SortBy() As String
        Get
            If ViewState("sortBy") = String.Empty Then
                Return "AdBookingId"
            Else
                Return ViewState("sortBy")
            End If
        End Get
        Set(ByVal value As String)
            ViewState("sortBy") = value
        End Set
    End Property

    Private Property SortDirection() As String
        Get
            If ViewState("SortDirection") = String.Empty Then
                Return "DESC"
            Else
                Return ViewState("SortDirection")
            End If
        End Get
        Set(ByVal value As String)
            If value = String.Empty Then
                ViewState("SortDirection") = "DESC"
            Else
                ViewState("SortDirection") = value
            End If
        End Set
    End Property

End Class