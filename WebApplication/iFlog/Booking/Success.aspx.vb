Imports BetterclassifiedsCore
Imports BetterclassifiedsWeb.Payment
Imports Paramount.Betterclassified.Utilities.PayPal

Partial Public Class Success
    Inherits System.Web.UI.Page

    Public Shared ReadOnly Property Settings() As PayPalSettings
        Get
            Return ConfigurationManager.GetSection("paypal")
        End Get
    End Property

    ' This helper method encodes a string correctly for an HTTP POST
    Private Function Encode(ByVal oldValue As String) As String
        Dim newValue As String = oldValue.Replace("""", "'")
        newValue = System.Web.HttpUtility.UrlEncode(newValue)
        newValue = newValue.Replace("%2f", "/")
        Return newValue
    End Function

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.RedirectPermanent("~/Booking/Step/1")

        ''if the user came to this page by mistake, then redirect to default page
        If (String.IsNullOrEmpty(NotifyParameterAccess.ReferenceId)) Then
            Response.Redirect("~/Booking/Default.aspx")
            Return
        End If

        Dim url As String = Request.Url.AbsoluteUri + "&redirect=1"
        Response.AddHeader("REFRESH", String.Format("10;URL={0}", url))

        Dim redirect = Request.QueryString("redirect")
        If redirect Is Nothing Then
            Return
        End If


        If redirect = 1 Then
            Response.ClearHeaders()
            If BookingController.IsBooked(NotifyParameterAccess.ReferenceId) Then
                Response.Redirect("~/Booking/Default.aspx?action=successful")
                Return
            Else
                Response.Redirect("~/Booking/Default.aspx?action=fail")
            End If
        End If

    End Sub


End Class