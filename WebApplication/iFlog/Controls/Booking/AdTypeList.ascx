<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AdTypeList.ascx.vb" Inherits="BetterclassifiedsWeb.AdTypeList" %>

<asp:UpdatePanel ID="pnlUpdate1" runat="server">
    
    <ContentTemplate>
        
        <asp:GridView ID="grdAdList" runat="server" AutoGenerateColumns="False" CellSpacing="0" CellPadding="10"
            GridLines="None" ShowHeader="False" DataKeyNames="AdTypeId" Width="520px" Align="center">
            
            <Columns>
                
                <asp:TemplateField ItemStyle-Width="20" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Image ID="imgRightArrow" runat="server" 
                            ImageUrl="~/Resources/Images/blue_arrow_right.jpg" AlternateText="blue_arrow" />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-Width="235">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("AdTypeId") %>' />
                        <h4><asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>' /></h4>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-Width="155">
                    <ItemTemplate>
                        <asp:Image ID="imgAdType" runat="server" 
                            ImageUrl='<%# String.Format("{0}{1}", "~/Resources/Images/AdTypes/", Eval("ImageUrl")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField ItemStyle-Width="20">
                    <ItemTemplate>
                        <asp:RadioButton ID="rdoTypeSelect" runat="server" AutoPostBack="true" OnCheckedChanged="rdoTypeSelect" />
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
            
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdateProgress ID="pnlProgressUpdatePrice" runat="server" AssociatedUpdatePanelID="pnlUpdate1" DisplayAfter="2000">
    <ProgressTemplate>
        <asp:Image ID="imgProgress" runat="server" ImageUrl="~/Resources/Images/indicator.gif" />&nbsp;
        Please Wait...
    </ProgressTemplate>
</asp:UpdateProgress>