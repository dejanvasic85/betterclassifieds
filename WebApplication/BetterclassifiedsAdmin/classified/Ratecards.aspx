<%@ Page Title="Classified Casual Rates" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="Ratecards.aspx.vb" Inherits="BetterclassifiedAdmin.Ratecards" %>

<%@ Register Src="~/classified/UserControls/RateNavigation.ascx" TagName="RateNavigation" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        
        function btnEditRatecard_ClientClick(id) {
            var openUrl = '<%= ResolveUrl("~/classified/ModalDialog/Edit_Ratecard.aspx") %>' + '?ratecardId=' + id;
            var clientEditDialog = radopen(openUrl, "wndEditRateCard");
            clientEditDialog.add_close(cancelClose);
        }

        function cancelClose(sender, args) {
            document.location = '<%= ResolveUrl("~/classified/Ratecards.aspx") %>';
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Ratecards
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:RateNavigation ID="ucxRateNavigation" runat="server" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="cphContentHeading" runat="server">
    Casual Rate List
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">

    <div style="margin-bottom: 10px">
    
        <%-- Actions toolbar --%>
        <asp:Button ID="btnCreateNew" runat="server" Text="Create New" />

        <div style="margin-top: 10px">
            
            <telerik:RadGrid ID="grdPriceList" runat="server" 
                AllowSorting="True"
                AllowPaging="True" 
                AutoGenerateColumns="False"
                AllowMultiRowEdit="False"
                Gridlines="None"
                PageSize="20">
                <MasterTableView>
                <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" />
                <HeaderStyle HorizontalAlign="Left" Wrap="true" Height="25" />
                <AlternatingItemStyle VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="RateCardId" HeaderText="ID" UniqueName="RateCardId" />
                        <telerik:GridBoundColumn DataField="RateCardName" HeaderText="Name" />
                        <telerik:GridBoundColumn DataField="MinimumCharge" HeaderText="Minimum Charge" DataFormatString="{0:N}" />
                        <telerik:GridBoundColumn DataField="MaximumCharge" HeaderText="Maximum Charge" DataFormatString="{0:N}" />
                        <telerik:GridBoundColumn DataField="PublicationCount" HeaderText="Assigned Publications"
                            ItemStyle-HorizontalAlign="Center" />
                        <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd-MMM-yyyy}" />
                        <telerik:GridBoundColumn DataField="CreatedByUser" HeaderText="Created by" />
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <a href="#"
                                    onclick="btnEditRatecard_ClientClick('<%# Eval("RateCardId")%>'); return false;">
                                    <asp:ImageButton ID="btnEdit" runat="server"
                                        ImageUrl="~/App_Themes/blue/Images/Edit.png"
                                        ToolTip="Edit" /></a>
                                <asp:ImageButton ID="btnDeleteEdition" runat="server" 
                                    CommandName="Remove" CommandArgument='<%# Eval("RateCardId") %>'
                                    ImageUrl="~/App_Themes/blue/images/delete.gif" 
                                    OnClientClick="javascript:return confirm('Are you sure you want to delete?');"
                                    ToolTip="Delete" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>  
            </telerik:RadGrid>
        </div>
    </div>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" 
        Width="650px" Height="550px" 
        VisibleOnPageLoad="false" DestroyOnClose="true" 
        ReloadOnShow="true" Modal="true">
        <Windows>
            <telerik:RadWindow ID="wndEditRateCard" runat="server"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
</asp:Content>
