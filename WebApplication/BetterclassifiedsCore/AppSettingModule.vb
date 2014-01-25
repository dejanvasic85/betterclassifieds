Public Module AppSettingModule

    Public Structure AppKey
        Public Const BookingReference = "BookingReference"
        Public Const WordSeparators = "WordSeparators"
        Public Const WordMaxLength = "WordMaxLength"
        Public Const AdTitleLength = "AdTitleLength"
        Public Const MaximumInsertions = "MaximumInsertions"
        Public Const AdDurationDays = "AdDurationDays"
    End Structure

    Public Class AppKeyReader(Of TKey)

        Public Shared Function ReadFromStore(ByVal key As String, Optional ByVal defaultIfNotExists As TKey = Nothing, Optional ByVal updateFunc As Func(Of TKey, TKey) = Nothing) As TKey

            Dim target As Type = GetType(TKey)

            Using db = DataModel.BetterclassifiedsDataContext.NewContext

                Dim appSetting = db.AppSettings.SingleOrDefault(Function(setting) setting.AppKey = key.ToString)

                If appSetting Is Nothing And defaultIfNotExists Is Nothing Then

                    Throw New NullReferenceException(String.Format("Application Key [{1}] is not set and no default value was provided.", key))

                ElseIf appSetting Is Nothing Then

                    ' Create the new value in the database and return the default value specified
                    appSetting = New DataModel.AppSetting With {.Module = "System", .AppKey = key, .SettingValue = defaultIfNotExists.ToString, .DataType = target.Name, .Description = key}

                    db.AppSettings.InsertOnSubmit(appSetting)

                End If

                ' Update the value if required
                If updateFunc IsNot Nothing Then

                    Dim converted = Convert.ChangeType(appSetting.SettingValue, target)

                    Dim updatedValue = updateFunc(converted)

                    appSetting.SettingValue = updatedValue.ToString

                End If

                ' Commit the final changes to the database
                db.SubmitChanges()

                ' Simply return the value casted to the required type
                Return Convert.ChangeType(appSetting.SettingValue, target)

            End Using

        End Function

    End Class

End Module
