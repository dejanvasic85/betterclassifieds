Imports System.Runtime.CompilerServices
Imports Paramount.Utility.Dsl
Imports Paramount.Utility
Imports Paramount.Betterclassified.Utilities.Configuration

Module HttpExtensions

    <Extension()>
    Public Function ToImageUrl(ByVal httpRequest As HttpRequest, ByVal imageId As String, _
                                     Optional ByVal usePlaceholder As Boolean = True)

        If String.IsNullOrEmpty(imageId) Then
            If usePlaceholder Then
                Return "~/Resources/Images/noimage.gif"
            End If

            Return String.Empty
        End If

        Dim param As New DslQueryParam(httpRequest.QueryString)
        param.DocumentId = imageId
        param.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
        param.Resolution = BetterclassifiedSetting.DslDefaultResolution
        param.Width = BetterclassifiedSetting.DslThumbWidth
        param.Height = BetterclassifiedSetting.DslThumbHeight
        Return param.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
    End Function

End Module