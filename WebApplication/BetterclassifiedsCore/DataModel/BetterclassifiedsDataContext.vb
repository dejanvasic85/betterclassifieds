Imports Paramount.ApplicationBlock.Data

Namespace DataModel
    Partial Public Class BetterclassifiedsDataContext
        Private Const configSection As String = "paramount/services"
        Private Const betterclassifiedConnection As String = "BetterclassifiedsConnection"

        Public Shared Function NewContext() As BetterclassifiedsDataContext
            Return New BetterclassifiedsDataContext(ConfigReader.GetConnectionString(configSection, betterclassifiedConnection))
        End Function

    End Class
End Namespace