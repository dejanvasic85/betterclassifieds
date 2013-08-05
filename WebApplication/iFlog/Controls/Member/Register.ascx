<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Register.ascx.vb" Inherits="BetterclassifiedsWeb.Register1" %>

<asp:CreateUserWizard ID="SiteCreateUserWizard" runat="server"
    CssClass="wizardCreateUser" ActiveStepIndex="0">
    
    <StartNextButtonStyle CssClass="btn btn-default"></StartNextButtonStyle>
    <ContinueButtonStyle CssClass="btn btn-default"></ContinueButtonStyle>
    <CancelButtonStyle CssClass="btn btn-default"></CancelButtonStyle>
    <CreateUserButtonStyle CssClass="btn btn-default"></CreateUserButtonStyle>
    <FinishCompleteButtonStyle CssClass="btn btn-default"></FinishCompleteButtonStyle>
    <FinishPreviousButtonStyle CssClass="btn btn-default"></FinishPreviousButtonStyle>
    <NavigationButtonStyle CssClass="btn btn-default"></NavigationButtonStyle>
    <SideBarButtonStyle CssClass="btn btn-default"></SideBarButtonStyle>
    <StepNextButtonStyle CssClass="btn btn-default"></StepNextButtonStyle>

    <WizardSteps>
        <asp:WizardStep ID="wizCreateUser" runat="server">
           

            <div id="myAccountTableChgDet">
                <h3>Personal Details</h3>
            </div>

            <%-- first last name --%>
            <div id="myAccountTableChgDet">
                <table width="750" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                    <tr>
                        <td width="360">
                            <p>
                                First Name*
                            </p>
                        </td>
                        <td width="360">
                            <p>
                                Last Name*
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td width="360">
                            <asp:TextBox ID="FirstNameTextBox" runat="server" size="40%" />
                            <asp:RequiredFieldValidator ID="RequiredFirstNameTextBox" ControlToValidate="FirstNameTextBox"
                                runat="server" Text="*" ToolTip="Please enter your first name." />
                        </td>
                        <td width="360">
                            <asp:TextBox ID="LastNameTextBox" runat="server" size="40%" />
                            <asp:RequiredFieldValidator ID="RequiredLastNameTextBox" ControlToValidate="LastNameTextBox"
                                runat="server" Text="*" ToolTip="Please enter your last name." />
                        </td>
                    </tr>
                </table>
            </div>

            <%-- Address --%>
            <div id="myAccountTableChgDet">
                <table width="360" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                    <tr>
                        <td width="360">
                            <p>
                                Address*
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="Address1TextBox" runat="server" size="40%" />
                            <asp:RequiredFieldValidator ID="RequiredAddress1TextBox" ControlToValidate="Address1TextBox"
                                runat="server" Text="*" ToolTip="Please enter your street address." /></td>
                    </tr>
                </table>
            </div>

            <%-- suburb --%>
            <div id="myAccountTableChgDet">
                <table width="360" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                    <tr>
                        <td width="360">
                            <p>
                                Suburb*
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="CityTextBox" runat="server" size="40%" />
                            <asp:RequiredFieldValidator ID="RequiredCityTextBox" ControlToValidate="CityTextBox"
                                runat="server" Text="*" ToolTip="Please enter your city." /></td>
                    </tr>
                </table>
            </div>

            <%-- state territory --%>
            <div id="myAccountTableChgDet">
                <table width="750" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                    <tr>
                        <td width="360">
                            <p>State or Territory*</p>
                        </td>
                        <td width="360">
                            <p>Postcode*</p>
                        </td>
                    </tr>
                    <tr>
                        <td width="360">
                            <asp:DropDownList ID="StateDropDownList" runat="server" Width="40%">
                                <asp:ListItem>VIC</asp:ListItem>
                                <asp:ListItem>NSW</asp:ListItem>
                                <asp:ListItem>WA</asp:ListItem>
                                <asp:ListItem>QLD</asp:ListItem>
                                <asp:ListItem>ACT</asp:ListItem>
                                <asp:ListItem>NT</asp:ListItem>
                                <asp:ListItem>SA</asp:ListItem>
                                <asp:ListItem>TAS</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldState" ControlToValidate="StateDropDownList" runat="server" ToolTip="Please provide the state" />
                        </td>
                        <td width="360">
                            <asp:TextBox ID="ZipCodeTextBox" runat="server" size="10%" MaxLength="4" />
                            <ajax:FilteredTextBoxExtender ID="postCodeFilter" runat="server" TargetControlID="ZipCodeTextBox" FilterType="Numbers" />
                            <asp:RequiredFieldValidator ID="RequiredZipCodeTextBox" ControlToValidate="ZipCodeTextBox"
                                runat="server" Text="*" ToolTip="Please enter your zip code." />
                        </td>
                    </tr>
                </table>
            </div>

            <%--telephone number--%>
            <div id="myAccountTableChgDet">
                <table width="360" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                    <tr>
                        <td width="360">
                            <p>
                                Telephone Number*
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td width="360">
                            <asp:TextBox ID="PhoneNumberTextBox" runat="server" size="40%" />
                            <ajax:FilteredTextBoxExtender ID="Filteredtextboxextender1" runat="server" TargetControlID="PhoneNumberTextBox" FilterType="Numbers" />
                            <asp:RequiredFieldValidator ID="RequiredPhoneNumberTextBox" ControlToValidate="PhoneNumberTextBox"
                                runat="server" Text="*" ToolTip="Please enter your phone number." />
                        </td>
                    </tr>
                </table>
            </div>

            <%--secondary number--%>
            <div id="myAccountTableChgDet">
                <table width="360" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                    <tr>
                        <td width="360">
                            <p>
                                Secondary Telephone Number
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td width="360">
                            <asp:TextBox ID="SecondaryPhoneTextBox" runat="server" size="40%" />
                            <ajax:FilteredTextBoxExtender ID="Filteredtextboxextender2" runat="server" TargetControlID="SecondaryPhoneTextBox" FilterType="Numbers" />
                        </td>
                    </tr>
                </table>
            </div>

            <div id="myAccountTableChgDet">
                <h3>Business Details</h3>
            </div>

            <%--business name and abn--%>
            <div id="myAccountTableChgDet">
                <table width="750" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                    <tr>
                        <td width="360">
                            <p>
                                Company Name
                            </p>
                        </td>
                        <td width="360">
                            <p>
                                ABN
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td width="360">
                            <asp:TextBox ID="BusinessNameTextBox" runat="server" size="40%" />

                        </td>
                        <td width="360">
                            <asp:TextBox ID="ABNTextBox" runat="server" size="40%" />
                            <ajax:FilteredTextBoxExtender ID="Filteredtextboxextender3" runat="server" TargetControlID="ABNTextBox" FilterType="Numbers" />
                        </td>
                    </tr>
                </table>
            </div>

            <asp:UpdatePanel ID="pnlBusiness" runat="server">
                <ContentTemplate>

                    <%--business name and abn--%>
                    <div id="myAccountTableChgDet">
                        <table width="750" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                            <tr>
                                <td width="360">
                                    <p>
                                        Industry
                                    </p>
                                </td>
                                <td width="360">
                                    <p>
                                        Category
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td width="360">
                                    <asp:DropDownList ID="ddlIndustry" runat="server" AutoPostBack="True"
                                        DataTextField="Title" DataValueField="IndustryId" Width="300px" />
                                </td>
                                <td width="360">
                                    <asp:DropDownList ID="ddlBusinesscategory" runat="server" DataTextField="Title"
                                        DataValueField="BusinessCategoryId" Width="300px" />
                                </td>
                            </tr>
                        </table>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

        </asp:WizardStep>

        <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
            <ContentTemplate>

                <div id="myAccountTableChgDet">
                    <h3>Choose username and password</h3>
                </div>

                <%-- username --%>
                <div id="myAccountTableChgDet">
                    <table width="750" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                        <tr>
                            <td width="360">
                                <p>
                                    <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">
                                        <asp:Label ID="Label2" runat="server" Text="Create your username"></asp:Label>*</asp:Label>
                                </p>
                            </td>
                            <td width="360"></td>
                        </tr>
                        <tr>
                            <td width="360">
                                <asp:TextBox ID="UserName" runat="server" size="40%"></asp:TextBox>
                                <ajax:FilteredTextBoxExtender ID="usernameFilter" runat="server" FilterType="Custom" InvalidChars="`~!@#$%^&*()-_+=\|}]{[';<>,.?/" FilterMode="InvalidChars" TargetControlID="UserName" />
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server"
                                    ControlToValidate="UserName" ErrorMessage="User Name is required."
                                    ToolTip="User Name is required." ValidationGroup="SiteCreateUserWizard">*</asp:RequiredFieldValidator>
                            </td>
                            <td width="360">
                                <div class="verify">
                                    <asp:UpdatePanel ID="pnlCheckUsername" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="btnCheckUsername" runat="server" OnClick="Check_Username" CssClass="btn btn-info btn-mini">Check Availability</asp:LinkButton>
                                            <asp:Label ID="lblUsernameAvailability" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <asp:UpdateProgress ID="UpdateProgressPanel" runat="server" AssociatedUpdatePanelID="pnlCheckUsername">
                                        <ProgressTemplate>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Images/indicator.gif" Height="20" Width="20" /></td>
                                                    <td>
                                                        <asp:Label ID="lblProgress" runat="server" Text="Checking..." /></td>
                                                </tr>
                                            </table>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>

                <%-- password --%>
                <div id="myAccountTableChgDet">
                    <table width="360" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                        <tr>
                            <td width="360">
                                <p>
                                    <asp:Label ID="Label3" runat="server" AssociatedControlID="Password">
                                        <asp:Label ID="Label4" runat="server" Text="Create your password"></asp:Label>*</asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td width="360">
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" size="40%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server"
                                    ControlToValidate="Password" ErrorMessage="Password is required."
                                    ToolTip="Password is required." ValidationGroup="SiteCreateUserWizard">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="360">
                                <p style="font-size: 10px; font-weight: normal;">Password must be at least 6 characters</p>
                            </td>
                        </tr>
                    </table>
                </div>

                <%-- confirm password --%>
                <div id="myAccountTableChgDet">
                    <table width="360" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                        <tr>
                            <td width="360">
                                <p>
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">
                                        <asp:Label ID="Label22" runat="server" Text="Confirm Password"></asp:Label>*</asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td width="360">
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" size="40%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server"
                                    ControlToValidate="ConfirmPassword"
                                    ErrorMessage="Confirm Password is required."
                                    ToolTip="Confirm Password is required." ValidationGroup="SiteCreateUserWizard">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>

                <%-- Email --%>
                <div id="myAccountTableChgDet">
                    <table width="360" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                        <tr>
                            <td width="360">
                                <p>
                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">
                                        <asp:Label ID="Label23" runat="server" Text="E-mail"></asp:Label>*</asp:Label>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td width="360">
                                <asp:TextBox ID="Email" runat="server" size="40%"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ErrorMessage="please enter a valid email address" SetFocusOnError="True" ControlToValidate="Email"
                                    ToolTip="Invalid email address"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server"
                                    ControlToValidate="Email" ErrorMessage="E-mail is required."
                                    ToolTip="E-mail is required." ValidationGroup="SiteCreateUserWizard">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                    </table>
                </div>

                <%-- Confirm Email --%>
                <div id="myAccountTableChgDet">
                    <table width="360" border="0" cellspacing="10px 10px 0px 10px" cellpadding="0px">
                        <tr>
                            <td width="360">
                                <p>
                                    Confirm Email*
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td width="360">
                                <asp:TextBox ID="txtConfirmEmail" runat="server" size="40%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="Email" ErrorMessage="Confirm E-mail is required."
                                    ToolTip="Confirm E-mail is required." ValidationGroup="SiteCreateUserWizard">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>

                <%-- validation --%>
                <table border="0">
                    <tr>
                        <td align="center" colspan="2">
                            <asp:CompareValidator ID="PasswordCompare" runat="server"
                                ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                Display="Dynamic"
                                ErrorMessage="The Password and Confirmation Password must match."
                                ValidationGroup="SiteCreateUserWizard"></asp:CompareValidator>
                            <asp:CompareValidator ID="EmailCompare" runat="server"
                                ControlToCompare="Email" ControlToValidate="txtConfirmEmail"
                                Display="Dynamic"
                                ErrorMessage="The Email and Confirmation Email must match."
                                ValidationGroup="SiteCreateUserWizard" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="color: Red;">
                            <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>
        </asp:CreateUserWizardStep>

        <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            <ContentTemplate>
                <table border="0">
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="Label18" runat="server" Text="Complete"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="Your account has been successfully created."></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CssClass="btn btn-default"
                                CommandName="Continue" Text="Continue" ValidationGroup="SiteCreateUserWizard" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:CompleteWizardStep>
       

    </WizardSteps>

</asp:CreateUserWizard>
