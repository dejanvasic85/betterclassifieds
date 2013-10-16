Imports System.Web.Http
Imports System.Web.Routing

Public Class RouteConfig
    Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.MapHttpRoute(
           name:="DefaultApi",
           routeTemplate:="api/{controller}/{id}",
           defaults:=New With {Key .id = RouteParameter.[Optional]})
    End Sub
End Class
