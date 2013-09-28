Imports BetterClassified.Models

Public Class TutorAdView
    Inherits OnlineAdViewControl

    Public Overrides Sub DatabindAd(Of T)(ByVal adDetails As T)
        Dim tutorAdModel = TryCast(adDetails, TutorAdModel)
        If tutorAdModel Is Nothing Then
            Throw New InvalidCastException("Unable to cast to TutorAdModel")
        End If
        lblSubjects.InnerText = tutorAdModel.Subjects

        If (tutorAdModel.IsOpenForAllAges()) Then
            lblAge.InnerText = "Open for all ages"
        Else
            lblAge.InnerText = String.Format("{0} to {1}", _
                                             IIf(tutorAdModel.AgeGroupMin Is Nothing, "Any", tutorAdModel.AgeGroupMin), _
                                             IIf(tutorAdModel.AgeGroupMax Is Nothing, "any", tutorAdModel.AgeGroupMax))
        End If

        lblLevel.InnerText = tutorAdModel.ExpertiseLevel
        lblTravelOption.InnerText = tutorAdModel.TravelOption
        lblPricingOption.InnerText = tutorAdModel.PricingOption
        whatToBring.Visible = Not String.IsNullOrEmpty(tutorAdModel.WhatToBring)
        lblWhatToBring.InnerText = tutorAdModel.WhatToBring
        Objective.Visible = Not String.IsNullOrEmpty(tutorAdModel.Objective)
        lblObjective.InnerText = tutorAdModel.Objective
    End Sub

End Class