<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterWithRightBar.Master"
    CodeBehind="Contact.aspx.vb" Inherits="BetterclassifiedsWeb.Contact" Title="Contact Us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .style1
        {
            width: 90px;
            font-size: 0.8em;
        }
        .style2
        {
            width: 263px;
            font-size: 0.8em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="mainInfo">
        <div id="mainHeaderInfo">
            <h2>
                Contact Us</h2>
        </div>
        <div id="mainContentInfo">
            <div style="font-size: 0.9em">
                <asp:Label ID="lblSubmit" runat="server" Font-Bold="true" ForeColor="Green" 
                    Text="Your enquiry has been submitted successfully." Visible="false" />
                <p>
                    <asp:Label ID="lblNew" runat="server" Font-Bold="true" Text="NEW!" />&nbsp; Have
                    a suggestion/feedback or simply require support for our website? Use the following
                    support form to voice your concerns and the friendly team will get back
                    to you as soon as possible!
                </p>
                <table cellpadding="5">
                    <tr>
                        <td>
                            Enquiry Type*:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEnquiryType" runat="server">
                                <asp:ListItem Value="SupportGeneralEnquiry" Text="General Enquiry" />
                                <asp:ListItem Value="SupportTechnical" Text="Support" />
                                <asp:ListItem Value="SupportSalesBilling" Text="Sales/Billing" />
                                <asp:ListItem Value="SupportFeedback" Text="Feedback" />
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Full Name*:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="200px" MaxLength="100">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="valNameRequired" runat="server" ControlToValidate="txtFirstName"
                                Text="*" EnableClientScript="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email*:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="200px" MaxLength="100">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="valEmailRegularExpression" runat="server" Text="*"
                                ControlToValidate="txtEmail" ValidationExpression=".*@.*\..*" EnableClientScript="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Phone:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtPhone" runat="server" Width="200px" MaxLength="15" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Subject:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtSubject" runat="server" Width="200px" MaxLength="100">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Comments*:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="200px"
                                Rows="5">
                            </telerik:RadTextBox>
                        </td>
                        <td valign="top">
                            <asp:RequiredFieldValidator ID="valCommentsRequired" runat="server" ControlToValidate="txtComments"
                                Text="*" EnableClientScript="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <telerik:RadCaptcha ID="RadCaptcha1" runat="server" 
                                ErrorMessage="The code you entered is not valid."
                                Display="Dynamic">
                            </telerik:RadCaptcha>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CausesValidation="true" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <p>
                    <asp:Label ID="lblNote" runat="server" Font-Bold="true" Text="Note: For any support enquiries, please provide as much detail as possible 
                     including username, browser and version, error message, booking id, Ad ID." /></p>
            </div>
            <div style="margin-top: 15px">
                <p>
                    <b>Classies are operated under licence by Dharma Media Pty Ltd (ABN: 54 117 132 402). Dharma
                        Media Pty Ltd is part of the StreetPress Australia Pty Ltd group of companies.</b>
                    <table style="width: 359px">
                        <tr>
                            <td class="style1">
                                Address:
                            </td>
                            <td class="style2">
                                Locked Bag 2001, Clifton Hill VIC 3068
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Telephone:
                            </td>
                            <td class="style2">
                                (03) 9421 4499
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Fax:
                            </td>
                            <td class="style2">
                                (03) 9421 1011
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                Email:
                            </td>
                            <td class="style2">
                                <a href="mailto:support@themusic.com.au">support@themusic.com.au</a>
                            </td>
                        </tr>
                    </table>
                </p>
            </div>
        </div>
        <div id="mainFooterInfo">
            <em>RETURN TO:</em> <a href="#0">TOP</a> |
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/">HOME</asp:HyperLink>
        </div>
    </div>
</asp:Content>
