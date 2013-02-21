Imports BetterclassifiedsCore.Controller
Imports System.Data.SqlClient

Namespace DataModel
    Partial Public Class Search
        Implements IDisposable

        Private Shared _context As BetterclassifiedsDataContext
        Private Property Context() As BetterclassifiedsDataContext
            Get
                Return _context
            End Get
            Set(ByVal value As BetterclassifiedsDataContext)
                _context = value
            End Set
        End Property

        Public Sub New()
            Context = BetterclassifiedsDataContext.NewContext
        End Sub

#Region "Search Ad Bookings"

        Public Function SearchAdBookings(ByVal adDesignId As Nullable(Of Integer), ByVal bookReference As String, ByVal username As String, _
                                         ByVal bookingDateStart As Nullable(Of DateTime), ByVal bookingDateEnd As Nullable(Of DateTime), _
                                         ByVal bookingStatus As Nullable(Of Integer), ByVal publicationId As Nullable(Of Integer), _
                                         ByVal parentCategoryId As Nullable(Of Integer), _
                                         ByVal mainCategoryId As Nullable(Of Integer), ByVal searchText As String, _
                                         ByVal editionStart As Nullable(Of DateTime), ByVal editionEnd As Nullable(Of DateTime)) As IList
            ' set up all the parameters for the sql stored procedure to work properly

            ' book reference
            Dim reference As String = Nothing
            If Not String.IsNullOrEmpty(bookReference) Then
                reference = bookReference
            End If

            ' username
            Dim user As String = Nothing
            If Not String.IsNullOrEmpty(username) Then
                user = username
            End If

            ' booking date start
            Dim bookStart As Nullable(Of DateTime) = Nothing
            If bookingDateStart.HasValue And bookingDateStart > DateTime.MinValue Then
                bookStart = bookingDateStart
            End If

            ' booking date end
            Dim bookEnd As Nullable(Of DateTime) = Nothing
            If bookingDateEnd.HasValue And bookingDateEnd > DateTime.MinValue Then
                bookEnd = bookingDateEnd
            End If

            ' booking status
            Dim status As Nullable(Of Integer) = Nothing
            If bookingStatus.HasValue And bookingStatus > 0 Then
                status = bookingStatus
            End If

            ' publication 
            Dim publication As Nullable(Of Integer) = Nothing
            If publicationId.HasValue And publicationId > 0 Then
                publication = publicationId
            End If

            ' main category/sub
            Dim parentCategory As Nullable(Of Integer) = Nothing
            If parentCategoryId.HasValue And parentCategoryId > 0 Then
                parentCategory = parentCategoryId
            End If

            ' main category/sub
            Dim mainCategory As Nullable(Of Integer) = Nothing
            If mainCategoryId.HasValue And mainCategoryId > 0 Then
                mainCategory = mainCategoryId
            End If

            ' search text
            Dim search As String = Nothing
            If Not String.IsNullOrEmpty(searchText) Then
                search = searchText
            End If

            ' booking date start
            Dim edStart As Nullable(Of DateTime) = Nothing
            If editionStart.HasValue And editionStart > DateTime.MinValue Then
                edStart = editionStart
            End If

            ' booking date end
            Dim edEnd As Nullable(Of DateTime) = Nothing
            If editionEnd.HasValue And editionEnd > DateTime.MinValue Then
                edEnd = bookingDateEnd
            End If

            ' execute the DB Command using LINQ DBML
            Return Context.spAdBookingsSearch(adDesignId, reference, user, bookStart, bookEnd, status, _
                                              publication, parentCategory, mainCategory, search, _
                                              editionStart, editionEnd).ToList

        End Function

#End Region

#Region "Search Online Ads"

        Public Function SearchOnlineAdsCount(ByVal category As Nullable(Of Integer), ByVal subCategoryId As Nullable(Of Integer), _
                                           ByVal locationId As Nullable(Of Integer), ByVal areaId As Nullable(Of Integer), _
                                           ByVal keyword As String, ByVal startRowIndex As Integer, ByVal maximumRows As Integer) As Integer
            If keyword Is Nothing Then
                keyword = ""
            End If
            Dim query = Context.spOnlineAdSelect(category, subCategoryId, locationId, areaId, _
                                                 keyword, BookingStatus.BOOKED).ToList
            Return query.Count
        End Function

        Public Shared Function SearchOnlineAds(ByVal category As Nullable(Of Integer), ByVal subCategoryId As Nullable(Of Integer), _
                                           ByVal locationId As Nullable(Of Integer), ByVal areaId As Nullable(Of Integer), _
                                           ByVal keyword As String, ByVal startRowIndex As Integer, ByVal maximumRows As Integer) As DataSource
            If keyword Is Nothing Then
                keyword = ""
            End If
            Using proxy = BetterclassifiedsDataContext.NewContext

                Dim cmd As New SqlCommand With {.Connection = proxy.Connection, .CommandType = CommandType.StoredProcedure, .CommandText = Proc.GetOnlineAd.Name}

                If category.HasValue Then
                    cmd.Parameters.Add(New SqlParameter(Proc.GetOnlineAd.Params.ParentCategoryId, category))
                End If

                If (subCategoryId.HasValue) Then
                    cmd.Parameters.AddWithValue(Proc.GetOnlineAd.Params.SubCategoryId, subCategoryId)
                End If

                If (locationId.HasValue) Then
                    cmd.Parameters.AddWithValue(Proc.GetOnlineAd.Params.LocationId, locationId)
                End If

                If (areaId.HasValue) Then
                    cmd.Parameters.AddWithValue(Proc.GetOnlineAd.Params.AreaId, areaId)
                End If

                If (Not String.IsNullOrEmpty(keyword)) Then
                    cmd.Parameters.AddWithValue(Proc.GetOnlineAd.Params.Keyword, keyword)
                End If

                cmd.Parameters.AddWithValue(Proc.GetOnlineAd.Params.PageIndex, startRowIndex)

                cmd.Parameters.AddWithValue(Proc.GetOnlineAd.Params.PageSize, maximumRows)

                cmd.Parameters.Add(New SqlParameter With {.Direction = ParameterDirection.Output, .ParameterName = Proc.GetOnlineAd.Params.TotalPopulation, .Size = 4})
                Dim ds = New DataSet
                Using adp As New SqlDataAdapter(cmd)
                    cmd.Connection.Open()
                    Try
                        adp.Fill(ds)
                    Catch ex As Exception
                    Finally
                        cmd.Connection.Close()
                    End Try
                End Using

                Return New DataSource(ds.Tables(0), CType(cmd.Parameters(Proc.GetOnlineAd.Params.TotalPopulation).Value, Int16))
            End Using
        End Function

#End Region

#Region "Search Online Ads by Category"

        Public Function SearchOnlineAdByCategoryCount(ByVal categoryId As Integer, _
                                                      ByVal startRowIndex As Integer, ByVal maximumRows As Integer) As Integer
            Dim query = Context.spOnlineAdSelectByCategory(categoryId, BookingStatus.BOOKED).ToList
            Return query.Count
        End Function

        Public Function SearchOnlineAdByCategory(ByVal categoryId As Integer, _
                                                 ByVal startRowIndex As Integer, ByVal maximumRows As Integer) As IList
            Dim query = Context.spOnlineAdSelectByCategory(categoryId, BookingStatus.BOOKED).Skip(startRowIndex).Take(maximumRows).ToList
            Return query
        End Function
#End Region

        Public Function GetTransaction(ByVal bookRef As String) As Transaction
            Return _context.Transactions.Where(Function(b) b.Title = bookRef).Single
        End Function

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free other state (managed objects).
                End If

                ' TODO: free your own state (unmanaged objects).
                ' TODO: set large fields to null.
                _context.Connection.Close()
                _context = Nothing
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

