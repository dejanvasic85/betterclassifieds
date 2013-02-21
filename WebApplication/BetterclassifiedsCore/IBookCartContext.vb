Imports BetterclassifiedsCore.DataModel
Imports BetterclassifiedsCore.BusinessEntities

Public Interface IBookCartContext
    Property LineAdIsNormalAdHeading As Boolean
    Property LineAdHeadingText As String
    Property LineAdIsSuperBoldHeading As Boolean
    Property LineAdText As String
    Property LineAdTextWordCount As Integer
    Property LineAdIsColourHeading As Boolean
    Property LineAdHeaderColourCode As String
    Property LineAdIsColourBorder As Boolean
    Property LineAdBorderColourCode As String
    Property LineAdIsColourBackground As Boolean
    Property LineAdBackgroundColourCode As String
    Property LineAdIsMainPhoto As Boolean
    Property LineAdIsSecondPhoto As Boolean
    Property MainCategoryName As String
    Property SubCategoryName As String
    Property BookingOrderPrice As BookingOrderPrice
    Property EditionCount As Integer

End Interface

