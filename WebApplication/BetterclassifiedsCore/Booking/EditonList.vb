Imports BetterclassifiedsCore.DataModel

Namespace Booking
    <Serializable()> _
    Public Class EditionList
        Inherits Publication

        Private _dates As List(Of Nullable(Of DateTime))
        Public Property EditionList() As List(Of Nullable(Of DateTime))
            Get
                Return _dates
            End Get
            Set(ByVal value As List(Of Nullable(Of DateTime)))
                _dates = value
            End Set
        End Property

        Public ReadOnly Property FormattedDates() As List(Of String)
            Get
                Dim list As New List(Of String)
                For Each item In _dates
                    list.Add(String.Format("{0:D}", item))
                Next
                Return list
            End Get
        End Property

    End Class

End Namespace