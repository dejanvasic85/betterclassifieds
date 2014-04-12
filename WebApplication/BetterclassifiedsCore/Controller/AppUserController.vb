Imports BetterclassifiedsCore.DataModel

Public Class AppUserController

#Region "Create"



#End Region

#Region "Retrieve"

    Public Shared Function GetAppUserProfile(ByVal username As String) As DataModel.UserProfile

        Using db = AppUserDataContext.NewContext
            Dim user = From u In db.aspnet_Users _
                       Where u.UserName = username _
                       Join p In db.UserProfiles On u.UserId Equals p.UserID _
                       Select p
            Return user.Single
        End Using

    End Function

    Public Shared Function UsernameExists(ByVal username As String) As Boolean
        Using db = AppUserDataContext.NewContext
            Dim user = From u In db.aspnet_Users _
                       Where u.UserName = username _
                       Select u

            Return user.Count > 0
        End Using
    End Function

    Public Shared Function EmailExists(ByVal email As String) As Boolean
        Using db = AppUserDataContext.NewContext
            Return db.UserProfiles.Any(Function(u) u.Email.ToLower = email.ToLower) _
                Or db.aspnet_Memberships.Any(Function(m) m.LoweredEmail = email.ToLower)
        End Using
    End Function

    Public Shared Function GetIndustries() As IList
        Using db = AppUserDataContext.NewContext
            Return db.Industries.OrderBy(Function(i) i.Title).ToList
        End Using
    End Function

    Public Shared Function GetIndustryById(ByVal id As Integer) As DataModel.Industry
        Using db = AppUserDataContext.NewContext
            Dim ind = From i In db.Industries Where i.IndustryId = id Select i

            If ind.Count > 0 Then
                Return ind.FirstOrDefault
            Else
                Return Nothing
            End If
        End Using
    End Function

    Public Shared Function GetBusinessCategoriesByIndustryId(ByVal industryId As Integer) As IList

        Using db = AppUserDataContext.NewContext
            Dim ind = From i In db.BusinessCategories Where i.IndustryId = industryId Select i
            Return ind.ToList
        End Using
    End Function

    Public Shared Function GetBusinessCategoryById(ByVal id As Integer) As DataModel.BusinessCategory
        Using db = AppUserDataContext.NewContext
            Dim ind = From i In db.BusinessCategories Where i.BusinessCategoryId = id Select i
            If ind.Count > 0 Then
                Return ind.FirstOrDefault
            Else
                Return Nothing
            End If
        End Using
    End Function

#End Region

#Region "Update"

    Public Shared Function UpdateUserProfile(ByVal userId As Guid, ByVal firstName As String, ByVal lastName As String, ByVal address1 As String, ByVal address2 As String, _
                                     ByVal city As String, ByVal state As String, ByVal postcode As String, ByVal phone As String, ByVal secPhone As String, _
                                     ByVal abn As String, ByVal businessName As String, ByVal industry As Integer, ByVal category As Integer, ByVal email As String) As Boolean
        Using db = AppUserDataContext.NewContext
            Dim user = (From prof In db.UserProfiles Where prof.UserID = userId Select prof).Single

            With user
                .FirstName = firstName
                .LastName = lastName
                .Address1 = address1
                .Address2 = address2
                .City = city
                .State = state
                .PostCode = postcode
                .Phone = phone
                .SecondaryPhone = secPhone
                .Email = email

                ' business details
                .ABN = abn
                .BusinessName = businessName
                .Industry = industry
                .BusinessCategory = category
            End With

            ' Also update the aspnet_Membership ( maintain consistency )
            Dim member = db.aspnet_Memberships.Single(Function(m) m.UserId = userId)
            member.Email = email
            member.LoweredEmail = email.ToLower

            db.SubmitChanges()
            Return True
        End Using

    End Function

    Public Shared Sub UpdateProfileEmailForUsername(ByVal username As String, ByVal newEmail As String)

        If EmailExists(newEmail) Then
            Throw New ArgumentException("Email belongs to another registered account")
        End If

        Using db = AppUserDataContext.NewContext
            Dim user = db.aspnet_Users.Single(Function(u) u.UserName = username)
            Dim profile = db.UserProfiles.Single(Function(p) p.UserID = user.UserId)

            profile.Email = newEmail
            db.SubmitChanges()
        End Using

    End Sub
#End Region

End Class
