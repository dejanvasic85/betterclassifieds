Imports Paramount.ApplicationBlock.Data
Imports System.Configuration

Namespace DataModel
    Partial Public Class AppUserDataContext
        Private Const configSection As String = "paramount/services"
        Private Const appuserConnection As String = "AppUserConnection"

        Public Shared Function NewContext() As AppUserDataContext
            '    Return New AppUserDataContext(ConfigReader.GetConnectionString(configSection, appuserConnection))
            Return New AppUserDataContext(ConfigurationManager.ConnectionStrings("AppUserConnection").ConnectionString)
        End Function

    End Class
End Namespace