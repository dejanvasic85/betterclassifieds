-- EventGuestRemoved
exec [EmailTemplate_Upsert]
	@DocType = 'EventGuestRemoved',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sri Lankan Events Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
       <div class="row">
            <h3>Ticket Canceled</h3>
            <p>
            	Hey there, we just wanted to let you know that your ticket number [/EventBookingTicketId/] for event <strong> [/EventName/] </strong> on [/EventDate/] has been canceled by the event organiser.
            </p>
            <p>
            	Please visit the <a href="[/EventUrl/]">event page</a> to contact the organiser should 
            	you have any issues with this.
            </p>
            <p>Thank you</p>
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

-- EventGuest
exec [EmailTemplate_Upsert]
	@DocType = 'EventGuest',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sri Lankan Events Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
         <div class="row">
            <h3>Congrats! You have tickets to [/EventName/]</h3>
            <p><strong>[/PurchaserName/]</strong> has purchased the tickets on your behalf over at <a href="http://srilankanevents.com.au" target="_blank">[/ClientName/]</a>. Full 
            event details <a href="[/EventUrl/]">here.</a></p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                <p class="well well-lg">
                    <h3>Location : [/Location/]</h3>

                    <h3>Starts : [/EventStartDate/]</h3>

                    <h3>Finishes : [/EventEndDate/]</h3>

                    <h3>Ticket Type : [/Ticket/]</h3>
                </p>
            </div>
            <p>We have included a calendar invite for your convenience.</p>
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

-- EventPaymentRequest
exec [EmailTemplate_Upsert]
	@DocType = 'EventPaymentRequest',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sri Lankan Events Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
        <div class="row">
            <h3>Event Payment Request</h3>
            <p>User <strong>[/Username/]</strong> has requested a payment for the Event <strong>[/EventId/]</strong> Ad <strong>[/AdId/]</strong></p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                <p class="well well-lg">
                    <h3>Preferred Payment Method : [/PreferredPaymentMethod/]</h3>

                    <h3>Amount Requested : [/RequestedAmount/]</h3>
                </p>
            </div>
            <p></p>
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

-- EventTicketsBooked
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
            <h3>Congrats! You have tickets to [/EventName/]</h3>
            <p>Your tickets should be attached to this email.</p>
            <p>Full event details <a href="[/EventUrl/]">here.</a></p>
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

-- AdShare
exec [EmailTemplate_Upsert]
	@DocType = 'AdShare',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sri Lankan Events Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight 300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
        <div class="row">
            <h3>
                Your friend [/ClientName/] just posted something at
                <a target="_blank" href="http://srilankanevents.com.au" style="text-decoration: underline; color: #222">
                    Sri Lankan Events.
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
                <a target="_blank" href="http://srilankanevents.com.au/Home/ContactUs" style="text-decoration:underline; color: #222;">
                    Sri Lankan Events.
                </a>
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
            </a>.
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>
';

-- ActivityReport
exec [EmailTemplate_Upsert]
	@DocType = 'ActivityReport',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<h3>Daily Activity for [/ReportDate/] Environment : [/Environment/]</h3>
[/ClassifiedsTable/]
<br>
<h3>Elmah Errors</h3>
<br>
[/LogTable/]
';

-- AdEnquiry
exec [EmailTemplate_Upsert]
	@DocType = 'AdEnquiry',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sri Lankan Events Notification</title>
</head>
<body style='' font-family "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight 300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
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
            </a>.
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>
';

-- SupportRequest
exec [EmailTemplate_Upsert]
	@DocType = 'SupportRequest',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sri Lankan Events Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
        <div class="row">
            <h3>Support requested at Sri Lankan Events Classifieds</h3>
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
    <div style="margin-top: 15px;background: #263745;color: #f5f5f5;padding: 15px;font-size: 0.8em;">
        <p>
            <a target="_blank" href="http://http://srilankanevents.com.au" style="text-decoration: underline;color: #fff">Home</a> |
            <a target="_blank" href="http://http://srilankanevents.com.au/Home/ContactUs" style="text-decoration: underline;color:#fff">Contact Us</a>
        </p>
        <p>
            This is an automated email sent to you officially by
            <a target="_blank" href="http://http://srilankanevents.com.au" style="color: #fff; text-decoration: underline">
                http://srilankanevents.com.au.
            </a>
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>
';

-- NewBooking
exec [EmailTemplate_Upsert]
	@DocType = 'NewBooking',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sri Lankan Events Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
        <div class="row">
            <h3>Your ad at Sri Lankan Events has been placed successfully</h3>
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
    <div style="margin-top: 15px;background: #263745;color: #f5f5f5;padding: 15px;font-size: 0.8em;">
        <p>
            <a target="_blank" href="http://srilankanevents.com.au" style="text-decoration: underline;color: #fff">Home</a> |
            <a target="_blank" href="http://srilankanevents.com.au/Home/ContactUs" style="text-decoration: underline;color:#fff">Contact Us</a>
        </p>
        <p>
            This is an automated email sent to you officially by
            <a target="_blank" href="http://srilankanevents.com.au" style="color: #fff; text-decoration: underline">
                srilankanevents.com.au.
            </a>
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>  </p>
    </div>
</body>
</html>
';

-- ExpirationReminder
exec [EmailTemplate_Upsert]
	@DocType = 'ExpirationReminder',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sri Lankan Events Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
        <!--<div style="border-bottom:1px solid #e3e3e3; padding:15px">
            <img alt="Logo" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" height="100" />
        </div>-->
        <div class="row">
            <h3>Reminder - Your ad is expiring</h3>
            <p>The ad that is about to expire is [/AdReference/]</p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                Skip the process process by extending your ad. Click on the link below to begin:
                <p>
                    <a target="_blank" href="[/LinkForExtension/]" style="text-decoration: none;display: inline-block;margin-bottom: 0;font-weight: normal;text-align: center;vertical-align: middle;cursor: pointer;background-image: none;border: 1px solid transparent;white-space: nowrap;padding: 16px 20px;font-size: 16px;line-height: 1.4;border-radius: 4px;color: #333;background-color: #fff;border-color: #ccc">
                        Extend My Ad
                    </a>
                </p>
            </div>
            <p></p>
        </div>
    </div>
    <div style="margin-top: 15px;background: #263745;color: #f5f5f5;padding: 15px;font-size: 0.8em;">
        <p>
            <a target="_blank" href="http://http://srilankanevents.com.au" style="text-decoration: underline;color: #fff">Home</a> |
            <a target="_blank" href="http://http://srilankanevents.com.au/Home/ContactUs" style="text-decoration: underline;color:#fff">Contact Us</a>
        </p>
        <p>
            This is an automated email sent to you officially by
            <a target="_blank" href="http://http://srilankanevents.com.au" style="color: #fff; text-decoration: underline">
                http://srilankanevents.com.au.
            </a>
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>
';

-- ForgottenPassword
exec [EmailTemplate_Upsert]
	@DocType = 'ForgottenPassword',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sri Lankan Events Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
        <!--<div style="border-bottom:1px solid #e3e3e3; padding:15px">
            <img alt="Logo" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" height="100" />
        </div>-->
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
    <div style="margin-top: 15px;background: #263745;color: #f5f5f5;padding: 15px;font-size: 0.8em;">
        <p>
            <a target="_blank" href="http://http://srilankanevents.com.au" style="text-decoration: underline;color: #fff">Home</a> |
            <a target="_blank" href="http://http://srilankanevents.com.au/Home/ContactUs" style="text-decoration: underline;color:#fff">Contact Us</a>
        </p>
        <p>
            This is an automated email sent to you officially by
            <a target="_blank" href="http://http://srilankanevents.com.au" style="color: #fff; text-decoration: underline">
                http://srilankanevents.com.au.
            </a>
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>
';

-- NewRegistration
exec [EmailTemplate_Upsert]
	@DocType = 'NewRegistration',
	@Brand = 'SriLankanEvents',
	@BodyTemplate = '
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sri Lankan Events Notification</title>
</head>
<body style=''font-family: "Open Sans" ,"helvetica neue",helvetica,arial,sans-serif;font-weight:300;''>
    <div style="padding-right: 15px;padding-left: 15px;margin-right: auto;margin-left: auto;border:1px solid #eee">
        <!--<div style="border-bottom:1px solid #e3e3e3; padding:15px">
            <img alt="Logo" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" height="100" />
        </div>-->
        <div class="row">
            <h3>Welcome to Sri Lankan Events!</h3>
            <p>Thank you for signing up with us [/FirstName/]. </p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                Please verify that you own this email by clicking on the confirmation
                link below:
                <p>
                    <a target="_blank" href="[/VerificationLink/]" 
						style="text-decoration: none;display: inline-block;margin-bottom: 0;font-weight: normal;text-align: center;vertical-align: middle;cursor: pointer;background-image: none;border: 1px solid transparent;white-space: nowrap;padding: 16px 20px;font-size: 16px;line-height: 1.4;border-radius: 4px;color: #333;background-color: #fff;border-color: #ccc">
                        This is my email!
                    </a>
                </p>
            </div>
            <p></p>
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
</html>
';
