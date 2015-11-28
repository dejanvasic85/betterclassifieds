Imports System.Data.SqlClient

Namespace Services
    <Serializable()> _
    Public Class Parameter
        Private ReadOnly _name As String
        Private ReadOnly _sqlDbType As SqlDbType
        Private ReadOnly _value As Object


        Private Sub New(ByVal name As String, ByVal sqlDBType As SqlDbType, ByVal value As Object)
            _name = name
            _sqlDbType = sqlDBType
            _value = value
        End Sub

        Public Sub New(ByVal name As String, ByVal value As System.Nullable(Of Integer))
            Me.New(name, SqlDBType.Int, IIf(value.HasValue, DirectCast(value.Value, Object), DBNull.Value))
        End Sub
        Public Sub New(ByVal name As String, ByVal value As System.Nullable(Of DateTime))
            Me.New(name, System.Data.SqlDbType.DateTime, IIf(value.HasValue, DirectCast(value.Value, Object), DBNull.Value))
        End Sub
        Public Sub New(ByVal name As String, ByVal value As String, ByVal stringType As StringType)
            Me.New(name, GetSqlDbType(stringType), value)
        End Sub

        Private Shared Function GetSqlDbType(ByVal stringType As StringType) As SqlDbType
            Select Case stringType
                Case stringType.[Char]
                    Return System.Data.SqlDbType.[Char]
                Case stringType.Text
                    Return System.Data.SqlDbType.Text
                Case stringType.VarChar
                    Return System.Data.SqlDbType.VarChar
                Case Else
                    Throw New ArgumentException("StringType """ + stringType + """ is not recognised by Parameter.GetSqlDbType")
            End Select
        End Function

        Public ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        Public ReadOnly Property Value() As Object
            Get
                Return _value
            End Get
        End Property

        Public ReadOnly Property SqlDBType() As SqlDbType
            Get
                Return _sqlDbType
            End Get
        End Property

        Public ReadOnly Property SqlParameter() As SqlParameter
            Get
                Return New SqlParameter With {.SqlDbType = _sqlDbType, .ParameterName = _name, .Value = _value}
            End Get
        End Property
    End Class

    <Serializable()> _
    Public Enum StringType
        VarChar
        [Char]
        Text
    End Enum
End Namespace