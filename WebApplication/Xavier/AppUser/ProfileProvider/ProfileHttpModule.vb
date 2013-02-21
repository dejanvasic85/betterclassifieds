Imports System.Web.Profile
Imports System.Web

Namespace ProfileProvider
    Public Class ProfileHttpModule
        Implements IHttpModule

        Public Sub Dispose() Implements System.Web.IHttpModule.Dispose

        End Sub

        Public Sub Init(ByVal context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init
            AddHandler context.AuthorizeRequest, AddressOf Me.CheckProfile
        End Sub

        Public Sub CheckProfile(ByVal s As Object, ByVal e As EventArgs)
            Dim objApp As HttpApplication
            Dim objContext As HttpContext

            objApp = CType(s, HttpApplication)
            objContext = objApp.Context

            'User is authenticated, check profile
            If objApp.User.Identity.IsAuthenticated And objContext.Request.Path <> "editprofile.aspx" Then
                Dim ProfileVersion As Integer = SqlTableProfileProvider.ProfileVersion

                Dim UserProfileVersion As Integer = HttpContext.Current.Profile.GetPropertyValue("ProfileVersion")

                If CInt(UserProfileVersion) < ProfileVersion And objContext.Request.Path.EndsWith("editprofile.aspx") = False Then
                    objContext.Response.Redirect("~/editprofile.aspx")
                End If
            End If
        End Sub
    End Class
End Namespace