Imports BetterclassifiedsCore.DataModel
Imports BetterclassifiedsCore

Partial Public Class MemberDetails2
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub BindUserDetails(ByVal profile As UserProfile)

        ' perform databinding on the controls


        ddlIndustry.DataSource = AppUserController.GetIndustries()

        Me.DataBind()

        Dim item As New ListItem("", -1)
        ddlIndustry.Items.Insert(0, item)

        With profile
            UserId = profile.UserID
            Me.FirstNameTextBox.Text = .FirstName
            Me.LastNameTextBox.Text = .LastName
            Me.Address1TextBox.Text = .Address1
            Me.CityTextBox.Text = .City
            Me.StateDropDownList.SelectedValue = .State
            Me.ZipCodeTextBox.Text = .PostCode
            Me.PhoneNumberTextBox.Text = .Phone
            Me.SecondaryPhoneTextBox.Text = .SecondaryPhone
            Me.txtEmail.Text = .Email

            ' business details
            Me.txtBusinessName.Text = .BusinessName
            Me.txtABN.Text = .ABN
        End With

        ' only bind the drop down if they have something
        If (profile.Industry > 0) Then
            Dim industry = AppUserController.GetIndustryById(profile.Industry)

            If industry IsNot Nothing Then
                ddlIndustry.SelectedValue = industry.IndustryId

                ddlCategory.DataSource = AppUserController.GetBusinessCategoriesByIndustryId(industry.IndustryId)
                ddlCategory.DataBind()
            End If

        End If

        ' if they have chosen an industry they should also have business category
        ' but we do this just to be sure
        If (profile.BusinessCategory > 0) Then
            Dim category = AppUserController.GetBusinessCategoryById(profile.BusinessCategory)

            If category IsNot Nothing Then
                ddlCategory.SelectedValue = category.BusinessCategoryId
            End If

            'ddlCategory.SelectedValue = AppUserController.GetBusinessCategoryByCode(profile.BusinessCategory).BusinessCategoryId
        End If

    End Sub

    Private Sub ddlIndustry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlIndustry.SelectedIndexChanged
        '' perform databind on the business category
        If (ddlIndustry.SelectedIndex = 0) Then
            ' they have selected no item so we clear all from business category drop down
            ddlCategory.Items.Clear()
        Else
            ddlCategory.DataSource = AppUserController.GetBusinessCategoriesByIndustryId(ddlIndustry.SelectedValue)
            ddlCategory.DataBind()
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        ' we send the information back to the database.

        Dim industryId As Integer = Nothing
        Dim businessCategory As Integer = Nothing
        If ddlIndustry.SelectedIndex > 0 Then
            industryId = ddlIndustry.SelectedValue
            businessCategory = ddlCategory.SelectedValue
        End If

        pnlAlertSuccess.Visible = AppUserController.UpdateUserProfile(UserId, FirstNameTextBox.Text, LastNameTextBox.Text, _
                                               Address1TextBox.Text, "", CityTextBox.Text, _
                                               StateDropDownList.SelectedValue, ZipCodeTextBox.Text, _
                                               PhoneNumberTextBox.Text, SecondaryPhoneTextBox.Text, txtABN.Text, _
                                               txtBusinessName.Text, industryId, businessCategory, txtEmail.Text)



    End Sub

#Region " Properties "

    Public Property ShowEmail() As Boolean
        Get
            Return pnlEmail.Visible
        End Get
        Set(ByVal value As Boolean)
            pnlEmail.Visible = value
        End Set
    End Property

    Private Property UserId() As Guid
        Get
            Return ViewState("userId")
        End Get
        Set(ByVal value As Guid)
            ViewState("userId") = value
        End Set
    End Property

#End Region

End Class