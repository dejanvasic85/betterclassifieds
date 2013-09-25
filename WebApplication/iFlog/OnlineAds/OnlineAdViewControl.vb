''' <summary>
''' User control type used for rendering online ad type objects
''' </summary>
''' <remarks></remarks>
Public MustInherit Class OnlineAdViewControl
    Inherits UserControl
    Public MustOverride Sub DatabindAd(Of T)(ByVal adDetails As T)
End Class