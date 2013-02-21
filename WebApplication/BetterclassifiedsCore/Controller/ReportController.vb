Imports BetterclassifiedsCore.DataModel

Namespace Controller
    Public Class ReportController

        Private _context As DataModel.BetterclassifiedsDataContext

        Public ReadOnly Property Context() As BetterclassifiedsDataContext
            Get
                Return _context
            End Get
        End Property

        Public Sub New()
            _context = BetterclassifiedsDataContext.NewContext
        End Sub

#Region "Retrieve"

        Public Function GetWeeklySalesReport(ByVal publicationId As Integer, ByVal editionDate As DateTime, _
                                             ByVal status As Nullable(Of Integer), ByVal subCategoryId As Nullable(Of Integer)) As List(Of spReportWeeklySalesResult)
            If status = 0 Then
                Return _context.spReportWeeklySales(publicationId, editionDate, Nothing, subCategoryId).ToList
            Else
                Return _context.spReportWeeklySales(publicationId, editionDate, status, subCategoryId).ToList
            End If
        End Function

        Public Function GetTransactionReport(ByVal startDate As Date, ByVal endDate As Date) As List(Of spReportIncomeReportResult)
            Return _context.spReportIncomeReport(startDate, endDate).ToList
        End Function

#End Region

    End Class
End Namespace
