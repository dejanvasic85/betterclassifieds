<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="NavigationButtons.ascx.vb" Inherits="BetterclassifiedsWeb.NavigationButtons" %>
 
<div id="myAccountTableButtonsAll">
    <div id="myAccountTableButtonsExtend" style="float: right;">
        <ul>
            <li><asp:LinkButton ID="btnPrevious" runat="server" Text="BACK" CausesValidation="false" CssClass="btnNav" /></li>
            <li><asp:LinkButton ID="btnNext" runat="server" Text="NEXT" CausesValidation="true" /></li>
        </ul>
    </div>
</div>

<div class="spacerBookAdBottom">&nbsp;</div>
    
<div id="myAccountTableButtonsRight">
    <div id="myAccountTableButtonsCancel">
        <ul>
            <li><asp:LinkButton ID="btnCancelBooking" runat="server" Text="CANCEL" PostBackUrl="~/Booking/Default.aspx?action=cancel" />   </li>
        </ul>
    </div>
</div>