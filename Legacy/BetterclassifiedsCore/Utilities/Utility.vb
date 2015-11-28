Imports System.IO

Namespace Utilities

    Public Class Utility

        Public Shared Function GetFileName(ByVal fullPath As String) As String
            If (fullPath <> String.Empty) Then
                Return New FileInfo(fullPath).Name
            Else
                Return ""
            End If
        End Function

        Public Shared Function GetAdTypeString(ByVal code As String) As String
            Select Case code
                Case "ONLINE"
                    Return "Online Ad"
                Case "LINE"
                    Return "Line Ad"
                Case Else
                    Return ""
            End Select
        End Function

        ''' <summary>
        ''' Checks the val parameter if it can be converted to a numeric number and returns true if successful.
        ''' </summary>
        ''' <param name="val">String value to check if it's a number.</param>
        ''' <param name="NumberStyle">Type of Number to check against.</param>
        ''' <returns>True if this is a string representation of a number.</returns>
        Public Shared Function isNumeric(ByVal val As String, ByVal NumberStyle As System.Globalization.NumberStyles) As Boolean
            Dim result As Double
            Return [Double].TryParse(val, NumberStyle, System.Globalization.CultureInfo.CurrentCulture, result)
        End Function

    End Class
End Namespace
