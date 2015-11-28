Imports BetterclassifiedsCore.Controller
Imports BetterclassifiedsCore.BusinessEntities

Namespace BundleBooking

    ''' <summary>
    ''' Serializable object containing booking details for Print and Online Booking
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class BundleCart
        Implements IBookCartContext

        Public Sub New()
            _lineAd = New DataModel.LineAd
        End Sub

        Public Sub New(ByVal username As String)
            _username = username
            _lineAd = New DataModel.LineAd
        End Sub

        Private _username As String
        Public Property Username() As String
            Get
                Return _username
            End Get
            Set(ByVal value As String)
                _username = value
            End Set
        End Property

        Private _startDate As DateTime
        Public Property StartDate() As DateTime
            Get
                Return _startDate
            End Get
            Set(ByVal value As DateTime)
                _startDate = value
            End Set
        End Property

        Private _endDate As DateTime
        Public Property EndDate() As DateTime
            Get
                Return _endDate
            End Get
            Set(ByVal value As DateTime)
                _endDate = value
            End Set
        End Property

        Private _firstEdition As DateTime
        Public Property FirstEdition() As DateTime
            Get
                Return _firstEdition
            End Get
            Set(ByVal value As DateTime)
                _firstEdition = value
            End Set
        End Property

        'Private _singleEditionPrice As Decimal
        'Public Property SingleEditionPrice() As Decimal
        '    Get
        '        Return _singleEditionPrice
        '    End Get
        '    Set(ByVal value As Decimal)
        '        _singleEditionPrice = value
        '    End Set
        'End Property

        Private _totalPrice As Decimal
        Public Property TotalPrice() As Decimal
            Get
                Return _totalPrice
            End Get
            Set(ByVal value As Decimal)
                _totalPrice = value
            End Set
        End Property

        Private _bookReference As String
        Public Property BookReference() As String
            Get
                Return _bookReference
            End Get
            Set(ByVal value As String)
                _bookReference = value
            End Set
        End Property

        Private _bookingStatus As BookingStatus
        Public Property BookStatus() As BookingStatus
            Get
                Return _bookingStatus
            End Get
            Set(ByVal value As BookingStatus)
                _bookingStatus = value
            End Set
        End Property

        Private _mainParentCategory As DataModel.MainCategory
        Public Property MainParentCategory() As DataModel.MainCategory
            Get
                Return _mainParentCategory
            End Get
            Set(ByVal value As DataModel.MainCategory)
                _mainParentCategory = value
            End Set
        End Property

        Private _mainSubCategory As DataModel.MainCategory
        Public Property MainSubCategory() As DataModel.MainCategory
            Get
                Return _mainSubCategory
            End Get
            Set(ByVal value As DataModel.MainCategory)
                _mainSubCategory = value
            End Set
        End Property

        Private _publicationList As List(Of DataModel.Publication)
        Public Property PublicationList() As List(Of DataModel.Publication)
            Get
                If _publicationList Is Nothing Then
                    _publicationList = New List(Of DataModel.Publication)
                End If
                Return _publicationList
            End Get
            Set(ByVal value As List(Of DataModel.Publication))
                _publicationList = value
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

        Private _lineAdGraphic As DataModel.AdGraphic
        Public Property LineAdGraphic() As DataModel.AdGraphic
            Get
                Return _lineAdGraphic
            End Get
            Set(ByVal value As DataModel.AdGraphic)
                _lineAdGraphic = value
            End Set
        End Property

        Private _onlineAd As DataModel.OnlineAd
        Public Property OnlineAd() As DataModel.OnlineAd
            Get
                Return _onlineAd
            End Get
            Set(ByVal value As DataModel.OnlineAd)
                _onlineAd = value
            End Set
        End Property

        Private _onlineAdGraphics As List(Of DataModel.AdGraphic)
        Public Property OnlineAdGraphics() As List(Of DataModel.AdGraphic)
            Get
                If _onlineAdGraphics Is Nothing Then
                    _onlineAdGraphics = New List(Of DataModel.AdGraphic)
                End If
                Return _onlineAdGraphics
            End Get
            Set(ByVal value As List(Of DataModel.AdGraphic))
                _onlineAdGraphics = value
            End Set
        End Property

        Private _insertions As Integer
        Public Property Insertions() As Integer
            Get
                Return _insertions
            End Get
            Set(ByVal value As Integer)
                _insertions = value
            End Set
        End Property

        Private _bookEntries As List(Of DataModel.BookEntry)
        Public Property BookEntries() As List(Of DataModel.BookEntry)
            Get
                If _bookEntries Is Nothing Then
                    _bookEntries = New List(Of DataModel.BookEntry)
                End If
                Return _bookEntries
            End Get
            Set(ByVal value As List(Of DataModel.BookEntry))
                _bookEntries = value
            End Set
        End Property

#Region "IBookCartContext Members"

        Public Property MainCategoryName As String Implements IBookCartContext.MainCategoryName
            Get
                If _mainParentCategory IsNot Nothing Then
                    Return _mainParentCategory.Title
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _mainParentCategory IsNot Nothing Then
                    _mainParentCategory.Title = value
                End If
            End Set
        End Property

        Public Property SubCategoryName As String Implements IBookCartContext.SubCategoryName
            Get
                If _mainSubCategory IsNot Nothing Then
                    Return _mainSubCategory.Title
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _mainSubCategory IsNot Nothing Then
                    _mainSubCategory.Title = value
                End If
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

    End Class


End Namespace
