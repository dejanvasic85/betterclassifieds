Imports BetterclassifiedsCore

Partial Public Class AdDetails
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("action") = "back" Then
                ' we fill in the controls that we need
                txtAdTitle.Text = BookingController.AdBookCart.Ad.Title
                txtComments.Text = BookingController.AdBookCart.Ad.Comments
            End If
        End If
    End Sub

    Public Sub BindAdDetails(ByVal adDetails As DataModel.Ad)
        With adDetails
            Me.AdTitle = .Title
            Me.Comments = .Comments
        End With
    End Sub

    Public Property AdTitle() As String
        Get
            Return HttpUtility.HtmlEncode(txtAdTitle.Text)
        End Get
        Set(ByVal value As String)
            txtAdTitle.Text = HttpUtility.HtmlEncode(value)
        End Set
    End Property

    Public Property Comments() As String
        Get
            Return HttpUtility.HtmlEncode(txtComments.Text)
        End Get
        Set(ByVal value As String)
            txtComments.Text = HttpUtility.HtmlEncode(value)
        End Set
    End Property

End Class