Namespace BusinessEntities

    Public Class UserProfileEntity
        Inherits DataModel.UserProfile

        Public ReadOnly Property FullName() As String
            Get
                Return FirstName + " " + LastName
            End Get
        End Property
    End Class

End Namespace
