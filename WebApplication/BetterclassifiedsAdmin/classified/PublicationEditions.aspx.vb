Imports BetterclassifiedsCore

Partial Public Class PublicationEditions
    Inherits System.Web.UI.Page

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' bind the publications
            ddlPublications.DataSource = PublicationController.GetPublications()
            ddlPublications.DataBind()
            If (ddlPublications.Items.Count > 0) Then
                BindFrequencyDetails(ddlPublications.Items(0).Value)
            End If
            ' bind controls for deadlines
            BindDeadlineControls()
        End If
    End Sub

    Private Sub calStartDate_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calStartDate.DayRender
        ' don't allow dates before today to be selectabe.
        If e.Day.Date < Today.Date Then
            e.Day.IsSelectable = False
        Else
            e.Cell.BorderStyle = BorderStyle.Solid
            e.Cell.BorderColor = Drawing.Color.DarkBlue
            e.Cell.BorderWidth = 1
        End If
    End Sub

#End Region

#Region "Data binding"

    Private Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        ' bind the editions
        BindEditions()
    End Sub

    Private Sub ddlPublications_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPublications.SelectedIndexChanged
        BindFrequencyDetails(ddlPublications.SelectedValue)
        grdEditions.DataSource = Nothing
        grdEditions.DataBind()
    End Sub

    Private Sub BindEditions()
        grdEditions.DataSource = PublicationController.PublicationEditions(Me.ddlPublications.SelectedValue, ddlDays.SelectedValue)
        grdEditions.DataBind()

        divEditions.Visible = True
    End Sub

    Private Sub BindDeadlineControls()
        ' bind the deadline days
        For d As Integer = 1 To 7
            ddlDaysBeforeEdition.Items.Add(d.ToString)
        Next
        ' bind the hours
        For h As Integer = 1 To 24
            ddlDeadlineHours.Items.Add(h.ToString)
        Next
        ' bind the mins
        For m As Integer = 0 To 60
            ddlDeadlineMins.Items.Add(m.ToString)
        Next
    End Sub

    Private Sub BindFrequencyDetails(ByVal publicationId As Integer)
        Try
            Dim publication As DataModel.Publication = PublicationController.GetPublicationById(publicationId)
            Dim dayOfWeek As DayOfWeek = publication.FrequencyValue
            lblEditionDay.Text = dayOfWeek.ToString
            lblFrequency.Text = publication.FrequencyType
        Catch ex As Exception
            lblGenerateSuccess.Text = "Could not load publication information: " + ex.Message
        End Try
    End Sub

#End Region

#Region "Delete Editions"

    ' delete single edition
    Private Sub grdEditions_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdEditions.RowDeleting
        Try
            Dim list As New List(Of Integer)
            list.Add(grdEditions.DataKeys(e.RowIndex).Value)
            If PublicationController.DeletePaperEditions(list) Then
                ' print msg
                lblDeleteSuccess.Text = "Successfully deleted edition(s)."
                lblDeleteSuccess.ForeColor = Drawing.Color.Green

                BindEditions()
            Else
                ' print fail msg
                lblDeleteSuccess.Text = "Failed to delete edition(s). Contact Administrator."
                lblDeleteSuccess.ForeColor = Drawing.Color.Red
            End If
        Catch ex As Exception
            lblDeleteSuccess.Text = "Failed to delete edition(s): " + ex.Message
            lblDeleteSuccess.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Private Sub btnDeleteSelected_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteSelected.Click
        Try
            Dim editions As New List(Of Integer)
            For Each row As GridViewRow In grdEditions.Rows
                Dim cb As CheckBox = DirectCast(row.FindControl("chkRows"), CheckBox)
                If cb IsNot Nothing AndAlso cb.Checked Then
                    editions.Add(grdEditions.DataKeys(row.RowIndex).Value)
                End If
            Next
            If PublicationController.DeletePaperEditions(editions) Then
                lblDeleteSuccess.Text = "Successfully deleted selected edition(s)."
                lblDeleteSuccess.ForeColor = Drawing.Color.Green

                BindEditions()
            Else
                lblDeleteSuccess.Text = "Failed to delete edition(s). Contact Administrator."
                lblDeleteSuccess.ForeColor = Drawing.Color.Red
            End If

        Catch ex As Exception
            lblDeleteSuccess.Text = "Failed to delete: " + ex.Message
            lblDeleteSuccess.ForeColor = Drawing.Color.Red
        End Try

    End Sub

#End Region

#Region "Generate Editions"

    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
            If Page.IsValid Then
                Dim insertions As Integer = Convert.ToInt32(txtNumberOfEditions.Text)
                If PublicationController.CreatedPublicationEditions(ddlPublications.SelectedValue, calStartDate.SelectedDate, _
                                                                    insertions, ddlDaysBeforeEdition.SelectedValue, _
                                                                    ddlDeadlineHours.SelectedValue, ddlDeadlineMins.SelectedValue) Then
                    ' print msg
                    lblGenerateSuccess.Text = "Successfully generated editions: Please check the upcoming editions tab and perform search to ensure that editions are in the system."
                    lblGenerateSuccess.ForeColor = Drawing.Color.Green
                Else
                    ' print fail msg
                    lblGenerateSuccess.Text = "Generate Failed: Please contact administrator."
                    lblGenerateSuccess.ForeColor = Drawing.Color.Red
                End If
            End If
        Catch f As FormatException
            lblGenerateSuccess.Text = "Generate Failed: Please enter a valid number for insertions."
            lblGenerateSuccess.ForeColor = Drawing.Color.Red
        Catch ex As Exception
            lblGenerateSuccess.Text = "Generate Failed: " + ex.Message
            lblGenerateSuccess.ForeColor = Drawing.Color.Red
        End Try
    End Sub

#End Region

#Region "Page Validation"

    ' custom calendar server validation
    Protected Sub calStartDate_ServerValidate(ByVal sender As Object, ByVal e As ServerValidateEventArgs)
        If tabContainer.ActiveTabIndex = 1 Then
            e.IsValid = calStartDate.SelectedDate >= Today.Date
        End If
    End Sub


#End Region

End Class