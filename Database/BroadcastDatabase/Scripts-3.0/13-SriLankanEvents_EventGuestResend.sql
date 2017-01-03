-- EventGuest - Kandobay
exec [EmailTemplate_Upsert]
	@DocType = 'EventGuestResend',
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
            <h3>Hello [/GuestFullName/]</h3>
            <p>On behalf of the event organiser for <em>[/EventName/]</em> we are resending your tickets due to potential changes to the event or your tickets.</p> <p>Please use this email (tickets included) for entry and discard the old one(s).</p>
            <div style="min-height: 20px;padding: 19px;margin-bottom: 20px;margin-right: 20px;background-color: #f5f5f5;border: 1px solid #e3e3e3;border-radius: 0;-webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);box-shadow: inset 0 1px 1px rgba(0, 0, 0, .05);">
                 <p class="well well-lg">
                    <h3>Location : [/Location/]</h3>

                    <h3>Starts : [/EventStartDate/]</h3>

                    <h3>Finishes : [/EventEndDate/]</h3>

                    <h3>Ticket Type : [/TicketType/]</h3>

					<h3>Group : [/GroupName/]</h3>
                </p>
            </div>
			<div class="text-centre">
				Simply show this barcode on entry: <br />
				<img class="thumbnail" alt="Barcode" src="[/BarcodeImgUrl/]"></img>
			</div>
			
            <p>We have included a calendar invite for your convenience.</p>  Full 
            event details <a href="[/EventUrl/]">here.</a></p>
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