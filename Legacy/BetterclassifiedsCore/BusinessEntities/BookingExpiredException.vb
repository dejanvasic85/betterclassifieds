Imports System
Namespace BusinessEntities
    Public Class BookingExpiredException
        Inherits System.Exception

        Public Sub New()

        End Sub

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

    End Class
End Namespace
