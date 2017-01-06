-- EventGuest - Kandobay
exec [EmailTemplate_Upsert]
	@DocType = 'EventGuestTransferFrom',
	@Brand = 'KandoBay',
	@SubjectTemplate = 'Tickets transferred for [/EventName/]',
	@From = 'events@kandobay.com.au',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>KandoBay Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
        <div style="border-bottom:1px solid #e3e3e3; padding:15px">
            <img alt="Logo" src="http://img.kandobay.com.au/kandobay_logo.png" height="100" />
        </div>
        <div class="row">
            <h3>Ticket Transferred for Event <em>[/EventName/]</em> on [/EventStartDate/]</h3>
            <p>We are sorry your ticket <em>[/TicketName/]</em> has been transferred to [/NewGuestName/] [/NewGuestEmail/]</p>

            <p>If you believe this happened in error, please visit the <a href="[/EventUrl/]">event details here </a> and contact the event organiser.</p>
        </div>
    </div>
    <div style="margin-top: 15px;background: #222;color: #f5f5f5;padding: 15px;font-size: 0.8em;">
        <p>
            <a target="_blank" href="http://kandobay.com.au" style="text-decoration: underline;color: #fff">Home</a> |
            <a target="_blank" href="http://kandobay.com.au/home/contactUs" style="text-decoration: underline;color:#fff">Contact Us</a>
        </p>
        <p>
            This is an automated email sent to you officially by
            <a target="_blank" href="http://kandobay.com.au" style="color: #fff; text-decoration: underline">
                kandobay.com.au
            </a> for security reasons.
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>';