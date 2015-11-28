Public Enum UserMessageType
    ItemSavedSuccessfully = 1
    ItemDeletedSuccessfully = 2
    RatecardSetupSuccessful = 3
End Enum

Public Class UserMessageHelper
    Public Shared Function GetUserMessage(ByVal userMsgType As UserMessageType)
        Select Case userMsgType
            Case UserMessageType.ItemSavedSuccessfully
                Return "Item has been saved successfully"
            Case UserMessageType.ItemDeletedSuccessfully
                Return "Item has been removed successfully"
            Case UserMessageType.RatecardSetupSuccessful
                Return "RateCard has been set up successfully. Click Done to return to manage Rates."
            Case Else
                Return String.Empty
        End Select
    End Function
End Class
