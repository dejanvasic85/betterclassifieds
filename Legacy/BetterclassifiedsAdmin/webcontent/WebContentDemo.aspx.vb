Imports System.Threading
Imports System.Globalization

Partial Public Class WebContentDemo
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        Session("Language") = DropDownList1.SelectedValue
        Response.Redirect(Request.Url.AbsolutePath)
    End Sub

    Protected Overrides Sub InitializeCulture()
        Try
            If (Not String.IsNullOrEmpty(Session("Language"))) Then
                Thread.CurrentThread.CurrentCulture = New CultureInfo(Session("Language").ToString)
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub WebContentDemo_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        DropDownList1.SelectedValue = Session("Language")
    End Sub
End Class