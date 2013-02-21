Imports BetterclassifiedsCore

Partial Public Class Scheduling
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub calStartDate_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calStartDate.DayRender

    End Sub

    Public Sub BindSelectableDates(ByVal dateList As List(Of DateTime))

    End Sub

#Region "Properties"

    Public Property InsertionLimit() As Integer
        Get
            Return ddlInserts.Items.Count
        End Get
        Set(ByVal value As Integer)
            For i As Integer = 1 To value
                ddlInserts.Items.Add(i)
            Next
        End Set
    End Property

    Public Property SelectedInsertions() As Integer
        Get
            Return ddlInserts.SelectedValue
        End Get
        Set(ByVal value As Integer)
            ddlInserts.SelectedValue = value
        End Set
    End Property

    Public Property SelectedStartDate() As DateTime
        Get
            Return calStartDate.SelectedDate
        End Get
        Set(ByVal value As DateTime)
            calStartDate.SelectedDate = value
        End Set
    End Property

    Public Property DisplayNumberOfInserts() As Boolean
        Get
            Return rowInsertions.Visible
        End Get
        Set(ByVal value As Boolean)
            rowInsertions.Visible = value
        End Set
    End Property

    Public Property DisplayOnlineInformation() As Boolean
        Get
            Return ViewState("displayOnlineInfo")
        End Get
        Set(ByVal value As Boolean)
            ViewState("displayOnlineInfo") = value
            rowOnlineInfo.Visible = value
            trEndDate.Visible = value
        End Set
    End Property

    Public Property DisplayLineAdInformation() As Boolean
        Get
            Return ViewState("displayLineAdInfo")
        End Get
        Set(ByVal value As Boolean)
            ViewState("displayLineAdInfo") = value
            pnlEditionDates.Visible = value
        End Set
    End Property

    Public Property DisplayCheckEditions() As Boolean
        Get
            Return lnkCheckEditions.Visible
        End Get
        Set(ByVal value As Boolean)
            lnkCheckEditions.Visible = value
        End Set
    End Property

#End Region

End Class