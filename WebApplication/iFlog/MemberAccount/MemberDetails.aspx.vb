Imports BetterclassifiedsCore.DataModel
Imports BetterclassifiedsCore

Partial Public Class MemberDetails1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            ' we need to get the linq user object and pass into the control to load data
            Dim username As String = Membership.GetUser().UserName
            Dim userProfile As DataModel.UserProfile = AppUserController.GetAppUserProfile(username)

            ' bind to the control's UI elements
            ucxMemberDetails.BindUserDetails(userProfile)
        End If
    End Sub

End Class