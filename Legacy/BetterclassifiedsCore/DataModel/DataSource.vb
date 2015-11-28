Namespace DataModel
    Public Class DataSource
        Private _totalPopulation As Integer
        Private _data As DataTable

        Public ReadOnly Property TotalPopulation() As Integer
            Get
                Return _totalPopulation
            End Get
        End Property

        Public ReadOnly Property Data() As DataTable
            Get
                Return _data
            End Get
        End Property

        Public Sub New(ByVal data As DataTable, ByVal population As Integer)
            Me._data = data
            Me._totalPopulation = population
        End Sub
    End Class
End Namespace