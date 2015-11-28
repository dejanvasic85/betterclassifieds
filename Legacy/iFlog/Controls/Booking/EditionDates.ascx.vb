
Namespace Controls.Booking
    ''' <summary>
    ''' User control used for binding a list of Publications containing Edition Dates
    ''' </summary>
    ''' <remarks></remarks>
    Partial Public Class EditionDates
        Inherits System.Web.UI.UserControl

        Public Event GridPageIndexChanged()
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        End Sub

        Public Sub BindPaperEditions(ByVal editions As List(Of BetterclassifiedsCore.Booking.EditionList))
            ' hide/display the no selection text based on number of editions in the paramter
            lblNoDates.Visible = (editions.Count = 0)
            ' databind to the UI
            lstEditions.DataSource = editions
            lstEditions.DataBind()
        End Sub

        Public Sub RemoveBindings()
            lblNoDates.Visible = True
            lstEditions.DataSource = Nothing
            lstEditions.DataBind()
        End Sub

        Private Sub lstEditions_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles lstEditions.ItemDataBound

        End Sub

        Protected Sub GridIndexChanged(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
            RaiseEvent GridPageIndexChanged()
            CType(sender, GridView).PageIndex = e.NewPageIndex
        End Sub
    End Class
End Namespace