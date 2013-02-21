Imports System.Web
Imports System.Web.Services

Public Class SessionRefresher
    Implements System.Web.IHttpHandler
    Implements IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        context.Response.ContentType = "text/plain"
        context.Response.Write("Hello World!")
        context.Session("KeepSessionAlive") = DateTime.Now
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class