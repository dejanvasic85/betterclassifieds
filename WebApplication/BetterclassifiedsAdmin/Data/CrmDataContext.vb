Imports Paramount.ApplicationBlock.Data

Partial Public Class CRMDataContext
    Public Shared Function NewContext() As CRMDataContext
        Return New CRMDataContext(ConfigReader.GetConnectionString("AppUserConnection"))
    End Function
End Class
