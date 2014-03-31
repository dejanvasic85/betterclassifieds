﻿

DECLARE @currentDateTime AS DATETIME;
SET		@currentDateTime = GETDATE();
DECLARE @currentDateTimeUtc AS DATETIME;
SET		@currentDateTimeUtc = GETUTCDATE();



IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE DocType = 'ForgottenPassword' )
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
			   ,[ModifiedDateUtc])
		 VALUES
			   ('ForgottenPassword'
			   ,'User will be sent a new temporary password'
			   ,'<todo>' -- Subject
			   ,'<todo>' -- Body
			   ,1
			   ,'<todo>' -- From
			   ,'SquareBracketParser'
			   ,'System'
			   ,@currentDateTime
			   ,@currentDateTimeUtc)
end


UPDATE	EmailTemplate
SET	[SubjectTemplate]  = 'Classies Password Reset',
	[From] = 'classies@themusic.com.au',
	[BodyTemplate] = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TheMusic classies forgotten password - Notification</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <style type="text/css">
        <!--
        body
        {
            font-family: tahoma, verdana, arial, sans-serif;
            margin: 0;
            margin-left: 10px;
            padding: 0;
            color: #000;
        }

        img
        {
            margin: 0px 0px;
            padding: 0px;
            border-style: none;
            
        }

        a
        {
            color: #666666;
        }

            a:hover
            {
                color: #01aee9;
            }

        .blueBackground
        {
            background: #01aee9;
            display: block;
            width: 100px;
            height: 100px;
        }

        .sidebarHeader
        {
            padding: 0px;
            margin: 4px;
            color: #285ea1;
            font-size: .9em;
            line-height: .9em;
            margin-bottom: 5px;
        }

        .sidebarPara
        {
            padding: 0px;
            margin: 0px;
            color: #01aee9;
            font-size: .7em;
            line-height: .7em;
            text-decoration: underline;
        }

        .headline
        {
            font-size: 20px;
            color: #263745;
            text-weight: bold;
            line-height: 20px;
            margin: 20px 20px 15px 20px;
            padding: 0;
            border-bottom: #00aef1 2px solid;
            display: block;
            height: 30px;
        }

        .mainPara
        {
            font-size: .8em;
            color: #333;
            line-height: 1.2em;
            margin: 4px 20px 5px 20px;
            padding: 0;
        }

        .mainLinks
        {
            font-size: 11px;
            color: #333;
            line-height: 1em;
            display: block;
            margin: 0;
            padding: 0px 20px;
            width: 430px;
            text-align: right;
        }

        .highlight
        {
            font-size: 1.2em;
            color: #01aee9;
        }

        .highlightBlue
        {
            color: #01aee9;
        }

        .highlightBlack
        {
            color: #000;
        }

        .highlightGrey
        {
            color: #333;
        }

        .spacer
        {
            margin: 15px 0px;
        }

        .headlineBox
        {
            font-size: 1em;
            color: #333;
            line-height: 30px;
            margin: 0;
            padding: 0;
            display: block;
            width: 450px;
            height: 30px;
            text-align: center;
            background: #fff;
            border-bottom: 2px solid #333;
        }

        .subHeadlineBox
        {
            font-size: 1em;
            color: #01aee9;
            line-height: 1em;
            margin: 6px 10px 4px 5px;
            padding: 0;
        }

        .boxLinks
        {
            font-size: 13px;
            color: #fff;
            line-height: 13px;
            display: block;
            margin: 40px 20px 20px;
            padding: 10px;
            width: 540px;
            background-color: #aeddec;
            text-align: right;
            text-align: center;
        }

        .boxLinks3
        {
            font-size: 11px;
            color: #fff;
            line-height: 1em;
            display: block;
            margin: 10px auto;
            padding: 0px;
            text-decoration: none;
        }

        .outline
        {
            border: none;
        }

        .vertLine
        {
            border-right: 1px dashed #01aee9;
        }
        -->
    </style>
</head>
<body>
    <table width="600" align="center" style="margin: 20px auto; background: #fff;" border="0" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td colspan="4">
                    <div style="text-align: center;">
                        <a href="http://classies.themusic.com.au/" target="_blank">
                            <img  src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png"  width="300px"
                                alt="The Music | theMusic.com.au | Australia’s Premier Music News &amp; Reviews Website" />
                        </a>
                    </div>

                </td>
            </tr>

            <tr>
                <td valign="top" style="width: 600px;">
                    <h2 class="headline">Classies Password Recovery</h2>
                    <p class="mainPara">Hi, [/Username/], seems you may have misplaced your password. Please find your username and new temporary password below:</p>
                    <p class="mainPara">Username: [/Username/]</p>
                    <p class="mainPara">Password: [/Password/]</p>
                    <br />
                    <p class="mainPara">Thanks,</p>
                    <p class="mainPara">TheMusic Team</p>
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 600px;">
                    <div class="boxLinks">
                        <p class="mainPara">
                            Thank you for using TheMusic Classies, Street Press Australia''s classifieds site. 
                        Did you try our new premium advert this time? Maximise your print advertisements 
                        potential by choosing our <a href="http://classies.themusic.com.au/">premium advert option</a> which includes a bold heading and a colour image. 
                        If you want to see your advertisement in print, you can check the electronic versions of our 
                        publications <a href="http://www.streetpress.com.au">here.</a>
                            If you have any concerns or would like to provide feedback, 
                        please don''t hesitate to <a href="http://classies.themusic.com.au/Help/Contact.aspx">contact us.</a>
                        </p>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td align="center" class="outline" valign="middle" style="background-color: #e3f6fd; width: 600px;" colspan="4">
                    <p class="boxLinks3">
                        <a href="http://classies.themusic.com.au/Help/FAQ.aspx" target="_blank">FAQ</a> | 
                        <a href="http://classies.com.au/Rates.aspx" target="_blank">Rates</a> | 
                        <a href="http://classies.themusic.com.au/Publications.aspx" target="_blank">Deadlines</a> | 
                        <a href="http://classies.themusic.com.au/Terms.aspx" target="_blank">Terms and Conditions</a> | 
                        <a href="http://classies.themusic.com.au/Help/PrivacyPolicy.aspx" target="_blank">Privacy Policy</a> | 
                        <a href="http://classies.themusic.com.au/Help/Contact.aspx" target="_blank">Contact Us</a> </p>
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>

'
WHERE	DocType	= 'ForgottenPassword'