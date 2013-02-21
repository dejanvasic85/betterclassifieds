Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Serialization
'Imports Paramount.Banners
Imports BetterclassifiedsCore
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
'<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
'<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
'<ToolboxItem(False)> _
'<System.Web.Script.Services.ScriptService()> _
Public Class AjaxMethods
    Inherits BetterClassified.UI.AjaxWebService

    '<WebMethod(True)>
    'Public Function GetNextBanner(ByVal params As String) As String
    '    'Dim js As New JavaScriptSerializer()
    '    'Dim bannerParams As UI.BannerParameters = js.Deserialize(Of UI.BannerParameters)(params)
    '    'Return bannerParams.Category + (New Random()).Next().ToString()
    '    Return String.Empty
    'End Function

    '<WebMethod(True)> _
    'Public Function GetAdWordCount(ByVal adText As String) As Integer
    '    Return GeneralRoutine.LineAdWordCount(adText)
    'End Function

End Class