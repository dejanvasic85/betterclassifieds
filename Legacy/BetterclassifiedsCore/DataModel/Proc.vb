Namespace DataModel
    Public Structure Proc
        Event dummy()
        Public Structure GetOnlineAd
            Public Const Name As String = "psp_Betterclassified_GetOnlineAd"

            Public Structure Params
                Event dummy()
                Public Const Keyword As String = "keyword"
                Public Const LocationId As String = "locationId"
                Public Const AreaId As String = "areaId"
                Public Const SubCategoryId As String = "subCategoryId"
                Public Const ParentCategoryId As String = "parentCategoryId"
                Public Const PageSize As String = "pageSize"
                Public Const PageIndex As String = "pageIndex"
                Public Const TotalPopulation As String = "totalPopulationSize"
            End Structure
            Event dummy()
        End Structure

        Public Structure GetAdBookingByEndDate
            Public Const Name As String = "psp_Betterclassified_GetAdBookingByEndDate"
            Public Structure Params
                Event dummy()
                Public Const EndDate As String = "endDate"
            End Structure
            Event dummy()
        End Structure
    End Structure
End Namespace