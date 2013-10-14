Imports BetterclassifiedsCore

Partial Public Class ViewTransaction
    Inherits System.Web.UI.Page

    Private _bookReference As String
    Private _userId As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _bookReference = Request.QueryString("ref")
        _userId = Membership.GetUser.UserName
    End Sub

    Private Sub dtlTransaction_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlTransaction.DataBound
        If dtlTransaction.DataItem IsNot Nothing Then

            Dim typeLabel As Label = TryCast(dtlTransaction.FindControl("lblType"), Label)

            If typeLabel IsNot Nothing Then

                If dtlTransaction.DataItem.TransactionType IsNot Nothing Then
                    Select Case dtlTransaction.DataItem.TransactionType
                        Case TransactionType.CREDIT
                            typeLabel.Text = "Credit Card"
                        Case TransactionType.PAYPAL
                            typeLabel.Text = "PayPal"
                        Case TransactionType.FREEAD
                            dtlTransaction.Fields(4).Visible = False
                    End Select
                End If
            End If
        End If
    End Sub

    Private Sub grdInvoiceItems_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdInvoiceItems.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim typeLabel As Literal = TryCast(e.Row.FindControl("lblAdType"), Literal)
            If typeLabel IsNot Nothing Then
                If e.Row.DataItem.AdType IsNot Nothing Then
                    Select Case e.Row.DataItem.AdType
                        Case SystemAdType.LINE.ToString
                            typeLabel.Text = "Print Advertisement"
                        Case SystemAdType.ONLINE.ToString
                            typeLabel.Text = "Online Classified"
                    End Select
                End If
            End If
        End If
    End Sub

    Private Sub dtlUser_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlUser.DataBound
        If dtlUser.DataItem IsNot Nothing Then
            dtlUser.Fields(1).Visible = (dtlUser.DataItem.BusinessName <> String.Empty)
        End If
    End Sub

    Private Sub srcUserProfile_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles srcUserProfile.Selected

    End Sub

    Private Sub srcUserProfile_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles srcUserProfile.Selecting
        ' specify the object parameters here so we do not use the query string!
        e.InputParameters.Add("userId", _userId)
    End Sub

    Private Sub srcTransaction_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles srcTransaction.Selecting
        e.InputParameters.Add("transactionTitle", _bookReference)
        e.InputParameters.Add("userId", _userId)
    End Sub

    Private Sub srcItems_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles srcItems.Selecting
        e.InputParameters.Add("transactionTitle", _bookReference)
        e.InputParameters.Add("userId", _userId)
    End Sub
End Class