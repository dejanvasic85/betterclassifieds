<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SetupAdSpace.aspx.vb"
    Inherits="BetterclassifiedAdmin.SetupAdSpace" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 20px;">
        <div style="padding: 10px;">
            <asp:DetailsView ID="dtlAdSpace" runat="server" AutoGenerateRows="false" 
                HeaderText="Configure Ad Space Details"
                DataKeyNames="WebAdSpaceId" DataSourceID="lnqSource">
                <Fields>
                    <asp:BoundField HeaderText="Title" DataField="Title" />
                    <asp:TemplateField HeaderText="Sort Order" ControlStyle-Width="200px">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlSortOrder" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Ad Link Url" DataField="AdLinkUrl" />
                    <asp:TemplateField HeaderText="Target">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlTarget" runat="server">
                                <asp:ListItem Text="New Window" Value="_blank" />
                                <asp:ListItem Text="Same Window" Value="_top" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Tool Tip" DataField="ToolTipText" ConvertEmptyStringToNull="true" />
                    <asp:CheckBoxField HeaderText="Enabled" DataField="Active" />
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="False">
                                <asp:ListItem Text="Image" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Text" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Ad Text" DataField="DisplayText" />
                    <asp:TemplateField HeaderText="Ad Image">
                        <ItemTemplate>
                            <asp:FileUpload ID="fileUpload" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
            <asp:LinqDataSource ID="lnqSource" runat="server" EnableUpdate="true" EnableInsert="true" 
                EnableObjectTracking="false"
                ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext"
                TableName="WebAdSpaces" />
            
            <div style="padding: 10px;" runat="server" id="divImage" visible="false">
                <asp:Image ID="imgDisplay" runat="server" />
                <br />
                <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove Image" />
            </div>
            
        </div>
        <div class="controlDiv">
            <asp:Button ID="btnUpdate" runat="server" Text="Submit" />
        </div>
        <br />
        <div>
            <asp:Label ID="lblMsh" runat="server" />
        </div>
        
        <div style="padding: 10px; color: Blue; font-size: 10px;">
            <b>Please note:</b>
            <ul>
                <li>Do not include http:// within the Ad Link Url.</li>
                <li>If Type Text is selected then Ad Text is used, otherwise an Image is used.</li>
                <li>Please click on the Remove Image link before uploading a new one.</li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
