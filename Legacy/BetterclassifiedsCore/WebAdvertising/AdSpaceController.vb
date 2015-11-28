Imports BetterclassifiedsCore.DataModel

Namespace WebAdvertising
    Public Class AdSpaceController
        Implements IDisposable

        Private disposedValue As Boolean = False        ' To detect redundant calls
        Private _context As BetterclassifiedsDataContext

        Public ReadOnly Property DataContext() As BetterclassifiedsDataContext
            Get
                Return _context
            End Get
        End Property

        Public Sub New()
            ' uses the default connection string
            _context = BetterclassifiedsDataContext.NewContext
        End Sub

#Region "Retrieve"

        Public Function GetAdSpaceSettings() As IList
            Return _context.WebAdSpaceSettings().ToList
        End Function

        Public Function GetAdSpaceSettingById(ByVal id As Integer) As DataModel.WebAdSpaceSetting
            Return _context.WebAdSpaceSettings.Where(Function(i) i.SettingId = id).Single
        End Function

        Public Function GetAdSpaces(ByVal settingId As Integer) As IList
            Dim query = From w In _context.WebAdSpaces _
                        Where w.SettingID = settingId _
                        Order By w.SortOrder _
                        Select w
            Return query.ToList
        End Function

        Public Function GetAdSpaceById(ByVal id As Integer) As DataModel.WebAdSpace
            Return _context.WebAdSpaces.Where(Function(i) i.WebAdSpaceId = id).Single
        End Function

        Public Function GetActiveAdSpaces(ByVal settingId As Integer) As IList
            Dim query = From w In _context.WebAdSpaces _
                        Where w.SettingID = settingId And w.Active = True _
                        Order By w.SortOrder _
                        Select w
            Return query.ToList
        End Function

#End Region

#Region "Update"

        Public Sub UpdateAdSpace(ByVal item As DataModel.WebAdSpace)
            Dim obj As DataModel.WebAdSpace = _context.WebAdSpaces.Where(Function(i) i.WebAdSpaceId = item.WebAdSpaceId).Single

            With obj
                .Title = item.Title
                .SettingID = item.SettingID
                .SortOrder = item.SortOrder
                .AdLinkUrl = item.AdLinkUrl
                .AdTarget = item.AdTarget
                .SpaceType = item.SpaceType
                .ImageUrl = item.ImageUrl
                .DisplayText = item.DisplayText
                .ToolTipText = item.ToolTipText
                .Active = item.Active
            End With

            _context.SubmitChanges()

        End Sub

        Public Sub UpdateStatus(ByVal id As Integer, ByVal enable As Boolean)
            Dim obj As DataModel.WebAdSpace = _context.WebAdSpaces.Where(Function(i) i.WebAdSpaceId = id).Single
            obj.Active = enable
            _context.SubmitChanges()
        End Sub

#End Region

#Region "Delete"

        Public Sub DeleteImageUrl(ByVal spaceId As Integer)
            Dim obj As DataModel.WebAdSpace = _context.WebAdSpaces.Where(Function(i) i.WebAdSpaceId = spaceId).Single
            obj.ImageUrl = Nothing
            _context.SubmitChanges()
        End Sub

        Public Sub DeleteWebSpace(ByVal id As Integer)
            Dim item As DataModel.WebAdSpace = _context.WebAdSpaces.Where(Function(i) i.WebAdSpaceId = id).Single
            _context.WebAdSpaces.DeleteOnSubmit(item)
            _context.SubmitChanges()
        End Sub

#End Region

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free other state (managed objects).
                End If

                ' TODO: free your own state (unmanaged objects).
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
