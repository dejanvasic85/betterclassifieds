Imports System.Web.Http
Imports System.Web.Routing
Imports System.Web.Optimization

Public Class RouteConfig

    Public Const AdRoute As String = "AdRoute"

    Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)

        routes.Ignore("{resource}.axd/{*pathInfo}")
        routes.Ignore("{resource}.aspx/{*pathInfo}")
        routes.Ignore("{resources}.ashx/{*pathInfo}")
        routes.Ignore("Image/View.ashx")

        routes.MapHttpRoute(
           name:="DefaultApi",
           routeTemplate:="api/{controller}/{id}",
           defaults:=New With {Key .id = RouteParameter.[Optional]})

        routes.MapPageRoute(
            routeName:=AdRoute,
            routeUrl:="Ad/{title}/{id}",
            physicalFile:="~/OnlineAds/AdView.aspx")

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

