Namespace Controls.Booking.ExpiredAds
    Public Class LinkItemTemplate
        Implements ITemplate


        Public Sub InstantiateIn(ByVal container As System.Web.UI.Control) Implements System.Web.UI.ITemplate.InstantiateIn
            Dim control As New LinkItemControl
            AddHandler control.DataBinding, AddressOf OnDataBinding
            container.Controls.Add(control)
        End Sub

        Private Sub OnDataBinding(ByVal sender As Object, ByVal e As EventArgs)
            Dim control = TryCast(sender, LinkItemControl)
            If (control Is Nothing) Then
                Return
            End If

            Dim dataItem = TryCast(control.NamingContainer, DataGridItem)

            If (dataItem Is Nothing) Then
                Return
            End If


        End Sub
    End Class
End Namespace