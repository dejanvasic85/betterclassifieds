
DECLARE @currentDateTime AS DATETIME;
SET		@currentDateTime = GETDATE();
DECLARE @currentDateTimeUtc AS DATETIME;
SET		@currentDateTimeUtc = GETUTCDATE();

-- Account Confirmation
UPDATE	EmailTemplate
SET	[BodyTemplate] = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Confirm Account Registration</title>
<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
<link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans:400,400italic,600,600italic,700,700italic,800,800italic"/>
<style type="text/css">
body {
	background-color: #ffffff;
	margin: 0px;
	padding: 0px;
	text-align: center;
	width: 100%;
}
html { width: 100%; }
.contentbg
{
	 background-color: #ffffff;
}
img {
	border:0px;
	outline:none;
	text-decoration:none;
	display:block;
}
a img {
	border: none;
}
.textheader {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: right;
}
.textcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.on-white-bg{
  color: #263745;
}
.headingcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: left;
}
.headingmain {
	font-family: ''Open Sans'', sans-serif;
	font-size: 36px;
	font-weight: 600;
	color: #ffffff;
	text-align: center;
}
.subimagehilight {
	font-family: ''Open Sans'', sans-serif;
	font-size: 48px;
	color: #ffffff;
	text-align:left;
	text-decoration: none;
	line-height: 50px;
	font-weight: 600;
}
.iconheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: center;
}
.textfooter {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.footerheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 20px;
	font-weight: 400;
	color: #ffffff;
	line-height: 24px;
	text-align:left;
}
a {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a.headerlink:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.headerlink:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.bottomicontext:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
@media only screen and (max-width:640px)

{
	body{width:auto!important;background-color: #fff;}
	.main{width:446px !important;}
	.righttop-details{width:226px !important;}
	.contentbox{width:386px !important; display:block; padding:0px 25px !important;}
	.subcontentbox{width:386px !important;}
	.details-box{height:auto !important;}
	.content-space{width:386px !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:188px !important; height:188px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

@media only screen and (max-width:450px)
{
	body{width:auto!important; background-color:#fff;}
	.main{width:320px !important;}
	.logo{width:100% !important;}
	.righttop-details{width:100% !important;}
	.social-top{width:100% !important;}
	.viewonline-field{padding: 10px 0px 0px 0px !important;}
	#viewonline{text-align:center !important;}
	.contentbox{width:100% !important; display:block; padding:0px !important;}
	.subcontentbox{width:100% !important;}
	.details-box{height:auto !important;}
	.content-space{width:100% !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:150px !important; height:150px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

.btn, .btn:hover {
  display: inline-block;
  margin-bottom: 0;
  font-weight: normal;
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  -ms-touch-action: manipulation;
      touch-action: manipulation;
  cursor: pointer;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
  background-image: none;
  border: 1px solid transparent;
  
  padding: 10px 16px;
  font-size: 18px;
  line-height: 1.33;
  border-radius: 6px;
}
.btn-primary {
  color: #fff;
  background-color: #263745;
  border-color: #357ebd;
}
.btn-primary .badge {
  color: #428bca;
  background-color: #fff;
}

.btn-lg {
  
}

</style>

<!-- Internet Explorer fix -->
<!--[if IE]>
<style type="text/css">

@media only screen and (max-width:640px)
{
	.contentbox{width:386px !important; float:left; display:block; padding:0px 25px !important;}
    .subcontentbox{width:386px !important; float:left;}
    .content-space{width:386px !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

@media only screen and (max-width:450px)
{
	.contentbox{width:100% !important; float:left; display:block; padding:0px !important;}
    .subcontentbox{width:100% !important; float:left;}
    .content-space{width:100% !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

</style>
<![endif]-->
<!-- / Internet Explorer fix -->

</head>

<body>

<!--Table Start-->
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="contentbg">
  <tr>
    <td align="center" valign="top" style="background-color: #fff">
    
    <!--Header Start-->
    <table width="610" border="0" cellspacing="0" cellpadding="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding-bottom:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="top">
            
            <!--Logo Start-->
            <table width="250" border="0" align="left" cellpadding="0" cellspacing="0" class="logo">
              <tr>
                <td align="center" valign="top"><a href="#" target="_blank"><img editable="true" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" width="250" height="100" alt="logo" style="display:block;"/></a></td>
              </tr>
            </table>
            <!--Logo End-->
            

            <table width="200" border="0" align="right" cellpadding="0" cellspacing="0" class="righttop-details">
              <tr>
                <td align="right" style="padding-top:18px;">
                
                <!--Social Top Start-->
                <table width="100" border="0" cellspacing="0" cellpadding="0" class="social-top">
                  <tr>
                    <td align="center"><table width="100" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <!--Social Top End-->
                
                </td>
              </tr>
             
            </table>
            </td>
          </tr>
        </table></td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Header End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top">
    
    <!--Content Start-->
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td valign="top" style="background-color : #263745">
        
        <!--Heading Text Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" mc:edit="heading-text" class="headingmain" style="padding:18px 25px 25px 25px;"><singleline>Confirm Registration</singleline></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Heading Text End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top">
        

        <!--Content Body Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td valign="top">
                
<table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody><tr>
                    <td valign="top" style="padding:20px 25px 30px 25px;background-color: #b01e00"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tbody><tr>
                        <td valign="top" mc:edit="text-area-heading" class="headingcontent"><singleline>Welcome to TheMusic!</singleline></td>
                      </tr>
                      <tr>
                        <td valign="top" mc:edit="text-area-content" class="textcontent" style="padding:10px 0px 20px 0px;"><singleline>Thank you
                        for signing up with us <strong>[/FirstName/]</strong>.</singleline></td>
                      </tr>
                    
                    </tbody></table></td>
                  </tr>
                </tbody></table>


                <!--Main Image Start-->
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                 
                  <tr>
                    <td valign="top" style="padding:20px 25px 30px 25px; border: 1px solid #263745; color: #263745; background-color: #fff">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                     
                      <tr>
                        <td valign="top" class="textcontent on-white-bg" mc:edit="main-image-content" style="padding:10px 0px 20px 0px;"><p>You will need to verify that you own this email address by clicking on the following link below. </p> </td>
                      </tr>
                    
                      <tr>
                        <td align="center" valign="top">
                        <a href="[/VerificationLink/]" target="_blank" class="btn btn-lg btn-primary">Confirm</a></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <!--Main Image End-->
                
                </td>
              </tr>
                
                </td>
              </tr>
             
            </table></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Content Body End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top" style="background-color : #b01e00">
        
        <!--Icon Bottom Start-->
       
        <!--Icon Bottom End-->
        
        </td>
      </tr>
    </table>
    <!--Content End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top" style="background-color : #263745">
    
    <!--Footer Start-->
    <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding:35px 0px;">
        
        <!--Address Contact Start-->
        <table width="300" border="0" align="left" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              
              <tr>
                <td valign="top" mc:edit="address-contact" class="textfooter" style="padding:15px 0px 17px 0px;"><singleline>TheMusic <br />
                  PH: 61 3 9421 4499<br />
                  <a href="#" class="headerlink">info@themusic.com.au</a></singleline></td>
              </tr>
              <tr>
                <td valign="top"><table width="100" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Address Contact End-->
        
        <!--Footer Link Start-->
        <table width="300" border="0" align="right" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td class="hidebox-footer" style="display:none;"></td>
          </tr>
          <tr>
            <td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="50%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="recommend" class="footerheading"><singleline>Classies</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="recommend-link" class="textfooter" style="padding-top:12px;">
                      
                      <singleline><a href="http://classies.themusic.com.au" target="_blank" class="headerlink">Classies Home</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Booking/Step/1" target="_blank" class="headerlink">Place New Ad</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Home/ContactUs" target="_blank" class="headerlink">Contact Us</a></singleline><br />
                  </tr>
                </table></td>
                <td width="50" valign="top" style="padding-left:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="newsletter" class="footerheading"><singleline>TheMusic</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="newsletter-link" class="textfooter" style="padding-top:12px;">
                      <singleline><a href="http://themusic.com.au" target="_blank" class="headerlink">Home</a></singleline><br />
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Footer Link End-->
        
        </td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Footer End-->
    
    </td>
  </tr>
</table>
<!--Table End-->

</body>
</html>
', 
Brand = 'TheMusic'
WHERE	DocType	= 'NewRegistration'

-- Password Reset
UPDATE	EmailTemplate
SET [BodyTemplate] = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Password Recovery</title>
<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
<link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans:400,400italic,600,600italic,700,700italic,800,800italic"/>
<style type="text/css">
body {
	background-color: #ffffff;
	margin: 0px;
	padding: 0px;
	text-align: center;
	width: 100%;
}
html { width: 100%; }
.contentbg
{
	 background-color: #ffffff;
}
img {
	border:0px;
	outline:none;
	text-decoration:none;
	display:block;
}
a img {
	border: none;
}
.textheader {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: right;
}
.textcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.on-white-bg{
  color: #263745;
}
.headingcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: left;
}
.headingmain {
	font-family: ''Open Sans'', sans-serif;
	font-size: 36px;
	font-weight: 600;
	color: #ffffff;
	text-align: center;
}
.subimagehilight {
	font-family: ''Open Sans'', sans-serif;
	font-size: 48px;
	color: #ffffff;
	text-align:left;
	text-decoration: none;
	line-height: 50px;
	font-weight: 600;
}
.iconheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: center;
}
.textfooter {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.footerheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 20px;
	font-weight: 400;
	color: #ffffff;
	line-height: 24px;
	text-align:left;
}
a {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a.headerlink:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.headerlink:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.bottomicontext:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
@media only screen and (max-width:640px)

{
	body{width:auto!important;background-color: #fff;}
	.main{width:446px !important;}
	.righttop-details{width:226px !important;}
	.contentbox{width:386px !important; display:block; padding:0px 25px !important;}
	.subcontentbox{width:386px !important;}
	.details-box{height:auto !important;}
	.content-space{width:386px !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:188px !important; height:188px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

@media only screen and (max-width:450px)
{
	body{width:auto!important; background-color:#fff;}
	.main{width:320px !important;}
	.logo{width:100% !important;}
	.righttop-details{width:100% !important;}
	.social-top{width:100% !important;}
	.viewonline-field{padding: 10px 0px 0px 0px !important;}
	#viewonline{text-align:center !important;}
	.contentbox{width:100% !important; display:block; padding:0px !important;}
	.subcontentbox{width:100% !important;}
	.details-box{height:auto !important;}
	.content-space{width:100% !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:150px !important; height:150px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

.btn, .btn:hover {
  display: inline-block;
  margin-bottom: 0;
  font-weight: normal;
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  -ms-touch-action: manipulation;
      touch-action: manipulation;
  cursor: pointer;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
  background-image: none;
  border: 1px solid transparent;
  
  padding: 10px 16px;
  font-size: 18px;
  line-height: 1.33;
  border-radius: 6px;
}
.btn-primary {
  color: #fff;
  background-color: #263745;
  border-color: #357ebd;
}
.btn-primary .badge {
  color: #428bca;
  background-color: #fff;
}

.btn-lg {
  
}

</style>

<!-- Internet Explorer fix -->
<!--[if IE]>
<style type="text/css">

@media only screen and (max-width:640px)
{
	.contentbox{width:386px !important; float:left; display:block; padding:0px 25px !important;}
    .subcontentbox{width:386px !important; float:left;}
    .content-space{width:386px !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

@media only screen and (max-width:450px)
{
	.contentbox{width:100% !important; float:left; display:block; padding:0px !important;}
    .subcontentbox{width:100% !important; float:left;}
    .content-space{width:100% !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

</style>
<![endif]-->
<!-- / Internet Explorer fix -->

</head>

<body>

<!--Table Start-->
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="contentbg">
  <tr>
    <td align="center" valign="top" style="background-color: #fff">
    
    <!--Header Start-->
    <table width="610" border="0" cellspacing="0" cellpadding="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding-bottom:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="top">
            
            <!--Logo Start-->
            <table width="250" border="0" align="left" cellpadding="0" cellspacing="0" class="logo">
              <tr>
                <td align="center" valign="top"><a href="#" target="_blank"><img editable="true" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" width="250" height="100" alt="logo" style="display:block;"/></a></td>
              </tr>
            </table>
            <!--Logo End-->
            

            <table width="200" border="0" align="right" cellpadding="0" cellspacing="0" class="righttop-details">
              <tr>
                <td align="right" style="padding-top:18px;">
                
                <!--Social Top Start-->
                <table width="100" border="0" cellspacing="0" cellpadding="0" class="social-top">
                  <tr>
                    <td align="center"><table width="100" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <!--Social Top End-->
                
                </td>
              </tr>
             
            </table>
            </td>
          </tr>
        </table></td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Header End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top">
    
    <!--Content Start-->
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td valign="top" style="background-color : #263745">
        
        <!--Heading Text Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" mc:edit="heading-text" class="headingmain" style="padding:18px 25px 25px 25px;"><singleline>Password Recovery</singleline></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Heading Text End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top">
        

        <!--Content Body Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td valign="top">
                
<table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody><tr>
                    <td valign="top" style="padding:20px 25px 30px 25px;background-color: #b01e00"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tbody><tr>
                        <td valign="top" mc:edit="text-area-heading" class="headingcontent"><singleline>Forgot your password?</singleline></td>
                      </tr>
                      <tr>
                        <td valign="top" mc:edit="text-area-content" class="textcontent" style="padding:10px 0px 20px 0px;"><singleline>That''s ok,
                        we have reset your password for you and it has been set to:</singleline>

                        </td>
                      </tr>
                    
                    </tbody></table></td>
                  </tr>
                </tbody></table>


                <!--Main Image Start-->
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                 
                  <tr>
                    <td valign="top" style="padding:20px 25px 30px 25px; border: 1px solid #263745; color: #263745; background-color: #fff">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                     
                      <tr>
                        <td valign="top" class="textcontent on-white-bg" mc:edit="main-image-content" style="padding:10px 0px 20px 0px;">
                          <table>
                            <tr><td>Username</td><td><strong>[/Username/]</strong></td></tr>
                            <tr><td>Password</td><td><strong>[/Password/]</strong></td></tr>
                          </table>

                         </td>
                      </tr>
                    
                      <tr>
                        <td align="center" valign="top">
                        <a href="http://classies.themusic.com.au/" target="_blank" class="btn btn-lg btn-primary">Go To Login</a></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <!--Main Image End-->
                
                </td>
              </tr>
                
                </td>
              </tr>
             
            </table></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Content Body End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top" style="background-color : #b01e00">
        
        <!--Icon Bottom Start-->
       
        <!--Icon Bottom End-->
        
        </td>
      </tr>
    </table>
    <!--Content End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top" style="background-color : #263745">
    
    <!--Footer Start-->
    <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding:35px 0px;">
        
        <!--Address Contact Start-->
        <table width="300" border="0" align="left" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              
              <tr>
                <td valign="top" mc:edit="address-contact" class="textfooter" style="padding:15px 0px 17px 0px;"><singleline>TheMusic <br />
                  PH: 61 3 9421 4499<br />
                  <a href="#" class="headerlink">info@themusic.com.au</a></singleline></td>
              </tr>
              <tr>
                <td valign="top"><table width="100" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Address Contact End-->
        
        <!--Footer Link Start-->
        <table width="300" border="0" align="right" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td class="hidebox-footer" style="display:none;"></td>
          </tr>
          <tr>
            <td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="50%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="recommend" class="footerheading"><singleline>Classies</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="recommend-link" class="textfooter" style="padding-top:12px;">
                      
                      <singleline><a href="http://classies.themusic.com.au" target="_blank" class="headerlink">Classies Home</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Booking/Step/1" target="_blank" class="headerlink">Place New Ad</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Home/ContactUs" target="_blank" class="headerlink">Contact Us</a></singleline><br />
                  </tr>
                </table></td>
                <td width="50" valign="top" style="padding-left:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="newsletter" class="footerheading"><singleline>TheMusic</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="newsletter-link" class="textfooter" style="padding-top:12px;">
                      <singleline><a href="http://themusic.com.au" target="_blank" class="headerlink">Home</a></singleline><br />
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Footer Link End-->
        
        </td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Footer End-->
    
    </td>
  </tr>
</table>
<!--Table End-->

</body>
</html>
', 
Brand = 'TheMusic'
WHERE	DocType	= 'ForgottenPassword'

-- ExpirationReminder
UPDATE	EmailTemplate
SET [BodyTemplate] = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Password Recovery</title>
<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
<link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans:400,400italic,600,600italic,700,700italic,800,800italic"/>
<style type="text/css">
body {
	background-color: #ffffff;
	margin: 0px;
	padding: 0px;
	text-align: center;
	width: 100%;
}
html { width: 100%; }
.contentbg
{
	 background-color: #ffffff;
}
img {
	border:0px;
	outline:none;
	text-decoration:none;
	display:block;
}
a img {
	border: none;
}
.textheader {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: right;
}
.textcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.on-white-bg{
  color: #263745;
}
.headingcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: left;
}
.headingmain {
	font-family: ''Open Sans'', sans-serif;
	font-size: 36px;
	font-weight: 600;
	color: #ffffff;
	text-align: center;
}
.subimagehilight {
	font-family: ''Open Sans'', sans-serif;
	font-size: 48px;
	color: #ffffff;
	text-align:left;
	text-decoration: none;
	line-height: 50px;
	font-weight: 600;
}
.iconheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: center;
}
.textfooter {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.footerheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 20px;
	font-weight: 400;
	color: #ffffff;
	line-height: 24px;
	text-align:left;
}
a {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a.headerlink:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.headerlink:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.bottomicontext:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
@media only screen and (max-width:640px)

{
	body{width:auto!important;background-color: #fff;}
	.main{width:446px !important;}
	.righttop-details{width:226px !important;}
	.contentbox{width:386px !important; display:block; padding:0px 25px !important;}
	.subcontentbox{width:386px !important;}
	.details-box{height:auto !important;}
	.content-space{width:386px !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:188px !important; height:188px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

@media only screen and (max-width:450px)
{
	body{width:auto!important; background-color:#fff;}
	.main{width:320px !important;}
	.logo{width:100% !important;}
	.righttop-details{width:100% !important;}
	.social-top{width:100% !important;}
	.viewonline-field{padding: 10px 0px 0px 0px !important;}
	#viewonline{text-align:center !important;}
	.contentbox{width:100% !important; display:block; padding:0px !important;}
	.subcontentbox{width:100% !important;}
	.details-box{height:auto !important;}
	.content-space{width:100% !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:150px !important; height:150px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

.btn, .btn:hover {
  display: inline-block;
  margin-bottom: 0;
  font-weight: normal;
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  -ms-touch-action: manipulation;
      touch-action: manipulation;
  cursor: pointer;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
  background-image: none;
  border: 1px solid transparent;
  
  padding: 10px 16px;
  font-size: 18px;
  line-height: 1.33;
  border-radius: 6px;
}
.btn-primary {
  color: #fff;
  background-color: #263745;
  border-color: #357ebd;
}
.btn-primary .badge {
  color: #428bca;
  background-color: #fff;
}

.btn-lg {
  
}

</style>

<!-- Internet Explorer fix -->
<!--[if IE]>
<style type="text/css">

@media only screen and (max-width:640px)
{
	.contentbox{width:386px !important; float:left; display:block; padding:0px 25px !important;}
    .subcontentbox{width:386px !important; float:left;}
    .content-space{width:386px !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

@media only screen and (max-width:450px)
{
	.contentbox{width:100% !important; float:left; display:block; padding:0px !important;}
    .subcontentbox{width:100% !important; float:left;}
    .content-space{width:100% !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

</style>
<![endif]-->
<!-- / Internet Explorer fix -->

</head>

<body>

<!--Table Start-->
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="contentbg">
  <tr>
    <td align="center" valign="top" style="background-color: #fff">
    
    <!--Header Start-->
    <table width="610" border="0" cellspacing="0" cellpadding="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding-bottom:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="top">
            
            <!--Logo Start-->
            <table width="250" border="0" align="left" cellpadding="0" cellspacing="0" class="logo">
              <tr>
                <td align="center" valign="top"><a href="#" target="_blank"><img editable="true" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" width="250" height="100" alt="logo" style="display:block;"/></a></td>
              </tr>
            </table>
            <!--Logo End-->
            

            <table width="200" border="0" align="right" cellpadding="0" cellspacing="0" class="righttop-details">
              <tr>
                <td align="right" style="padding-top:18px;">
                
                <!--Social Top Start-->
                <table width="100" border="0" cellspacing="0" cellpadding="0" class="social-top">
                  <tr>
                    <td align="center"><table width="100" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <!--Social Top End-->
                
                </td>
              </tr>
             
            </table>
            </td>
          </tr>
        </table></td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Header End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top">
    
    <!--Content Start-->
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td valign="top" style="background-color : #263745">
        
        <!--Heading Text Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" mc:edit="heading-text" class="headingmain" style="padding:18px 25px 25px 25px;"><singleline>Reminder - Expiring Ad</singleline></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Heading Text End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top">
        

        <!--Content Body Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td valign="top">
                
<table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody><tr>
                    <td valign="top" style="padding:20px 25px 30px 25px;background-color: #b01e00"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tbody><tr>
                        <td valign="top" mc:edit="text-area-heading" class="headingcontent"><singleline>Your ad is expiring soon.</singleline></td>
                      </tr>
                      <tr>
                        <td valign="top" mc:edit="text-area-content" class="textcontent" style="padding:10px 0px 20px 0px;">
	                        <singleline>
	                        	The ad that is about to expire is ID: <strong>[/AdReference/]</strong>
	                    	</singleline>
                        </td>
                      </tr>
                    
                    </tbody></table></td>
                  </tr>
                </tbody></table>


                <!--Main Image Start-->
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                 
                  <tr>
                    <td valign="top" style="padding:20px 25px 30px 25px; border: 1px solid #263745; color: #263745; background-color: #fff">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">                     
                      <tr>
                        <td valign="top" class="textcontent on-white-bg" mc:edit="main-image-content" style="padding:10px 0px 20px 0px;">
                      		Skip the booking process by extending your ad. Click the link below to begin:
                         </td>
                      </tr>
                    
                      <tr>
                        <td align="center" valign="top">
                        <a href="[/LinkForExtension/]" target="_blank" class="btn btn-lg btn-primary">Extend Ad</a></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <!--Main Image End-->
                
                </td>
              </tr>
                
                </td>
              </tr>
             
            </table></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Content Body End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top" style="background-color : #b01e00">
        
        <!--Icon Bottom Start-->
       
        <!--Icon Bottom End-->
        
        </td>
      </tr>
    </table>
    <!--Content End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top" style="background-color : #263745">
    
    <!--Footer Start-->
    <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding:35px 0px;">
        
        <!--Address Contact Start-->
        <table width="300" border="0" align="left" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              
              <tr>
                <td valign="top" mc:edit="address-contact" class="textfooter" style="padding:15px 0px 17px 0px;"><singleline>TheMusic <br />
                  PH: 61 3 9421 4499<br />
                  <a href="#" class="headerlink">info@themusic.com.au</a></singleline></td>
              </tr>
              <tr>
                <td valign="top"><table width="100" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Address Contact End-->
        
        <!--Footer Link Start-->
        <table width="300" border="0" align="right" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td class="hidebox-footer" style="display:none;"></td>
          </tr>
          <tr>
            <td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="50%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="recommend" class="footerheading"><singleline>Classies</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="recommend-link" class="textfooter" style="padding-top:12px;">
                      
                      <singleline><a href="http://classies.themusic.com.au" target="_blank" class="headerlink">Classies Home</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Booking/Step/1" target="_blank" class="headerlink">Place New Ad</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Home/ContactUs" target="_blank" class="headerlink">Contact Us</a></singleline><br />
                  </tr>
                </table></td>
                <td width="50" valign="top" style="padding-left:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="newsletter" class="footerheading"><singleline>TheMusic</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="newsletter-link" class="textfooter" style="padding-top:12px;">
                      <singleline><a href="http://themusic.com.au" target="_blank" class="headerlink">Home</a></singleline><br />
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Footer Link End-->
        
        </td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Footer End-->
    
    </td>
  </tr>
</table>
<!--Table End-->

</body>
</html>
', 
Brand = 'TheMusic'
WHERE	DocType	= 'ExpirationReminder'

-- NewBooking
UPDATE	EmailTemplate
SET [BodyTemplate] = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>New Classies Ad</title>
<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
<link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans:400,400italic,600,600italic,700,700italic,800,800italic"/>
<style type="text/css">
body {
	background-color: #ffffff;
	margin: 0px;
	padding: 0px;
	text-align: center;
	width: 100%;
}
html { width: 100%; }
.contentbg
{
	 background-color: #ffffff;
}
img {
	border:0px;
	outline:none;
	text-decoration:none;
	display:block;
}
a img {
	border: none;
}
.textheader {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: right;
}
.textcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.on-white-bg{
  color: #263745;
}
.headingcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: left;
}
.headingmain {
	font-family: ''Open Sans'', sans-serif;
	font-size: 36px;
	font-weight: 600;
	color: #ffffff;
	text-align: center;
}
.subimagehilight {
	font-family: ''Open Sans'', sans-serif;
	font-size: 48px;
	color: #ffffff;
	text-align:left;
	text-decoration: none;
	line-height: 50px;
	font-weight: 600;
}
.iconheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: center;
}
.textfooter {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.footerheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 20px;
	font-weight: 400;
	color: #ffffff;
	line-height: 24px;
	text-align:left;
}
a {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a.headerlink:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.headerlink:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.bottomicontext:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
@media only screen and (max-width:640px)

{
	body{width:auto!important;background-color: #fff;}
	.main{width:446px !important;}
	.righttop-details{width:226px !important;}
	.contentbox{width:386px !important; display:block; padding:0px 25px !important;}
	.subcontentbox{width:386px !important;}
	.details-box{height:auto !important;}
	.content-space{width:386px !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:188px !important; height:188px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

@media only screen and (max-width:450px)
{
	body{width:auto!important; background-color:#fff;}
	.main{width:320px !important;}
	.logo{width:100% !important;}
	.righttop-details{width:100% !important;}
	.social-top{width:100% !important;}
	.viewonline-field{padding: 10px 0px 0px 0px !important;}
	#viewonline{text-align:center !important;}
	.contentbox{width:100% !important; display:block; padding:0px !important;}
	.subcontentbox{width:100% !important;}
	.details-box{height:auto !important;}
	.content-space{width:100% !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:150px !important; height:150px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

.btn, .btn:hover {
  display: inline-block;
  margin-bottom: 0;
  font-weight: normal;
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  -ms-touch-action: manipulation;
      touch-action: manipulation;
  cursor: pointer;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
  background-image: none;
  border: 1px solid transparent;
  
  padding: 10px 16px;
  font-size: 18px;
  line-height: 1.33;
  border-radius: 6px;
}
.btn-primary {
  color: #fff;
  background-color: #263745;
  border-color: #357ebd;
}
.btn-primary .badge {
  color: #428bca;
  background-color: #fff;
}

.btn-lg {
  
}

</style>

<!-- Internet Explorer fix -->
<!--[if IE]>
<style type="text/css">

@media only screen and (max-width:640px)
{
	.contentbox{width:386px !important; float:left; display:block; padding:0px 25px !important;}
    .subcontentbox{width:386px !important; float:left;}
    .content-space{width:386px !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

@media only screen and (max-width:450px)
{
	.contentbox{width:100% !important; float:left; display:block; padding:0px !important;}
    .subcontentbox{width:100% !important; float:left;}
    .content-space{width:100% !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

</style>
<![endif]-->
<!-- / Internet Explorer fix -->

</head>

<body>

<!--Table Start-->
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="contentbg">
  <tr>
    <td align="center" valign="top" style="background-color: #fff">
    
    <!--Header Start-->
    <table width="610" border="0" cellspacing="0" cellpadding="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding-bottom:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="top">
            
            <!--Logo Start-->
            <table width="250" border="0" align="left" cellpadding="0" cellspacing="0" class="logo">
              <tr>
                <td align="center" valign="top"><a href="#" target="_blank"><img editable="true" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" width="250" height="100" alt="logo" style="display:block;"/></a></td>
              </tr>
            </table>
            <!--Logo End-->
            

            <table width="200" border="0" align="right" cellpadding="0" cellspacing="0" class="righttop-details">
              <tr>
                <td align="right" style="padding-top:18px;">
                
                <!--Social Top Start-->
                <table width="100" border="0" cellspacing="0" cellpadding="0" class="social-top">
                  <tr>
                    <td align="center"><table width="100" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <!--Social Top End-->
                
                </td>
              </tr>
             
            </table>
            </td>
          </tr>
        </table></td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Header End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top">
    
    <!--Content Start-->
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td valign="top" style="background-color : #263745">
        
        <!--Heading Text Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" mc:edit="heading-text" class="headingmain" style="padding:18px 25px 25px 25px;"><singleline>Ad Booked!</singleline></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Heading Text End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top">
        

        <!--Content Body Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td valign="top">
                
<table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody><tr>
                    <td valign="top" style="padding:20px 25px 30px 25px;background-color: #b01e00"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tbody><tr>
                        <td valign="top" mc:edit="text-area-heading" class="headingcontent"><singleline>Ad was booked successfully</singleline></td>
                      </tr>
                      <tr>
                        <td valign="top" mc:edit="text-area-content" class="textcontent" style="padding:10px 0px 20px 0px;"><singleline>
                          Hi, <strong>[/username/]</strong> you have successfully booked your classified. Please check the details below!
                        </singleline>

                        </td>
                      </tr>
                    
                    </tbody></table></td>
                  </tr>
                </tbody></table>


                <!--Main Image Start-->
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                 
                  <tr>
                    <td valign="top" style="padding:20px 25px 30px 25px; border: 1px solid #eee; color: #263745; background-color: #fff">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                     
                      <tr>
                        <td valign="top" class="textcontent on-white-bg" mc:edit="main-image-content" style="padding:10px 0px 20px 0px;">
                          <table>
                            <tr><td>Ad ID</td><td>[/adid/]</td></tr>
                            <tr><td>Booking Price</td><td>[/price/]</td></tr>
                            <tr><td>Start Date</td><td>[/startdate/]</td></tr>
                            <tr><td>End Date</td><td>[/enddate/]</td></tr>
                          </table>
                         </td>
                      </tr>
                    
                      <!-- <tr>
                        <td align="center" valign="top">
                        <a href="http://classies.themusic.com.au/" target="_blank" class="btn btn-lg btn-primary">Go To Login</a></td>
                      </tr> -->
                    </table></td>
                  </tr>
                </table>

                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody>
                    <tr>
                      <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                          <td valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">                          
                              <tr>
                                <td valign="top" style="padding:20px 25px 30px 25px; border: 1px solid #eee" colspan="2">
                                  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td valign="top" mc:edit="maintitle" class="headingcontent on-white-bg">
                                        <span class="on-white-bg"> 
                                          [/adheading/] 
                                        </span>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td valign="top" mc:edit="maincontent" class="textcontent on-white-bg" style="padding:10px 0px 21px 0px;">
                                          [/addescription/]
                                      </td>
                                    </tr>
                                  </table>  
                                </td>
                              </tr>
                            </table>
                            <!-- End of Ad Details -->
                          </td>
                        </tr>

                      </tbody></table>
                        <table width="203" border="0" align="right" cellpadding="0" cellspacing="0" class="contentbox">
                          <tbody><tr>
                            <td class="maincontent-hidebox" style="display:none;"></td>
                          </tr>
                          <tr>
                            
                          </tr>
                        </tbody></table></td>
                    </tr>
                  </tbody>
                </table>

                <!--Main Image End-->
                
                </td>
              </tr>
                
                </td>
              </tr>
             
            </table></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Content Body End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top" style="background-color : #b01e00">
        
        <!--Icon Bottom Start-->
       
        <!--Icon Bottom End-->
        
        </td>
      </tr>
    </table>
    

    <!--Content End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top" style="background-color : #263745">
    
    <!--Footer Start-->
    <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding:35px 0px;">
        
        <!--Address Contact Start-->
        <table width="300" border="0" align="left" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              
              <tr>
                <td valign="top" mc:edit="address-contact" class="textfooter" style="padding:15px 0px 17px 0px;"><singleline>TheMusic <br />
                  PH: 61 3 9421 4499<br />
                  <a href="#" class="headerlink">info@themusic.com.au</a></singleline></td>
              </tr>
              <tr>
                <td valign="top"><table width="100" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Address Contact End-->
        
        <!--Footer Link Start-->
        <table width="300" border="0" align="right" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td class="hidebox-footer" style="display:none;"></td>
          </tr>
          <tr>
            <td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="50%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="recommend" class="footerheading"><singleline>Classies</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="recommend-link" class="textfooter" style="padding-top:12px;">
                      
                      <singleline><a href="http://classies.themusic.com.au" target="_blank" class="headerlink">Classies Home</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Booking/Step/1" target="_blank" class="headerlink">Place New Ad</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Home/ContactUs" target="_blank" class="headerlink">Contact Us</a></singleline><br />
                  </tr>
                </table></td>
                <td width="50" valign="top" style="padding-left:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="newsletter" class="footerheading"><singleline>TheMusic</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="newsletter-link" class="textfooter" style="padding-top:12px;">
                      <singleline><a href="http://themusic.com.au" target="_blank" class="headerlink">Home</a></singleline><br />
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Footer Link End-->
        
        </td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Footer End-->
    
    </td>
  </tr>
</table>
<!--Table End-->

</body>
</html>
', 
Brand = 'TheMusic'
WHERE	DocType	= 'NewBooking'

-- SupportRequest
UPDATE	EmailTemplate
SET [BodyTemplate] = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>New Classies Ad</title>
<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
<link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans:400,400italic,600,600italic,700,700italic,800,800italic"/>
<style type="text/css">
body {
	background-color: #ffffff;
	margin: 0px;
	padding: 0px;
	text-align: center;
	width: 100%;
}
html { width: 100%; }
.contentbg
{
	 background-color: #ffffff;
}
img {
	border:0px;
	outline:none;
	text-decoration:none;
	display:block;
}
a img {
	border: none;
}
.textheader {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: right;
}
.textcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.on-white-bg{
  color: #263745;
}
.headingcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: left;
}
.headingmain {
	font-family: ''Open Sans'', sans-serif;
	font-size: 36px;
	font-weight: 600;
	color: #ffffff;
	text-align: center;
}
.subimagehilight {
	font-family: ''Open Sans'', sans-serif;
	font-size: 48px;
	color: #ffffff;
	text-align:left;
	text-decoration: none;
	line-height: 50px;
	font-weight: 600;
}
.iconheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: center;
}
.textfooter {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.footerheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 20px;
	font-weight: 400;
	color: #ffffff;
	line-height: 24px;
	text-align:left;
}
a {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a.headerlink:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.headerlink:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.bottomicontext:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
@media only screen and (max-width:640px)

{
	body{width:auto!important;background-color: #fff;}
	.main{width:446px !important;}
	.righttop-details{width:226px !important;}
	.contentbox{width:386px !important; display:block; padding:0px 25px !important;}
	.subcontentbox{width:386px !important;}
	.details-box{height:auto !important;}
	.content-space{width:386px !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:188px !important; height:188px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

@media only screen and (max-width:450px)
{
	body{width:auto!important; background-color:#fff;}
	.main{width:320px !important;}
	.logo{width:100% !important;}
	.righttop-details{width:100% !important;}
	.social-top{width:100% !important;}
	.viewonline-field{padding: 10px 0px 0px 0px !important;}
	#viewonline{text-align:center !important;}
	.contentbox{width:100% !important; display:block; padding:0px !important;}
	.subcontentbox{width:100% !important;}
	.details-box{height:auto !important;}
	.content-space{width:100% !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:150px !important; height:150px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

.btn, .btn:hover {
  display: inline-block;
  margin-bottom: 0;
  font-weight: normal;
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  -ms-touch-action: manipulation;
      touch-action: manipulation;
  cursor: pointer;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
  background-image: none;
  border: 1px solid transparent;
  
  padding: 10px 16px;
  font-size: 18px;
  line-height: 1.33;
  border-radius: 6px;
}
.btn-primary {
  color: #fff;
  background-color: #263745;
  border-color: #357ebd;
}
.btn-primary .badge {
  color: #428bca;
  background-color: #fff;
}

.btn-lg {
  
}

</style>

<!-- Internet Explorer fix -->
<!--[if IE]>
<style type="text/css">

@media only screen and (max-width:640px)
{
	.contentbox{width:386px !important; float:left; display:block; padding:0px 25px !important;}
    .subcontentbox{width:386px !important; float:left;}
    .content-space{width:386px !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

@media only screen and (max-width:450px)
{
	.contentbox{width:100% !important; float:left; display:block; padding:0px !important;}
    .subcontentbox{width:100% !important; float:left;}
    .content-space{width:100% !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

</style>
<![endif]-->
<!-- / Internet Explorer fix -->

</head>

<body>

<!--Table Start-->
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="contentbg">
  <tr>
    <td align="center" valign="top" style="background-color: #fff">
    
    <!--Header Start-->
    <table width="610" border="0" cellspacing="0" cellpadding="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding-bottom:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="top">
            
            <!--Logo Start-->
            <table width="250" border="0" align="left" cellpadding="0" cellspacing="0" class="logo">
              <tr>
                <td align="center" valign="top"><a href="#" target="_blank"><img editable="true" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" width="250" height="100" alt="logo" style="display:block;"/></a></td>
              </tr>
            </table>
            <!--Logo End-->
            

            <table width="200" border="0" align="right" cellpadding="0" cellspacing="0" class="righttop-details">
              <tr>
                <td align="right" style="padding-top:18px;">
                
                <!--Social Top Start-->
                <table width="100" border="0" cellspacing="0" cellpadding="0" class="social-top">
                  <tr>
                    <td align="center"><table width="100" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <!--Social Top End-->
                
                </td>
              </tr>
             
            </table>
            </td>
          </tr>
        </table></td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Header End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top">
    
    <!--Content Start-->
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td valign="top" style="background-color : #263745">
        
        <!--Heading Text Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" mc:edit="heading-text" class="headingmain" style="padding:18px 25px 25px 25px;"><singleline>Support Requested</singleline></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Heading Text End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top">
        

        <!--Content Body Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td valign="top">
                
<table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody><tr>
                    <td valign="top" style="padding:20px 25px 30px 25px;background-color: #b01e00"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tbody><tr>
                        <td valign="top" mc:edit="text-area-heading" class="headingcontent"><singleline>A user has submitted a support request</singleline></td>
                      </tr>
                      <tr>
                        <td valign="top" mc:edit="text-area-content" class="textcontent" style="padding:10px 0px 20px 0px;"><singleline>
                          Hi, a user submitted the contact us form and the details are below. Please contact them back directly 
                          as soon as possible.
                        </singleline>

                        </td>
                      </tr>
                    
                    </tbody></table></td>
                  </tr>
                </tbody></table>


                <!--Main Image Start-->
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                 
                  <tr>
                    <td valign="top" style="padding:20px 25px 30px 25px; border: 1px solid #eee; color: #263745; background-color: #fff">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                     
                      <tr>
                        <td valign="top" class="textcontent on-white-bg" mc:edit="main-image-content" style="padding:10px 0px 20px 0px;">
                          <table>
                            <tr><td>Name</td><td><strong>[/name/]</strong></td></tr>
                            <tr><td>Email</td><td><strong>[/email/]</strong></td></tr>
                            <tr><td>Phone</td><td><strong>[/phone/]</strong></td></tr>
                          </table>
                         </td>
                      </tr>
                    
                      <!-- <tr>
                        <td align="center" valign="top">
                        <a href="http://classies.themusic.com.au/" target="_blank" class="btn btn-lg btn-primary">Go To Login</a></td>
                      </tr> -->
                    </table></td>
                  </tr>
                </table>

                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody>
                    <tr>
                      <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                          <td valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">                          
                              <tr>
                                <td valign="top" style="padding:20px 25px 30px 25px; border: 1px solid #eee" colspan="2">
                                  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td valign="top" mc:edit="maintitle" class="headingcontent on-white-bg">
                                        <span class="on-white-bg"> 
                                          Comments:
                                        </span>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td valign="top" mc:edit="maincontent" class="textcontent on-white-bg" style="padding:10px 0px 21px 0px;">
                                          [/content/]
                                      </td>
                                    </tr>
                                  </table>  
                                </td>
                              </tr>
                            </table>
                            <!-- End of Ad Details -->
                          </td>
                        </tr>

                      </tbody></table>
                        <table width="203" border="0" align="right" cellpadding="0" cellspacing="0" class="contentbox">
                          <tbody><tr>
                            <td class="maincontent-hidebox" style="display:none;"></td>
                          </tr>
                          <tr>
                            
                          </tr>
                        </tbody></table></td>
                    </tr>
                  </tbody>
                </table>

                <!--Main Image End-->
                
                </td>
              </tr>
                
                </td>
              </tr>
             
            </table></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Content Body End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top" style="background-color : #b01e00">
        
        <!--Icon Bottom Start-->
       
        <!--Icon Bottom End-->
        
        </td>
      </tr>
    </table>
    

    <!--Content End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top" style="background-color : #263745">
    
    <!--Footer Start-->
    <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding:35px 0px;">
        
        <!--Address Contact Start-->
        <table width="300" border="0" align="left" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              
              <tr>
                <td valign="top" mc:edit="address-contact" class="textfooter" style="padding:15px 0px 17px 0px;"><singleline>TheMusic <br />
                  PH: 61 3 9421 4499<br />
                  <a href="#" class="headerlink">info@themusic.com.au</a></singleline></td>
              </tr>
              <tr>
                <td valign="top"><table width="100" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Address Contact End-->
        
        <!--Footer Link Start-->
        <table width="300" border="0" align="right" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td class="hidebox-footer" style="display:none;"></td>
          </tr>
          <tr>
            <td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="50%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="recommend" class="footerheading"><singleline>Classies</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="recommend-link" class="textfooter" style="padding-top:12px;">
                      
                      <singleline><a href="http://classies.themusic.com.au" target="_blank" class="headerlink">Classies Home</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Booking/Step/1" target="_blank" class="headerlink">Place New Ad</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Home/ContactUs" target="_blank" class="headerlink">Contact Us</a></singleline><br />
                  </tr>
                </table></td>
                <td width="50" valign="top" style="padding-left:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="newsletter" class="footerheading"><singleline>TheMusic</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="newsletter-link" class="textfooter" style="padding-top:12px;">
                      <singleline><a href="http://themusic.com.au" target="_blank" class="headerlink">Home</a></singleline><br />
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Footer Link End-->
        
        </td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Footer End-->
    
    </td>
  </tr>
</table>
<!--Table End-->

</body>
</html>
', 
Brand = 'TheMusic'
WHERE	DocType	= 'SupportRequest'

-- SupportRequest
UPDATE	EmailTemplate
SET [BodyTemplate] = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>New Classies Ad</title>
<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
<link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans:400,400italic,600,600italic,700,700italic,800,800italic"/>
<style type="text/css">
body {
	background-color: #ffffff;
	margin: 0px;
	padding: 0px;
	text-align: center;
	width: 100%;
}
html { width: 100%; }
.contentbg
{
	 background-color: #ffffff;
}
img {
	border:0px;
	outline:none;
	text-decoration:none;
	display:block;
}
a img {
	border: none;
}
.textheader {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: right;
}
.textcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.on-white-bg{
  color: #263745;
}
.headingcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: left;
}
.headingmain {
	font-family: ''Open Sans'', sans-serif;
	font-size: 36px;
	font-weight: 600;
	color: #ffffff;
	text-align: center;
}
.subimagehilight {
	font-family: ''Open Sans'', sans-serif;
	font-size: 48px;
	color: #ffffff;
	text-align:left;
	text-decoration: none;
	line-height: 50px;
	font-weight: 600;
}
.iconheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: center;
}
.textfooter {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.footerheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 20px;
	font-weight: 400;
	color: #ffffff;
	line-height: 24px;
	text-align:left;
}
a {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a.headerlink:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.headerlink:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.bottomicontext:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
@media only screen and (max-width:640px)

{
	body{width:auto!important;background-color: #fff;}
	.main{width:446px !important;}
	.righttop-details{width:226px !important;}
	.contentbox{width:386px !important; display:block; padding:0px 25px !important;}
	.subcontentbox{width:386px !important;}
	.details-box{height:auto !important;}
	.content-space{width:386px !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:188px !important; height:188px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

@media only screen and (max-width:450px)
{
	body{width:auto!important; background-color:#fff;}
	.main{width:320px !important;}
	.logo{width:100% !important;}
	.righttop-details{width:100% !important;}
	.social-top{width:100% !important;}
	.viewonline-field{padding: 10px 0px 0px 0px !important;}
	#viewonline{text-align:center !important;}
	.contentbox{width:100% !important; display:block; padding:0px !important;}
	.subcontentbox{width:100% !important;}
	.details-box{height:auto !important;}
	.content-space{width:100% !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:150px !important; height:150px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

.btn, .btn:hover {
  display: inline-block;
  margin-bottom: 0;
  font-weight: normal;
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  -ms-touch-action: manipulation;
      touch-action: manipulation;
  cursor: pointer;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
  background-image: none;
  border: 1px solid transparent;
  
  padding: 10px 16px;
  font-size: 18px;
  line-height: 1.33;
  border-radius: 6px;
}
.btn-primary {
  color: #fff;
  background-color: #263745;
  border-color: #357ebd;
}
.btn-primary .badge {
  color: #428bca;
  background-color: #fff;
}

.btn-lg {
  
}

</style>

<!-- Internet Explorer fix -->
<!--[if IE]>
<style type="text/css">

@media only screen and (max-width:640px)
{
	.contentbox{width:386px !important; float:left; display:block; padding:0px 25px !important;}
    .subcontentbox{width:386px !important; float:left;}
    .content-space{width:386px !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

@media only screen and (max-width:450px)
{
	.contentbox{width:100% !important; float:left; display:block; padding:0px !important;}
    .subcontentbox{width:100% !important; float:left;}
    .content-space{width:100% !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

</style>
<![endif]-->
<!-- / Internet Explorer fix -->

</head>

<body>

<!--Table Start-->
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="contentbg">
  <tr>
    <td align="center" valign="top" style="background-color: #fff">
    
    <!--Header Start-->
    <table width="610" border="0" cellspacing="0" cellpadding="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding-bottom:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="top">
            
            <!--Logo Start-->
            <table width="250" border="0" align="left" cellpadding="0" cellspacing="0" class="logo">
              <tr>
                <td align="center" valign="top"><a href="#" target="_blank"><img editable="true" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" width="250" height="100" alt="logo" style="display:block;"/></a></td>
              </tr>
            </table>
            <!--Logo End-->
            

            <table width="200" border="0" align="right" cellpadding="0" cellspacing="0" class="righttop-details">
              <tr>
                <td align="right" style="padding-top:18px;">
                
                <!--Social Top Start-->
                <table width="100" border="0" cellspacing="0" cellpadding="0" class="social-top">
                  <tr>
                    <td align="center"><table width="100" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <!--Social Top End-->
                
                </td>
              </tr>
             
            </table>
            </td>
          </tr>
        </table></td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Header End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top">
    
    <!--Content Start-->
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td valign="top" style="background-color : #263745">
        
        <!--Heading Text Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" mc:edit="heading-text" class="headingmain" style="padding:18px 25px 25px 25px;"><singleline>Support Requested</singleline></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Heading Text End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top">
        

        <!--Content Body Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td valign="top">
                
<table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody><tr>
                    <td valign="top" style="padding:20px 25px 30px 25px;background-color: #b01e00"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tbody><tr>
                        <td valign="top" mc:edit="text-area-heading" class="headingcontent"><singleline>A user has submitted a support request</singleline></td>
                      </tr>
                      <tr>
                        <td valign="top" mc:edit="text-area-content" class="textcontent" style="padding:10px 0px 20px 0px;"><singleline>
                          Hi, a user submitted the contact us form and the details are below. Please contact them back directly 
                          as soon as possible.
                        </singleline>

                        </td>
                      </tr>
                    
                    </tbody></table></td>
                  </tr>
                </tbody></table>


                <!--Main Image Start-->
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                 
                  <tr>
                    <td valign="top" style="padding:20px 25px 30px 25px; border: 1px solid #eee; color: #263745; background-color: #fff">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                     
                      <tr>
                        <td valign="top" class="textcontent on-white-bg" mc:edit="main-image-content" style="padding:10px 0px 20px 0px;">
                          <table>
                            <tr><td>Name</td><td><strong>[/name/]</strong></td></tr>
                            <tr><td>Email</td><td><strong>[/email/]</strong></td></tr>
                            <tr><td>Phone</td><td><strong>[/phone/]</strong></td></tr>
                          </table>
                         </td>
                      </tr>
                    
                      <!-- <tr>
                        <td align="center" valign="top">
                        <a href="http://classies.themusic.com.au/" target="_blank" class="btn btn-lg btn-primary">Go To Login</a></td>
                      </tr> -->
                    </table></td>
                  </tr>
                </table>

                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody>
                    <tr>
                      <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                          <td valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">                          
                              <tr>
                                <td valign="top" style="padding:20px 25px 30px 25px; border: 1px solid #eee" colspan="2">
                                  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td valign="top" mc:edit="maintitle" class="headingcontent on-white-bg">
                                        <span class="on-white-bg"> 
                                          Comments:
                                        </span>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td valign="top" mc:edit="maincontent" class="textcontent on-white-bg" style="padding:10px 0px 21px 0px;">
                                          [/content/]
                                      </td>
                                    </tr>
                                  </table>  
                                </td>
                              </tr>
                            </table>
                            <!-- End of Ad Details -->
                          </td>
                        </tr>

                      </tbody></table>
                        <table width="203" border="0" align="right" cellpadding="0" cellspacing="0" class="contentbox">
                          <tbody><tr>
                            <td class="maincontent-hidebox" style="display:none;"></td>
                          </tr>
                          <tr>
                            
                          </tr>
                        </tbody></table></td>
                    </tr>
                  </tbody>
                </table>

                <!--Main Image End-->
                
                </td>
              </tr>
                
                </td>
              </tr>
             
            </table></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Content Body End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top" style="background-color : #b01e00">
        
        <!--Icon Bottom Start-->
       
        <!--Icon Bottom End-->
        
        </td>
      </tr>
    </table>
    

    <!--Content End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top" style="background-color : #263745">
    
    <!--Footer Start-->
    <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding:35px 0px;">
        
        <!--Address Contact Start-->
        <table width="300" border="0" align="left" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              
              <tr>
                <td valign="top" mc:edit="address-contact" class="textfooter" style="padding:15px 0px 17px 0px;"><singleline>TheMusic <br />
                  PH: 61 3 9421 4499<br />
                  <a href="#" class="headerlink">info@themusic.com.au</a></singleline></td>
              </tr>
              <tr>
                <td valign="top"><table width="100" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Address Contact End-->
        
        <!--Footer Link Start-->
        <table width="300" border="0" align="right" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td class="hidebox-footer" style="display:none;"></td>
          </tr>
          <tr>
            <td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="50%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="recommend" class="footerheading"><singleline>Classies</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="recommend-link" class="textfooter" style="padding-top:12px;">
                      
                      <singleline><a href="http://classies.themusic.com.au" target="_blank" class="headerlink">Classies Home</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Booking/Step/1" target="_blank" class="headerlink">Place New Ad</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Home/ContactUs" target="_blank" class="headerlink">Contact Us</a></singleline><br />
                  </tr>
                </table></td>
                <td width="50" valign="top" style="padding-left:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="newsletter" class="footerheading"><singleline>TheMusic</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="newsletter-link" class="textfooter" style="padding-top:12px;">
                      <singleline><a href="http://themusic.com.au" target="_blank" class="headerlink">Home</a></singleline><br />
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Footer Link End-->
        
        </td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Footer End-->
    
    </td>
  </tr>
</table>
<!--Table End-->

</body>
</html>
', 
Brand = 'TheMusic'
WHERE	DocType	= 'SupportRequest'

-- AdEnquiry
UPDATE	EmailTemplate
SET [BodyTemplate] = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>New Classies Ad</title>
<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
<link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans:400,400italic,600,600italic,700,700italic,800,800italic"/>
<style type="text/css">
body {
	background-color: #ffffff;
	margin: 0px;
	padding: 0px;
	text-align: center;
	width: 100%;
}
html { width: 100%; }
.contentbg
{
	 background-color: #ffffff;
}
img {
	border:0px;
	outline:none;
	text-decoration:none;
	display:block;
}
a img {
	border: none;
}
.textheader {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: right;
}
.textcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.on-white-bg{
  color: #263745;
}
.headingcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: left;
}
.headingmain {
	font-family: ''Open Sans'', sans-serif;
	font-size: 36px;
	font-weight: 600;
	color: #ffffff;
	text-align: center;
}
.subimagehilight {
	font-family: ''Open Sans'', sans-serif;
	font-size: 48px;
	color: #ffffff;
	text-align:left;
	text-decoration: none;
	line-height: 50px;
	font-weight: 600;
}
.iconheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: center;
}
.textfooter {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.footerheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 20px;
	font-weight: 400;
	color: #ffffff;
	line-height: 24px;
	text-align:left;
}
a {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a.headerlink:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.headerlink:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.bottomicontext:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
@media only screen and (max-width:640px)

{
	body{width:auto!important;background-color: #fff;}
	.main{width:446px !important;}
	.righttop-details{width:226px !important;}
	.contentbox{width:386px !important; display:block; padding:0px 25px !important;}
	.subcontentbox{width:386px !important;}
	.details-box{height:auto !important;}
	.content-space{width:386px !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:188px !important; height:188px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

@media only screen and (max-width:450px)
{
	body{width:auto!important; background-color:#fff;}
	.main{width:320px !important;}
	.logo{width:100% !important;}
	.righttop-details{width:100% !important;}
	.social-top{width:100% !important;}
	.viewonline-field{padding: 10px 0px 0px 0px !important;}
	#viewonline{text-align:center !important;}
	.contentbox{width:100% !important; display:block; padding:0px !important;}
	.subcontentbox{width:100% !important;}
	.details-box{height:auto !important;}
	.content-space{width:100% !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:150px !important; height:150px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

.btn, .btn:hover {
  display: inline-block;
  margin-bottom: 0;
  font-weight: normal;
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  -ms-touch-action: manipulation;
      touch-action: manipulation;
  cursor: pointer;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
  background-image: none;
  border: 1px solid transparent;
  
  padding: 10px 16px;
  font-size: 18px;
  line-height: 1.33;
  border-radius: 6px;
}
.btn-primary {
  color: #fff;
  background-color: #263745;
  border-color: #357ebd;
}
.btn-primary .badge {
  color: #428bca;
  background-color: #fff;
}

.btn-lg {
  
}

</style>

<!-- Internet Explorer fix -->
<!--[if IE]>
<style type="text/css">

@media only screen and (max-width:640px)
{
	.contentbox{width:386px !important; float:left; display:block; padding:0px 25px !important;}
    .subcontentbox{width:386px !important; float:left;}
    .content-space{width:386px !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

@media only screen and (max-width:450px)
{
	.contentbox{width:100% !important; float:left; display:block; padding:0px !important;}
    .subcontentbox{width:100% !important; float:left;}
    .content-space{width:100% !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

</style>
<![endif]-->
<!-- / Internet Explorer fix -->

</head>

<body>

<!--Table Start-->
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="contentbg">
  <tr>
    <td align="center" valign="top" style="background-color: #fff">
    
    <!--Header Start-->
    <table width="610" border="0" cellspacing="0" cellpadding="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding-bottom:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="top">
            
            <!--Logo Start-->
            <table width="250" border="0" align="left" cellpadding="0" cellspacing="0" class="logo">
              <tr>
                <td align="center" valign="top"><a href="#" target="_blank"><img editable="true" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" width="250" height="100" alt="logo" style="display:block;"/></a></td>
              </tr>
            </table>
            <!--Logo End-->
            

            <table width="200" border="0" align="right" cellpadding="0" cellspacing="0" class="righttop-details">
              <tr>
                <td align="right" style="padding-top:18px;">
                
                <!--Social Top Start-->
                <table width="100" border="0" cellspacing="0" cellpadding="0" class="social-top">
                  <tr>
                    <td align="center"><table width="100" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <!--Social Top End-->
                
                </td>
              </tr>
             
            </table>
            </td>
          </tr>
        </table></td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Header End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top">
    
    <!--Content Start-->
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td valign="top" style="background-color : #263745">
        
        <!--Heading Text Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" mc:edit="heading-text" class="headingmain" style="padding:18px 25px 25px 25px;"><singleline>Ad Enquiry</singleline></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Heading Text End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top">
        

        <!--Content Body Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td valign="top">
                
<table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody><tr>
                    <td valign="top" style="padding:20px 25px 30px 25px;background-color: #b01e00"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tbody><tr>
                        <td valign="top" mc:edit="text-area-heading" class="headingcontent"><singleline>Your ad is getting attention!</singleline></td>
                      </tr>
                      <tr>
                        <td valign="top" mc:edit="text-area-content" class="textcontent" style="padding:10px 0px 20px 0px;"><singleline>
                          Hi, a user submitted the contact advertiser form and they need some help. Please contact them back as soon as possible.
                        </singleline>

                        </td>
                      </tr>
                    
                    </tbody></table></td>
                  </tr>
                </tbody></table>


                <!--Main Image Start-->
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                 
                  <tr>
                    <td valign="top" style="padding:20px 25px 30px 25px; border: 1px solid #eee; color: #263745; background-color: #fff">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                     
                      <tr>
                        <td valign="top" class="textcontent on-white-bg" mc:edit="main-image-content" style="padding:10px 0px 20px 0px;">
                          <table>
                            <tr><td>Name</td><td><strong>[/name/]</strong></td></tr>
                            <tr><td>Email</td><td><strong>[/email/]</strong></td></tr>
                          </table>
                          <p>Go to our classifieds website to see the full details and comments of their request.</p>
                         </td>
                      </tr>
                      <tr>
                        <td align="center" valign="top">                        
                        <a href="http://classies.themusic.com.au" target="_blank" class="btn btn-lg btn-primary">Go to our Website</a></td>
                      </tr>                     
                    </table></td>
                  </tr>
                </table>

                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody>
                    <tr>
                      <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                        <tr>
                          <td valign="top">
                           
                            <!-- End of Ad Details -->
                          </td>
                        </tr>

                      </tbody></table>
                        <table width="203" border="0" align="right" cellpadding="0" cellspacing="0" class="contentbox">
                          <tbody><tr>
                            <td class="maincontent-hidebox" style="display:none;"></td>
                          </tr>
                          <tr>
                            
                          </tr>
                        </tbody></table></td>
                    </tr>
                  </tbody>
                </table>

                <!--Main Image End-->
                
                </td>
              </tr>
                
                </td>
              </tr>
             
            </table></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Content Body End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top" style="background-color : #b01e00">
        
        <!--Icon Bottom Start-->
       
        <!--Icon Bottom End-->
        
        </td>
      </tr>
    </table>
    

    <!--Content End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top" style="background-color : #263745">
    
    <!--Footer Start-->
    <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding:35px 0px;">
        
        <!--Address Contact Start-->
        <table width="300" border="0" align="left" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              
              <tr>
                <td valign="top" mc:edit="address-contact" class="textfooter" style="padding:15px 0px 17px 0px;"><singleline>TheMusic <br />
                  PH: 61 3 9421 4499<br />
                  <a href="#" class="headerlink">info@themusic.com.au</a></singleline></td>
              </tr>
              <tr>
                <td valign="top"><table width="100" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Address Contact End-->
        
        <!--Footer Link Start-->
        <table width="300" border="0" align="right" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td class="hidebox-footer" style="display:none;"></td>
          </tr>
          <tr>
            <td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="50%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="recommend" class="footerheading"><singleline>Classies</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="recommend-link" class="textfooter" style="padding-top:12px;">
                      
                      <singleline><a href="http://classies.themusic.com.au" target="_blank" class="headerlink">Classies Home</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Booking/Step/1" target="_blank" class="headerlink">Place New Ad</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Home/ContactUs" target="_blank" class="headerlink">Contact Us</a></singleline><br />
                  </tr>
                </table></td>
                <td width="50" valign="top" style="padding-left:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="newsletter" class="footerheading"><singleline>TheMusic</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="newsletter-link" class="textfooter" style="padding-top:12px;">
                      <singleline><a href="http://themusic.com.au" target="_blank" class="headerlink">Home</a></singleline><br />
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Footer Link End-->
        
        </td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Footer End-->
    
    </td>
  </tr>
</table>
<!--Table End-->

</body>
</html>
', 
Brand = 'TheMusic'
WHERE	DocType	= 'AdEnquiry'

-- ActivityReport
UPDATE  EmailTemplate
SET		Brand = 'TheMusic'
WHERE	DocType = 'ActivityReport'

-- AdShare
UPDATE	EmailTemplate
SET		Brand = 'TheMusic',
		SubjectTemplate = '[/AdTitle/] - by [/AdvertiserName/]',
		BodyTemplate = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>New Classies Ad</title>
<meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;" />
<link rel="stylesheet" type="text/css" href="http://fonts.googleapis.com/css?family=Open+Sans:400,400italic,600,600italic,700,700italic,800,800italic"/>
<style type="text/css">
body {
	background-color: #ffffff;
	margin: 0px;
	padding: 0px;
	text-align: center;
	width: 100%;
}
html { width: 100%; }
.contentbg
{
	 background-color: #ffffff;
}
img {
	border:0px;
	outline:none;
	text-decoration:none;
	display:block;
}
a img {
	border: none;
}
.textheader {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: right;
}
.textcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.on-white-bg{
  color: #263745;
}
.headingcontent {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: left;
}
.headingmain {
	font-family: ''Open Sans'', sans-serif;
	font-size: 36px;
	font-weight: 600;
	color: #ffffff;
	text-align: center;
}
.subimagehilight {
	font-family: ''Open Sans'', sans-serif;
	font-size: 48px;
	color: #ffffff;
	text-align:left;
	text-decoration: none;
	line-height: 50px;
	font-weight: 600;
}
.iconheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 18px;
	font-weight: 600;
	color: #ffffff;
	line-height: 22px;
	text-align: center;
}
.textfooter {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-align: left;
}
.footerheading {
	font-family: ''Open Sans'', sans-serif;
	font-size: 20px;
	font-weight: 400;
	color: #ffffff;
	line-height: 24px;
	text-align:left;
}
a {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:none;
}
a:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	text-decoration:underline;
}
a.headerlink:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.headerlink:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: underline;
}
a.headerlink:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 13px;
	font-weight: 400;
	color: #ffffff;
	line-height: 18px;
	text-decoration: none;
}
a.bottomicontext:link {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:hover {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:active {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
a.bottomicontext:visited {
	font-family: ''Open Sans'', sans-serif;
	font-size: 24px;
	font-weight: 400;
	color: #ffffff;
	text-decoration: none;
}
@media only screen and (max-width:640px)

{
	body{width:auto!important;background-color: #fff;}
	.main{width:446px !important;}
	.righttop-details{width:226px !important;}
	.contentbox{width:386px !important; display:block; padding:0px 25px !important;}
	.subcontentbox{width:386px !important;}
	.details-box{height:auto !important;}
	.content-space{width:386px !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:188px !important; height:188px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

@media only screen and (max-width:450px)
{
	body{width:auto!important; background-color:#fff;}
	.main{width:320px !important;}
	.logo{width:100% !important;}
	.righttop-details{width:100% !important;}
	.social-top{width:100% !important;}
	.viewonline-field{padding: 10px 0px 0px 0px !important;}
	#viewonline{text-align:center !important;}
	.contentbox{width:100% !important; display:block; padding:0px !important;}
	.subcontentbox{width:100% !important;}
	.details-box{height:auto !important;}
	.content-space{width:100% !important; height:20px !important; display:block; padding:0px 25px !important;}
	.iconbox{width:150px !important; height:150px !important;}
	.icon-bottom{width:100% !important;}
	.hidebox{height:15px !important; display:block !important;}
	.hidebox-footer{height:23px !important; display:block !important;}
	.footerbox{width:100% !important;}
}

.btn, .btn:hover {
  display: inline-block;
  margin-bottom: 0;
  font-weight: normal;
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  -ms-touch-action: manipulation;
      touch-action: manipulation;
  cursor: pointer;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
  background-image: none;
  border: 1px solid transparent;
  
  padding: 10px 16px;
  font-size: 18px;
  line-height: 1.33;
  border-radius: 6px;
}
.btn-primary {
  color: #fff;
  background-color: #263745;
  border-color: #357ebd;
}
.btn-primary .badge {
  color: #428bca;
  background-color: #fff;
}

.btn-lg {
  
}

</style>

<!-- Internet Explorer fix -->
<!--[if IE]>
<style type="text/css">

@media only screen and (max-width:640px)
{
	.contentbox{width:386px !important; float:left; display:block; padding:0px 25px !important;}
    .subcontentbox{width:386px !important; float:left;}
    .content-space{width:386px !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

@media only screen and (max-width:450px)
{
	.contentbox{width:100% !important; float:left; display:block; padding:0px !important;}
    .subcontentbox{width:100% !important; float:left;}
    .content-space{width:100% !important; height:20px !important; float:left; display:block; padding:0px 25px !important;}
}

</style>
<![endif]-->
<!-- / Internet Explorer fix -->

</head>

<body>

<!--Table Start-->
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="contentbg">
  <tr>
    <td align="center" valign="top" style="background-color: #fff">
    
    <!--Header Start-->
    <table width="610" border="0" cellspacing="0" cellpadding="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding-bottom:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="top">
            
            <!--Logo Start-->
            <table width="250" border="0" align="left" cellpadding="0" cellspacing="0" class="logo">
              <tr>
                <td align="center" valign="top"><a href="#" target="_blank"><img editable="true" src="http://dc53ba3rukcsx.cloudfront.net/images/tmlogo.png" width="250" height="100" alt="logo" style="display:block;"/></a></td>
              </tr>
            </table>
            <!--Logo End-->
            

            <table width="200" border="0" align="right" cellpadding="0" cellspacing="0" class="righttop-details">
              <tr>
                <td align="right" style="padding-top:18px;">
                
                <!--Social Top Start-->
                <table width="100" border="0" cellspacing="0" cellpadding="0" class="social-top">
                  <tr>
                    <td align="center"><table width="100" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                        <td width="5">&nbsp;</td>
                        <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
                <!--Social Top End-->
                
                </td>
              </tr>
             
            </table>
            </td>
          </tr>
        </table></td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Header End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top">
    
    <!--Content Start-->
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td valign="top" style="background-color : #263745">
        
        <!--Heading Text Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" mc:edit="heading-text" class="headingmain" style="padding:18px 25px 25px 25px;"><singleline>[/AdTitle/]</singleline></td>
            <td width="5">&nbsp;</td>
          </tr>
        </table>
        <!--Heading Text End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top">
        

        <!--Content Body Start-->
        <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
          <tr>
            <td width="5">&nbsp;</td>
            <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td valign="top">
                
<table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody><tr>
                    <td valign="top" style="padding:20px 25px 30px 25px;background-color: #b01e00"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tbody><tr>
                        <td valign="top" mc:edit="text-area-heading" class="headingcontent"><singleline>Your friend [/AdvertiserName/] has posted the ad below at TheMusic website. They requested us to let you know.</singleline></td>
                      </tr>
                      
                    </tbody></table></td>
                  </tr>
                </tbody></table>


                <!--Main Image Start-->
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                 
                  <tr>
                    <td valign="top" style="padding:20px 25px 30px 25px; border: 1px solid #eee; color: #263745; background-color: #fff">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td valign="top" mc:edit="maintitle" class="headingcontent on-white-bg">
                                        <span class="on-white-bg"> 
                                          [/AdTitle/] 
                                        </span>
                                      </td>
                                    </tr>
                                    <tr>
                                      <td valign="top" mc:edit="maincontent" class="textcontent on-white-bg" style="padding:10px 0px 21px 0px;">
                                          [/AdDescription/]
                                      </td>
                                    </tr>
                                  </table>  </td>
                  </tr>
                </table>

                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tbody>
                    <tr>
                      <td valign="top" style="padding:20px 0px;"><table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                        

                      </tbody></table>
                        </td>
                    </tr>
                  </tbody>
                </table>

                <!--Main Image End-->
                
                </td>
              </tr>
                
                </td>
              </tr>
             
            </table></td>
           
          </tr>
        </table>
        <!--Content Body End-->
        
        </td>
      </tr>
      <tr>
        <td valign="top" style="background-color : #b01e00">
        
        <!--Icon Bottom Start-->
       
        <!--Icon Bottom End-->
        
        </td>
      </tr>
    </table>
    

    <!--Content End-->
    
    </td>
  </tr>
  <tr>
    <td valign="top" style="background-color : #263745">
    
    <!--Footer Start-->
    <table width="610" border="0" align="center" cellpadding="0" cellspacing="0" class="main">
      <tr>
        <td width="5">&nbsp;</td>
        <td valign="top" style="padding:35px 0px;">
        
        <!--Address Contact Start-->
        <table width="300" border="0" align="left" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              
              <tr>
                <td valign="top" mc:edit="address-contact" class="textfooter" style="padding:15px 0px 17px 0px;"><singleline>TheMusic <br />
                  PH: 61 3 9421 4499<br />
                  <a href="#" class="headerlink">info@themusic.com.au</a></singleline></td>
              </tr>
              <tr>
                <td valign="top"><table width="100" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td><a href="https://twitter.com/TheMusicComAu" target="_blank"><img editable="true" mc:edit="social-2" src="http://classies.themusic.com.au:8787/twitter-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td><a href="https://www.facebook.com/themusic.com.au" target="_blank"><img editable="true" mc:edit="social-1" src="http://classies.themusic.com.au:8787/facebook-icon.png" width="36" height="36" alt="social icon" style="display:block;"/></a></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                    <td width="5">&nbsp;</td>
                    <td></td>
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Address Contact End-->
        
        <!--Footer Link Start-->
        <table width="300" border="0" align="right" cellpadding="0" cellspacing="0" class="footerbox">
          <tr>
            <td class="hidebox-footer" style="display:none;"></td>
          </tr>
          <tr>
            <td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="50%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="recommend" class="footerheading"><singleline>Classies</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="recommend-link" class="textfooter" style="padding-top:12px;">
                      
                      <singleline><a href="http://classies.themusic.com.au" target="_blank" class="headerlink">Classies Home</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Booking/Step/1" target="_blank" class="headerlink">Place New Ad</a></singleline><br />
                      <singleline><a href="http://classies.themusic.com.au/Home/ContactUs" target="_blank" class="headerlink">Contact Us</a></singleline><br />
                  </tr>
                </table></td>
                <td width="50" valign="top" style="padding-left:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td valign="top" mc:edit="newsletter" class="footerheading"><singleline>TheMusic</singleline></td>
                  </tr>
                  <tr>
                    <td valign="top" mc:edit="newsletter-link" class="textfooter" style="padding-top:12px;">
                      <singleline><a href="http://themusic.com.au" target="_blank" class="headerlink">Home</a></singleline><br />
                  </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
        <!--Footer Link End-->
        
        </td>
        <td width="5">&nbsp;</td>
      </tr>
    </table>
    <!--Footer End-->
    
    </td>
  </tr>
</table>
<!--Table End-->

</body>
</html>' 
WHERE	DocType = 'AdShare'
