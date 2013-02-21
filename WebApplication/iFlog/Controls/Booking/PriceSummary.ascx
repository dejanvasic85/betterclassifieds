<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PriceSummary.ascx.vb"
    Inherits="BetterclassifiedsWeb.PriceSummary" %>

<div style=" height: 400px; overflow: auto;">
    <asp:DataList ID="lstPrices" runat="server" CellPadding="4" ForeColor="#333333" Width="100%">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderTemplate>
            <div align="center"><asp:Label ID="summary" runat="server" Text="Price Summary" /></div> 
        </HeaderTemplate>
        
        <ItemTemplate>
            
            <table cellpadding="3" style="font-size: 12px;">
                <tr>
                    <th colspan="2"><asp:Label ID="lblPaper" runat="server" Text='<%# Eval("PaperName") %>' Font-Bold="true" />, 
                        <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>' Font-Bold="true" /></th>
                </tr>
                
                <tr>
                    <td align="right">
                        <asp:Label ID="Label6" runat="server" Text="Type of Ad"></asp:Label>:</td>
                    <% If AdType = BetterclassifiedsCore.SystemAdType.LINE Then%>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Line Ad"></asp:Label></td>
                    <% ElseIf AdType = BetterclassifiedsCore.SystemAdType.ONLINE Then%>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Online Ad"></asp:Label></td>
                    <% End If%>
                </tr>
                
                <% If AdType = BetterclassifiedsCore.SystemAdType.LINE Then%>
                <tr runat="server" id="divMinCharge">
                    <td align="right">
                        <asp:Label ID="lblMinCharge" runat="server" Text="Base Price" />:</td>
                    <td>
                        <asp:Label ID="lblMinimumCharge" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="wordsCounted" runat="server" Text="Words Counted" />:</td>
                    <td><asp:Label ID="numOfWords" runat="server" Text='<%# Eval("NumOfWords") %>' /></td>
                </tr>
                <tr>
                    <td align="right">Rate Per Word:</td>
                    <td>
                        <asp:Literal ID="lblPricePerWord" runat="server" Text='<%# String.Format("{0:C}", Eval("RatePerUnit")) %>' /></td>
                </tr>
                <% End If%>
                
                <% If ShowHeader Then%>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label4" runat="server" Text="Bold Heading"></asp:Label>:</td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text='<%# String.Format("{0:C}", Eval("BoldHeading")) %>'></asp:Label></td>
                </tr>
                <% End If%>
                
                <% If ShowImage = True Then%>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label2" runat="server" Text="Photo Charge"></asp:Label>:</td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text='<%# String.Format("{0:C}", Eval("PhotoCharge")) %>'></asp:Label></td>
                </tr>
                <% End If%>
                
                <% If AdType = BetterclassifiedsCore.SystemAdType.LINE Then%>
                <tr style="font-weight: bold;">
                    <td align="right">
                        <asp:Literal ID="Literal1" runat="server" Text='<%# String.Format("{0} Total", Eval("PaperName")) %>' />:
                    </td>
                    <td>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# String.Format("{0:C}", Eval("PaperPrice")) %>' />
                        <span style="font-size: 10px; font-style: italic;">
                            <asp:Literal ID="lblMaxCharge" runat="server"></asp:Literal>
                        </span></td>
                </tr>
                <% End If%>
                
            </table>    
        </ItemTemplate>
        
    </asp:DataList>
    <br />
    <asp:Label ID="summaryPrice" runat="server" Text="Sub Total" /> &nbsp;
    <asp:Label ID="lblSummaryPrice" runat="server" Font-Bold="true" Font-Underline="true" Font-Size="Medium" />
</div>