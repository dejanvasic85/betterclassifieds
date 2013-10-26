IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE [Name] = 'SystemHealthCheck')
begin
	INSERT INTO EmailTemplate ([Name], [Description], [Subject], [Sender], [EntityCode])
	VALUES ( 'SystemHealthCheck', 'Sends alert to admins about the daily activity', 'iFlog Activity Report', 'alert@iflog.com.au', 'P000000005' );
end

UPDATE EmailTemplate
SET EmailContent = '
<h3>iFlog activity for [/ReportDate/] Environment : [/Environment/]</h3>
<table border="0">
    <tbody>
        <tr>
            <td style="width: 120px; text-align: left">Ad Bookings</td>
            <td style="text-align: left">[/TotalBookings/]</td>
        </tr>
        <tr>
            <td style="width: 120px; text-align: left">Sum of Bookings</td>
            <td style="text-align: left">$[/SumOfBookings/]</td>
        </tr>
        <tr>
            <td style="width: 120px; text-align: left">Total Emails Sent</td>
            <td style="text-align: left">[/TotalEmails/]</td>
        </tr>
    </tbody>
</table>
<br>
<h3>Elmah Errors</h3>
<br>
[/LogTable/]
'
WHERE Name = 'SystemHealthCheck'