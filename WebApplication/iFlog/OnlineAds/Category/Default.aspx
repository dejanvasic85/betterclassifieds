<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master" CodeBehind="Default.aspx.vb" Inherits="BetterclassifiedsWeb._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="grdSearchResults"  DataSourceID="objSourceResults" runat="server" 
        AutoGenerateColumns="False" PagerSettings-Position="TopAndBottom"
        AllowPaging="True" GridLines="Horizontal" EmptyDataText="No results found" CellPadding="5"
        BackColor="White" BorderColor="#999999" BorderStyle="None">
        <Columns>
        
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HiddenField ID="hdnOnlineAdId" runat="server" Value='<%# Eval("OnlineAdId") %>' />
                    <table cellpadding="2">
                        <tr>
                            <td colspan="2">
                                <div class="ad-header">
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Heading") %>' NavigateUrl='<%# String.Format("{0}{1}", "~/OnlineAds/AdView.aspx?preview=false&id=", Eval("OnlineAdId")) %>'
                                        Font-Bold="true" />
                                </div>
                            </td>
                        </tr>
                        <tr align="left" valign="middle">
                            <td align="left">
                                <asp:Image ID="imgDocument" runat="server" ImageUrl='<% Eval("ImageUrl") %>' Height="80"
                                    Width="100" />
                            </td>
                            <td align="left" valign="top">
                            <div class="grid-description">
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("AdText") %>'  />
                                </div>
                                <div>
                                <asp:Label ID="Label3" runat="server" Text='<%# String.Format("{0}{1}{2}", "Viewed: ", Eval("NumOfViews"), " times") %>'
                                    Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </div>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="Location" ItemStyle-HorizontalAlign="Right" >
<ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Category" ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Posted" DataFormatString="{0:dd/MM/yy}" 
                ItemStyle-HorizontalAlign="Center" >
            
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>
            
        </Columns>
        <FooterStyle BackColor="White" />
        <PagerStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
<PagerSettings Position="TopAndBottom"></PagerSettings>

        <RowStyle BackColor="White" />
        <AlternatingRowStyle BackColor="White" />
        
    </asp:GridView>

    <asp:ObjectDataSource ID="objSourceResults" runat="server"
            SelectMethod="SearchOnlineAdByCategory" 
            SelectCountMethod="SearchOnlineAdByCategoryCount"
            TypeName="BetterclassifiedsCore.DataModel.Search" 
            OldValuesParameterFormatString="original_{0}" EnablePaging="True">
        <SelectParameters>
            <asp:QueryStringParameter Name="categoryId" QueryStringField="categoryId" Type="Int32" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        
    </asp:ObjectDataSource>
    

</asp:Content>
