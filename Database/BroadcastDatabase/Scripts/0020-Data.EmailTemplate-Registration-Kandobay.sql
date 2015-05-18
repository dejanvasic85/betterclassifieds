
DECLARE @currentDateTime AS DATETIME;
SET		@currentDateTime = GETDATE();
DECLARE @currentDateTimeUtc AS DATETIME;
SET		@currentDateTimeUtc = GETUTCDATE();

-- New Registration
UPDATE	EmailTemplate
SET	[From] = 'kandobay-support@kandobay.com.au',
	[ModifiedDate] = @currentDateTime,
	[ModifiedDateUtc] = @currentDateTimeUtc,
	[BodyTemplate] = '<!DOCTYPE html>
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
            <h3>Welcome to kandobay!</h3>
            <p>Thank you for signing up with us [/FirstName/]. </p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                To confirm your registration, please use the code below.
                <p class="well well-lg">
                    <h3>Code: [/ConfirmationCode/]</h3>
                </p>
            </div>
            <p></p>
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
                kandobay.com.au
            </a> for security reasons.
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>
'
WHERE	DocType	= 'NewRegistration'
AND Brand='KandoBay'