<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="InvoiceGrid.ascx.vb"
    Inherits="BetterclassifiedsWeb.InvoiceGrid" %>
<%--<div style="text-align: right">
    <asp:Button ID="Button1" runat="server" Text="Print" Style="height: 26px" />
</div>--%>
<asp:UpdatePanel runat="server" ID="pnlUpdatePanel">
    <ContentTemplate>
        <telerik:RadGrid ID="radgrid1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1"
            Skin="Sunset">
            <MasterTableView DataSourceID="ObjectDataSource1" GroupsDefaultExpanded="false">
                <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="BookReference" HeaderText="Reference Id" FieldAlias="Ref" />
                        </GroupByFields>
                        <SelectFields>
                            <telerik:GridGroupByField FieldName="BookReference" HeaderText="Reference Id" />
                            <telerik:GridGroupByField FieldName="TransactionDate" HeaderText="Date" FormatString="{0:dd/MM/yyyy}" />
                            <telerik:GridGroupByField FieldName="TotalPrice" HeaderText="Total Price" FormatString="{0:c}" />
                        </SelectFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
                <RowIndicatorColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                </RowIndicatorColumn>
                <ExpandCollapseColumn CommandName="opo">
                    <HeaderStyle Width="20px"></HeaderStyle>
                </ExpandCollapseColumn>
                <Columns>
                    <%-- <telerik:gridboundcolumn datafield="BookReference" headertext="Book Reference"></telerik:gridboundcolumn>--%>
                    <telerik:GridBoundColumn DataField="Publication" HeaderText="Publication">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Category" HeaderText="Category">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EditionDate" HeaderText="Running Date" DataFormatString="{0:dd/MM/yyyy}">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <HeaderContextMenu EnableTheming="True">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </HeaderContextMenu>
            <ExportSettings OpenInNewWindow="true">
                <Pdf Producer="Spress Media" AllowPrinting="true" />
            </ExportSettings>
            <FilterMenu EnableTheming="True">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </FilterMenu>
        </telerik:RadGrid>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetInvoice"
            TypeName="BetterclassifiedsCore.BookingController">
            <SelectParameters>
                <asp:Parameter Name="userId" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </ContentTemplate>
</asp:UpdatePanel>
