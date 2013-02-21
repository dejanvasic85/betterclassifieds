Imports BetterclassifiedsCore

Partial Public Class FAQ
    Inherits System.Web.UI.Page

    Private Const _pageId As String = "FAQ.aspx"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_SYSTEM, _
                                             Utilities.Constants.CONST_KEY_System_EnableFAQPage) Then
                ' show the inner html
                faqContent.InnerHtml = GeneralController.GetWebContent(_pageId)
            Else
                ' forward to page not found
                Response.Redirect("~/Error/NotFound.aspx")
            End If
        End If
    End Sub

End Class