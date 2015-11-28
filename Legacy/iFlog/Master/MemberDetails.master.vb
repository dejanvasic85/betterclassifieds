Imports BetterClassified

Partial Public Class MemberDetails
    Inherits System.Web.UI.MasterPage

    Public Sub SetActiveMenuItem(ByVal menuItem As String)
        Dim control = Me.FindControl(menuItem)
    End Sub
End Class