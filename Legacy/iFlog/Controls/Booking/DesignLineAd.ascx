<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DesignLineAd.ascx.vb"
    Inherits="BetterclassifiedsWeb.DesignLineAd" %>
<%@ Register Src="~/Controls/ImageSpecification.ascx" TagName="ImageSpecification"
    TagPrefix="ucx" %>
<div id="bookAdMainContent">
    <h1>
        Print Advertisement</h1>
</div>
<%--Bold Heading--%>
<asp:Panel ID="pnlBoldHeading" runat="server">
    <div id="bookAdMainContent">
    <table>
        <tr>
            <td>
            <h2>Bold Header</h2>
            </td>
            <td style=" vertical-align:middle;"> <h5 style=" margin-top:10px;">
            The title for your advertisement</h5> 
            </td>
            <td><div class="help-context-panel">
            <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl1"
                ImageUrl="~/Resources/Images/question_button.gif" runat="server">
                <ContentTemplate>
                    <span class="text-wrapper"><b>Bold Header:</b> Decide a heading to appear above your ad, for example 'Guitarist Wanted' </span>
                </ContentTemplate>
            </paramountItCommon:HelpContextControl>
        </div></td>
        </tr>
    </table>
        
    </div>
    <div id="bookAdMainContent">
        <table width="520" border="0" cellspacing="0px" cellpadding="0px">
            <tr>
                <td>
                    <div class="myAccountTableButtonsPremium">
                        <asp:LinkButton ID="btnUpgrade" runat="server" Text="Upgrade to Premium" Visible="false" />
                    </div>
                </td>
            </tr>
            <tr>
                <td width="356">
                    <asp:TextBox ID="txtHeader" runat="server" Width="300px" MaxLength="255" ToolTip="Bold Header is optional" />
                </td>
                <td width="164">
                    <h6>
                        <asp:Label ID="lblHeadingLimit" runat="server" Text="" /></h6>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<%--Body Text--%>
<div id="bookAdMainContent">
    <h2>
        Body Text*</h2>
    <div class="spacerBookAd">
        &nbsp;</div>
    <h5>
        This is the main text describing your product or service.
        <asp:Label ID="lblWordLimit" runat="server" Text="" CssClass="red" />
        <br />
    </h5>
    <div class="help-context-panel">
        <paramountItCommon:HelpContextControl Position="Bottom" ID="HelpContextControl2"
            ImageUrl="~/Resources/Images/question_button.gif" runat="server">
            <ContentTemplate>
                <span class="text-wrapper"><b>Body Text: </b>Go into more detail to describe what
                    you are looking for, for example the type of person you would like you in your band
                    and what type of music you play. </span>
            </ContentTemplate>
        </paramountItCommon:HelpContextControl>
    </div>
</div>
<div id="bookAdMainContent">
    <asp:UpdatePanel ID="pnlWordsUpdate" runat="server">
        <ContentTemplate>
            <table width="520" border="0" cellspacing="0px" cellpadding="2px">
                <tr>
                    <td>
                        <div class="myAccountTableButtonsPremium">
                            <asp:LinkButton ID="btnUpgradeText" runat="server" Text="Upgrade To Premium" Visible="false" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <paramountIt:LineAdDescriptionBox ID="lineAdDescriptionBox" runat="server" />
                    </td>
                    <td width="114" valign="top">
                        <div class="wordcount">
                            <div style="margin-bottom: 10px;">
                                <asp:LinkButton ID="btnPreview" runat="server" Text="Preview Ad" CausesValidation="false"
                                    ValidationGroup="z" ToolTip="Please click to preview how the ad description will be formatted on paper." />
                            </div>
                            <asp:LinkButton ID="btnGetWords" runat="server" Text="Word Count" CausesValidation="false" />
                            &nbsp; :
                            <asp:Label ID="lblWords" runat="server" Text="0"></asp:Label>
                        </div>
                    </td>
                    <td width="40">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
