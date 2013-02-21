<%@ control language="vb" autoeventwireup="false" codebehind="EditionDates.ascx.vb"
    inherits="BetterclassifiedsWeb.Controls.Booking.EditionDates" %>
<div class="edition-dates-control">
    <p><asp:Label ID="lblNoDates" runat="server" Text="Please ensure you have selected a start date and at least one edition." ForeColor="Red" Font-Bold="true" /></p>
    <asp:datalist id="lstEditions" runat="server" repeatdirection="Horizontal" cellpadding="4" ItemStyle-VerticalAlign="Top">
        <itemtemplate>
            <asp:label id="lblPaperName" runat="server" text='<%# Eval("Title") %>' font-bold="true" 
                CssClass="edition-dates-header" Width="160px" />
            <br />
            <asp:gridview id="grdEditions" runat="server" datasource='<%# Eval("FormattedDates") %>'
                gridlines="None" showheader="false" width="100%"  
                onpageindexchanging="GridIndexChanged" pagesize="2"  pagersettings-mode="Numeric"
                AlternatingRowStyle-BackColor="Beige" />
        </itemtemplate>
    </asp:datalist>
</div>
