GO
IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE Name = 'ForgottenPassword' )
begin
	INSERT INTO EmailTemplate (Name, Description, EmailContent, Subject, Sender, EntityCode)
	VALUES ('ForgottenPassword', 'ForgottenPassword template', '', 'Password Recovery', 'classies@themusic.com.au', 'P000000005');
end
GO
UPDATE	EmailTemplate
SET	Description = 	'ForgottenPassword template',
	Subject = 'Password Recovery',
	Sender = 'classies@themusic.com.au',
	EmailContent = '
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
                            <img src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" 
                                alt="The Music | theMusic.com.au | Australia’s Premier Music News &amp; Reviews Website" />
                        </a>
                    </div>

                </td>
            </tr>

            <tr>
                <td valign="top" style="width: 600px;">
                    <h2 class="headline">Classies Password Recovery</h2>
                    <p class="mainPara">Hi, [/username/], seems you may have misplaced your password. Please find it, and your username below:</p>
                    <p class="mainPara">Username: [/username/]</p>
                    <p class="mainPara">Password: [/password/]</p>
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
            <!--  VERY BOTTOM LINKS, SHOULDN''T NEED TO CHANGE -->
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
WHERE	Name	= 'ForgottenPassword'


GO 
IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE Name = 'Registration' )
begin
	INSERT INTO EmailTemplate (Name, Description, EmailContent, Subject, Sender, EntityCode)
	VALUES ('Registration', 'Registration template', '', 'Classies Account Registration Details', 'classies@themusic.com.au', 'P000000005');
end
GO
UPDATE	EmailTemplate
SET	Description = 'Registration template',
	Subject = 'Classies Account Registration Details',
	Sender = 'classies@themusic.com.au',
	EmailContent = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TheMusic Classies Registration - Notification</title>
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
                            <img src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png"
                                alt="The Music | theMusic.com.au | Australia’s Premier Music News &amp; Reviews Website" />
                        </a>
                    </div>

                </td>
            </tr>

            <tr>
                <td valign="top" style="width: 600px;">
                    <h2 class="headline">Classies Account Registration!</h2>
                    <p class="mainPara">You have successfully set up an account with TheMusic Classies, congratulations! 
                        You will find the login directly under the logo on <a href="http://classies.themusic.com.au">classies.themusic.com.au</a>. Please keep the below details confidential.</p>
                    <p class="mainPara">Username: [/username/]</p>
                    <p class="mainPara">Password: [/password/]</p>
                    <br />
                    <p class="mainPara">Thanks,</p>
                    <p class="mainPara">TheMusic Team</p>
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 600px;">
                    <div class="boxLinks">
                        <p class="mainPara">Thank you for using TheMusic Classies, Street Press Australia''s classifieds site. Did you try our new premium advert this time? Maximise your print advertisement’s potential by choosing our <a href="https://classies.themusic.com.au/login.aspx?ReturnUrl=/Default.aspx">premium advert option</a> which includes a bold heading and a colour image. If you want to see your advertisement in print, you can check the electronic versions of our publications <a href="http://www.streetpress.com.au">here.</a> If you have any concerns or would like to provide feedback, please don''t hesitate to <a href="http://classies.themusic.com.au/Help/Contact.aspx">contact us.</a></p>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" class="outline" valign="middle" style="background-color: #e3f6fd; width: 600px;" colspan="4">
                    <p class="boxLinks3"><a href="http://classies.themusic.com.au/Help/FAQ.aspx" target="_blank">FAQ</a> | <a href="http://classies.themusic.com.au/Rates.aspx" target="_blank">Rates</a> | <a href="http://classies.themusic.com.au/Publications.aspx" target="_blank">Deadlines</a> | <a href="http://classies.themusic.com.au/Terms.aspx" target="_blank">Terms and Conditions</a> | <a href="http://classies.themusic.com.au/Help/PrivacyPolicy.aspx" target="_blank">Privacy Policy</a> | <a href="http://classies.themusic.com.au/Help/Contact.aspx" target="_blank">Contact Us</a> </p>
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>
'
WHERE	Name	= 'Registration'

GO

IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE Name = 'SupportNotification' )
begin
	INSERT INTO EmailTemplate (Name, Description, EmailContent, Subject, Sender, EntityCode)
	VALUES ('SupportNotification', 'Classies support', '', 'Classies Support', 'classies@themusic.com.au', 'P000000005');
end
GO
UPDATE	EmailTemplate
SET	Description = 'Classies support',
	Subject = 'Classies support',
	Sender = 'classies@themusic.com.au',
	EmailContent = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TheMusic Classies Support - Notification</title>
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
                            <img src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png"
                                alt="The Music | theMusic.com.au | Australia�s Premier Music News &amp; Reviews Website" />
                        </a>
                    </div>

                </td>
            </tr>

            <tr>
                <td valign="top" style="width: 600px;">
                    <h2 class="headline">Classies support request sent</h2>
                    <p class="mainPara">
                        Your request for information has been sent to our support team. Please be patient and we will endeavour to get back to you as soon as possible.
                    </p>
                    <p class="mainPara">Your Request:</p>
                    <p class="mainPara">[/content/]</p>
                    <br />
                    <p class="mainPara">Thanks,</p>
                    <p class="mainPara">TheMusic Team</p>
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 600px;">
                    <div class="boxLinks">
                        <p class="mainPara">Thank you for using TheMusic Classies, Street Press Australia''s classifieds site. Did you try our new premium advert this time? Maximise your print advertisements potential by choosing our <a href="https://classies.themusic.com.au/login.aspx?ReturnUrl=/Default.aspx">premium advert option</a> which includes a bold heading and a colour image. If you want to see your advertisement in print, you can check the electronic versions of our publications <a href="http://www.streetpress.com.au">here.</a> If you have any concerns or would like to provide feedback, please don''t hesitate to <a href="http://classies.themusic.com.au/Help/Contact.aspx">contact us.</a></p>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" class="outline" valign="middle" style="background-color: #e3f6fd; width: 600px;" colspan="4">
                    <p class="boxLinks3">
                        <a href="http://classies.themusic.com.au/Help/FAQ.aspx" target="_blank">FAQ</a> | 
                        <a href="http://classies.themusic.com.au/Rates.aspx" target="_blank">Rates</a> | 
                        <a href="http://classies.themusic.com.au/Publications.aspx" target="_blank">Deadlines</a> | <a href="http://classies.themusic.com.au/Terms.aspx" target="_blank">Terms and Conditions</a> | <a href="http://classies.themusic.com.au/Help/PrivacyPolicy.aspx" target="_blank">Privacy Policy</a> | <a href="http://classies.themusic.com.au/Help/Contact.aspx" target="_blank">Contact Us</a>
                    </p>
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>

'
WHERE	Name	= 'SupportNotification'
GO

IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE Name = 'AfterAdBookingNotification' )
begin
	INSERT INTO EmailTemplate (Name, Description, EmailContent, Subject, Sender, EntityCode)
	VALUES ('AfterAdBookingNotification', 'After Ad Booking Notification', '', 'Classies Booking Notification', 'classies@themusic.com.au', 'P000000005');
end
GO
UPDATE	EmailTemplate
SET	Description = 'After Ad Booking Notification',
	Subject = 'Classies Booking Notification',
	Sender = 'classies@themusic.com.au',
	EmailContent = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TheMusic Classies New Booking - Notification</title>
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
                            <img src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png"
                                alt="The Music | theMusic.com.au | Australia�s Premier Music News &amp; Reviews Website" />
                        </a>
                    </div>

                </td>
            </tr>

            <tr>
                    <td valign="top" style="width: 600px;">
                    <h2 class="headline">Classies New Ad</h2>
                    <p class="mainPara">Hi, [/username/] you have successfully booked your classified. </p>
                    <p class="mainPara">[/content/]</p>
                    <br />
                    <p class="mainPara">Thanks,</p>
                    <p class="mainPara">TheMusic Team</p>
                    </td>
                </tr>
            <tr>
                <td valign="top" style="width: 600px;">
                    <div class="boxLinks">
                        <p class="mainPara">Thank you for using TheMusic Classies, Street Press Australia''s classifieds site. Did you try our new premium advert this time? Maximise your print advertisements potential by choosing our <a href="https://classies.themusic.com.au/login.aspx?ReturnUrl=/Default.aspx">premium advert option</a> which includes a bold heading and a colour image. If you want to see your advertisement in print, you can check the electronic versions of our publications <a href="http://www.streetpress.com.au">here.</a> If you have any concerns or would like to provide feedback, please don''t hesitate to <a href="http://classies.themusic.com.au/Help/Contact.aspx">contact us.</a></p>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" class="outline" valign="middle" style="background-color: #e3f6fd; width: 600px;" colspan="4">
                    <p class="boxLinks3"><a href="http://classies.themusic.com.au/Help/FAQ.aspx" target="_blank">FAQ</a> | 
                        <a href="http://classies.themusic.com.au/Rates.aspx" target="_blank">Rates</a> | 
                        <a href="http://classies.themusic.com.au/Publications.aspx" target="_blank">Deadlines</a> | <a href="http://classies.themusic.com.au/Terms.aspx" target="_blank">Terms and Conditions</a> | <a href="http://classies.themusic.com.au/Help/PrivacyPolicy.aspx" target="_blank">Privacy Policy</a> | <a href="http://classies.themusic.com.au/Help/Contact.aspx" target="_blank">Contact Us</a> </p>
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>
'
WHERE	Name	= 'AfterAdBookingNotification'
GO


IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE Name = 'Notification' )
begin
	INSERT INTO EmailTemplate (Name, Description, EmailContent, Subject, Sender, EntityCode)
	VALUES ('Notification', 'Expiry Notice', '', 'Extend your expiring Classies Ad', 'classies@themusic.com.au', 'P000000005');
end
GO
UPDATE	EmailTemplate
SET	Description = 'Expiry Notice',
	Subject = 'Extend your expiring Classies Ad',
	Sender = 'classies@themusic.com.au',
	EmailContent = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TheMusic Classies Registration - Notification</title>
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
                            <img src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png"
                                alt="The Music | theMusic.com.au | Australia’s Premier Music News &amp; Reviews Website" />
                        </a>
                    </div>

                </td>
            </tr>

            <tr>
                <td valign="top" style="width: 600px;">
                    <h2
                        class="headline">Classies Ad Expiring</h2>
                    <p class="mainPara">
                        This is a reminder that your ad(s) is about to expire.
                    </p>
                    <p class="mainPara">[/adReference/]</p>
                    <p class="mainPara">
                        Skip the booking processing by <a href="[/linkForExtension/]" target="_blank">extending your ad here.</a>
                    </p>
                    <br />
                    <p
                        class="mainPara">
                        Thanks,
                    </p>
                    <p class="mainPara">TheMusic Team</p>
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 600px;">
                    <div class="boxLinks">
                        <p class="mainPara">Thank you for using TheMusic Classies, Street Press Australia''s classifieds site. Did you try our new premium advert this time? Maximise your print advertisement’s potential by choosing our <a href="https://classies.themusic.com.au/login.aspx?ReturnUrl=/Default.aspx">premium advert option</a> which includes a bold heading and a colour image. If you want to see your advertisement in print, you can check the electronic versions of our publications <a href="http://www.streetpress.com.au">here.</a> If you have any concerns or would like to provide feedback, please don''t hesitate to <a href="http://classies.themusic.com.au/Help/Contact.aspx">contact us.</a></p>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" class="outline" valign="middle" style="background-color: #e3f6fd; width: 600px;" colspan="4">
                    <p class="boxLinks3"><a href="http://classies.themusic.com.au/Help/FAQ.aspx" target="_blank">FAQ</a> | <a href="http://classies.themusic.com.au/Rates.aspx" target="_blank">Rates</a> | <a href="http://classies.themusic.com.au/Publications.aspx" target="_blank">Deadlines</a> | <a href="http://classies.themusic.com.au/Terms.aspx" target="_blank">Terms and Conditions</a> | <a href="http://classies.themusic.com.au/Help/PrivacyPolicy.aspx" target="_blank">Privacy Policy</a> | <a href="http://classies.themusic.com.au/Help/Contact.aspx" target="_blank">Contact Us</a> </p>
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>
'
WHERE	Name	= 'Notification'
GO


IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE Name = 'OnlineAdEnquiry' )
begin
	INSERT INTO EmailTemplate (Name, Description, EmailContent, Subject, Sender, EntityCode)
	VALUES ('OnlineAdEnquiry', 'Online Ad Enquiry', '', 'Classies Ad Enquiry', 'classies@themusic.com.au', 'P000000005');
end
GO
UPDATE	EmailTemplate
SET	Description = 'Online Ad Enquiry',
	Subject = 'Classies Ad Enquiry',
	Sender = 'classies@themusic.com.au',
	EmailContent = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TheMusic Classies Ad Enquiry - Notification</title>
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
                            <img src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png"
                                alt="The Music | theMusic.com.au | Australia�s Premier Music News &amp; Reviews Website" />
                        </a>
                    </div>

                </td>
            </tr>

            <tr>
                <td valign="top" style="width: 600px;">
                    <h2 class="headline">Classies Ad Enquiry</h2>
                    <p class="mainPara">
                        Hi, you have recieved interest in your Ad #[/adNumber/]. To find out what has 
                        been left for you, go to <a href="http://classies.themusic.com.au">our classies site</a>, 
                        login and you will see a message alert just below our logo. Click to find out more!
                    </p>
                    <br />
                    <p class="mainPara">Thanks,</p>
                    <p class="mainPara">TheMusic Team</p>
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 600px;">
                    <div class="boxLinks">
                        <p class="mainPara">Thank you for using TheMusic Classies, Street Press Australia''s classifieds site. Did you try our new premium advert this time? Maximise your print advertisements potential by choosing our <a href="https://classies.themusic.com.au/login.aspx?ReturnUrl=/Default.aspx">premium advert option</a> which includes a bold heading and a colour image. If you want to see your advertisement in print, you can check the electronic versions of our publications <a href="http://www.streetpress.com.au">here.</a> If you have any concerns or would like to provide feedback, please don''t hesitate to <a href="http://classies.themusic.com.au/Help/Contact.aspx">contact us.</a></p>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" class="outline" valign="middle" style="background-color: #e3f6fd; width: 600px;" colspan="4">
                    <p class="boxLinks3">
                        <a href="http://classies.themusic.com.au/Help/FAQ.aspx" target="_blank">FAQ</a> | 
                        <a href="http://classies.themusic.com.au/Rates.aspx" target="_blank">Rates</a> | 
                        <a href="http://classies.themusic.com.au/Publications.aspx" target="_blank">Deadlines</a> | 
                        <a href="http://classies.themusic.com.au/Terms.aspx" target="_blank">Terms and Conditions</a> | 
                        <a href="http://classies.themusic.com.au/Help/PrivacyPolicy.aspx" target="_blank">Privacy Policy</a> | 
                        <a href="http://classies.themusic.com.au/Help/Contact.aspx" target="_blank">Contact Us</a>
                    </p>
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>
'
WHERE	Name	= 'OnlineAdEnquiry'
GO