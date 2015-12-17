

DECLARE @currentDateTime AS DATETIME;
SET		@currentDateTime = GETDATE();
DECLARE @currentDateTimeUtc AS DATETIME;
SET		@currentDateTimeUtc = GETUTCDATE();

DECLARE @docTypeName AS VARCHAR(20) = 'EventPaymentRequest',
		@kandoBayBrand AS VARCHAR(20) = 'KandoBay',
		@theMusicBrand AS VARCHAR(20) = 'TheMusic';

IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE DocType = @docTypeName AND Brand = @kandoBayBrand)
begin
	INSERT INTO [dbo].[EmailTemplate]
			   ([DocType]
			   ,[Description]
			   ,[SubjectTemplate]
			   ,[BodyTemplate]
			   ,[IsBodyHtml]
			   ,[From]
			   ,[ParserName]
			   ,[ModifiedBy]
			   ,[ModifiedDate]
			   ,[ModifiedDateUtc]
			   ,[Brand])
		 VALUES
			   (@docTypeName
			   ,'Email directly to customer who purchased the tickets and should contain attachments so they can print.'
			   ,'<todo>' -- Subject
			   ,'<todo>' -- Body
			   ,1
			   ,'<todo>' -- From
			   ,'SquareBracketParser'
			   ,'System'
			   ,@currentDateTime
			   ,@currentDateTimeUtc
			   ,@kandoBayBrand)
end


IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE DocType = @docTypeName AND Brand = @theMusicBrand)
begin
	INSERT INTO [dbo].[EmailTemplate]
			   ([DocType]
			   ,[Description]
			   ,[SubjectTemplate]
			   ,[BodyTemplate]
			   ,[IsBodyHtml]
			   ,[From]
			   ,[ParserName]
			   ,[ModifiedBy]
			   ,[ModifiedDate]
			   ,[ModifiedDateUtc]
			   ,[Brand])
		 VALUES
			   (@docTypeName
			   ,'Email directly to customer who purchased the tickets and should contain attachments so they can print.'
			   ,'<todo>' -- Subject
			   ,'<todo>' -- Body
			   ,1
			   ,'<todo>' -- From
			   ,'SquareBracketParser'
			   ,'System'
			   ,@currentDateTime
			   ,@currentDateTimeUtc
			   ,@theMusicBrand)
end

UPDATE	EmailTemplate
SET	[SubjectTemplate]  = 'Event Payment Request',
	[From] = 'kandobay-support@kandobay.com.au',
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
</html>'
WHERE	DocType	= @docTypeName
	and Brand = @kandoBayBrand

UPDATE	EmailTemplate
SET	[SubjectTemplate]  = 'Event Payment Request',
	[From] = 'classies@themusic.com.au',
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
            <a target="_blank" href="http://classies.themusic.com.au" style="text-decoration: underline;color: #fff">Home</a> |
            <a target="_blank" href="http://classies.themusic.com.au/Home/ContactUs" style="text-decoration: underline;color:#fff">Contact Us</a>
        </p>
        <p>
            This is an automated email sent to you officially by
            <a target="_blank" href="http://classies.themusic.com.au" style="color: #fff; text-decoration: underline">
                classies.themusic.com.au
            </a> for security reasons.
            If you believe that you received this in error please ignore it or contact our friendly team.
        </p>
    </div>
</body>
</html>'
WHERE	DocType	= @docTypeName
	and Brand = @theMusicBrand
