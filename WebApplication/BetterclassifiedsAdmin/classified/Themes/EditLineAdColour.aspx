<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/Dialog.Master" 
    CodeBehind="EditLineAdColour.aspx.vb" Inherits="BetterclassifiedAdmin.EditLineAdColour" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:Panel ID="pnlDetails" runat="server">
        <asp:DetailsView ID="dtlLineAdColour" runat="server" HeaderText="Edit Colour Details"
            DefaultMode="Edit" AutoGenerateRows="false" DataKeyNames="LineAdColourId"
            DataSourceID="srcLineAdColour">
            <Fields>
                <asp:BoundField HeaderText="Colour Name" DataField="LineAdColourName" />
                <asp:TemplateField HeaderText="Colour" HeaderStyle-VerticalAlign="Top">
                    <EditItemTemplate>
                        <telerik:RadColorPicker ShowIcon="true" ID="colourPicker" runat="server" PaletteModes="All" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cyan Code" HeaderStyle-VerticalAlign="Top">
                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="txtCyanCode" runat="server" NumberFormat-DecimalDigits="0"
                            Value='<%# Convert.ToDouble(Eval("Cyan")) %>'  />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Magent Code" HeaderStyle-VerticalAlign="Top">
                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="txtMagentaCode" runat="server" NumberFormat-DecimalDigits="0"
                            Value='<%# Convert.ToDouble(Eval("Magenta")) %>'  />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Yellow Code" HeaderStyle-VerticalAlign="Top">
                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="txtYellowCode" runat="server"  NumberFormat-DecimalDigits="0"
                            Value='<%# Convert.ToDouble(Eval("Yellow")) %>'  />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Key" HeaderStyle-VerticalAlign="Top">
                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="txtKeyCode" runat="server" NumberFormat-DecimalDigits="0"
                            Value='<%# Convert.ToDouble(Eval("KeyCode")) %>'  />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sort Order" HeaderStyle-VerticalAlign="Top">
                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="txtSortOrder" runat="server"  NumberFormat-DecimalDigits="0"
                            Value='<%# Convert.ToDouble(Eval("SortOrder")) %>'  />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Fields>
        </asp:DetailsView>
        <asp:ObjectDataSource ID="srcLineAdColour" runat="server"
            TypeName="BetterClassified.UIController.LineAdController"
            UpdateMethod="UpdateLineAdColour"
            SelectMethod="GetLineAdColour"
            EnablePaging="false" />
        
        <div class="page-commandPanel">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" />
            <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="javascript:cancelRadWindow(); " />
        </div>
        <br />
    </asp:Panel>
</asp:Content>
