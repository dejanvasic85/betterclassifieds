Imports BetterclassifiedsCore.DataModel
Imports BetterclassifiedsCore.BusinessEntities

Namespace Booking
    <Serializable()> _
    Public Class BookCart
        Inherits AdBooking
        Implements IDisposable
        Implements IBookCartContext

        Private _mainAdType As AdType
        ''' <summary>
        ''' Gets or sets the Main Ad Type object related to the booking.
        ''' </summary>
        Public Property MainAdType() As AdType
            Get
                Return _mainAdType
            End Get
            Set(ByVal value As AdType)
                _mainAdType = value
            End Set
        End Property

        Private _paperIdList As List(Of Integer)
        ''' <summary>
        ''' Gets or Sets the list of Publication Id's associated with the booking.
        ''' </summary>
        Public Property PublicationList() As List(Of Integer)
            Get
                Return _paperIdList
            End Get
            Set(ByVal value As List(Of Integer))
                _paperIdList = value
            End Set
        End Property

        Private _rateList As Dictionary(Of Integer, Ratecard)
        ''' <summary>
        ''' Gets and Sets the List of Ratecards associated with the booking with each list indexed by the Paper Id
        ''' </summary>
        Public Property RatecardList() As Dictionary(Of Integer, Ratecard)
            Get
                Return _rateList
            End Get
            Set(ByVal value As Dictionary(Of Integer, Ratecard))
                _rateList = value
            End Set
        End Property

        Private _insertions As Integer
        ''' <summary>
        ''' Gets or Sets the number of editions the user has chosen during Scheduling
        ''' </summary>
        Public Overloads Property Insertions() As Integer
            Get
                Return _insertions
            End Get
            Set(ByVal value As Integer)
                _insertions = value
            End Set
        End Property

        Private _singleAdPrice As Decimal
        ''' <summary>
        ''' Gets or Sets the Price of an Ad for a single Edition
        ''' </summary>
        Public Property SingleAdPrice() As Decimal
            Get
                Return _singleAdPrice
            End Get
            Set(ByVal value As Decimal)
                _singleAdPrice = value
            End Set
        End Property

        Private _parentCategoryId As Integer
        ''' <summary>
        ''' Gets or Sets the Parent Category ID related to the database value for the booking object
        ''' </summary>
        Public Property ParentCategoryId() As Integer
            Get
                Return _parentCategoryId
            End Get
            Set(ByVal value As Integer)
                _parentCategoryId = value
            End Set
        End Property

        Private _imageList As List(Of String)
        ''' <summary>
        ''' Gets or sets the Images in the session as an array list in the booking.
        ''' </summary>
        Public Property ImageList() As List(Of String)
            Get
                Return _imageList
            End Get
            Set(ByVal value As List(Of String))
                _imageList = value
            End Set
        End Property

        Private _lineAdGraphic As DataModel.AdGraphic
        Public Property LineAdGraphic() As DataModel.AdGraphic
            Get
                Return _lineAdGraphic
            End Get
            Set(ByVal value As DataModel.AdGraphic)
                _lineAdGraphic = value
            End Set
        End Property

        Private _lineAd As DataModel.LineAd
        Public Property LineAd() As DataModel.LineAd
            Get
                Return _lineAd
            End Get
            Set(ByVal value As DataModel.LineAd)
                _lineAd = value
            End Set
        End Property


#Region "IBookCartContext Members"

        Public Property MainCategoryName As String Implements IBookCartContext.MainCategoryName
            Get
                Throw New NotImplementedException()
            End Get
            Set(ByVal value As String)
                Throw New NotImplementedException
            End Set
        End Property

        Public Property SubCategoryName As String Implements IBookCartContext.SubCategoryName
            Get
                Throw New NotImplementedException
            End Get
            Set(ByVal value As String)
                Throw New NotImplementedException
            End Set
        End Property

        Public Property LineAdIsNormalAdHeading() As Boolean Implements IBookCartContext.LineAdIsNormalAdHeading
            Get
                If _lineAd IsNot Nothing Then
                    If _lineAd.UseBoldHeader IsNot Nothing Then
                        Return _lineAd.UseBoldHeader
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _lineAd IsNot Nothing Then
                    _lineAd.UseBoldHeader = value
                End If
            End Set
        End Property

        Public Property LineAdHeadingText() As String Implements IBookCartContext.LineAdHeadingText
            Get
                If _lineAd IsNot Nothing Then
                    Return _lineAd.AdHeader
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _lineAd IsNot Nothing Then
                    _lineAd.AdHeader = value
                End If
            End Set
        End Property

        Public Property LineAdIsSuperBoldHeading() As Boolean Implements IBookCartContext.LineAdIsSuperBoldHeading
            Get
                If _lineAd IsNot Nothing Then
                    If _lineAd.IsSuperBoldHeading IsNot Nothing Then
                        Return _lineAd.IsSuperBoldHeading
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _lineAd IsNot Nothing Then
                    _lineAd.IsSuperBoldHeading = value
                End If
            End Set
        End Property

        Public Property LineAdText() As String Implements IBookCartContext.LineAdText
            Get
                If _lineAd IsNot Nothing Then
                    Return _lineAd.AdText
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _lineAd IsNot Nothing Then
                    _lineAd.AdText = value
                End If
            End Set
        End Property

        Public Property LineAdTextWordCount() As Integer Implements IBookCartContext.LineAdTextWordCount
            Get
                If _lineAd IsNot Nothing Then
                    If _lineAd.NumOfWords IsNot Nothing Then
                        Return _lineAd.NumOfWords
                    End If
                End If
                Return 0
            End Get
            Set(ByVal value As Integer)
                If _lineAd IsNot Nothing Then
                    _lineAd.NumOfWords = value
                End If
            End Set
        End Property

        Public Property LineAdIsColourHeading() As Boolean Implements IBookCartContext.LineAdIsColourHeading
            Get
                If _lineAd IsNot Nothing Then
                    If _lineAd.IsColourBoldHeading IsNot Nothing Then
                        Return _lineAd.IsColourBoldHeading
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _lineAd IsNot Nothing Then
                    _lineAd.IsColourBoldHeading = value
                End If
            End Set
        End Property

        Public Property LineAdHeaderColourCode() As String Implements IBookCartContext.LineAdHeaderColourCode
            Get
                If _lineAd IsNot Nothing Then
                    Return _lineAd.BoldHeadingColourCode
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _lineAd IsNot Nothing Then
                    _lineAd.BoldHeadingColourCode = value
                End If
            End Set
        End Property

        Public Property LineAdIsColourBorder() As Boolean Implements IBookCartContext.LineAdIsColourBorder
            Get
                If _lineAd IsNot Nothing Then
                    If _lineAd.IsColourBorder IsNot Nothing Then
                        Return _lineAd.IsColourBorder
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _lineAd IsNot Nothing Then
                    _lineAd.IsColourBorder = value
                End If
            End Set
        End Property

        Public Property LineAdBorderColourCode() As String Implements IBookCartContext.LineAdBorderColourCode
            Get
                If _lineAd IsNot Nothing Then
                    Return _lineAd.BorderColourCode
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _lineAd IsNot Nothing Then
                    _lineAd.BorderColourCode = value
                End If
            End Set
        End Property

        Public Property LineAdIsColourBackground() As Boolean Implements IBookCartContext.LineAdIsColourBackground
            Get
                If _lineAd IsNot Nothing Then
                    If _lineAd.IsColourBackground IsNot Nothing Then
                        Return _lineAd.IsColourBackground
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _lineAd IsNot Nothing Then
                    _lineAd.IsColourBackground = value
                End If
            End Set
        End Property

        Public Property LineAdBackgroundColourCode() As String Implements IBookCartContext.LineAdBackgroundColourCode
            Get
                If _lineAd IsNot Nothing Then
                    Return _lineAd.BackgroundColourCode
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _lineAd IsNot Nothing Then
                    _lineAd.BackgroundColourCode = value
                End If
            End Set
        End Property

        Public Property BookingOrderPrice As BookingOrderPrice Implements IBookCartContext.BookingOrderPrice

        Public Property EditionCount As Integer Implements IBookCartContext.EditionCount
            Get
                Return _insertions
            End Get
            Set(ByVal value As Integer)
                _insertions = value
            End Set
        End Property

        Public Property LineAdIsMainPhoto() As Boolean Implements IBookCartContext.LineAdIsMainPhoto
            Get
                If _lineAd IsNot Nothing Then
                    If _lineAd.UsePhoto IsNot Nothing Then
                        Return _lineAd.UsePhoto
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _lineAd IsNot Nothing Then
                    _lineAd.UsePhoto = value
                End If
            End Set
        End Property

        Public Property LineAdIsSecondPhoto() As Boolean Implements IBookCartContext.LineAdIsSecondPhoto
            Get
                If _lineAd IsNot Nothing Then
                    If _lineAd.IsFooterPhoto IsNot Nothing Then
                        Return _lineAd.IsFooterPhoto
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _lineAd IsNot Nothing Then
                    _lineAd.IsFooterPhoto = value
                End If
            End Set
        End Property

#End Region

#Region " IDisposable Support "

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free other state (managed objects).
                End If

                ' TODO: free your own state (unmanaged objects).
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        Public Sub New()
            Me._imageList = New List(Of String)
            Me._mainAdType = New AdType
            Me._paperIdList = New List(Of Integer)
            Me._rateList = New Dictionary(Of Integer, Ratecard)
            Me._lineAd = New LineAd()
        End Sub
    End Class
End Namespace
