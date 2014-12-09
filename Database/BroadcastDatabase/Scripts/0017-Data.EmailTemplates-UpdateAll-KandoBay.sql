
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
                Please verify that you own this email by clicking on the confirmation
                link below:
                <p>
                    <a target="_blank" href="[/VerificationLink/]" style="text-decoration: none;display: inline-block;margin-bottom: 0;font-weight: normal;text-align: center;vertical-align: middle;cursor: pointer;background-image: none;border: 1px solid transparent;white-space: nowrap;padding: 16px 20px;font-size: 16px;line-height: 1.4;border-radius: 0;color: #fff;background-color: #008cba;border-color: #0079a1;">
                        This is my email!
                    </a>
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

-- Ad Enquiry
UPDATE	EmailTemplate
SET	[From] = 'kandobay-support@kandobay.com.au',
	[ModifiedDate] = @currentDateTime,
	[ModifiedDateUtc] = @currentDateTimeUtc,
	[BodyTemplate] = '<!DOCTYPE html>
<html lang="en">
<head>
    <title>KandoBay Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight: 300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
        <div style="border-bottom:1px solid #e3e3e3; padding:15px">
            <img alt="Logo" src="http://img.kandobay.com.au/kandobay_logo.png" height="100" />
        </div>
        <div class="row">
            <h3>An enquiry has been submitted for your ad ID <strong>[/adnumber/]</strong></h3>
            <p>
                Hello, a user noticed your ad and submitted the contact advertiser form for help.
                You will find their details below:
            </p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                <ul>
                    <li>Name: [/name/]</li>
                    <li>Email: [/email/]</li>
                    <li>Question: [/question/]</li>
                </ul>
            </div>
            <p>
                If you find that this is spam,
                <a target="_blank" href="http://kandobay.com.au/Home/ContactUs" style="text-decoration:underline; color: #222;">
                    please report it to us
                </a>
                as soon as possible. Thank you.
            </p>
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
            </a>.
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>
'
WHERE	DocType	= 'AdEnquiry'
AND Brand='KandoBay'

-- AdShare
UPDATE	EmailTemplate
SET	[From] = 'kandobay-support@kandobay.com.au',
	[ModifiedDate] = @currentDateTime,
	[ModifiedDateUtc] = @currentDateTimeUtc,
	[BodyTemplate] = '<!DOCTYPE html>
<html lang="en">
<head>
    <title>KandoBay Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight 300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
        <div style="border-bottom:1px solid #e3e3e3; padding:15px">
            <img alt="Logo" src="http://img.kandobay.com.au/kandobay_logo.png" height="100" />
        </div>
        <div class="row">
            <h3>
                Your friend [/ClientName/] just posted something at
                <a target="_blank" href="http://kandobay.com.au" style="text-decoration: underline; color: #222">
                    kandobay.
                </a>
            </h3>
            <p>
                They wanted us to let you know so we are sending this email on their behalf. 
            </p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                <h4>[/AdTitle/]</h4>
                <div>[/AdDescription/]</div> 
            </div>
            <p>
                View more at 
                <a target="_blank" href="http://kandobay.com.au/Home/ContactUs" style="text-decoration:underline; color: #222;">
                    kandobay.
                </a>
            </p>
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
            </a>.
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>
'
WHERE	DocType	= 'AdShare'
AND Brand='KandoBay'

-- ExpirationReminder
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
            <h3>Reminder - Your ad is expiring</h3>
            <p>The ad that is about to expire is [/AdReference/]</p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                Skip the process process by extending your ad. Click on the link below to begin:
                <p>
                    <a target="_blank" href="[/LinkForExtension/]" style="text-decoration: none;display: inline-block;margin-bottom: 0;font-weight: normal;text-align: center;vertical-align: middle;cursor: pointer;background-image: none;border: 1px solid transparent;white-space: nowrap;padding: 16px 20px;font-size: 16px;line-height: 1.4;border-radius: 0;color: #fff;background-color: #008cba;border-color: #0079a1;">
                        Extend My Ad
                    </a>
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
                kandobay.com.au.
            </a>
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>
'
WHERE	DocType	= 'ExpirationReminder'
AND Brand='KandoBay'

-- Forgotten Password
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
            <h3>Password Recovery</h3>
            <p>Forgot your password? That''s ok, we got you covered.</p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                We have generated a new password for you so please use the credentials below
                <ul>
                    <li>Username: [/Username/]</li>
                    <li>Password: [/Password/]</li>
                </ul>
            </div>
            <p>If you did not request a password reset, please contact our team.</p>
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
</html>'
WHERE	DocType	= 'ForgottenPassword'
AND Brand='KandoBay'

-- New Booking
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
            <h3>Your ad at kandobay has been placed successfully</h3>
            <p>Hi [/username/]. This is a courtesy email to let you know that your ad details have been posted online.</p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                <p>Summary:</p>
                <table>
                    <tr>
                        <td>Ad ID</td>
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
</html>'
WHERE	DocType	= 'NewBooking'
AND Brand='KandoBay'

-- Support Request
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
            <h3>Support requested at kandobay</h3>
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
                </table>
            </div>
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
</html>'
WHERE	DocType	= 'SupportRequest'
AND Brand='KandoBay'

