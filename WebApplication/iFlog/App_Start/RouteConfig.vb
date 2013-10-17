Imports System.Web.Http
Imports System.Web.Routing

Public Class RouteConfig
    Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.MapHttpRoute(
           name:="DefaultApi",
           routeTemplate:="api/{controller}/{id}",
           defaults:=New With {Key .id = RouteParameter.[Optional]})

        routes.MapPageRoute(
            routeName:="AdRoute",
            routeUrl:="Ad/{id}",
            physicalFile:="~/OnlineAds/AdView.aspx")
    End Sub
End Class
