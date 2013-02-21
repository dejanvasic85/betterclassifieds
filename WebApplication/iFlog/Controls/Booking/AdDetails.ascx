<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AdDetails.ascx.vb" Inherits="BetterclassifiedsWeb.AdDetails" %>


<div id="bookAdMainContent">
    <h1>Ad Details</h1>
</div>

<%--Ad Title--%>
<div id="bookAdMainContent"> 
    <h2>
        Title*</h2>
    <div class="spacerBookAd">
        &nbsp;</div>
    <h5>
        This reference is for your iFlog booking.</h5>
</div>
<div id="bookAdMainContent">
    <table width="520" border="0" cellspacing="0px" cellpadding="0px">
        <tr>
            <td width="356">
                <asp:TextBox ID="txtAdTitle" runat="server" Width="300px" MaxLength="50" />
            </td>
            <td width="164">
                <h6><asp:RequiredFieldValidator ID="valAdTitleRequired" runat="server" ControlToValidate="txtAdTitle"
                    ErrorMessage="RequiredFieldValidator" Text="*" EnableClientScript="false"></asp:RequiredFieldValidator></h6>
            </td>
        </tr>
    </table>
</div>

<%--Comments--%>
<div id="bookAdMainContent"> 
    <h2>
        Comments</h2>
    <div class="spacerBookAd">
        &nbsp;</div>
    <h5>
        Place any special comments you may have about this booking.</h5>
</div>
<div id="bookAdMainContent">
    <table width="520" border="0" cellspacing="0px" cellpadding="0px">
        <tr>
            <td width="356">
                <asp:TextBox ID="txtComments" runat="server" Width="300px" MaxLength="255" />
            </td>
            <td width="164">
                <h6></h6>
            </td>
        </tr>
    </table>
</div>