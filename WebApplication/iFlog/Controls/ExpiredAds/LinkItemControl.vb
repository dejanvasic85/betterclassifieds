Namespace Controls.Booking.ExpiredAds
    Public Class LinkItemControl
        Inherits CompositeControl

        Protected _link As LinkButton
        Public Event Click(ByVal sender As Object, ByVal e As EventArgs)



        Public Property Text() As String
            Get
                Return _link.Text
            End Get
            Set(ByVal value As String)
                _link.Text = value
            End Set
        End Property

        Public Property Value() As String
            Get
                Return ViewState("Value")

            End Get
            Set(ByVal value As String)
                ViewState("Value") = value
            End Set
        End Property

        Public Sub New()
            Me._link = New LinkButton
            AddHandler Me._link.Click, AddressOf OnItemClick
            AddHandler Me.DataBinding, AddressOf DataBindMethod

        End Sub

        Private Sub DataBindMethod(ByVal sender As Object, ByVal e As EventArgs)
            Dim control = TryCast(sender, LinkItemControl)
            If (control Is Nothing) Then
                Return
            End If


            Dim dataItem = TryCast(control.NamingContainer, DataGridItem)

            If (dataItem Is Nothing) Then
                Return
            End If
        End Sub

        Protected Overrides Sub CreateChildControls()
            MyBase.CreateChildControls()
            Me.Controls.Add(Me._link)
        End Sub

        Private Sub OnItemClick(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent Click(Me, e)
        End Sub
    End Class
End Namespace