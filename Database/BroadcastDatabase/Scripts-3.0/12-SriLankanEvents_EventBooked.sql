-- EventGuest - Kandobay
exec [EmailTemplate_Upsert]
	@DocType = 'EventTicketsBooked',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>SriLankanEvents Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
         <div class="row">
            <h3>Your booking for <em>[/EventName/]</em> has been processed successfully.</h3>
            <p>Each guest will receive an email with their ticket. Thank you for using KandoBay.</p>
            <p>Click <a href="[/EventUrl/]">here</a> for full event details.</p>
        </div>
    </div>
    <div style="margin-top: 15px;background: #263745;color: #f5f5f5;padding: 15px;font-size: 0.8em;">
        <p>
            <a target="_blank" href="http://srilankanevents.com.au" style="text-decoration: underline;color: #fff">Home</a> |
            <a target="_blank" href="http://srilankanevents.com.au/Home/ContactUs" style="text-decoration: underline;color:#fff">Contact Us</a>
        </p>
        <p>
            This is an automated email sent to you officially by
            <a target="_blank" href="http://srilankanevents.com.au" style="color: #fff; text-decoration: underline">
                srilankanevents.com.au
            </a> for security reasons.
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>';