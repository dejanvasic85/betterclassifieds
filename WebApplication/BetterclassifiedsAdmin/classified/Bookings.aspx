<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="Bookings.aspx.vb" Inherits="BetterclassifiedAdmin.Bookings1" 
    title="Classified Bookings" %>
<%@ Register Src="~/classified/UserControls/BookingNavigation.ascx" TagName="BookingNavigation" TagPrefix="ucx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

     //  toggle checkboxes in gridview javascript function 
     function SelectAllCheckboxes(spanChk){
     
       var oItem = spanChk.children;
       var theBox= (spanChk.type=="checkbox") ? 
            spanChk : spanChk.children.item[0];
       xState=theBox.checked;
       elm=theBox.form.elements;

       for(i=0;i<elm.length;i++)
         if(elm[i].type=="checkbox" && 
                  elm[i].id!=theBox.id)
         {
           if(elm[i].checked!=xState)
             elm[i].click();
         }
     }
     
     </script>
     
     <%-- greybox javascript sources --%>
    <script language="javascript" type="text/javascript">
        var GB_ROOT_DIR = "../greybox/";
    </script>
    <script type="text/javascript" src="../greybox/AJS.js"></script>
    <script type="text/javascript" src="../greybox/AJS_fx.js"></script>
    <script type="text/javascript" src="../greybox/gb_scripts.js"></script>
    <link href="../greybox/gb_styles.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Manage Classified Bookings
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:BookingNavigation ID="ucxBookingNavigation" runat="server" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">

    <asp:Label ID="lblSearchTitle" runat="server" Font-Bold="true" Text="Search Ad Bookings" />
    
    <%-- Search Criteria --%>
    <div id="searchDiv" style="padding: 10px;">
        <fieldset style="padding: 10px;">
            <legend><asp:Label ID="Label2" runat="server" Text="Search Criteria" /></legend>
            <table cellpadding="6">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Booking Reference" />:</td>
                    <td>
                        <asp:TextBox ID="txtBookReference" runat="server" MaxLength="10" AutoCompleteType="Disabled" /></td>
                    <td>
                       <asp:Label ID="Label4" runat="server" Text="Start Date" />:</td> 
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" Width="100px" AutoCompleteType="Disabled" />
                        <asp:Image ID="imgCalStart" runat="server" ImageUrl="~/App_Themes/blue/Images/Calendar_schedule.png" />
                        <ajax:CalendarExtender ID="ajaxCalendarExtender" runat="server" TargetControlID="txtStartDate"
                            PopupButtonID="imgCalStart" FirstDayOfWeek="Monday" Animated="false" Format="dd-MMM-yyyy" />
                        <%--<asp:CompareValidator ID="compValStartDate" runat="server" Text="* Please use correct date." Operator="DataTypeCheck"
                            ControlToValidate="txtStartDate" Type="Date" EnableClientScript="true" />--%></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Username [Starts With]" />:</td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server" AutoCompleteType="Disabled" /></td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="End Date" AutoCompleteType="Disabled" />:</td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server"  Width="100px" />
                        <asp:Image ID="imgCalEnd" runat="server" ImageUrl="~/App_Themes/blue/Images/Calendar_schedule.png" />
                        <ajax:CalendarExtender ID="ajaxCalendarExtender2" runat="server" TargetControlID="txtEndDate"
                            PopupButtonID="imgCalEnd" FirstDayOfWeek="Monday" Animated="false" Format="dd-MMM-yyyy" />
                        <%--<asp:CompareValidator ID="CompareValidator1" runat="server" Text="* Please use correct date." Operator="DataTypeCheck"
                            ControlToValidate="txtEndDate" Type="Date" EnableClientScript="true" />--%></td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <asp:CheckBox ID="chkIncludeCancelled" runat="server" Text="Include Cancelled Bookings" Checked="true" /></td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                         <asp:Button ID="btnSearch" runat="server" Text="Search" ToolTip="Click to start search." /></td>
                </tr>
            </table>
        </fieldset>
        
    </div>
    
    <br />
    <asp:Label ID="lblSearchCount" runat="server" Text="" Font-Bold="true" /><br /><br />
    
 
    <div id="searchResults" runat="server" visible="false">
        <%-- search results --%>
        <asp:GridView   ID="grdSearchResults" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found."
                        DataKeyNames="AdBookingId" DataSourceID="searchDataSource" 
                        AllowPaging="True" PageSize="20" PagerSettings-Position="Top">
            <Columns>
            
                <asp:TemplateField>
                    <HeaderStyle CssClass="gridheaderBG" Width="1px" />
                    <ItemStyle CssClass="gridheaderBG" Width="1px" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Del">
                    <HeaderTemplate>
                        <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" type="checkbox" title="Check all checkboxes" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkRows" runat="server" ToolTip="Select for deletion" />
                    </ItemTemplate>
                    <ItemStyle Width="25px" HorizontalAlign="Center" />
                </asp:TemplateField>
                
                
                <asp:BoundField DataField="BookReference" HeaderText="Reference No" SortExpression="" />
                <asp:BoundField DataField="ParentCategory" HeaderText="Category" />
                <asp:BoundField DataField="SubCategory" HeaderText="Sub Category" />
                <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:D}" />
                <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:D}" />
                <asp:BoundField DataField="UserId" HeaderText="Username" />
                
                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image ID="imgStatus" runat="server" Height="20" Width="20" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkBookingId" runat="server" rel="gb_page_center[525, 600]" 
                            NavigateUrl='<%# String.Format("~/classified/ModalDialog/Edit_BookEntries.aspx?adBookingId={0}", Eval("AdBookingId")) %>' 
                            Text="Editions" Title="View Publication Edition Entries" />
                    </ItemTemplate>    
                </asp:TemplateField>
                
                <asp:BoundField DataField="TotalPrice" HeaderText="Price" DataFormatString="{0:C}" />

            </Columns> 
        </asp:GridView>
       
        <asp:ObjectDataSource ID="searchDataSource" runat="server" 
            SelectMethod="SearchAdBookings" SelectCountMethod="SearchAdBookingsCount"
            TypeName="BetterclassifiedsCore.DataModel.Search" 
            OldValuesParameterFormatString="original_{0}" EnablePaging="True">
        
            <SelectParameters>
                <asp:ControlParameter ControlID="txtBookReference" Name="bookReference" 
                    PropertyName="Text" Type="String" DefaultValue="" />
                <asp:ControlParameter ControlID="chkIncludeCancelled" Name="includeCancelled" 
                    PropertyName="Checked" Type="Boolean" />
                <asp:ControlParameter ControlID="txtUsername" Name="username" 
                    PropertyName="Text" Type="String" DefaultValue="" />
                <asp:ControlParameter ControlID="txtStartDate" Name="startDate" 
                    PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="txtEndDate" Name="endDate" PropertyName="Text" 
                    Type="DateTime" />
                <asp:Parameter Name="startRowIndex" Type="Int32" />
                <asp:Parameter Name="maximumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        
        <%-- Actions toolbar --%>
        <div class="membersToggle" style="padding-bottom: 20px;">
            <asp:LinkButton ID="btnActivate" runat="server" Text="Activate" ToolTip="Set selected Bookings to have Active Status" />
            <asp:LinkButton ID="btnCancelSelected" runat="server" Text="Cancel" ToolTip="Set selected bookings to have cancelled status" />
        </div><br />
    
        <asp:Label ID="lblActionMsgSuccess" runat="server" ForeColor="Green" />
    
    </div>
</asp:Content>
