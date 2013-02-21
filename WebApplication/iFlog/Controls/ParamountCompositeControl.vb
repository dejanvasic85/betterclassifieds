Namespace Controls
    Public MustInherit Class ParamountCompositeControl
        Inherits CompositeControl

        Public Sub Redirect(ByVal url As String)
            HttpContext.Current.Response.Redirect(url)
        End Sub
    End Class
End Namespace