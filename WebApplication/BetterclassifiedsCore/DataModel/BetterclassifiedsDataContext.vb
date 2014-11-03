Imports Paramount.ApplicationBlock.Data
Imports System.Configuration

Namespace DataModel
    Partial Public Class BetterclassifiedsDataContext

        Public Shared Function NewContext() As BetterclassifiedsDataContext
            Return New BetterclassifiedsDataContext(ConfigurationManager.ConnectionStrings("ClassifiedConnection").ConnectionString)
        End Function

    End Class
End Namespace