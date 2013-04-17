Imports BetterClassified.UI

Public Class ExtendBooking
    Inherits BasePage(Of Presenters.ExtendBookingPresenter, Views.IExtendBookingView)
    Implements Views.IExtendBookingView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Presenter.Load()
        End If
    End Sub

    Public ReadOnly Property AdBookingId As Integer Implements Views.IExtendBookingView.AdBookingId
        Get
            Return Me.ReadQueryString(Of Integer)("AdBookingId")
        End Get
    End Property

    Public Sub DataBindEditions(editions As IEnumerable(Of Models.PublicationEditionModel)) Implements Views.IExtendBookingView.DataBindEditions
        rptEditions.DataSource = editions
        rptEditions.DataBind()
    End Sub

    Public Sub DataBindInsertionList(insertions As IEnumerable(Of Integer)) Implements Views.IExtendBookingView.DataBindInsertionList
        ddlEditions.DataSource = insertions
        ddlEditions.DataBind()
    End Sub

    Private Sub ddlEditions_IndexChanged(sender As Object, eventArgs As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlEditions.IndexChanged
        Me.Presenter.LoadForInsertions(eventArgs.Value)
    End Sub
End Class