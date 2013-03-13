Imports System.Web.Services
Imports System.ComponentModel
Imports BetterclassifiedsCore
Imports Paramount.Broadcast.Components
Imports BetterClassified.UIController

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _

Namespace Service
    <System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
        <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
        <ToolboxItem(False)> _
    Public Class NotificationService
        Inherits System.Web.Services.WebService

        <WebMethod()> _
        Public Function ExpreiredAdEmailNotification(ByVal daysBeforeExpiry As Date) As Boolean
            Dim email As New AdExpiryNotification
            Dim expiringAdbookings = AdBookingController.GetExpiredAdList(daysBeforeExpiry)


            Dim a = (From b In expiringAdbookings Group b By b.Username Into Group).ToList

            For Each item In a
                Dim user = Membership.GetUser(item.Username)
                If user IsNot Nothing Then
                    Dim reference As String = String.Empty
                    Dim sb As New StringBuilder

                    For Each ref In item.Group
                        sb.AppendFormat("Iflog Id: {0} - Last Print Date: {1}", ref.AdId, ref.LastPrintInsertionDate.ToString("dd/MM/yyyy"))
                        sb.AppendLine("<br>")
                    Next
                    Dim recip = New EmailRecipientView() With {.Email = user.Email, .Name = user.UserName}
                    recip.TemplateFields.Add(New TemplateItemView With {.Name = "adReference", .Value = sb.ToString})
                    email.Recipients.Add(recip)
                End If
            Next
            email.Send()
            Return True
        End Function

    End Class
End Namespace