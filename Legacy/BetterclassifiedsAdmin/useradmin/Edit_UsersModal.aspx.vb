Imports BetterclassifiedAdmin.Configuration.ConfigManager
Imports BetterclassifiedAdmin.CRM
Imports BetterclassifiedAdmin.Configuration
Namespace UserAdmin
    Partial Public Class Edit_UsersModal
        Inherits System.Web.UI.Page

#Region "Global Variables"


        ' declare global variables
        Private username As String
        Private _user As MembershipUser
        Private createRoleSuccess As Boolean = True
#End Region

#Region "On Page Prerender"

        Private Sub Page_PreRender() Handles Me.PreRender

            ' Load the User Roles into checkboxes.
            chkUserRoles.DataSource = Roles.GetAllRoles()
            chkUserRoles.DataBind()

            ' Disable checkboxes if appropriate:
            If UserInfo.CurrentMode <> DetailsViewMode.Edit Then
                Dim checkbox As ListItem
                For Each checkbox In chkUserRoles.Items
                    checkbox.Enabled = False
                Next checkbox
            End If

            ' Bind these checkboxes to the User's own set of roles.
            Dim _userRoles As String() = Roles.GetRolesForUser(username)
            Dim _role As String
            For Each _role In _userRoles
                Dim checkbox As ListItem = chkUserRoles.Items.FindByValue(_role)
                checkbox.Selected = True
            Next _role

        End Sub 'Page_PreRender

#End Region

#Region "On Page Load"

        Private Sub Page_Load() Handles Me.Load
            ' check if username exists in the query string
            username = Request.QueryString("username")
            If username Is Nothing OrElse username = "" Then
                Response.Redirect("Users.aspx")
            End If

            ' get membership user account based on username sent in query string
            _user = ConfigManager.UserAdminMembershipProvider.GetUser(username, False)
            If _user.IsLockedOut Then
                Return
            End If
            UserUpdateMessage.Text = ""

            ' get selected user's password start.................................................................
            Dim password As String = ConfigManager.UserAdminMembershipProvider.GetPassword(username, Nothing)
            lblCurrentPassword.Text = password
            ' get selected user's password end...................................................................

        End Sub

#End Region



#Region "Update Membership User Info"

        Protected Sub UserInfo_ItemUpdating(ByVal sender As Object, ByVal e As DetailsViewUpdateEventArgs)
            ' Need to handle the update manually because MembershipUser does not have a
            ' parameterless constructor  

            _user.Email = DirectCast(e.NewValues(0), String)
            _user.Comment = DirectCast(e.NewValues(1), String)
            _user.IsApproved = CBool(e.NewValues(2))

            Try
                ' Update user info:
                ConfigManager.UserAdminMembershipProvider.UpdateUser(_user)

                ' Update user roles:
                UpdateUserRoles()

                UserUpdateMessage.Text = "Update Successful."

                ' make cancel button available
                e.Cancel = True

                ' make detailsview read only
                UserInfo.ChangeMode(DetailsViewMode.[ReadOnly])
            Catch ex As Exception
                ' if there is a problem
                UserUpdateMessage.Text = "Update Failed: " + ex.Message

                e.Cancel = True
                UserInfo.ChangeMode(DetailsViewMode.[ReadOnly])
            End Try
        End Sub

#End Region

#Region "Update User Roles"

        Private Sub UpdateUserRoles()
            ' add or remove user from role based on selection
            For Each rolebox As ListItem In chkUserRoles.Items
                If rolebox.Selected Then
                    If Not Roles.IsUserInRole(username, rolebox.Text) Then
                        Roles.AddUserToRole(username, rolebox.Text)
                    End If
                Else
                    If Roles.IsUserInRole(username, rolebox.Text) Then
                        Roles.RemoveUserFromRole(username, rolebox.Text)
                    End If
                End If
            Next
        End Sub

#End Region

#Region "Delete User"

        Public Sub DeleteUser(ByVal sender As Object, ByVal e As EventArgs)
            ' Membership.DeleteUser(username, false);
            ProfileManager.DeleteProfile(username)
            ConfigManager.UserAdminMembershipProvider.DeleteUser(username, True)
            Response.Redirect("Edit_user_modal_success.aspx")
        End Sub

#End Region

#Region "Unlock User"

        Public Sub UnlockUser(ByVal sender As Object, ByVal e As EventArgs)

            ' Unlock the user.
            _user.UnlockUser()

            ' DataBind the DetailsView to reflect same.
            UserInfo.DataBind()
        End Sub

#End Region

        '#Region "Add Role"

        '        Public Sub AddRole(ByVal sender As Object, ByVal e As EventArgs)
        '            ' create new roles
        '            Try
        '                Roles.CreateRole(NewRole.Text)
        '                ConfirmationMessage.InnerText = "The new role was added."
        '                createRoleSuccess = True
        '            Catch ex As Exception
        '                ConfirmationMessage.InnerText = ex.Message
        '                createRoleSuccess = False
        '            End Try
        '        End Sub

        '#End Region

#Region "Change Password Button Click"

        Public Sub ChangePassword_OnClick(ByVal sender As Object, ByVal args As EventArgs)
            ' Update the password.
            ' check if username exists in the query string
            username = Request.QueryString("username")
            If username Is Nothing OrElse username = "" Then
                Response.Redirect("users.aspx")
            End If

            Dim u As MembershipUser = ConfigManager.UserAdminMembershipProvider.GetUser(username, False)

            Try
                If u.ChangePassword(OldPasswordTextbox.Text, PasswordTextbox.Text) Then
                    Msg.Text = "Password changed successfully and will show upon refresh."
                Else
                    Msg.Text = "Password change failed. Please re-enter your values and try again."
                End If
            Catch e As Exception
                Msg.Text = "An exception occurred: " + Server.HtmlEncode(e.Message) + ". Please re-enter your values and try again."
            End Try
        End Sub

#End Region
    End Class
End Namespace