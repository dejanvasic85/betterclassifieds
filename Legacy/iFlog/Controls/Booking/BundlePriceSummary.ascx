<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BundlePriceSummary.ascx.vb" Inherits="BetterclassifiedsWeb.BundlePriceSummary" %>

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
            <div style="font-size: 12px;" id="divItemContainer" runat="server">
                <table cellpadding="3" width="100%">
                    <tr>
                        <td style="width: 40%;" align="right">Publication :</td>
                        <td><asp:Label ID="lblPaper" runat="server" Text='<%# Eval("PaperName") %>' Font-Bold="true" /></td>
                    </tr>
                    <tr>
                        <td align="right">Category :</td>
                        <td><asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>' Font-Bold="true" /></td>
                    </tr>
                    <tr>
                        <td align="right">Ad Type :</td>
                        <td><asp:Label ID="lblAdType" runat="server" Text='<%# Eval("AdType").ToString %>'></asp:Label></td>
                    </tr>
                    <tr id="trMinCharge" runat="server">
                        <td align="right">Base Price :</td>
                        <td><asp:Label ID="lblMinimumCharge" runat="server" Text='<%# String.Format("{0:C}", Eval("MinimumCharge")) %>' /></td>
                    </tr>
                </table>
                
                <asp:Panel ID="pnlLineAd" runat="server">
                    <table cellpadding="3" width="100%">
                        <tr>
                            <td align="right" style="width: 40%;">Words Counted :</td>
                            <td><asp:Label ID="numOfWords" runat="server" Text='<%# Eval("NumOfWords") %>' /></td>
                        </tr>
                        <tr>
                            <td align="right">Rate Per Word :</td>
                            <td><asp:Literal ID="lblPricePerWord" runat="server" Text='<%# String.Format("{0:C}", Eval("RatePerUnit")) %>' /></td>
                        </tr>
                        <tr id="trShowHeading" runat="server">
                            <td align="right">Bold Heading :</td>
                            <td><asp:Label ID="Label5" runat="server" Text='<%# String.Format("{0:C}", Eval("BoldHeading")) %>'></asp:Label></td>
                        </tr>
                        <tr id="trShowImage" runat="server">
                            <td align="right">Photo Charge :</td>
                            <td><asp:Label ID="Label3" runat="server" Text='<%# String.Format("{0:C}", Eval("PhotoCharge")) %>'></asp:Label></td>
                        </tr>
                    </table>
                </asp:Panel>
                
                <table cellpadding="3"  width="100%">
                    <tr>
                        <td align="right" style="width: 40%;">Paper Total :</td>
                        <td><asp:Label ID="lblPaperTotal" runat="server" Text='<%# String.Format("{0:C}", Eval("PaperPrice")) %>' Font-Bold="true" /></td>
                    </tr>
                </table>
            </div>
            <asp:Label ID="lblOtherInfo" runat="server" Text="Online Ad Price is fixed and will be added on top of the Total Price at the end." Font-Size="Smaller" Font-Italic="true" Font-Bold="true" Visible="false"></asp:Label>
        </ItemTemplate>
        
    </asp:DataList>
    <table cellpadding="3" style="width: 100%;">
        <tr>
            <td align="right" style="width: 40%;">Sub Total : </td>
            <td><asp:Label ID="lblSummaryPrice" runat="server" Font-Bold="true" Font-Underline="true" Font-Size="Medium" /> &nbsp; <asp:Label ID="Label1" runat="server" Text="(per edition)" Font-Italic="true"></asp:Label></td>
        </tr>
    </table>
</div>