<%@ Page Title="iFlog Member Details" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.Master" CodeBehind="Default.aspx.vb" Inherits="BetterclassifiedsWeb._Default3"  %>


<asp:Content ID="ContentHead" ContentPlaceHolderID="memberHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">

    <div id="mainHeaderMyAccount">
        <asp:Image ID="imgAccHeader" runat="server" ImageUrl="~/Resources/Images/my_account_header.gif" AlternateText="My Account" />
        <h2>
            Welcome, 
            <asp:LoginName ID="LoginName1" runat="server"  />
            <paramountIt:MessageNotifyControl ID="notifyControl" runat="server" />
        </h2>
            <h3>
                <asp:Label ID="lblHeader" runat="server" Text="Your Account Details"></asp:Label></h3>
    </div>
    
    <div id="mainContentMyAccount">
        <%--First name--%>
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                First Name:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblFirstName" runat="server" /></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div> 
        
        <%--Last name--%>
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                Last Name:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblLastName" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
           
           
        <%--Street Address--%>              
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                Street Address:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblStreetAddress" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                    <tr>
                        <td width="160">
                            <p>
                                &nbsp;</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblSuburb" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <%--State/Territory--%>
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                State or Territory:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblState" runat="server" Text="Label"></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <%--Post Code--%>
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                Postcode:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblPostCode" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <%--Telephone Number--%>
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                Telephone Number:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblTelephone" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <%--Mobile Number--%>
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                Secondary Telephone:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblSecondaryTelephone" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <%--Email--%>
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                Email Address:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <div id="myAccountTableChgDet">
            <h3>
                Business Details</h3>
        </div>
        
        <%--Company Name--%>
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                Company Name:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <%--ABN--%>
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                 ABN:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblABN" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <%--Industry --%>
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                Industry:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblIndustry" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        <%--Category --%>
        <div id="myAccountTableChgDet">
            <div class="floatLeftBlock">
                <table width="520" border="0" cellspacing="0px" cellpadding="0px">
                    <tr>
                        <td width="160">
                            <p>
                                Category:</p>
                        </td>
                        <td width="360">
                            <p>
                                <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label></p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        
    </div>

</asp:Content>
