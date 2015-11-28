
Imports Paramount.Common.DataTransferObjects.Betterclassifieds
Imports Paramount.Common.DataTransferObjects.LoggingService
Imports BetterclassifiedAdmin.CRM

Partial Public Class Add_user
    Inherits System.Web.UI.Page


#Region "get roles and databind it to role list"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' Reference the SpecifyRolesStep WizardStep
            Dim SpecifyRolesStep As WizardStep = TryCast(RegisterUserWithRoles.FindControl("SpecifyRolesStep"), WizardStep)

            ' Reference the RoleList CheckBoxList
            Dim RoleList As CheckBoxList = TryCast(SpecifyRolesStep.FindControl("RoleList"), CheckBoxList)

            ' Bind the set of roles to RoleList
            RoleList.DataSource = Roles.GetAllRoles()
            RoleList.DataBind()
        End If
    End Sub

#End Region

#Region "add user to role"

    Protected Sub RegisterUserWithRoles_ActiveStepChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Have we JUST reached the Complete step?
        If RegisterUserWithRoles.ActiveStep.Title = "Complete" Then
            ' Reference the SpecifyRolesStep WizardStep
            Dim SpecifyRolesStep As WizardStep = TryCast(RegisterUserWithRoles.FindControl("SpecifyRolesStep"), WizardStep)

            ' Reference the RoleList CheckBoxList
            Dim RoleList As CheckBoxList = TryCast(SpecifyRolesStep.FindControl("RoleList"), CheckBoxList)

            ' Add the checked roles to the just-added user
            For Each li As ListItem In RoleList.Items
                If li.Selected Then
                    Roles.AddUserToRole(RegisterUserWithRoles.UserName, li.Text)
                End If
            Next
        End If
    End Sub

#End Region

#Region "do not show newly created user as online"

    ' this code has been depricated as the number of online users now work 
    ' with the global.asax file. It won't hurt anything though.
    Protected Sub RegisterUserWithRoles_CreatedUser(ByVal sender As Object, ByVal e As EventArgs)
        ' do not show newly created user as Online
        Dim muser As MembershipUser = Membership.GetUser(RegisterUserWithRoles.UserName)
        muser.LastActivityDate = DateTime.Now.AddDays(-1)
        Membership.UpdateUser(muser)
    End Sub

#End Region



End Class