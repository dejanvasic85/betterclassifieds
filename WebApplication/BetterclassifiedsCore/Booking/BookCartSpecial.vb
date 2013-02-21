Imports BetterclassifiedsCore.BusinessEntities

Namespace Booking

    Public Class BookCartSpecial
        Inherits BusinessEntities.AdBookingEntity
        Implements IDisposable
        Implements IBookCartContext

        Public Sub New()
            _lineAd = New DataModel.LineAd
        End Sub

        Private _MainCategory As String

        Private _Publications As List(Of Integer)

        Private _Insertions As Integer

        Private _SetPrice As Decimal

        Private _MaximumWords As Integer

        Private _AllowLineImage As Boolean

        Private _AllowLineBoldHeader As Boolean

        Private _Ad As DataModel.Ad

        Private _IsOnlineAd As Boolean

        Private _OnlineAd As DataModel.OnlineAd

        Private _OnlineImage As List(Of DataModel.AdGraphic)

        Private _LineAdImage As DataModel.AdGraphic

        Private _IsLineAd As Boolean

        Private _bookEntries As List(Of DataModel.BookEntry)

        Public Property MainCategory() As String
            Get
                Return _MainCategory
            End Get
            Set(ByVal value As String)
                _MainCategory = value
            End Set
        End Property

        Public Property Publications() As List(Of Integer)
            Get
                If _Publications Is Nothing Then
                    _Publications = New List(Of Integer)
                End If
                Return _Publications
            End Get
            Set(ByVal value As List(Of Integer))
                _Publications = value
            End Set
        End Property

        Public Property Insertions() As Integer
            Get
                Return _Insertions
            End Get
            Set(ByVal value As Integer)
                _Insertions = value
            End Set
        End Property

        Public Property SetPrice() As Decimal
            Get
                Return _SetPrice
            End Get
            Set(ByVal value As Decimal)
                _SetPrice = value
            End Set
        End Property

        Public Property MaximumWords() As Integer
            Get
                Return _MaximumWords
            End Get
            Set(ByVal value As Integer)
                _MaximumWords = value
            End Set
        End Property

        Public Property AllowLineImage() As Boolean
            Get
                Return _AllowLineImage
            End Get
            Set(ByVal value As Boolean)
                _AllowLineImage = value
            End Set
        End Property

        Public Property AllowLineBoldHeader() As Boolean
            Get
                Return _AllowLineBoldHeader
            End Get
            Set(ByVal value As Boolean)
                _AllowLineBoldHeader = value
            End Set
        End Property

        Public Property Ad() As DataModel.Ad
            Get
                Return _Ad
            End Get
            Set(ByVal value As DataModel.Ad)
                _Ad = value
            End Set
        End Property

        Public Property IsOnlineAd() As Boolean
            Get
                Return _IsOnlineAd
            End Get
            Set(ByVal value As Boolean)
                _IsOnlineAd = value
            End Set
        End Property

        Public Property OnlineAd() As DataModel.OnlineAd
            Get
                Return _OnlineAd
            End Get
            Set(ByVal value As DataModel.OnlineAd)
                _OnlineAd = value
            End Set
        End Property

        Public Property OnlineImages() As List(Of DataModel.AdGraphic)
            Get
                Return _OnlineImage
            End Get
            Set(ByVal value As List(Of DataModel.AdGraphic))
                _OnlineImage = value
            End Set
        End Property

        Public Property LineAdImage() As DataModel.AdGraphic
            Get
                Return _LineAdImage
            End Get
            Set(ByVal value As DataModel.AdGraphic)
                _LineAdImage = value
            End Set
        End Property

        Public Property IsLineAd() As Boolean
            Get
                Return _IsLineAd
            End Get
            Set(ByVal value As Boolean)
                _IsLineAd = value
            End Set
        End Property

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

        Private _LineAd As DataModel.LineAd

        Public Property LineAd() As DataModel.LineAd
            Get
                Return _LineAd
            End Get
            Set(ByVal value As DataModel.LineAd)
                _LineAd = value
            End Set
        End Property

#Region "IBookCartContext Members"

        Public Property MainCategoryName() As String Implements IBookCartContext.MainCategoryName
            Get
                Throw New NotImplementedException
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
                If _LineAd IsNot Nothing Then
                    If _LineAd.UseBoldHeader IsNot Nothing Then
                        Return _LineAd.UseBoldHeader
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _LineAd IsNot Nothing Then
                    _LineAd.UseBoldHeader = value
                End If
            End Set
        End Property

        Public Property LineAdHeadingText() As String Implements IBookCartContext.LineAdHeadingText
            Get
                If _LineAd IsNot Nothing Then
                    Return _LineAd.AdHeader
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _LineAd IsNot Nothing Then
                    _LineAd.AdHeader = value
                End If
            End Set
        End Property

        Public Property LineAdIsSuperBoldHeading() As Boolean Implements IBookCartContext.LineAdIsSuperBoldHeading
            Get
                If _LineAd IsNot Nothing Then
                    If _LineAd.IsSuperBoldHeading IsNot Nothing Then
                        Return _LineAd.IsSuperBoldHeading
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _LineAd IsNot Nothing Then
                    _LineAd.IsSuperBoldHeading = value
                End If
            End Set
        End Property

        Public Property LineAdText() As String Implements IBookCartContext.LineAdText
            Get
                If _LineAd IsNot Nothing Then
                    Return _LineAd.AdText
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _LineAd IsNot Nothing Then
                    _LineAd.AdText = value
                End If
            End Set
        End Property

        Public Property LineAdTextWordCount() As Integer Implements IBookCartContext.LineAdTextWordCount
            Get
                If _LineAd IsNot Nothing Then
                    If _LineAd.NumOfWords IsNot Nothing Then
                        Return _LineAd.NumOfWords
                    End If
                End If
                Return 0
            End Get
            Set(ByVal value As Integer)
                If _LineAd IsNot Nothing Then
                    _LineAd.NumOfWords = value
                End If
            End Set
        End Property

        Public Property LineAdIsColourHeading() As Boolean Implements IBookCartContext.LineAdIsColourHeading
            Get
                If _LineAd IsNot Nothing Then
                    If _LineAd.IsColourBoldHeading IsNot Nothing Then
                        Return _LineAd.IsColourBoldHeading
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _LineAd IsNot Nothing Then
                    _LineAd.IsColourBoldHeading = value
                End If
            End Set
        End Property

        Public Property LineAdHeaderColourCode() As String Implements IBookCartContext.LineAdHeaderColourCode
            Get
                If _LineAd IsNot Nothing Then
                    Return _LineAd.BoldHeadingColourCode
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _LineAd IsNot Nothing Then
                    _LineAd.BoldHeadingColourCode = value
                End If
            End Set
        End Property

        Public Property LineAdIsColourBorder() As Boolean Implements IBookCartContext.LineAdIsColourBorder
            Get
                If _LineAd IsNot Nothing Then
                    If _LineAd.IsColourBorder IsNot Nothing Then
                        Return _LineAd.IsColourBorder
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _LineAd IsNot Nothing Then
                    _LineAd.IsColourBorder = value
                End If
            End Set
        End Property

        Public Property LineAdBorderColourCode() As String Implements IBookCartContext.LineAdBorderColourCode
            Get
                If _LineAd IsNot Nothing Then
                    Return _LineAd.BorderColourCode
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _LineAd IsNot Nothing Then
                    _LineAd.BorderColourCode = value
                End If
            End Set
        End Property

        Public Property LineAdIsColourBackground() As Boolean Implements IBookCartContext.LineAdIsColourBackground
            Get
                If _LineAd IsNot Nothing Then
                    If _LineAd.IsColourBackground IsNot Nothing Then
                        Return _LineAd.IsColourBackground
                    End If
                End If
                Return False
            End Get
            Set(ByVal value As Boolean)
                If _LineAd IsNot Nothing Then
                    _LineAd.IsColourBackground = value
                End If
            End Set
        End Property

        Public Property LineAdBackgroundColourCode() As String Implements IBookCartContext.LineAdBackgroundColourCode
            Get
                If _LineAd IsNot Nothing Then
                    Return _LineAd.BackgroundColourCode
                End If
                Return String.Empty
            End Get
            Set(ByVal value As String)
                If _LineAd IsNot Nothing Then
                    _LineAd.BackgroundColourCode = value
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
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace