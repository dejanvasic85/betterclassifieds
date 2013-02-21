Imports System.Runtime.CompilerServices

Module TypeExtentions
    <Extension()> _
    Public Function HasChild(ByVal parentNode As SiteMapNode, ByVal childNode As SiteMapNode) As Boolean
        Try

            Dim xNode As SiteMapNode

            If childNode Is parentNode Then
                Return True
            ElseIf parentNode.HasChildNodes Then
                For Each xNode In parentNode.ChildNodes
                    If childNode Is xNode Then
                        Return True
                    Else
                        If HasChild(xNode, _
                                                     childNode) Then
                            Return True
                        End If
                    End If
                Next
            Else
                Return False
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

End Module
