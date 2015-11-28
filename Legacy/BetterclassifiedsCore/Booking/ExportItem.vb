Imports BetterclassifiedsCore.DataModel

Namespace Booking

    Public Class ExportItem
        Private _categoryName As String
        Private _categoryId As Integer
        Private _subCategory As String
        Private _subCategoryId As Integer
        Private _lineAdList As List(Of DataModel.LineAd)

        Public Sub New()
            _lineAdList = New List(Of DataModel.LineAd)
        End Sub

        Public Sub New(ByVal categoryName As String, ByVal categoryId As Integer, ByVal subCategoryName As String, _
                       ByVal subCategoryId As Integer, ByVal lineAd As LineAd)
            _categoryName = categoryName
            _categoryId = categoryId
            _subCategory = subCategoryName
            _subCategoryId = subCategoryId
            _lineAdList = New List(Of LineAd)
            _lineAdList.Add(lineAd)
        End Sub

        Public Sub New(ByVal categoryName As String)
            _categoryName = categoryName
            _lineAdList = New List(Of LineAd)
        End Sub

        Public Property CategoryName() As String
            Get
                Return _categoryName
            End Get
            Set(ByVal value As String)
                _categoryName = value
            End Set
        End Property

        Public Property CategoryId() As Integer
            Get
                Return _categoryId
            End Get
            Set(ByVal value As Integer)
                _categoryId = value
            End Set
        End Property

        Public Property SubCategory() As String
            Get
                Return _subCategory
            End Get
            Set(ByVal value As String)
                _subCategory = value
            End Set
        End Property

        Public Property SubCategoryId() As Integer
            Get
                Return _subCategoryId
            End Get
            Set(ByVal value As Integer)
                _subCategoryId = value
            End Set
        End Property

        Public ReadOnly Property LineAdList() As List(Of LineAd)
            Get
                If _lineAdList Is Nothing Then
                    _lineAdList = New List(Of LineAd)
                End If
                Return _lineAdList
            End Get
        End Property

    End Class

End Namespace