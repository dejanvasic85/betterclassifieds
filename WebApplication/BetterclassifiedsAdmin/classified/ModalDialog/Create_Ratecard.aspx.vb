﻿Imports BetterclassifiedsCore

Partial Public Class Create_Ratecard
    Inherits System.Web.UI.Page

    Private _baseRateId As String = "baseRateId"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msgInsert.Text = ""
        msgInsert.ForeColor = Drawing.Color.Red
    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        dtlGeneralInfo.InsertItem(True)
    End Sub

    Private Sub dtlGeneralInfo_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertEventArgs) Handles dtlGeneralInfo.ItemInserting
        Try
            ' get the template field values
            Dim description As TextBox = dtlGeneralInfo.FindControl("txtDescription")
            If description IsNot Nothing Then
                e.Values.Add("Description", description.Text)
            End If
        Catch ex As Exception
            msgInsert.Text = "Creating new Ratecard Failed: " + ex.Message
            e.Cancel = True
        End Try
    End Sub

    Private Sub linqSourceGeneral_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceInsertEventArgs) Handles linqSourceGeneral.Inserting
        Try
            If (e.Exception IsNot Nothing) Then
                For Each innerException As KeyValuePair(Of String, Exception) _
                       In e.Exception.InnerExceptions
                    Me.msgInsert.Text &= innerException.Key & ": " & _
                        innerException.Value.Message & "<br />"
                Next
                e.ExceptionHandled = True

            Else
                Dim item As DataModel.BaseRate = TryCast(e.NewObject, DataModel.BaseRate)
                If item IsNot Nothing Then
                    Dim baseId As Integer = GeneralController.CreateBaseRaseRate(item.Title, item.Description)
                    ' store the item in viewstate (parent Id)
                    ViewState(_baseRateId) = baseId

                    ' update the ratecard now
                    dtlRatecard.InsertItem(True)
                End If

            End If

        Catch ex As Exception
            msgInsert.Text = "Creating new Ratecard Failed: " + ex.Message
        Finally
            e.Cancel = True ' cancel the automatic insert generated by the data source since we handled this
        End Try
    End Sub

    Private Sub linqSourceRatecard_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceInsertEventArgs) Handles linqSourceRatecard.Inserting
        Try
            If (e.Exception IsNot Nothing) Then
                For Each innerException As KeyValuePair(Of String, Exception) _
                       In e.Exception.InnerExceptions
                    Me.msgInsert.Text &= innerException.Key & ": " & _
                        innerException.Value.Message & "<br />"
                Next
                e.ExceptionHandled = True
                ' delete the base rate if it has been added.
                If ViewState(_baseRateId) IsNot Nothing Then
                    GeneralController.DeleteBaseRate(ViewState(_baseRateId))
                End If
            Else
                If ViewState(_baseRateId) IsNot Nothing Then
                    Dim rate As DataModel.Ratecard = TryCast(e.NewObject, DataModel.Ratecard)
                    If rate IsNot Nothing Then
                        With rate
                            GeneralController.CreateRatecard(ViewState(_baseRateId), .MinCharge, .MaxCharge, .RatePerMeasureUnit, _
                                                             .MeasureUnitLimit, .PhotoCharge, .BoldHeading, .OnlineEditionBundle)
                            msgInsert.Text = "Successfully created new Ratecard. Reload the Rates grid to see new items."
                            msgInsert.ForeColor = Drawing.Color.Green
                        End With
                    End If
                End If
            End If

        Catch ex As Exception
            msgInsert.Text = "Creating new Ratecard Failed: " + ex.Message
            GeneralController.DeleteBaseRate(ViewState(_baseRateId))
        Finally
            e.Cancel = True ' cancel the automatic insert generated by the data source since we handled this
        End Try
    End Sub

End Class