<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PaperListSelection.ascx.vb" Inherits="BetterclassifiedsWeb.PaperListSelection" %>

    <asp:GridView ID="grdPapers" runat="server" AutoGenerateColumns="False" Width="520px" align="center"
        DataKeyNames="PublicationId" GridLines="None" ShowHeader="false" CellPadding="10" CellSpacing="0">
        
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Image ID="imgPaper" runat="server" style="max-width: 180px" />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField ItemStyle-Width="240">
                <ItemTemplate>
                    <asp:Label ID="lblPaperName" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField ItemStyle-Width="40">
                <ItemTemplate>
                
                    <% If AllowMultiplePapers = True Then%>
                    
                        <asp:CheckBox ID="chkPaper" runat="server" Text="" CssClass='<%# Eval("Title") %>' />
                        <asp:HiddenField ID="hdnPaperId" runat="server" Value='<%# Eval("PublicationId") %>' />
                    
                    <% Else%>
                    
                        <input id="RadioButton" name="rdoPaperId" type="radio" value='<%# Eval("PublicationId") %>' />
                    
                    <% End If %>
                    
                </ItemTemplate>    
            </asp:TemplateField>
            
        </Columns>
        
    </asp:GridView>


