<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CreateRatecardControl.ascx.vb" Inherits="BetterclassifiedAdmin.CreateRatecardControl" %>

<asp:DetailsView ID="dtlRatecard" runat="server" AutoGenerateRows="false" HeaderText="Rate Details"
    DefaultMode="Insert" DataKeyNames="RatecardId" DataSourceID="linqSourceRatecard">
    <FieldHeaderStyle Width="250px" />
    <Fields>                          
        <asp:TemplateField HeaderText="Ratecard Name">
            <ItemTemplate>
                <telerik:RadTextBox ID="txtRatecardName" runat="server" Width="200px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Minimum Edition Charge">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtMinimumCharge" runat="server"
                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Maximum Edition Charge">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtMaximumCharge" runat="server"
                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Number Of Free Words">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtFreeWords" runat="server"
                    NumberFormat-DecimalDigits="0" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Rate Per Word">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtRatePerWord" runat="server"
                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Line Ad Heading Rate">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtLineAdHeadingValue" runat="server"
                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Line Ad Bold Heading Rate">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtLineAduperBoldHeadingValue" runat="server"
                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Line Ad Colour Heading Rate">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtLineAdColourHeadingValue" runat="server"
                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Line Ad Colour Border Rate">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtLineAdColourBorderValue" runat="server"
                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Line Ad Colour Background Rate">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtLineAdColourBackgroundValue" runat="server"
                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Line Ad Main Image Rate">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtLineAdMainImageValue" runat="server"
                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Line Ad Extra Image Rate">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtLineAdExtraImage" runat="server"
                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="Online Ad">
            <ItemTemplate>
                <telerik:RadNumericTextBox ID="txtOnlineAdBundleCharge" runat="server"
                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" />
            </ItemTemplate>
        </asp:TemplateField>--%>
    </Fields>
</asp:DetailsView>
                    
<asp:LinqDataSource ID="linqSourceRatecard" runat="server" EnableObjectTracking="false"
                    ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" 
                    TableName="Ratecards" EnableInsert="true" >               
</asp:LinqDataSource>