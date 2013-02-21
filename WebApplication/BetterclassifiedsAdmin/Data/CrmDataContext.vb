Imports Paramount.ApplicationBlock.Logging.DataAccess

Partial Public Class CRMDataContext
    Private Const configSection As String = "paramount/services"
    Private Const appuserConnection As String = "AppUserConnection"

    Public Shared Function NewContext() As CRMDataContext
        Return New CRMDataContext(ConfigReader.GetConnectionString(configSection, appuserConnection))
    End Function
End Class
