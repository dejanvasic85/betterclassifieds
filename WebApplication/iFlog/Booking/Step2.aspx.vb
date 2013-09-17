﻿Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.DataModel
Imports Paramount.Modules.Logging.UIController
Imports BetterclassifiedsCore.Utilities
Imports BetterClassified.UI.WebPage

Partial Public Class Step2
    Inherits BaseBookingPage

    Protected Sub Page_Load (ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If BookingController.AdBookCart Is Nothing Then
            Response.Redirect (PageUrl.BookingStep_1 + "?action=expired")
        End If

        If (AdController.TempRecordExist (BookingController.AdBookCart.BookReference)) Then
            Response.Redirect (PageUrl.BookingStep_1 + "?action=expired")
        End If

        If Not Page.IsPostBack Then

            DataBindMainCategories()

            ' get the ad type from the querystring and load papers accordingly
            ' everything is based on the ad Type so we always retrieve this and store into Viewstate
            Dim adType As DataModel.AdType = BookingController.AdBookCart.MainAdType

            SetAdType (adType.Code)

            ' we hide the paper selection if they chose an online ad.
            ' there should be only one paper to host the online ads for now.
            ' in the future these "online papers" may change to "cars" or "real estate" or "gigs" etc etc.
            pnlPublications.Visible = (adType.Code = SystemAdType.LINE.ToString)

            Dim publicationList = PublicationController.GetAllPublicationByAdType (adType.AdTypeId, True)

            If publicationList IsNot Nothing Then

                If publicationList.Count = 0 Then
                    ' no publications exist for this type of Ad - log this as administration error
                    Try
                        Throw New Exception("No Publications have been set up for selected Ad Type or they need to be marked active.")
                    Catch ex As Exception
                        ExceptionLogController(Of Exception).AuditException(ex)
                    End Try

                    ' redirect to error page since the settings have not been made.
                    Response.Redirect (Constants.CONST_ERROR_DEFAULT_URL + "?type=" + _
                                       Constants.CONST_ERROR_SETTINGS)
                End If

            Else
                ' redirect to error page since the settings have not been made.
                Response.Redirect (Constants.CONST_ERROR_DEFAULT_URL + "?type=" + _
                                   Constants.CONST_ERROR_SETTINGS)

            End If

            ' Check if the user is coming back to this page.
            If Request.QueryString ("action") = "back" Then

                ucxPaperList.LoadPapers (publicationList)
                ucxPaperList.LoadCurrentData (BookingController.AdBookCart.PublicationList)

                ddlMainCategory.SelectedValue = BookingController.AdBookCart.ParentCategoryId
                DataBindSubCategories (BookingController.AdBookCart.ParentCategoryId)
                ddlSubCategory.SelectedValue = BookingController.AdBookCart.MainCategoryId

            ElseIf Request.QueryString ("action") = "home" Then
                If BookingController.AdBookCart.UserId = String.Empty Then
                    Dim user As MembershipUser = Membership.GetUser
                    If user IsNot Nothing Then
                        BookingController.AdBookCart.UserId = user.UserName
                    Else
                        Response.Redirect (PageUrl.Login)
                    End If
                End If
                DataBindUI (publicationList)
            Else
                DataBindUI (publicationList)
            End If

        End If

    End Sub

    Private Sub DataBindUI (ByVal list As List(Of Publication))
        ' we load data normally the first time
        ucxPaperList.LoadPapers (list)
    End Sub

    Private Sub SetAdType (ByVal type As String)
        Select Case type
            Case SystemAdType.LINE.ToString
                AdType = SystemAdType.LINE
            Case SystemAdType.ONLINE.ToString
                AdType = SystemAdType.ONLINE
        End Select
    End Sub

    Private Property AdType() As SystemAdType
        Get
            Return ViewState ("adType")
        End Get
        Set (ByVal value As SystemAdType)
            ViewState ("adType") = value
        End Set
    End Property

    Private Sub ucxNavButtons_NextStep (ByVal sender As Object, ByVal e As EventArgs) Handles ucxNavButtons.NextStep

        Dim pageValid As Boolean = True
        ' flag for validation
        Dim errorList As List(Of String) = New List(Of String)

        ' perform the validation
        pageValid = ValidatePage (errorList)

        If (pageValid) Then
            Dim mainCategory As Integer = Me.ddlSubCategory.SelectedValue
            Dim parentCategory As Integer = Me.ddlMainCategory.SelectedValue
            Dim paperList As List(Of Integer) = ucxPaperList.GetSelectedPapers (AdType)

            ' set selected list of publications into the session object
            BookingController.SetPublications (paperList)

            ' set the main category selected into the session
            BookingController.SetParentCategory (parentCategory)

            BookingController.SetMainCategory (mainCategory)

            ' set the ratecards used to calculate the cost for the ad
            If BookingController.SetRatecards (paperList, mainCategory) = False Then
                ' no ratecard was set - means that application setting or business logic error
                Try
                    Throw New Exception("Failed to assign the ratecard to booking procedure. Ensure that every publication category is assigned a ratecard. Paper List: " + _
                                                   paperList.ToString + ", Category Id: " + mainCategory.ToString)
                Catch ex As Exception
                    ExceptionLogController(Of Exception).AuditException(ex)
                End Try
                Response.Redirect(Constants.CONST_ERROR_DEFAULT_URL + "?type=" + Constants.CONST_ERROR_SETTINGS)
            End If

            BookingController.SetBookingReference (mainCategory)

            Response.Redirect ("Step3.aspx")
        Else
            Me.ucxPageErrors.ShowErrors (errorList)
        End If
    End Sub

    Private Function ValidatePage (ByRef errorList As List(Of String)) As Boolean
        Dim isValid As Boolean = True

        If (AdType = SystemAdType.LINE) Then
            ' perform validation to ensure user selected at least one publication
            If (ucxPaperList.GetSelectedPapers (SystemAdType.LINE).Count > 0) = False Then
                errorList.Add ("You need to select at least one publication from the list.")
                isValid = False
            End If
        End If

        If ddlSubCategory.SelectedValue < 0 Then
            errorList.Add ("Please select a sub category for your booking.")
            isValid = False
        End If
        Return isValid
    End Function

#Region "Category Binding"

    Private Sub DataBindMainCategories()
        Me.ddlMainCategory.DataSource = CategoryController.GetMainParentCategories()
        Me.ddlMainCategory.DataBind()

        If ddlMainCategory.Items.Count > 0 Then
            ddlMainCategory.SelectedIndex = 0
            DataBindSubCategories (ddlMainCategory.SelectedValue)
        End If
    End Sub

    Private Sub DataBindSubCategories (ByVal parentId As Integer)
        Me.ddlSubCategory.DataSource = CategoryController.GetMainCategoriesByParent (parentId)
        Me.ddlSubCategory.DataBind()
    End Sub

    Private Sub ddlMainCategory_SelectedIndexChanged (ByVal sender As Object, ByVal e As EventArgs) _
        Handles ddlMainCategory.SelectedIndexChanged
        DataBindSubCategories (ddlMainCategory.SelectedValue)
    End Sub

#End Region

End Class