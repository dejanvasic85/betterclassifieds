Imports System.Web.Http
Imports System.Web.Routing
Imports System.Web.Optimization

Public Class RouteConfig

    Public Const AdRoute As String = "AdRoute"

    Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)


        routes.MapPageRoute(
            routeName:="seoName",
            routeUrl:="listings/{seoName}",
            checkPhysicalUrlAccess:=True,
            physicalFile:="~/OnlineAds/Default.aspx",
            defaults:=New RouteValueDictionary(New With {Key .seoName = RouteParameter.[Optional]}))

        routes.MapPageRoute(
            routeName:="seoCategoryName",
            routeUrl:="listings/{seoName}/{catId}",
            checkPhysicalUrlAccess:=True,
            physicalFile:="~/OnlineAds/Default.aspx")
    End Sub
End Class

