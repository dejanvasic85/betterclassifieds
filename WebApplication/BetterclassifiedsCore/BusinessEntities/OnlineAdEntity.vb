Namespace BusinessEntities
    Public Class OnlineAdEntity
        Inherits DataModel.OnlineAd

        Private _bookingReference As String
        Public Property BookingReference() As String
            Get
                Return _bookingReference
            End Get
            Set(ByVal value As String)
                _bookingReference = value
            End Set
        End Property

        Private _parentCategory As DataModel.MainCategory
        Public Property ParentCategory() As DataModel.MainCategory
            Get
                Return _parentCategory
            End Get
            Set(ByVal value As DataModel.MainCategory)
                _parentCategory = value
            End Set
        End Property

        Private _subCategory As DataModel.MainCategory
        Public Property SubCategory() As DataModel.MainCategory
            Get
                Return _subCategory
            End Get
            Set(ByVal value As DataModel.MainCategory)
                _subCategory = value
            End Set
        End Property

        Private _ImageList As List(Of String)
        Public Property ImageList() As List(Of String)
            Get
                If Not _ImageList Is Nothing Then
                    Return _ImageList
                Else
                    Return New List(Of String)
                End If
            End Get
            Set(ByVal value As List(Of String))
                _ImageList = value
            End Set
        End Property

        Private _Location As String
        Public Property LocationValue() As String
            Get
                Return _Location
            End Get
            Set(ByVal value As String)
                _Location = value
            End Set
        End Property

        Private _Area As String
        Public Property AreaValue() As String
            Get
                Return _Area
            End Get
            Set(ByVal value As String)
                _Area = value
            End Set
        End Property

        Private _DatePosted As System.Nullable(Of DateTime)
        Public Property DatePosted() As System.Nullable(Of DateTime)
            Get
                Return _DatePosted
            End Get
            Set(ByVal value As System.Nullable(Of DateTime))
                _DatePosted = value
            End Set
        End Property

    End Class
End Namespace
