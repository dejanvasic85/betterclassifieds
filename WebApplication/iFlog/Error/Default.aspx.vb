Imports BetterclassifiedsCore.Utilities
Imports BetterclassifiedsCore

Partial Public Class _Default7
    Inherits System.Web.UI.Page

    Private _type As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _type = Request.QueryString("type")

        Select Case _type

            Case BetterclassifiedsCore.Utilities.Constants.CONST_ERROR_CONNECTION
                ' Connection error to SQL Server (the server is down or connection is no good)
                pnlDatabaseConnection.Visible = True

            Case Utilities.Constants.CONST_ERROR_SETTINGS
                pnlDatabaseConnection.Visible = True

            Case Utilities.Constants.CONST_ERROR_REQUEST_SIZE
                pnlRequestSize.Visible = True

            Case Else
                ' General Error Message
                pnlGeneralError.Visible = True

        End Select

    End Sub

End Class