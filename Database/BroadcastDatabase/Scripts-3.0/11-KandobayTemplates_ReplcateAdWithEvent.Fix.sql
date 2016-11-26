-- EventGuest - Kandobay
exec [EmailTemplate_Upsert]
	@DocType = 'NewBooking',
	@Brand = 'KandoBay',
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
            <h3>Your event at kandobay has been placed successfully</h3>
            <p>Hi [/username/]. This is a courtesy email to let you know that your event details have been posted online.</p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                <p>Summary:</p>
                <table>
                    <tr>
                        <td>Booking Id</td>
                        <td><strong>[/adid/]</strong></td>
                    </tr>
                    <tr>
                        <td>Price</td>
                        <td><strong>[/price/]</strong></td>
                    </tr>
                    <tr>
                        <td>Start Date</td>
                        <td><strong>[/startdate/]</strong></td>
                    </tr>
                    <tr>
                        <td>End Date</td>
                        <td><strong>[/enddate/]</strong></td>
                    </tr>
                </table>
            </div>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                Details:
                <h4>[/adheading/]</h4>
                <div>[/addescription/]</div>
            </div>
            <p>If this was not actioned by you, please contact our team and we''ll sort it out.</p>
        </div>
    </div>
    <div style="margin-top: 15px;background: #222;color: #f5f5f5;padding: 15px;font-size: 0.8em;">
        <p>
            <a target="_blank" href="http://kandobay.com.au" style="text-decoration: underline;color: #fff">Home</a> |
            <a target="_blank" href="http://kandobay.com.au/Home/ContactUs" style="text-decoration: underline;color:#fff">Contact Us</a>
        </p>
        <p>
            This is an automated email sent to you officially by
            <a target="_blank" href="http://kandobay.com.au" style="color: #fff; text-decoration: underline">
                kandobay.com.au.
            </a>
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>';