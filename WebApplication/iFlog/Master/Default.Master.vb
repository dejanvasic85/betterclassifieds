﻿Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.UI
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.ApplicationBlock.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterclassifiedsCore.DataModel

Partial Public Class _Default1
    Inherits System.Web.UI.MasterPage

    Private Const AnyCategory As String = "Any Category"
    Private Const AnySubCategory As String = "Any Sub Category"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim papers = PublicationController.GetPublications(True)

            Dim environment = ConfigSettingReader.GetConfigurationContext()
            If Not environment = "LIVE" Then
                lblVersion.Text = String.Format(" - Version : {0}", BetterclassifiedSetting.Version)
            End If

            ' Databind search filters
            DatabindSearchFilters()
            ' If search has executed - load controls to have the filtered data
            LoadSearchFilters()
        End If
    End Sub

#Region "Databinding"

    Protected Sub DatabindSearchFilters()
        ddlMainCategory.DataSource = CategoryController.GetMainParentCategories()
        ddlLocation.DataSource = GeneralController.GetLocations()
        ddlArea.DataSource = GeneralController.GetLocationAreas(0)

        ddlMainCategory.DataBind()
        ddlSubCategory.DataBind()
        ddlLocation.DataBind()
        ddlArea.DataBind()

        Dim mainItem As New ListItem(AnyCategory, 0)
        ddlMainCategory.Items.Insert(0, mainItem)

        Dim firstItem As New ListItem(AnySubCategory, 0)
        ddlSubCategory.Items.Insert(0, firstItem)
    End Sub

    Protected Sub ddlMainCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMainCategory.SelectedIndexChanged
        DataBindSubCategories(ddlMainCategory.SelectedValue)
    End Sub

    Protected Sub DataBindSubCategories(ByVal categoryId As Integer)
        ddlSubCategory.Items.Clear()
        ddlSubCategory.DataSource = CategoryController.GetMainCategoriesByParent(categoryId)
        ddlSubCategory.DataBind()
        Dim firstItem As New ListItem(AnySubCategory, 0)
        ddlSubCategory.Items.Insert(0, firstItem)
    End Sub

    Protected Sub ddlLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        LoadArea()
    End Sub

    Protected Sub LoadArea()
        If (ddlLocation.SelectedIndex > 0) Then
            ddlArea.DataSource = GeneralController.GetLocationAreas(ddlLocation.SelectedValue)
        Else
            ddlArea.DataSource = GeneralController.GetLocationAreas(0)
        End If
        ddlArea.DataBind()
    End Sub

    Protected Sub LoadSearchFilters()
        If Not String.IsNullOrEmpty(OnlineSearchParameter.KeyWord) Then
            Me.txtAdId.Text = OnlineSearchParameter.KeyWord
        End If

        If OnlineSearchParameter.Location.HasValue Then
            Me.ddlLocation.SelectedValue = OnlineSearchParameter.Location.Value
            LoadArea()
        End If

        If OnlineSearchParameter.Area.HasValue Then
            Me.ddlArea.SelectedValue = OnlineSearchParameter.Area.Value
        End If

        If OnlineSearchParameter.Category.HasValue Then
            Me.ddlMainCategory.SelectedValue = OnlineSearchParameter.Category.Value
            DataBindSubCategories(OnlineSearchParameter.Category.Value)
        End If
        If OnlineSearchParameter.SubCategory.HasValue Then
            Me.ddlSubCategory.SelectedValue = OnlineSearchParameter.SubCategory.Value
        End If

    End Sub

#End Region

#Region "Search"

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        OnlineSearchParameter.Clear()

        ' perform the main search if they didn't provide the Ad ID
        If String.IsNullOrEmpty(txtAdId.Text) Then

            If Not String.IsNullOrEmpty(txtKeywords.Text) Then
                OnlineSearchParameter.KeyWord = txtKeywords.Text
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

            If (ddlArea.SelectedIndex > 0) Then
                OnlineSearchParameter.Area = ddlArea.SelectedValue
            End If
            Response.Redirect(PageUrl.OnlineAdSearch)

        ElseIf txtAdId.Text.Contains("-") Then
            Dim combinedSearch = txtAdId.Text.Split("-")
            Dim searchLog As New SearchLog
            If Integer.TryParse(combinedSearch(0), searchLog.PublicationId) _
                And Integer.TryParse(combinedSearch(1), searchLog.AdId) _
                And PublicationController.GetPublications(activateOnly:=True).Any(Function(p As Publication) p.PublicationId = searchLog.PublicationId) Then
                ' Log the search
                ' todo - audit Paramount.Modules.Logging.UIController.AuditLogController(Of SearchLog).AuditWebTransaction(searchLog, "PublicationAdSearch")
                Response.Redirect(PageUrl.AdViewItem(searchLog.AdId))
            End If
        Else
            Response.Redirect(PageUrl.AdViewItem(txtAdId.Text))
        End If
    End Sub

    Public Class SearchLog
        Public Property PublicationId As Integer
        Public Property AdId As Integer
    End Class

#End Region

End Class