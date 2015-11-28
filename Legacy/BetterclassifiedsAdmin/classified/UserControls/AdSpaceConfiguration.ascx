<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AdSpaceConfiguration.ascx.vb" Inherits="BetterclassifiedAdmin.AdSpaceConfiguration" %>

<style type="text/css">
    .info{ color: Blue; font-weight: bold; margin-top: 15px; padding-bottom:10px;}
    .dTitle{ font-weight: bold; margin-bottom: 5px;}
</style>

<div class="dTitle>
    Current Ads
</div>
<asp:GridView ID="grdAds" runat="server" AutoGenerateColumns="false" 
        EmptyDataText="There are no current ads in this section." DataKeyNames="WebAdSpaceId">
    <Columns>
        <asp:BoundField DataField="WebAdSpaceId" HeaderText="ID" />
        <asp:BoundField DataField="Title" HeaderText="Title" />
        <asp:TemplateField HeaderText="Ad Display">
            <ItemTemplate></ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ToolTipText" HeaderText="Tool Tip" />
        <asp:BoundField DataField="AdLinkUrl" HeaderText="Link Url" />
        <asp:BoundField DataField="AdTarget" HeaderText="Target" />
        <asp:ButtonField Text="Remove" ButtonType="Link" CommandName="Remove" HeaderText="Remove" />
    </Columns>
</asp:GridView>

<div class="info">
    <asp:Label ID="lblLimit" runat="server" Text="" CssClass="info"></asp:Label>
</div>
<asp:Panel ID="pnlInput" runat="server">
    <div class="dTitle">
        Add New Advertisement
    </div>
    <asp:DetailsView ID="dtlWebSpace" runat="server" DefaultMode="Insert" CellPadding="5" Width="500px">
        <Fields>
            <asp:BoundField DataField="Title" HeaderText="Title" ControlStyle-Width="200px" />
            <asp:BoundField DataField="AdLinkUrl" HeaderText="Link Url" ControlStyle-Width="200px" />
            <asp:TemplateField HeaderText="Open Target">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlTarget" runat="server">
                        <asp:ListItem>_blank</asp:ListItem>
                        <asp:ListItem>_parent</asp:ListItem>
                        <asp:ListItem>_search</asp:ListItem>
                        <asp:ListItem>_self</asp:ListItem>
                        <asp:ListItem>_top</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Display Type">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlDisplayType" runat="server">
                        <asp:ListItem Text="Image" Value="1" />
                        <asp:ListItem Text="Text" Value="2" />
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Image" ControlStyle-Width="300px">
                <ItemTemplate>
                    <asp:FileUpload ID="fileUpload" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DisplayText" HeaderText="AdText" ControlStyle-Width="200px" />
            <asp:BoundField DataField="ToolTipText" HeaderText="Tool Tip" ControlStyle-Width="200px" />
        </Fields>
    </asp:DetailsView>
    
    <div class="membersToggle" style="width: 500px; margin-bottom: 20px;">
        <div style="float: right; padding-top: 6px;">
            <asp:LinkButton ID="btnAdd" runat="server" Text="Add" />
        </div>
    </div>
        
</asp:Panel>
