Imports BetterclassifiedsCore

Partial Public Class _Default3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            ' we need to get the linq user object and pass into the control to load data
            Dim username As String = Membership.GetUser().UserName
            Dim userProfile As DataModel.UserProfile = AppUserController.GetAppUserProfile(username)

            ' bind to the control's UI elements
            With userProfile
                lblFirstName.Text = .FirstName
                lblLastName.Text = .LastName
                lblStreetAddress.Text = .Address1
                lblState.Text = .State
                lblSuburb.Text = .City
                lblPostCode.Text = .PostCode
                lblTelephone.Text = .Phone
                lblSecondaryTelephone.Text = .SecondaryPhone
                lblEmail.Text = .Email
                lblCompanyName.Text = .BusinessName
                lblABN.Text = .ABN

                Using cr As New CRM.CrmController
                    If .Industry > 0 Then
                        Dim ind = cr.GetIndustryById(.Industry)
                        If ind IsNot Nothing Then
                            lblIndustry.Text = ind.Title
                        End If

                        If .BusinessCategory > 0 Then
                            Dim cat = cr.GetBusinessCategoryById(.BusinessCategory)
                            If cat IsNot Nothing Then
                                lblCategory.Text = cat.Title
                            End If
                        End If
                        
                    End If

                End Using

            End With



        End If

    End Sub

End Class