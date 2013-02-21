<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DesignOnlineAd.ascx.vb"
    Inherits="BetterclassifiedsWeb.DesignOnlineAd" %>
<%@ Register Src="~/Controls/ImageSpecification.ascx" TagName="ImageSpecification"
    TagPrefix="ucx" %>
<div class="bookingMainContent">
    <h1>
        Online Classified Details</h1>
</div>
<%--Ad Title--%>
<div id="bookAdMainContent">
<table >
    <tr>
        <td><h2>
        Bold Header*</h2></td>
        <td><h5 style="margin-top:15px;"> The title for your advertisement</h5></td>
        <td><div class="help-context-panel" style="margin-top:10px">
        <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl2"
            ImageUrl="~/Resources/Images/question_button.gif" runat="server">
            <ContentTemplate>
                <span class="text-wrapper"><b>Bold Header: </b>Decide a heading to appear above your
                    ad, for example 'Guitarist Wanted'. </span>
            </ContentTemplate>
        </paramountItCommon:HelpContextControl>
    </div></td>
    </tr>
</table>

</div>
<div id="bookAdMainContent">
    <table width="520" border="0" cellspacing="0px" cellpadding="0px">
        <tr>
            <td width="356">
                <asp:TextBox ID="txtOnlineHeading" runat="server" size="40%" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" EnableClientScript="false"
                    ErrorMessage="Online heading is required." Text="*" ControlToValidate="txtOnlineHeading" />
            </td>
            <td width="164">
                <h6>
                    <asp:Label ID="lblHeadingLimit" runat="server" Text="" /></h6>
            </td>
        </tr>
    </table>
</div>
<%--Body Text--%>
<div id="bookAdMainContent">
    <table >
        <tr>
            <td>
                <h2> Body Text*</h2>
            </td>
            <td>
                <h5 style="margin-top:15px;">This is the main text describing your product or service</h5>
            </td>
            <td>
                <div class="help-context-panel" style="margin-top:10px">
                <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl1"
                    ImageUrl="~/Resources/Images/question_button.gif" runat="server">
                    <ContentTemplate>
                        <span class="text-wrapper"><b>Body Text: </b>Go into more detail to describe what you
                            are looking for, for example the type of person you would like you in your band
                            and what type of music you play. </span>
                    </ContentTemplate>
                </paramountItCommon:HelpContextControl>
            </div>
            </td>
        </tr>
    </table>
    
</div>
<div id="bookAdMainContent">
    <table width="520" border="0" cellspacing="0px" cellpadding="0px">
        <tr>
            <td valign="top">
                <telerik:RadEditor ID="radEditor" runat="server" NewLineBr="true" ToolbarMode="Default"
                    EditModes="All">
                    <Tools>
                        <telerik:EditorToolGroup Tag="EditToolbar">
                            <telerik:EditorTool Name="Print" Text="Print" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup Tag="FormatToolbar">
                            <telerik:EditorTool Name="FontName" Text="Font" />
                            <telerik:EditorTool Name="FontSize" Text="Font Size" />
                            <telerik:EditorTool Name="ForeColor" Text="Font Color" />
                            <telerik:EditorTool Name="BackColor" Text="Background Color" />
                            <telerik:EditorTool Name="Bold" Text="Bold" />
                            <telerik:EditorTool Name="Italic" Text="Italic" />
                            <telerik:EditorTool Name="Underline" Text="Underline" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                            <telerik:EditorTool Name="JustifyLeft" Text="JustifyLeft" />
                            <telerik:EditorTool Name="JustifyCenter" Text="JustifyCenter" />
                            <telerik:EditorTool Name="JustifyRight" Text="JustifyRight" />
                            <telerik:EditorTool Name="JustifyFull" Text="JustifyFull" />
                            <telerik:EditorTool Name="Indent" Text="Indent" />
                            <telerik:EditorTool Name="Outdent" Text="Outdent" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                            <telerik:EditorTool Name="InsertUnorderedList" Text="InsertUnorderedList" />
                            <telerik:EditorTool Name="InsertOrderedList" Text="InsertOrderedList" />
                        </telerik:EditorToolGroup>
                        <telerik:EditorToolGroup>
                            <telerik:EditorTool Name="AjaxSpellCheck" Text="Spell Checker" />
                        </telerik:EditorToolGroup>
                    </Tools>
                </telerik:RadEditor>
                <%--<asp:textbox ID="txtDescription" runat="server" Columns="40" Rows="10"
                    TextMode="MultiLine"  />
                <asp:requiredfieldvalidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="false"
                    ErrorMessage="Online Description is Required" Text="*" 
                    ControlToValidate="txtDescription" />--%>
            </td>
        </tr>
    </table>
</div>
<%--Sellect / Contact Name--%>
<div id="bookAdMainContent">
    <table >
        <tr>
            <td><h2> Seller/Contact Name</h2></td>
            <td><h5 style="margin-top:15px;"> Who to contact for future information</h5>
             </td>
            <td><div class="help-context-panel" style=" margin-top:10px;">
        <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl3"
            ImageUrl="~/Resources/Images/question_button.gif" runat="server">
            <ContentTemplate>
                <span class="text-wrapper"><b>Seller/Contact Name: </b>When replying to your ad, people
                    will ask for this person. </span>
            </ContentTemplate>
        </paramountItCommon:HelpContextControl>
    </div></td>
        </tr>
    </table>
</div>
<div id="bookAdMainContent">
    <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>
            <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                <tr>
                    <td width="320">
                        <asp:TextBox ID="txtSeller" runat="server" size="40%" MaxLength="199" ToolTip="Private contact won't display any contact details in the ad." />
                    </td>
                    <td width="200" align="left">
                        <div class="floatLeftBlock">
                            <h6>
                                <asp:CheckBox ID="chkNamePrivate" runat="server" Text="Check to make Private." AutoPostBack="true"
                                    ToolTip="Private contact won't display any contact details in the ad." /></h6>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<%--Contact Details--%>
<div id="bookAdMainContent">
    <table >
        <tr>
            <td><h2> Contact Details</h2></td>
            <td style=" vertical-align:middle;"> <h5 style="margin-top:15px;">Select method of contact</h5></td>
            <td><div class="help-context-panel" style=" margin-top:10px">
        <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl4"
            ImageUrl="~/Resources/Images/question_button.gif" runat="server">
            <ContentTemplate>
                <span class="text-wrapper"><b>Contact Details: </b>To make it easier for people to contact
                    you, select either the email, phone or fax option and leave the relevant details.
                </span>
            </ContentTemplate>
        </paramountItCommon:HelpContextControl>
    </div></td>
        </tr>
    </table>
    
    
</div>
<div id="bookAdMainContent" style="vertical-align: middle;">
    <table width="520" border="0" cellspacing="0px" cellpadding="0px">
        <tr>
            <td width="320" height="35">
                <asp:TextBox ID="txtContactValue" runat="server" size="40%" MaxLength="100" />
            </td>
            <td width="200" height="35" valign="top">
                <div class="floatLeftBlockPrick" style="line-height: 35px; vertical-align: middle;
                    text-align: center;">
                    <asp:Label ID="lblCType" runat="server" Text="Contact type:" CssClass="spanFormTitle" />
                    <asp:DropDownList ID="ddlContactType" runat="server" Width="100px">
                        <asp:ListItem>Email</asp:ListItem>
                        <asp:ListItem>Phone</asp:ListItem>
                        <asp:ListItem>Fax</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
    </table>
</div>
<%--Price--%>
<div id="bookAdMainContent">
<table >
    <tr>
        <td><h2>
        Price</h2></td>
        <td style=" vertical-align:middle;"> <h5 style=" margin-top:10px;">
        Approximate price of item if required</h5></td>
        <td><div class="help-context-panel" style=" margin-top:10px">
        <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl5"
            ImageUrl="~/Resources/Images/question_button.gif" runat="server">
            <ContentTemplate>
                <span class="text-wrapper"><b>Price: </b>Approximate price of item if required. </span>
            </ContentTemplate>
        </paramountItCommon:HelpContextControl>
    </div></td>
    </tr>
</table> 
    
</div>
<div id="bookAdMainContent">
    <table width="520" border="0" cellspacing="0px" cellpadding="0px">
        <tr>
            <td width="200">
                <div class="floatLeftBlockPrick">
                    <asp:Label ID="lblPriceLogo" runat="server" Text="$" CssClass="spanFormTitle" />
                    <asp:TextBox ID="txtPrice" runat="server" Width="75px" MaxLength="8"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="priceFilter" FilterType="Custom" FilterMode="ValidChars"
                        ValidChars=".1234567890" TargetControlID="txtPrice" runat="server" />
                </div>
            </td>
        </tr>
    </table>
</div>
<%--Location and Area--%>
<asp:UpdatePanel ID="pnlUpdateLocation" runat="server">
    <ContentTemplate>
        <div id="bookAdMainContent">
        <table >
            <tr>
                <td><h2>
                Location</h2></td>
                 <td style="vertical-align:middle;"> <h5 style=" margin-top:10px;">
                Where is the item located? Select a Location and corresponding Area.</h5></td>
                <td><div class="help-context-panel" style=" margin-top:10px">
                <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl6"
                    ImageUrl="~/Resources/Images/question_button.gif" runat="server">
                    <ContentTemplate>
                        <span class="text-wrapper"><b>Location: </b>The location allows people to know more
                            information, by selecting a state and area the information is upfront. For example:
                            If you are looking for a band member you would select the state you live in and
                            then the area. </span>
                    </ContentTemplate>
                </paramountItCommon:HelpContextControl>
            </div></td>
            </tr>
        </table>   
        </div>
        <div id="bookAdMainContent">
            <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                <tr>
                    <td width="270">
                        <asp:DropDownList ID="ddlOnlineLocation" runat="server" Width="200px" AutoPostBack="true"
                            DataTextField="Title" DataValueField="LocationId" />
                    </td>
                    <td width="250">
                        <asp:DropDownList ID="ddlOnlineLocationArea" Width="200px" runat="server" DataTextField="Title"
                            DataValueField="LocationAreaId" />
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
