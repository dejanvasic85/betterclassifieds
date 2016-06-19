exec [EmailTemplate_Upsert]
	@DocType = 'EventDailySummary',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = 'Your daily event summary for [/event-name/]',
	@Description = 'A daily summary for the ',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sri Lankan Events Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
       <div class="row">
            <h3>Your event <strong>[/event-name/]</strong> daily activity summary</h3>
            <p>
                Number of guests registered today : [/daily-guest-count/]
            </p>
            <p>
                Total tickets sold : [/daily-tickets-count/]
            </p>
            <p>
                Total tickets value : [/daily-tickets-value/]
            </p>
            <p>
                If you find that this is spam or you are the wrong recipient to this information,
                <a target="_blank" href="http://srilankanevents.com.au/Home/ContactUs" style="text-decoration:underline; color: #222;">
                    please report it to us
                </a>
                as soon as possible. Thank you.
            </p>
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