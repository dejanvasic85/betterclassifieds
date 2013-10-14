Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.ApplicationBlock.Data

Namespace Configuration
    Public Class ConfigManager
        Private Const _customerMembershipProvider As String = "CustomerMembershipProvider"
        Private Const _userAdminMembershipProvider As String = "UserAdminMembershipProvider"
        Private Const classifiedConnectionString As String = "BetterclassifiedsConnection"
        Private Const appuserConnection As String = "AppUserConnection"
        Private Const configSection As String = "paramount/services"

        Public Shared ReadOnly Property CustomerMembershipProvider() As MembershipProvider
            Get
                Return Membership.Providers(_customerMembershipProvider)
            End Get
        End Property

        Public Shared ReadOnly Property NewsletterSubscribersHandlerPath As String
            Get
                Return System.Configuration.ConfigurationManager.AppSettings("NewsletterSubscribersHandler")
            End Get
        End Property

        Public Shared ReadOnly Property UserAdminMembershipProvider() As MembershipProvider
            Get
                Return Membership.Providers(_userAdminMembershipProvider)
            End Get
        End Property

        Public Shared ReadOnly Property CustomerRoleProvider() As RoleProvider
            Get
                Return Roles.Providers("CustomerRoleProvider")
            End Get
        End Property

        Public Shared ReadOnly Property UserAdminProfileProvider() As ProfileProvider
            Get
                Return Profile.ProfileManager.Providers("UserAdminProfileProvider")
            End Get
        End Property

        Public Shared Function CustomerProfileProvider() As ProfileProvider
            Return Profile.ProfileManager.Providers("CustomerProfileProvider")
        End Function

        Public Shared ReadOnly Property WebResourcesDBConnection() As String
            Get
                Return ConfigReader.GetConnectionString(configSection, appuserConnection)
            End Get
        End Property

        Public Shared ReadOnly Property DBConnection() As String
            Get
                Return ConfigReader.GetConnectionString(configSection, appuserConnection)
            End Get
        End Property

        Public Shared ReadOnly Property ClassifiedDBConnection() As String
            Get
                Return ConfigReader.GetConnectionString(configSection, classifiedConnectionString)
            End Get
        End Property

        Public Shared ReadOnly Property ASPNetStateConnection() As String
            Get
                Return ConfigReader.GetConnectionString(configSection, appuserConnection)
            End Get
        End Property

    End Class

    Public Class CustomerMemberShip

        Public Shared Function GetUser(ByVal username As String) As MembershipUser
            If String.IsNullOrEmpty(username) Then Return Nothing
            Return Membership.Providers("CustomerMembershipProvider").GetUser(username, False)
        End Function

        Public Shared Sub UpdateUser(ByVal user As MembershipUser)
            If user Is Nothing Then Return
            Membership.Providers("CustomerMembershipProvider").UpdateUser(user)
        End Sub

    End Class
End Namespace