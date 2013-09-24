Imports BetterClassified
Imports BetterClassified.UI.Repository
Imports BetterClassified.UI.Models
Imports Microsoft.Practices.Unity

Public Class TutorAdForm
    Inherits System.Web.UI.UserControl
    Implements IOnlineAdView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim lookups = Unity.DefaultContainer.Resolve(Of ILookupRepository)()
            ddlExpertLevel.DatabindLookups(lookups.GetLookupsForGroup(LookupGroup.TutorLevels))
            ddlTravelOptions.DatabindLookups(lookups.GetLookupsForGroup(LookupGroup.TutorTravelOptions))
            ddlPricingOptions.DatabindLookups(lookups.GetLookupsForGroup(LookupGroup.TutorPricingOptions))
        End If
    End Sub

    Public Function GetTutorAd() As TutorAdModel
        ' Get the tutor model from the UI
        Return New TutorAdModel With {.AgeGroupMin = txtAgeMin.Text.ToInt(), _
                                .AgeGroupMax = txtAgeMax.Text.ToInt(), _
                                .ExpertiseLevel = ddlExpertLevel.SelectedValue, _
                                .TravelOption = ddlTravelOptions.SelectedValue, _
                                .Objective = txtObjective.Text, _
                                .PricingOption = ddlPricingOptions.SelectedValue, _
                                .Subjects = txtSubjects.GetTags(), _
                                .WhatToBring = txtWhatToBring.Text, _
                                .OnlineAdId = hdnOnlineAdId.Value.ToInt,
                                .TutorAdId = hdnTutorAdId.Value.ToInt}
    End Function

    Public Sub DatabindAd(Of T)(ByVal adDetails As T) Implements IOnlineAdView.DatabindAd
        Dim tutorAd = TryCast(adDetails, TutorAdModel)
        If tutorAd Is Nothing Then
            Throw New InvalidCastException("Unable to cast to TutorAdModel")
        End If

        hdnOnlineAdId.Value = tutorAd.OnlineAdId
        hdnTutorAdId.Value = tutorAd.TutorAdId

        txtSubjects.SetTags(tutorAd.Subjects)
        txtAgeMin.Text = IIf(tutorAd.AgeGroupMin.HasValue, tutorAd.AgeGroupMin, "")
        txtAgeMax.Text = IIf(tutorAd.AgeGroupMax.HasValue, tutorAd.AgeGroupMax, "")
        ddlExpertLevel.SelectedValue = tutorAd.ExpertiseLevel
        ddlTravelOptions.SelectedValue = tutorAd.TravelOption
        txtObjective.Text = tutorAd.Objective
        ddlPricingOptions.SelectedValue = tutorAd.PricingOption
        txtWhatToBring.Text = tutorAd.WhatToBring
    End Sub

End Class