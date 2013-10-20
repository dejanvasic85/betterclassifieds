﻿Imports System.Web.Http
Imports System.Web.Routing

Public Class RouteConfig

    Public Const AdRoute As String = "AdRoute"

    Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.MapHttpRoute(
           name:="DefaultApi",
           routeTemplate:="api/{controller}/{id}",
           defaults:=New With {Key .id = RouteParameter.[Optional]})

        routes.MapPageRoute(
            routeName:=AdRoute,
            routeUrl:="Ad/{title}/{id}",
            physicalFile:="~/OnlineAds/AdView.aspx")
    End Sub
End Class
