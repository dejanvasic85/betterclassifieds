Imports System.Web
Imports Paramount.ApplicationBlock.Logging.EventLogging

Namespace UI
    Public Class BasePage
        Inherits System.Web.UI.Page

        Private Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
            Dim currentContext As HttpContext = HttpContext.Current
            Dim exception As Exception = currentContext.Server.GetLastError()

            Dim sqlEx As SqlClient.SqlException = TryCast(exception, SqlClient.SqlException)

            If sqlEx IsNot Nothing Then

                ' check if this is a sql connection issue.
                If sqlEx.Class = 20 Then
                    ' handle it
                    EventLogManager.Log(sqlEx)
                    Response.Redirect(Utilities.Constants.CONST_ERROR_DEFAULT_URL + "?type=" + _
                                      Utilities.Constants.CONST_ERROR_CONNECTION)
                End If

                ' finish the page running - so clear the error
                currentContext.Server.ClearError()
            End If

        End Sub
    End Class

End Namespace
