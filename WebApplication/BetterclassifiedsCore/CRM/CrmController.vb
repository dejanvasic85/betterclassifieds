Imports BetterclassifiedsCore.DataModel

Namespace CRM

    Public Class CrmController
        Implements IDisposable

        Private _context As AppUserDataContext

        Public Sub New(ByVal connection As String)
            _context = New AppUserDataContext(connection)
        End Sub

        Public Sub New()
            _context = AppUserDataContext.NewContext
        End Sub

        Public ReadOnly Property Context() As AppUserDataContext
            Get
                Return _context
            End Get
        End Property

#Region "Create"

        Public Function CreateIndustry(ByVal title As String) As Integer
            Dim ind As New Industry With {.Title = title}
            _context.Industries.InsertOnSubmit(ind)
            _context.SubmitChanges()
            Return ind.IndustryId
        End Function

        Public Function CreateBusinessCategory(ByVal title As String, ByVal industryId As Integer) As Integer
            Dim category As New BusinessCategory With {.Title = title, .IndustryId = industryId}
            _context.BusinessCategories.InsertOnSubmit(category)
            _context.SubmitChanges()
            Return category.BusinessCategoryId
        End Function

#End Region

#Region "Retrieve"

        Public Function GetUserProfile(ByVal userId As String) As BusinessEntities.UserProfileEntity
            Dim user = From u In _context.UserProfiles Join a In _context.aspnet_Users On a.UserId Equals u.UserID _
                       Where a.UserName = userId Select New BusinessEntities.UserProfileEntity With {.FirstName = u.FirstName, .LastName = u.LastName, _
                                                                                                     .RefNumber = u.RefNumber, .Email = u.Email, _
                                                                                                     .Address1 = u.Address1, .Address2 = u.Address2, _
                                                                                                     .City = u.City, .State = u.State, .PostCode = u.PostCode, _
                                                                                                     .Phone = u.Phone, .SecondaryPhone = u.SecondaryPhone, _
                                                                                                     .PreferedContact = u.PreferedContact, _
                                                                                                     .BusinessName = u.BusinessName, .ABN = u.ABN, _
                                                                                                     .Industry = u.Industry, .BusinessCategory = u.BusinessCategory, _
                                                                                                     .ProfileVersion = u.ProfileVersion, .LastUpdatedDate = u.LastUpdatedDate}
            If user.Count > 0 Then
                Return user.Single
            Else
                Return Nothing
            End If
        End Function

        Public Function GetIndustries() As IList
            Return _context.Industries.OrderBy(Function(i) i.Title).ToList
        End Function

        Public Function GetIndustryById(ByVal id As Integer) As DataModel.Industry
            Dim ind = From i In _context.Industries Where i.IndustryId = id Select i
            If ind.Count > 0 Then
                Return ind.FirstOrDefault
            Else
                Return Nothing
            End If
        End Function

        Public Function GetBusinessCategoryByIndustry(ByVal industryId As Integer) As IList
            Return _context.BusinessCategories.Where(Function(i) i.IndustryId = industryId).OrderBy(Function(t) t.Title).ToList
        End Function

        Public Function GetBusinessCategoryById(ByVal id As Integer) As DataModel.BusinessCategory
            Dim cat = From c In _context.BusinessCategories Where c.BusinessCategoryId = id Select c
            If cat.Count > 0 Then
                Return cat.FirstOrDefault
            Else
                Return Nothing
            End If
        End Function

        Public Function IndustryExists(ByVal title As String) As Boolean
            Return _context.Industries.Where(Function(i) i.Title.ToLower = title.ToLower).Count > 0
        End Function

        Public Function BusinessCategoryExists(ByVal title As String, ByVal industryId As Integer) As Boolean
            Dim query = From b In _context.BusinessCategories Where b.Title.ToLower = title.ToLower And b.IndustryId = industryId Select b
            Return query.Count > 0
        End Function

        Public Function UsernameExists(ByVal username As String) As Boolean
            Using db As New DataModel.AppUserDataContext
                Return _context.aspnet_Users.Where(Function(i) i.UserName = username).Count
            End Using
        End Function

#End Region

#Region "Update"

        Public Sub UpdateIndustry(ByVal id As Integer, ByVal title As String, ByVal description As String)
            Dim ind = From i In _context.Industries Where i.IndustryId = id Select i
            If ind.Count > 0 Then
                With ind.FirstOrDefault()
                    .Title = title
                    .Description = description
                End With
            End If
            _context.SubmitChanges()
        End Sub

        Public Sub UpdateBusinessCategory(ByVal id As Integer, ByVal title As String, ByVal description As String)
            Dim cat = From c In _context.BusinessCategories Where c.BusinessCategoryId = id Select c
            If cat.Count > 0 Then
                With cat.FirstOrDefault
                    .Title = title
                    .Description = description
                End With
            End If
            _context.SubmitChanges()
        End Sub

#End Region

#Region "Delete"

        Public Sub DeleteIndustry(ByVal industryId As Integer)
            ' delete categories first
            Dim cat = From c In _context.BusinessCategories Where c.IndustryId = industryId Select c
            _context.BusinessCategories.DeleteAllOnSubmit(cat)
            ' delete industry
            Dim ind = From i In _context.Industries Where i.IndustryId = industryId Select i
            _context.Industries.DeleteAllOnSubmit(ind)
            _context.SubmitChanges()
        End Sub

        Public Sub DeleteBusinessCategory(ByVal businessCategoryId As Integer)
            Dim cat = From c In _context.BusinessCategories Where c.BusinessCategoryId = businessCategoryId Select c
            _context.BusinessCategories.DeleteAllOnSubmit(cat)
            _context.SubmitChanges()
        End Sub

#End Region

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free other state (managed objects).
                    _context.Connection.Close()
                    _context = Nothing
                End If

                ' TODO: free your own state (unmanaged objects).
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace