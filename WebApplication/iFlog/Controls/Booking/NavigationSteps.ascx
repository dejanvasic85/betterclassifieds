<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="NavigationSteps.ascx.vb" Inherits="BetterclassifiedsWeb.NavigationSteps" %>

<asp:Panel ID="pnlSteps" runat="server">

    <asp:Image ID="imgheader" runat="server" ImageUrl="~/Resources/Images/new_ad_header.jpg" AlternateText="New iFlog Ad" />

    <ul>
        <li runat="server" id="liStep1">
            <asp:Panel ID="pnlStep1" runat="server">
                <h2>
                    <asp:HyperLink ID="lnkStep1" runat="server" Text="Step 1" NavigateUrl="~/Booking/Step1.aspx?action=back" /></h2>
            </asp:Panel>
        </li>
        
        <li runat="server" id="liStep2">
            <asp:Panel ID="pnlStep2" runat="server">
                <h2>
                    <asp:HyperLink ID="lnkStep2" runat="server" Text="Step 2" NavigateUrl="~/Booking/Step2.aspx?action=back" /></h2>
            </asp:Panel>
        </li>
        
        <li runat="server" id="liStep3">
            <asp:Panel ID="pnlStep3" runat="server">
                <h2>
                    <asp:HyperLink ID="lnkStep3" runat="server" Text="Step 3" NavigateUrl="~/Booking/Step3.aspx?action=back" /></h2>
            </asp:Panel>
        </li>
        
         <li runat="server" id="liStep4">
            <asp:Panel ID="pnlStep4" runat="server">
                <h2>
                    <asp:HyperLink ID="lnkStep4" runat="server" Text="Step 4" NavigateUrl="~/Booking/Step4.aspx?action=back" /></h2>
            </asp:Panel>
        </li>
        
         <li runat="server" id="liStep5">
            <asp:Panel ID="pnlStep5" runat="server">
                <h2>
                    <asp:HyperLink ID="lnkStep5" runat="server" Text="Step 5" NavigateUrl="~/Booking/Step5.aspx?action=back" /></h2>
            </asp:Panel>
        </li>
    </ul>

    <h3>
        <asp:Literal ID="litStepNumber" runat="server" /></h3>
    <h5>
        <asp:Literal ID="litTitle" runat="server"></asp:Literal></h5>
    <p>
        <asp:Literal ID="litDescription" runat="server"></asp:Literal></p>

</asp:Panel>