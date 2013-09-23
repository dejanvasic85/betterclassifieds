Imports BetterClassified.UI.Models

Public Class TutorAdView
    Inherits System.Web.UI.UserControl
    Implements IOnlineAdView(Of TutorAdModel)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub DatabindAd(ByVal adDetails As TutorAdModel) Implements IOnlineAdView(Of TutorAdModel).DatabindAd

    End Sub

End Class