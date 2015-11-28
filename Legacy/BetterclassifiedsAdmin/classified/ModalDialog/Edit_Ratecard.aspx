<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit_Ratecard.aspx.vb" MasterPageFile="~/masterpage/Dialog.Master" Inherits="BetterclassifiedAdmin.Edit_Ratecard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/classified/UserControls/AssignRateCardControl.ascx" TagPrefix="ucx" TagName="AssignRates" %>

<asp:Content ContentPlaceHolderID="head" ID="cntHead" runat="server">
    <title>Edit Ratecard</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyContent" ID="cntBody" runat="server">

<ajax:TabContainer ID="tcntUserInfo" runat="server" ActiveTabIndex="0" 
    Width="100%" Font-Size="10px">
                    
    <ajax:TabPanel ID="TabPanel2" runat="server" HeaderText="General Info">
        <ContentTemplate>
            <div class="tabContainer">
                <asp:DetailsView ID="dtlRatecard" runat="server" AutoGenerateRows="false" HeaderText="Rate Details"
                                    DefaultMode="Edit" DataKeyNames="RatecardId" DataSourceID="objSourceRateCard">
                    <FieldHeaderStyle Width="250px" />
                    <Fields>                          
                        <asp:TemplateField HeaderText="Ratecard Name">
                            <ItemTemplate>
                                <telerik:RadTextBox ID="txtRatecardName" runat="server" Width="200px" 
                                    Text='<%# Eval("RateCardName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Minimum Edition Charge">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtMinimumCharge" runat="server"
                                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px"
                                    Value='<%# Convert.ToDouble(Eval("MinCharge")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Maximum Edition Charge">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtMaximumCharge" runat="server"
                                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px"
                                    Value='<%# Convert.ToDouble(Eval("MaxCharge")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Number Of Free Words">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtFreeWords" runat="server"
                                    NumberFormat-DecimalDigits="0" MaxValue="100000" Width="50px"
                                    Value='<%# Convert.ToDouble(Eval("MeasureUnitLimit")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate Per Word">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtRatePerWord" runat="server"
                                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px" 
                                    Value='<%# Convert.ToDouble(Eval("RatePerMeasureUnit")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Line Ad Heading Rate">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtLineAdHeadingValue" runat="server"
                                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px"
                                    Value='<%# Convert.ToDouble(Eval("BoldHeading")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Line Ad Bold Heading Rate">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtLineAduperBoldHeadingValue" runat="server"
                                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px"
                                    Value='<%# Convert.ToDouble(Eval("LineAdSuperBoldHeading")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Line Ad Colour Heading Rate">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtLineAdColourHeadingValue" runat="server"
                                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px"
                                    Value='<%# Convert.ToDouble(Eval("LineAdColourHeading")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Line Ad Colour Border Rate">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtLineAdColourBorderValue" runat="server"
                                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px"
                                    Value='<%# Convert.ToDouble(Eval("LineAdColourBorder")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Line Ad Colour Background Rate">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtLineAdColourBackgroundValue" runat="server"
                                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px"
                                    Value='<%# Convert.ToDouble(Eval("LineAdColourBackground")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Line Ad Main Image Rate">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtLineAdMainImageValue" runat="server"
                                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px"
                                    Value='<%# Convert.ToDouble(Eval("PhotoCharge")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Line Ad Extra Image Rate">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtLineAdExtraImage" runat="server"
                                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px"
                                    Value='<%# Convert.ToDouble(Eval("LineAdExtraImage")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Online Ad Rate">
                            <ItemTemplate>
                                <telerik:RadNumericTextBox ID="txtOnlineAdBundleCharge" runat="server"
                                    NumberFormat-DecimalDigits="2" MaxValue="100000" Width="50px"
                                    Value='<%# Convert.ToDouble(Eval("OnlineEditionBundle")) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Fields>
                </asp:DetailsView>
                        
                <asp:ObjectDataSource ID="objSourceRateCard" runat="server" EnablePaging="false"
                        TypeName="BetterClassified.UIController.RateController"
                        SelectMethod="GetRateCard"
                        UpdateMethod="UpdateRatecard" />

                <div class="page-commandPanel">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update Details" /></div>
                                    
            </div>
        </ContentTemplate>
    </ajax:TabPanel>
                    
    <ajax:TabPanel ID="TabPanel3" runat="server" HeaderText="Assigning">
        <ContentTemplate>
            <div class="tabContainer">
                <ucx:AssignRates ID="ucxAssignRates" runat="server" />
                <div class="page-commandPanel">
                    <asp:Button ID="btnUpdatePubCategories" runat="server" Text="Update Ratecard Assigning" />
                </div>
            </div>
        </ContentTemplate>
    </ajax:TabPanel>
</ajax:TabContainer>
    
</asp:Content>