<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Create_LineAdColour.aspx.vb" Inherits="BetterclassifiedAdmin.Create_LineAdColour" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Colour</title>
    <script type="text/javascript">

        // Cancel dialog (take no action)
        function cancelRadWindow() {
            var oWindow = getCurrentRadWindow();
            oWindow.argument = null;
            oWindow.close();
        }

        function getCurrentRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
        <asp:Panel ID="pnlDetails" runat="server">
            <asp:DetailsView ID="dtlLineAdColour" runat="server" HeaderText="New Colour"
                DefaultMode="Insert" AutoGenerateRows="false" DataKeyNames="LineAdColourId"
                DataSourceID="srcLinq">
                <Fields>
                    <asp:BoundField HeaderText="Colour Name" DataField="LineAdColourName" />
                    <asp:TemplateField HeaderText="Colour" HeaderStyle-VerticalAlign="Top">
                        <InsertItemTemplate>
                            <telerik:RadColorPicker ShowIcon="true" ID="colourPicker" runat="server" PaletteModes="All" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cyan Code" HeaderStyle-VerticalAlign="Top">
                        <InsertItemTemplate>
                            <telerik:RadNumericTextBox ID="txtCyanCode" runat="server" NumberFormat-DecimalDigits="0" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Magenta Code" HeaderStyle-VerticalAlign="Top">
                        <InsertItemTemplate>
                            <telerik:RadNumericTextBox ID="txtMagentaCode" runat="server" NumberFormat-DecimalDigits="0" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Yellow Code" HeaderStyle-VerticalAlign="Top">
                        <InsertItemTemplate>
                            <telerik:RadNumericTextBox ID="txtYellowCode" runat="server" NumberFormat-DecimalDigits="0" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Key" HeaderStyle-VerticalAlign="Top">
                        <InsertItemTemplate>
                            <telerik:RadNumericTextBox ID="txtKeyCode" runat="server" NumberFormat-DecimalDigits="0" />
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Sort Order" DataField="SortOrder" />
                </Fields>
            </asp:DetailsView>
            <asp:LinqDataSource ID="srcLinq" runat="server" EnableObjectTracking="false"
                    ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext"
                    TableName="LineAdColourCodes" EnableInsert="true" />
            <div style="float: right; margin-top: 10px;">
                <asp:Button ID="btnCreate" runat="server" Text="Save And New" />
                <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="javascript:cancelRadWindow(); " />
            </div>
            <br />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
