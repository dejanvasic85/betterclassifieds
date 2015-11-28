<%@ Page Title="Reports - Weekly Publication Sales" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="WeeklyPublication.aspx.vb"
    Inherits="BetterclassifiedAdmin.WeeklyPublication" %>

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
    Weekly Publication Report
    <p>
        Following report gives the pricing and details summary for each ad in the required
        edition and weekly paper.</p>
    <p><b>To view grouped data (for e.g. Cateogry/Sub Category) simply drag and drop the title
    name into the header section. Note: Not all columns are groupable.</b></p>
    
    <div>
        <table width="50%">
            <tr>
                <td>Publication: </td>
                <td><telerik:RadComboBox ID="ddlPublication" runat="server" 
                        DataTextField="Title" DataValueField="PublicationId" Skin="Vista" >
                        <CollapseAnimation Type="OutQuint" Duration="100"></CollapseAnimation>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>Edition:</td>
                <td><telerik:RadDatePicker ID="dtpEdition" runat="server" Skin="Vista" /></td>
            </tr>
            <tr>
                <td>Status:</td>
                <td><telerik:RadComboBox ID="ddlStatus" runat="server" Skin="Vista" >
                        <CollapseAnimation Type="OutQuint" Duration="100"></CollapseAnimation>
                    <Items>
                        <telerik:RadComboBoxItem Text="All" Value="0" />
                        <telerik:RadComboBoxItem Text="Booked" Value="1" />
                        <telerik:RadComboBoxItem Text="Cancelled" Value="3" />
                        <telerik:RadComboBoxItem Text="Unpaid" Value="4" />
                    </Items>
                </telerik:RadComboBox></td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="btnSearch" runat="server" Text="Generate Report" /></td>
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
    
        <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" 
            AllowPaging="True" AllowSorting="True" GridLines="None" 
            OnNeedDataSource="RadGrid1_NeedDataSource" ShowGroupPanel="True" 
            Skin="Office2007" AutoGenerateColumns="False">
            <HeaderContextMenu EnableTheming="True">
                <CollapseAnimation Type="OutQuint" Duration="100"></CollapseAnimation>
            </HeaderContextMenu>
            <exportsettings 
                ignorepaging="True" 
                ExportOnlyData="true" 
                Excel-Format="Html" />
            <MasterTableView width="100%" showfooter="True">
                <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="Category" />
                        </GroupByFields>
                        <SelectFields>
                            <telerik:GridGroupByField FieldName="Category" />
                        </SelectFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
                <Columns>
                    <telerik:GridBoundColumn DataField="iFlogID" UniqueName="iFlogID" HeaderText="iFlogID" Groupable="false" />
                    <telerik:GridBoundColumn DataField="Category" UniqueName="Category" HeaderText="Category" />
                    <telerik:GridBoundColumn DataField="SubCategory" UniqueName="SubCategory" HeaderText="Sub Category" />
                    <telerik:GridBoundColumn DataField="UserId" UniqueName="UserId" HeaderText="User" />
                    <telerik:GridBoundColumn DataField="BookReference" UniqueName="Ref" HeaderText="Ref" Groupable="false" />
                    <telerik:GridBoundColumn DataField="BookingType" UniqueName="BookType" HeaderText="Book Type" />
                    <telerik:GridBoundColumn DataField="Status" UniqueName="Status" HeaderText="Status" />
                    <telerik:GridBoundColumn DataField="NumOfWords" UniqueName="Words" HeaderText="Words" Groupable="false" />
                    <telerik:GridBoundColumn DataField="BoldHeadings" UniqueName="Headings" HeaderText="Headings" Groupable="false" />
                    <telerik:GridBoundColumn DataField="Photos" UniqueName="Photos" HeaderText="Photos" Groupable="false" />
                    <telerik:GridBoundColumn DataField="PriceExGST" UniqueName="PriceExGST" HeaderText="PriceExGST" Groupable="false" DataFormatString="{0:c}" />
                    <telerik:GridBoundColumn DataField="GST" UniqueName="GST" HeaderText="GST" Groupable="false" DataFormatString="{0:c}" />
                    <telerik:GridBoundColumn DataField="TotalPrice" UniqueName="AdTotal" HeaderText="Ad Total" Groupable="false" DataFormatString="{0:c}" />
                </Columns>
            </MasterTableView>
            <FooterStyle Font-Bold="true" ForeColor="Green" />
            <clientsettings allowdragtogroup="True" />
            <FilterMenu EnableTheming="True">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </FilterMenu>
            <PagerStyle AlwaysVisible="true" />
        </telerik:RadGrid>
        <div>
            <asp:Button ID="Button1" runat="server" Text="Export To Excel" OnClick="ExportToExcel" />
        </div>
    </div>
</asp:Content>
