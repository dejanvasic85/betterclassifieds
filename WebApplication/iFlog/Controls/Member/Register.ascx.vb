Imports BetterclassifiedsCore
Imports Paramount.Betterclassified.Xavier.ProfileProvider
Imports Paramount.Broadcast.Components

Partial Public Class Register1
    Inherits System.Web.UI.UserControl


    Private pageValid As String = "pageValid"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            ' perform databind on business industry list to drop down
            Me.ddlIndustry.DataSource = AppUserController.GetIndustries
            Me.DataBind()

            '' add extra value into the industry list so that if the user doesn't select it, an empty value will be saved into db
            Dim item As New ListItem()
            ddlIndustry.Items.Insert(0, item)

            ViewState(pageValid) = False
        End If
    End Sub

    Private Sub ddlIndustry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlIndustry.SelectedIndexChanged
        '' perform databind on the business category
        If (ddlIndustry.SelectedIndex = 0) Then
            ' they have selected no item so we clear all from business category drop down
            ddlBusinesscategory.Items.Clear()
        Else
            ddlBusinesscategory.DataSource = AppUserController.GetBusinessCategoriesByIndustryId(ddlIndustry.SelectedValue)
            ddlBusinesscategory.DataBind()
        End If
    End Sub

#Region "Validation - User exists"

    Protected Sub Check_Username(ByVal sender As Object, ByVal e As System.EventArgs)
        CheckUsernameExists()
    End Sub

    ''' <summary>
    ''' Performs a server side validation on the provided username whether it exists or not.
    ''' </summary>
    Private Function CheckUsernameExists() As Boolean
        Dim lbl As Label = TryCast(SiteCreateUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("lblUsernameAvailability"), Label)
        Dim userExists As Boolean = False
        If (SiteCreateUserWizard.UserName <> String.Empty) Then

            userExists = AppUserController.UsernameExists(SiteCreateUserWizard.UserName)
            If userExists = False And lbl IsNot Nothing Then
                lbl.Text = "Username Available."
                lbl.ForeColor = Drawing.Color.Green
            Else
                lbl.Text = "Username already exists."
            End If
        Else
            lbl.Text = "Please provide username."
        End If
        ViewState(pageValid) = (userExists = False)
        Return userExists
    End Function

#End Region

#Region "Create User Methods"

    Private Sub SiteCreateUserWizard_ContinueButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles SiteCreateUserWizard.ContinueButtonClick
        Response.Redirect(PageUrl.Home())
    End Sub

    Private Sub SiteCreateUserWizard_CreatingUser(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LoginCancelEventArgs) Handles SiteCreateUserWizard.CreatingUser
        If CheckUsernameExists() Then
            ' check if the username exists and cancel the controls action (don't create the membership)
            e.Cancel = True
        End If
    End Sub

    Private Sub SiteCreateUserWizard_CreatedUser(ByVal sender As Object, ByVal e As System.EventArgs) Handles SiteCreateUserWizard.CreatedUser
        If Page.IsValid And ViewState(pageValid) Then
            ' create the User Profile details (goes into UserProfile database table)
            Dim UserProfile As AppUserProfile = New AppUserProfile
            UserProfile.Initialize(SiteCreateUserWizard.UserName)
            UserProfile.FirstName = FirstNameTextBox.Text
            UserProfile.LastName = LastNameTextBox.Text
            UserProfile.Address1 = Address1TextBox.Text
            UserProfile.Address2 = ""
            UserProfile.City = CityTextBox.Text
            UserProfile.State = StateDropDownList.SelectedValue
            UserProfile.Postcode = ZipCodeTextBox.Text
            UserProfile.Phone = PhoneNumberTextBox.Text
            UserProfile.Email = SiteCreateUserWizard.Email
            UserProfile.ABN = ABNTextBox.Text
            UserProfile.BusinessName = BusinessNameTextBox.Text
            UserProfile.SecondaryPhone = SecondaryPhoneTextBox.Text
            UserProfile.ProfileVersion = SqlTableProfileProvider.ProfileVersion
            UserProfile.Industry = GetIndustryCode()
            UserProfile.BusinessCategory = GetCategoryCode()
            UserProfile.Save()
        End If
        NotiFyUser()
    End Sub

#End Region

#Region "Notify User - Email"

    Private Sub NotiFyUser()
        Dim email As New RegistrationNotification(SiteCreateUserWizard.UserName, SiteCreateUserWizard.Password, SiteCreateUserWizard.Email)
        email.Send()
    End Sub

#End Region

#Region "Helper Methods"

    Private Function GetIndustryCode() As Integer
        If (ddlIndustry.SelectedIndex > 0) Then
            Return ddlIndustry.SelectedValue
        Else
            Return Nothing
        End If
    End Function

    Private Function GetCategoryCode() As Integer
        If (ddlIndustry.SelectedIndex > 0) Then
            Return ddlBusinesscategory.SelectedValue
        Else
            Return Nothing
        End If
    End Function

#End Region

End Class