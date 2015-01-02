
-- Support Request
UPDATE	EmailTemplate
SET	[From] = 'classies@themusic.com.au',
	[ModifiedDate] = GETDATE(),
	[ModifiedDateUtc] = GETUTCDATE(),
	[BodyTemplate] = '<!DOCTYPE html>
<html lang="en">
<head>
    <title>TheMusic Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
        <div style="border-bottom:1px solid #e3e3e3; padding:15px">
            <img alt="Logo" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" height="100" />
        </div>
        <div class="row">
            <h3>Support requested at TheMusic Classifieds</h3>
            <p>
                A user has submitted a support request using the Contact-Us form. Details are below:
            </p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                <table>
                    <tr>
                        <td>Name</td>
                        <td><strong>[/name/]</strong></td>
                    </tr>
                    <tr>
                        <td>Email</td>
                        <td><strong>[/email/]</strong></td>
                    </tr>
                    <tr>
                        <td>Phone</td>
                        <td><strong>[/phone/]</strong></td>
                    </tr>
					<tr>
                        <td>Details</td>
                        <td><strong>[/content/]</strong></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div style="margin-top: 15px;background: #263745;color: #f5f5f5;padding: 15px;font-size: 0.8em;">
        <p>
            <a target="_blank" href="http://classies.themusic.com.au" style="text-decoration: underline;color: #fff">Home</a> |
            <a target="_blank" href="http://classies.themusic.com.au/Home/ContactUs" style="text-decoration: underline;color:#fff">Contact Us</a>
        </p>
        <p>
            This is an automated email sent to you officially by
            <a target="_blank" href="http://classies.themusic.com.au" style="color: #fff; text-decoration: underline">
                classies.themusic.com.au.
            </a>
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>'
WHERE	DocType	= 'SupportRequest'
AND Brand='TheMusic'