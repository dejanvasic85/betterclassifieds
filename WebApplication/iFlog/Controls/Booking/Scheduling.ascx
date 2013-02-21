<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Scheduling.ascx.vb" Inherits="BetterclassifiedsWeb.Scheduling" %>

<%@ Register Src="~/Controls/Booking/EditionDates.ascx" TagName="EditionDates" TagPrefix="ucx" %>

<asp:UpdatePanel ID="updatePanel" runat="server">
    <ContentTemplate>
    <fieldset>
    <legend>Scheduling Details</legend>
    <table width="50%" cellpadding="8">
        <tr>
            <td align="right" valign="top">
                <asp:Label ID="lblStartdate" runat="server" Text="Start Date" Width="100px" /></td>
            <td>
                <asp:Calendar ID="calStartDate" runat="server" BackColor="White" 
                    BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
                    DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                    ForeColor="#003399" Height="200px" Width="220px">
                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="Black" />
                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
                        Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                </asp:Calendar></td>
        </tr>
        
        <tr id="rowInsertions" runat="server">
            <td align="right">
                <asp:Label ID="lblInsertions" runat="server" Text="Insertions" />:</td>
            <td>
                <asp:DropDownList ID="ddlInserts" runat="server" AutoPostBack="true"></asp:DropDownList></td>
        </tr>
        
        <tr>
            <td>
                </td>
            <td>
                <asp:LinkButton ID="lnkCheckEditions" runat="server" Text="Check Editions" /></td>
        </tr>
        <tr>
            <td align="right" valign="top">
                <asp:Label ID="Label3" runat="server" Text="Deadlines"></asp:Label>:</td>
            <td>
                <asp:DataList ID="listPublicationDeadlines" runat="server">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Title") %>' Font-Bold="true"></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text='<%# String.Format("{0:f}", Eval("Deadline")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
        
        <tr id="rowOnlineInfo" runat="server">
            <td colspan="2">
                <asp:Label ID="Label2" runat="server" Text="Online Ads run for 30 days starting from the start date."></asp:Label></td>
        </tr>
        <tr id="trEndDate" runat="server">
            <td align="right">
                <asp:Label ID="Label1" runat="server" Text="End Date" Font-Bold="true"></asp:Label>:</td>
            <td>
                <asp:Label ID="lblEndDate" runat="server" Text="" Font-Bold="true"></asp:Label></td>
        </tr>
        
    </table>
    </fieldset>
    
    <!-- Panel used for Edition Dates" -->
    <asp:Panel ID="pnlEditionDates" runat="server">
        <asp:Panel ID="pnlEditions" runat="server" CssClass="modalPopup" Width="450px">
            <ucx:EditionDates ID="ucxEditionDates" runat="server" />
            <div align="center">
                <asp:Button ID="btnClose" runat="server" Text="Close" /></div>
        </asp:Panel>
        
        <ajax:ModalPopupExtender ID="modalPopup1" runat="server" BackgroundCssClass="modalBackground"
            CancelControlID="btnClose" PopupControlID="pnlEditions" TargetControlID="lnkCheckEditions" />
    </asp:Panel>    
    
    </ContentTemplate>
    </asp:UpdatePanel>