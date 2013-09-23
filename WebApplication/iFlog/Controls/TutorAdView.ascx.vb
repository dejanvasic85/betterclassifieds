Imports BetterClassified.UI.Models

Public Class TutorAdView
    Inherits System.Web.UI.UserControl
    Implements IOnlineAdView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub DatabindAd(Of T)(ByVal adDetails As T) Implements IOnlineAdView.DatabindAd
        Dim tutorAdModel = TryCast(adDetails, TutorAdModel)

    End Sub

End Class