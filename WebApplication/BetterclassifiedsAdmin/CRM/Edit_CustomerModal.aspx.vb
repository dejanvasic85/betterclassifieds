Imports ConfigManager = BetterclassifiedAdmin.Configuration.ConfigManager
Imports BetterclassifiedsCore

Namespace CRM
    Partial Public Class Edit_CustomerModal
        Inherits System.Web.UI.Page

        ' declare global variables
        Private username As String
        Private _user As MembershipUser
        Private createRoleSuccess As Boolean = True

#Region "On Page Load"

        Private Sub Page_Load() Handles Me.Load
            ' check if username exists in the query string
            username = Request.QueryString("username")
            If username Is Nothing OrElse username = "" Then
                Response.Redirect("Customers.aspx")
            End If
            _user = ConfigManager.CustomerMembershipProvider.GetUser(username, False)

            If Not Page.IsPostBack Then
                hdnEmailOriginal.Value = _user.Email
                ddlCountries.DataSource = CountryNames.CountryNames.GetCountries()
                ddlCountries.DataBind()
                UpdateScreen()
            End If
        End Sub
        Private Sub UpdateScreen()
            UserUpdateMessage.Text = ""

            Dim user = ConfigManager.CustomerMembershipProvider.GetUser(username, False)
            If user.IsLockedOut = False Then
                lblCurrentPassword.Text = user.GetPassword
            Else
                lblCurrentPassword.Text = "Account is locked out"
            End If
           
            ' get the selected user's profile based on query string
            ProfileManager.ApplicationName = ConfigManager.CustomerProfileProvider.ApplicationName
            Dim _profile As Object = ProfileCommon1.GetProfile(username)

            ' Subscriptions
            ddlNewsletter.SelectedValue = _profile.NewsletterSubscription

            ' Personal Info
            txtFirstName.Text = _profile.FirstName
            txtLastName.Text = _profile.LastName
          
            ProfileManager.ApplicationName = ConfigManager.UserAdminProfileProvider.ApplicationName
        End Sub
#End Region

#Region "Update Profile Sub"

        Public Sub SaveProfile()
            ' get the selected user's profile
            If String.IsNullOrEmpty(username) Then
                Return
            End If
            ProfileManager.ApplicationName = ConfigManager.CustomerProfileProvider.ApplicationName
            Dim _profile As Object = ProfileCommon1.GetProfile(username)

            ' Subscriptions
            _profile.NewsletterSubscription = ddlNewsletter.SelectedValue

            ' Personal Info
            _profile.FirstName = txtFirstName.Text
            _profile.LastName = txtLastName.Text

            ' to save the user's profile
            _profile.Save()
            ProfileManager.ApplicationName = ConfigManager.UserAdminProfileProvider.ApplicationName
        End Sub

#End Region

#Region "Update Profile Button Click"

        Protected Sub btnUpdateProfile_Click(ByVal sender As Object, ByVal e As EventArgs)
            SaveProfile()
            lblProfileMessage.Text = "Profile saved successfully!"
        End Sub

#End Region

#Region "Delete Profile Button Click"

        Protected Sub btnDeleteProfile_Click(ByVal sender As Object, ByVal e As EventArgs)
            ConfigManager.CustomerProfileProvider.DeleteProfiles(New String() {username})
            lblProfileMessage.Text = "Profile deleted successfully!"

            ' refresh the page to clear post back data from form fields
            Response.Redirect("edit_user_modal_success.aspx")
        End Sub

#End Region

#Region "Update Membership User Info"

        Protected Sub UserInfo_ItemUpdating(ByVal sender As Object, ByVal e As DetailsViewUpdateEventArgs)
            
            _user.Email = DirectCast(e.NewValues(0), String)
            _user.Comment = DirectCast(e.NewValues(1), String)
            _user.IsApproved = CBool(e.NewValues(2))

            Try
                If (String.Compare(_user.Email, hdnEmailOriginal.Value) <> 0) Then
                    AppUserController.UpdateProfileEmailForUsername(_user.UserName, _user.Email)
                End If

                ConfigManager.CustomerMembershipProvider.UpdateUser(_user)

                hdnEmailOriginal.Value = _user.Email
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

#Region "Delete User"

        Public Sub DeleteUser(ByVal sender As Object, ByVal e As EventArgs)
            ' Membership.DeleteUser(username, false);
            ConfigManager.CustomerProfileProvider.DeleteProfiles(New String() {username})
            ConfigManager.CustomerMembershipProvider.DeleteUser(username, True)
            Response.Redirect("edit_customermodal_success.aspx")
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

#Region "Change Password Button Click"

        Public Sub ChangePassword_OnClick(ByVal sender As Object, ByVal args As EventArgs)
            ' Update the password.
            ' check if username exists in the query string
            username = Request.QueryString("username")
            If username Is Nothing OrElse username = "" Then
                Response.Redirect("users.aspx")
            End If

            Dim u As MembershipUser = ConfigManager.CustomerMembershipProvider.GetUser(username, False)
            If (u.IsLockedOut) Then
                Msg.Text = "This account is locked out. Please unlock user before updating password"
                Return
            End If

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