Namespace Controls.Booking
    Public Class LineAdDescriptionBox
        Inherits CompositeControl
        Private ReadOnly _textBox As TextBox
        Private ReadOnly _requiredValidator As RequiredFieldValidator

        Public Sub New()
            _textBox = New TextBox With {.ID = "txtAdText", .TextMode = TextBoxMode.MultiLine, .Rows = 10, .Columns = 40, .MaxLength = 1000}
            '_requiredValidator = New RequiredFieldValidator With {.ID = "valRequiredAdText", .ErrorMessage = "Ad Text is required.", .Text = "*"}
            'Me._requiredValidator.ControlToValidate = Me._textBox.ClientID
        End Sub

        Protected Overrides Sub CreateChildControls()
            MyBase.CreateChildControls()
            Controls.Add(_textBox)
            'Controls.Add(_requiredValidator)
        End Sub

        Private Function FormatString(ByVal value As String) As String
            Dim r = New Regex("\s+")
            Return r.Replace(value, " ")
        End Function

        Public Property Text() As String
            Get
                Return FormatString(_textBox.Text)
            End Get
            Set(ByVal value As String)
                _textBox.Text = value
            End Set
        End Property
    End Class
End Namespace