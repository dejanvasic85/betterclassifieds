Imports System.Web

Namespace ParameterAccess
    Public Class OnlineSearchParameter
        Const ParameterKey As String = "OnlineSearchParameter"
        Const SearchKeywordParam As String = "SearchKeywordParam"
        Const CategoryIdParam As String = "CategoryIdParam"
        Const SubcategoryIdParam As String = "SubcategoryIdParam"
        Const LocationIdParam As String = "LocationIdParam"
        Const AreaIdParam As String = "AreaIdParam"

        Public Shared Property KeyWord() As String
            Get
                Return GetParameter(SearchKeywordParam)
            End Get
            Set(ByVal value As String)
                SetParameterValue(SearchKeywordParam, value)
            End Set
        End Property

        Public Shared Property Category() As Integer?
            Get
                Return GetParameter(CategoryIdParam)
            End Get
            Set(ByVal value As Integer?)
                SetParameterValue(CategoryIdParam, value)
            End Set
        End Property

        Public Shared Property SubCategory() As Integer?
            Get
                Return GetParameter(SubcategoryIdParam)
            End Get
            Set(ByVal value As Integer?)
                SetParameterValue(SubcategoryIdParam, value)
            End Set
        End Property

        Public Shared Property Area() As Integer?
            Get
                Return GetParameter(AreaIdParam)
            End Get
            Set(ByVal value As Integer?)
                SetParameterValue(AreaIdParam, value)
            End Set
        End Property

        Public Shared Property Location() As Integer?
            Get
                Return GetParameter(LocationIdParam)
            End Get
            Set(ByVal value As Integer?)
                SetParameterValue(LocationIdParam, value)
            End Set
        End Property

        Private Shared Function GetContext() As Dictionary(Of String, Object)
            Dim item = TryCast(HttpContext.Current.Session(ParameterKey), Dictionary(Of String, Object))
            If (item Is Nothing) Then
                HttpContext.Current.Session(ParameterKey) = New Dictionary(Of String, Object)
            End If
            Return HttpContext.Current.Session(ParameterKey)
        End Function

        Private Shared Sub SetParameterValue(ByVal key As String, ByVal value As Object)
            If GetContext.ContainsKey(key) Then
                GetContext.Item(key) = value
            Else
                GetContext.Add(key, value)
            End If

            HttpContext.Current.Session(ParameterKey) = GetContext()
        End Sub

        Private Shared Function GetParameter(ByVal key As String) As Object
            If GetContext.Keys.Contains(key) Then
                Return GetContext()(key)
            End If
            Return Nothing
        End Function

        Public Shared Sub Clear()
            HttpContext.Current.Session(ParameterKey) = Nothing
        End Sub

    End Class
End Namespace