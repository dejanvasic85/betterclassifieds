Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BundleBooking
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified.UI.WebPage

Imports BetterclassifiedsCore.BusinessEntities
Imports Paramount.Betterclassifieds.Business.Models

Partial Public Class BundlePage3
    Inherits BaseBundlePage

    Private _bundleController As BundleController

#Region "Page Load / Pre render"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _bundleController = New BundleController

        If Not Page.IsPostBack Then

            ' load any current session data (if any)
            LoadCurrentSessionData()

            ' Set up the online upload parameters
            UploadParameter.Clear()
            UploadParameter.IsOnlineAdUpload = True
            UploadParameter.IsPrintAdUpload = True
            UploadParameter.BookingReference = BundleController.BundleCart.BookReference
            radWindowImages.OpenerElementID = lnkUploadImages.ClientID

            ' Load the marketing content
            DataBindMarketingContent()

            ' Display the appropriate online ad
            Dim onlineAdType = String.Format("ucx{0}", _bundleController.GetOnlineAdTypeTagForBooking)
            Dim onlineControl = OnlineAdTypes.FindControl(onlineAdType)
            If onlineControl IsNot Nothing Then
                onlineControl.Visible = True
            End If

        End If
    End Sub

    Private Sub DataBindMarketingContent()
        Dim contentItem = GeneralController.GetWebContentDetail("BundlePage3.aspx")
        If contentItem IsNot Nothing Then
            divMarketingContent.InnerHtml = contentItem.WebContent
        End If
    End Sub

#End Region

#Region "Displaying / Calculate Single Price Information"

    Private Sub LoadCurrentSessionData()

        ' first check if the bundle cart is not null
        If BundleController.BundleCart IsNot Nothing Then
            ' bind the line ad - don't impose limit on the text box for ad description
            Dim isWordLimitEnforced = False
            ucxOnlineAd.BindOnlineAd(BundleController.BundleCart.OnlineAd, BundleController.BundleCart.OnlineAdGraphics, False)
            ' also set the booking reference
            ucxOnlineAd.BookingReference = BundleController.BundleCart.BookReference

            Dim tutorAd = BundleController.BundleCart.TutorAd
            If tutorAd Is Nothing Then
                Return
            End If
            With BundleController.BundleCart.TutorAd
                ucxTutors.DatabindAd(New TutorAdModel With { _
                                      .AgeGroupMin = tutorAd.AgeGroupMin, _
                                      .AgeGroupMax = tutorAd.AgeGroupMax, _
                                      .ExpertiseLevel = tutorAd.ExpertiseLevel, _
                                      .TravelOption = tutorAd.TravelOption, _
                                      .PricingOption = tutorAd.PricingOption, _
                                      .WhatToBring = tutorAd.WhatToBring, _
                                      .Objective = tutorAd.Objective, _
                                      .Subjects = tutorAd.Subjects, _
                                      .OnlineAdId = tutorAd.OnlineAdId, _
                                      .TutorAdId = tutorAd.TutorAdId})
            End With
        End If
    End Sub

#End Region

#Region "Navigation Buttons"

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
        ' validate the page from user missing any 
        If ValidatePage() Then
            ' call the controller to save the details into session
            _bundleController.SetLineAdDetails(lineAdNewDesign.LineAdHeading, lineAdNewDesign.LineAdText, _
                                               lineAdNewDesign.IsLineAdHeadingSelected, lineAdNewDesign.IsSuperBoldHeadingSelected, _
                                               lineAdNewDesign.IsColourHeadingSelected, lineAdNewDesign.ColourHeaderCode,
                                               lineAdNewDesign.IsColourBorderSelected, lineAdNewDesign.BorderColourCode,
                                               lineAdNewDesign.IsColourBackgroundSelected, lineAdNewDesign.BackgroundColourCode)

            _bundleController.SetOnlineAdDetails(ucxOnlineAd.Heading, ucxOnlineAd.Description, ucxOnlineAd.HtmlText, ucxOnlineAd.Price, ucxOnlineAd.LocationId, _
                                                 ucxOnlineAd.LocationArea, ucxOnlineAd.ContactName, ucxOnlineAd.ContactType, _
                                                 ucxOnlineAd.ContactValue)

            If ucxTutors.Visible Then
                Dim tutorAd = ucxTutors.GetTutorAd
                _bundleController.SetTutorAdDetails(tutorAd.AgeGroupMax, tutorAd.AgeGroupMin, tutorAd.ExpertiseLevel, tutorAd.Objective, tutorAd.PricingOption, tutorAd.Subjects, tutorAd.TravelOption, tutorAd.WhatToBring)
            End If

            Response.Redirect(PageUrl.BookingBundle_4)
        End If
    End Sub

    Protected Sub btnPrevious_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrevious.Click
        ' go back to the 2nd step of the bundled booking
        Response.Redirect(PageUrl.BookingBundle_2)
    End Sub

#End Region

#Region "Validation"

    Private Function ValidatePage() As Boolean
        Dim errorList As New List(Of String)

        ' Validate the Line Ad 
        If (lineAdNewDesign.IsValid = False) Then
            errorList.AddRange(lineAdNewDesign.ValidationSummaryList)
        End If

        ' check if online heading is provided
        If (ucxOnlineAd.Heading = String.Empty) Then
            errorList.Add("Please provide the heading for the Online Ad")
        End If

        ' check if the online ad description exists
        If (ucxOnlineAd.Description = String.Empty) Then
            errorList.Add("Please provide the description for the Online Ad")
        End If

        ucxPageErrors.ShowErrors(errorList)

        ' return true (validation ok) if the error list contains 0 elements
        Return errorList.Count = 0
    End Function

#End Region

    Private ReadOnly Property IsLineAdGraphicUploaded()
        Get
            Return (BundleController.BundleCart.LineAdGraphic IsNot Nothing)
        End Get
    End Property

End Class