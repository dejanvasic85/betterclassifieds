<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MemberDetails.ascx.vb"
    Inherits="BetterclassifiedsWeb.MemberDetails2" %>

<asp:HiddenField runat="server" ID="hdnEmail" />

<%--Alert--%>
<div class="accountRow">
    <asp:Panel runat="server" ID="pnlAlertSuccess" Visible="False" CssClass="alert alert-success">
        Details have been updated successfully
    </asp:Panel>
    <div id="memberUpdateError" runat="server" class="alert alert-error">
        
    </div>
</div>

<%--First and Last name--%>
<div id="myAccountTableChgDet">
    <table width="750" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
        <tr>
            <td width="360">
                <p>
                    First Name*</p>
            </td>
            <td width="360">
                <p>
                    Last Name*</p>
            </td>
        </tr>
        <tr>
            <td width="360">
                <asp:TextBox ID="FirstNameTextBox" runat="server" size="40%" />
                <asp:RequiredFieldValidator ID="RequiredFirstNameTextBox" ControlToValidate="FirstNameTextBox"
                    runat="server" Text="*" ToolTip="Please enter your first name." />
            </td>
            <td width="360">
                <asp:TextBox ID="LastNameTextBox" runat="server" size="40%"  />
                <asp:RequiredFieldValidator ID="RequiredLastNameTextBox" ControlToValidate="LastNameTextBox"
                    runat="server" Text="*" ToolTip="Please enter your last name." />
            </td>
        </tr>
    </table>
</div>

<%--Address--%>
<div id="myAccountTableChgDet">
    <table width="360" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
        <tr>
            <td width="360">
                <p>
                    Address*</p>
            </td>
        </tr>
        <tr>
            <td width="360">
                <asp:TextBox ID="Address1TextBox" runat="server" size="40%" />
                <asp:RequiredFieldValidator ID="RequiredAddress1TextBox" ControlToValidate="Address1TextBox"
                    runat="server" Text="*" ToolTip="Please enter your street address." />
            </td>
        </tr>
    </table>
</div>

<%--Suburb--%>
<div id="myAccountTableChgDet">
    <table width="360" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
        <tr>
            <td width="360">
                <p>
                    Suburb*</p>
            </td>
        </tr>
        <tr>
            <td width="360">
                <asp:TextBox ID="CityTextBox" runat="server" size="40%" />
                <asp:RequiredFieldValidator ID="RequiredCityTextBox" ControlToValidate="CityTextBox"
                    runat="server" Text="*" ToolTip="Please enter your city." />
            </td>
        </tr>
    </table>
</div>

<%--State and Postcode--%>
<div id="myAccountTableChgDet">
    <table width="750" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
        <tr>
            <td width="360">
                <p>
                    State or Territory</p>
            </td>
            <td width="360">
                <p>
                    Postcode*</p>
            </td>
        </tr>
        <tr>
            <td width="360">
                <asp:DropDownList ID="StateDropDownList" runat="server" Width="200px">
                    <asp:ListItem>ACT</asp:ListItem>
                    <asp:ListItem>NSW</asp:ListItem>
                    <asp:ListItem>NT</asp:ListItem>
                    <asp:ListItem>SA</asp:ListItem>
                    <asp:ListItem>TAS</asp:ListItem>
                    <asp:ListItem>VIC</asp:ListItem>
                    <asp:ListItem>QLD</asp:ListItem>
                    <asp:ListItem>WA</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="360">
                <asp:TextBox ID="ZipCodeTextBox" runat="server" size="10%" />
                <asp:RequiredFieldValidator ID="RequiredZipCodeTextBox" ControlToValidate="ZipCodeTextBox"
                    runat="server" Text="*" ToolTip="Please enter your zip code." />
            </td>
        </tr>
    </table>
</div>

<%--Email--%>
<div id="myAccountTableChgDet">
    <asp:Panel ID="pnlEmail" runat="server" Visible="false">
    <table width="360" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
        <tr>
            <td width="360">
                <p>
                    Email*</p>
            </td>
        </tr>
        <tr>
            <td width="360">
                <asp:TextBox ID="txtEmail" runat="server" size="40%" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmail"
                    runat="server" Text="*" ToolTip="Please enter your email addres." />
            </td>
        </tr>
    </table>
    </asp:Panel>
</div>

<%--Telephone and secondary phone--%>
<div id="myAccountTableChgDet">
    <table width="750" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
        <tr>
            <td width="360">
                <p>
                    Telephone Number*</p>
            </td>
            <td width="360">
                <p>
                    Secondary Telephone Number</p>
            </td>
        </tr>
        <tr>
            <td width="360">
                <asp:TextBox ID="PhoneNumberTextBox" runat="server" size="40%" />
                <asp:RequiredFieldValidator ID="RequiredPhoneNumberTextBox" ControlToValidate="PhoneNumberTextBox"
                    runat="server" Text="*" ToolTip="Please enter your phone number." />
            </td>
            <td width="360">
                <asp:TextBox ID="SecondaryPhoneTextBox" runat="server" size="40%" />
            </td>
        </tr>
    </table>
</div>

<div id="myAccountTableChgDet">
    <h3>
        Business Details</h3>
</div>

<asp:UpdatePanel ID="pnlBusinessUpdate" runat="server">
    <ContentTemplate>
    
        <%--Business Name and ABN--%>
        <div id="myAccountTableChgDet">
            <table width="750" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                <tr>
                    <td width="360">
                        <p>
                            Business Name</p>
                    </td>
                    <td width="360">
                        <p>
                            ABN</p>
                    </td>
                </tr>
                <tr>
                    <td width="360">
                        <asp:TextBox ID="txtBusinessName" runat="server" size="40%"></asp:TextBox>
                    </td>
                    <td width="360">
                        <asp:TextBox ID="txtABN" runat="server" size="40%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        
        <%--Business category and industry--%>
        <div id="myAccountTableChgDet">
            <table width="750" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                <tr>
                    <td width="360">
                        <p>
                            Industry</p>
                    </td>
                    <td width="360">
                        <p>
                            Category</p>
                    </td>
                </tr>
                <tr>
                    <td width="360">
                        <asp:DropDownList ID="ddlIndustry" runat="server" DataTextField="Title"
                            DataValueField="IndustryId" Width="270px" AutoPostBack="True" />
                    </td>
                    <td width="360">
                        <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="Title" DataValueField="BusinessCategoryId" Width="270px" />
                    </td>
                </tr>
            </table>
        </div>   
    
    </ContentTemplate>
</asp:UpdatePanel>

<div class="accountRow">
    <div class="btn-group pull-right">
        <asp:LinkButton ID="btnCancel" runat="server" PostBackUrl="~/MemberAccount/MemberDetails.aspx" Text="Cancel" CssClass="btn btn-warning" />
        <asp:LinkButton ID="btnSubmit" runat="server" Text="Update" CssClass="btn btn-default" />
    </div>
</div>
