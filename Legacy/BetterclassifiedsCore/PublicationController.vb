
''' <summary>
''' Provides the general data layer methods required to communicate with the backend Database.
''' </summary>
''' <remarks></remarks>
Public Class PublicationController

    ''' <summary>
    ''' Returns all the Papers and their details from the database.
    ''' </summary>
    ''' <returns><see cref="List(Of Paper)">List of Papers</see></returns>
    Public Shared Function GetAllPapers() As List(Of Publication)
        Using db As New BetterclassifiedsDataContext
            Return db.Publications.ToList
        End Using
    End Function

End Class
