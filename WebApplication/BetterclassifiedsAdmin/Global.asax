<%@ Application Language="VB" %>

<script runat="server">

    Private Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        Application("OnlineUsers") = 0
    
    End Sub

    Private Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        '  Code that runs on application shutdown
    
    End Sub

    Private Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Log the error
        Dim exception = Server.GetLastError()
        Paramount.Modules.Logging.UIController.ExceptionLogController(Of Exception).AuditException(exception)
    End Sub

    Private Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
        Application.Lock()
        Application("OnlineUsers") = CInt(Application("OnlineUsers")) + 1
        Application.UnLock()
    End Sub

    Private Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
        Application.Lock()
        Application("OnlineUsers") = CInt(Application("OnlineUsers")) - 1
        Application.UnLock()
    End Sub
       
</script>
