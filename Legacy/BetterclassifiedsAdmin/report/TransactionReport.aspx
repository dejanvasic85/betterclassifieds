<%@ Page Title="Reports - Transaction Report" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="TransactionReport.aspx.vb"
    Inherits="BetterclassifiedAdmin.TransactionReport" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/report/navigation/ClassifiedNav.ascx" TagName="ClassifiedNav"
    TagPrefix="ucx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Classified Reports
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:ClassifiedNav ID="ucxNavigation" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    Transaction / Sales Report
    <p>
        Purpose of this report is to list all the payments made within a required time period.</p>
    <p>
        <b>To view grouped data simply drag and drop the title name into the header section.
            Note: Not all columns are groupable.</b></p>
    <div>
        <table width="50%">
            <tr>
                <td>
                    Start Date:
                </td>
                <td>
                    <telerik:RadDatePicker ID="dtpStart" runat="server" Skin="Vista" />
                </td>
            </tr>
            <tr>
                <td>
                    End Date:
                </td>
                <td>
                    <telerik:RadDatePicker ID="dtpEndDate" runat="server" DateInput-DateFormat="dd/MM/yyyy" Skin="Vista">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Vista">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="btnSearch" runat="server" Text="Generate Report" />
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 90%; overflow: auto;">
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True"
                AllowSorting="True" GridLines="None"
                ShowGroupPanel="True" Skin="Office2007" AutoGenerateColumns="False">
            <HeaderContextMenu EnableTheming="True">
                <CollapseAnimation Type="OutQuint" Duration="100"></CollapseAnimation>
            </HeaderContextMenu>
            <ExportSettings IgnorePaging="True" ExportOnlyData="true" Excel-Format="Html" />
            <MasterTableView Width="100%" ShowFooter="True">
                <Columns>
                    <telerik:GridBoundColumn DataField="TransactionDate" UniqueName="TransactionDate" HeaderText="Transaction Date"
                        Groupable="false" DataFormatString="{0:dd-MMM-yyyy}" />
                    <telerik:GridBoundColumn DataField="UserId" UniqueName="UserId" HeaderText="Username"
                        Groupable="false"/>
                    <telerik:GridBoundColumn DataField="BookReference" UniqueName="BookReference" HeaderText="Reference"
                        Groupable="false"/>
                    <telerik:GridBoundColumn DataField="BookingType" UniqueName="BookingType" HeaderText="Booking Type"
                        Groupable="true"/>
                    <telerik:GridBoundColumn DataField="TransactionType" UniqueName="TransactionType" HeaderText="Transaction Type"
                        Groupable="true"/>
                    <telerik:GridBoundColumn DataField="PriceExGST" UniqueName="PriceExGST" HeaderText="PriceExGST" 
                        Groupable="false" DataFormatString="{0:c}" />
                    <telerik:GridBoundColumn DataField="GST" UniqueName="GST" HeaderText="GST" 
                        Groupable="false" DataFormatString="{0:c}" />
                    <telerik:GridBoundColumn DataField="TotalPrice" UniqueName="AdTotal" HeaderText="Ad Total" 
                        Groupable="false" DataFormatString="{0:c}" />
                </Columns>
            </MasterTableView>
            <FooterStyle Font-Bold="true" ForeColor="Green" />
            <ClientSettings AllowDragToGroup="True" />
            <FilterMenu EnableTheming="True">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </FilterMenu>
            <PagerStyle AlwaysVisible="true" />
        </telerik:RadGrid>
        <div>
            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" />
        </div>
    </div>
</asp:Content>
