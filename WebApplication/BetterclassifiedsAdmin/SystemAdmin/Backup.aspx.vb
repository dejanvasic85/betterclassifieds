Imports BetterclassifiedsCore

Namespace SystemAdmin
    Partial Public Class Backup
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            DataBindLastUpdate(False)
        End Sub

        Private Sub btnBackup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBackup.Click
            Try
                Dim filePath As String = txtPath.Text
                ' do some validation to ensure that we have a backslash as the last character
                If txtPath.Text.Last <> "\" Then
                    filePath += "\"
                End If

                ' call a general routine to do the backing up
                GeneralRoutine.BackupSystemDatabases(filePath)

                ' try and update the last back up time to be now
                GeneralRoutine.UpdateApplicationSetting(Utilities.Constants.CONST_MODULE_SYSTEM, _
                                                        Utilities.Constants.CONST_KEY_System_LastBackupTime, _
                                                        DateTime.Now)

                ' call method to display to admin when was the last update made.
                DataBindLastUpdate(True)
            Catch appEx As ApplicationException
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Text = "Successfully performed backup but unable to record the date and time."
            Catch ex As Exception
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Text = ex.Message
            End Try

        End Sub

        Private Sub DataBindLastUpdate(ByVal appendDone As Boolean)
            ' display to the user when the last backup was completed
            Dim lastBackupTime = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_SYSTEM, _
                                                              Utilities.Constants.CONST_KEY_System_LastBackupTime)

            ' show the message to the user
            If Not lastBackupTime Is Nothing Then
                lblMessage.Text = "Last Successful Backup: "
                lblMessage.Text += String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", Convert.ToDateTime(lastBackupTime))
                If appendDone Then
                    lblMessage.Text += " - DONE"
                End If
            End If
        End Sub

    End Class
End Namespace