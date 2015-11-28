<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="AdSpaces.aspx.vb" 
    Inherits="BetterclassifiedAdmin.AdSpaces" Title="Configure Ad Spaces" %>
<%@ Register Src="~/classified/UserControls/AdSpaceNavigation.ascx" TagName="Navigation" TagPrefix="ucx" %> 
<%@ Register Src="~/classified/UserControls/AdSpaceConfiguration.ascx" TagName="Configuration" TagPrefix="ucx" %>

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
    Configure Ad Spaces
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:Navigation ID="ucxNavigation" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Configuring Advertising Spaces" />
    
    <asp:Panel ID="pnlNotAvailable" runat="server" ForeColor="Red" Font-Bold="true">
        <p>This feature is not available at the moment. Please contact Paramount IT Solutions.</p>
    </asp:Panel>
    
    <asp:Panel ID="pnlNavigationDiv" runat="server">
        <p>The following ad spaces are available for set up. Please choose one of the following:</p>
        <asp:Repeater ID="rptNavigation" runat="server">
            <HeaderTemplate><ul></HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:HyperLink ID="lnkNav" runat="server" Text='<%# Eval("Title") %>'
                        NavigateUrl='<%# String.Format("~/classified/AdSpaces.aspx?settingId={0}", Eval("SettingId")) %>'></asp:HyperLink></li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
    
    <asp:Panel ID="pnlConfiguration" runat="server">
        <p>Use this easy set up tool to configure the banners available 
        around the classified website for external/internal advertising.</p>        
        
        <%--<ucx:Configuration ID="ucxAdConfig" runat="server" />--%>
        <asp:GridView ID="grdAds" runat="server" EmptyDataText="There's no ads to display">
            <Columns>
                <asp:TemplateField HeaderText="Del">
                    <HeaderTemplate>
                        <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" type="checkbox" title="Check all checkboxes" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkRows" runat="server" ToolTip="Select" />
                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("WebAdSpaceId") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="25px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="SortOrder" HeaderText="Order" />
                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkSetup" runat="server" Text='<%# Eval("Title") %>' rel="gb_page_center[525, 600]" 
                            NavigateUrl='<%# String.Format("~/classified/ModalDialog/SetupAdSpace.aspx?spaceId={0}&settingId={1}&mode=2", Eval("WebAdSpaceId"), Eval("SettingId")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AdLinkUrl" HeaderText="Link" />
                <asp:BoundField DataField="AdTarget" HeaderText="Target" />
                <asp:TemplateField HeaderText="Ad Display">
                    <ItemTemplate></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ToolTipText" HeaderText="Tool Tip" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate></ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        
        <%-- Actions toolbar --%>
        <div class="membersToggle" style="padding-bottom: 20px;">
            <asp:HyperLink ID="lnkCreate" runat="server" Text="New" ToolTip="Create New Ad Space." 
                rel="gb_page_center[525, 600]"  />
            <asp:LinkButton ID="btnEnable" runat="server" Text="Enable" ToolTip="Enable the selected Ads." />
            <asp:LinkButton ID="btnDisable" runat="server" Text="Disable" ToolTip="Disable the selected Ads." />
            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" ToolTip="Permanently delete the selected ads from the system."
                 OnClientClick="javascript:return confirm('This will permanently delete selected items from the system. Are you sure you want to do this?');" />
            <asp:LinkButton ID="btnRefresh" runat="server" Text="Refresh" ToolTip="Refresh the list" />
        </div><br />
    
        <asp:Label ID="lblActionMsgSuccess" runat="server" ForeColor="Green" />
        
    </asp:Panel>
    
</asp:Content>
