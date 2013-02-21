Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.UI

Partial Public Class Login1
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'this is the current url
        Dim currentUrl As System.Uri = System.Web.HttpContext.Current.Request.Url
        'don't redirect if this is localhost
        If Not currentUrl.IsLoopback And System.Configuration.ConfigurationManager.AppSettings("ConfigurationContext") = Utilities.Environment.Live Then
            If Not currentUrl.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.CurrentCultureIgnoreCase) Then
                'build the secure uri 
                Dim secureUrlBuilder As System.UriBuilder = New UriBuilder(currentUrl)
                secureUrlBuilder.Scheme = Uri.UriSchemeHttps
                'use the default port. 
                secureUrlBuilder.Port = -1
                'redirect and end the response. 
                System.Web.HttpContext.Current.Response.Redirect(secureUrlBuilder.Uri.ToString())
            End If
        End If
    End Sub

    
End Class