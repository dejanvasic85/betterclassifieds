<%@ Page Title="Line Ad Colour Palette" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="ColourPalette.aspx.vb" Inherits="BetterclassifiedAdmin.ColourPalette" %>

<%@ Register Src="~/classified/UserControls/LineAdThemeNavigation.ascx" TagName="ThemeNavigation" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openCreateColourWindow() {
            var oWnd = radopen("Create_LineAdColour.aspx", "RadWindow1");
            oWnd.center();
        }

        function openEditColourWindow(id) {
            var oWnd = radopen("EditLineAdColour.aspx?id=" + id, "RadWindow1");
            oWnd.center();
        }

        function createWindowClose() {
            // Reload the page to refresh the grid
            document.location = "ColourPalette.aspx";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Line Ad Themes
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:ThemeNavigation ID="ucxThemeNavigation" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">

    <div style="margin-bottom: 10px">
        <button onclick="openCreateColourWindow(); return false;" name="Create new colour">
            <span style="font-size:11px">Create New Colour</span>
        </button>
    </div>
    <telerik:RadGrid ID="grdLineAdColours" runat="server" 
            Width="97%" 
            AllowSorting="True"
            AllowPaging="True" 
            AutoGenerateColumns="False"
            AllowMultiRowEdit="true"
            Gridlines="None"
            PageSize="50">
        <MasterTableView Width="100%" EditMode="InPlace">
        <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" />
        <HeaderStyle HorizontalAlign="Left" Wrap="true" Height="25" />
        <AlternatingItemStyle VerticalAlign="Middle" />
        <ItemStyle VerticalAlign="Middle" />
        <Columns>
            <telerik:GridTemplateColumn HeaderText="Order" SortExpression="SortOrder">
                <ItemTemplate>
                    <telerik:RadNumericTextBox ID="txtSortOrder" runat="server" Value='<%# Convert.ToDouble(Eval("SortOrder")) %>'>
                        <NumberFormat DecimalDigits="0" />
                        <EnabledStyle Width="30" />
                    </telerik:RadNumericTextBox>
                    <asp:Literal ID="lblLineAdColourId" runat="server" Text='<%# Eval("LineAdColourId") %>' Visible="false" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="LineAdColourName" HeaderText="Colour Name" ReadOnly="true" />
            <telerik:GridBoundColumn DataField="ColourCode" HeaderText="Colour Code" ReadOnly="true" />
            <telerik:GridTemplateColumn HeaderText="Colour Preview" ReadOnly="true">
                <ItemTemplate>
                    <asp:Panel ID="pnlColourDisplay" runat="server" Height="25px" Width="200px"/>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
           <telerik:GridBoundColumn DataField="CreatedByUser" HeaderText="Created By" ReadOnly="true" />
            <telerik:GridTemplateColumn ReadOnly="true">
                <ItemTemplate>
                     <a href="#" onclick="openEditColourWindow(<%#DataBinder.Eval(Container.DataItem,"LineAdColourId")%>); return false;">
                        <asp:ImageButton ID="btnDeposits" runat="server" CommandName="Edit" CommandArgument='<%# Eval("LineAdColourId") %>' 
                            ImageUrl="~/App_Themes/blue/images/Edit.png"
                            ToolTip="Edit" />
                    </a>

                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" /> 
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn ReadOnly="true">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDelete" runat="server" 
                        ImageUrl="~/App_Themes/blue/images/delete.gif"
                        CommandArgument='<%# Eval("LineAdColourId") %>'
                        CommandName="Delete"
                        OnClientClick="javascript:return confirm('Are you sure you want to delete this colour from the palette?')" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" /> 
            </telerik:GridTemplateColumn>
            
        </Columns>
        </MasterTableView>
    </telerik:RadGrid>

    <div style="margin-top: 10px;">
        <asp:Button ID="btnUpdateOrder" runat="server" Text="Update Order" />
    </div>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" 
        Width="450px" Height="550px" 
        VisibleOnPageLoad="false" DestroyOnClose="true" 
        ReloadOnShow="true" Modal="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" OnClientClose="createWindowClose"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

</asp:Content>
