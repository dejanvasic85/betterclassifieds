namespace BetterClassified.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using AjaxControlToolkit;
    using BetterclassifiedsCore;
    using Paramount.Common.UI;
    using Paramount.Common.UI.BaseControls;
    using UIController.Constants;
    using Telerik.Web.UI;

    public class SendMessageControl : ParamountCompositeControl
    {
        private const string ValidationGroup = "messaging";
        protected ValidationSummary validationSumary;

        protected Label successText;

        protected TextBox messageBox;

        protected TextBox emailBox;

        protected RadCaptcha captcha;

        protected CustomValidator validator;
        protected RegularExpressionValidator emailValidator;
        protected RequiredFieldValidator emailRequiredField;
        protected RequiredFieldValidator messageRequiredField;
        protected RequiredFieldValidator nameRequired;

        protected TextBox nameBox;

        protected TextBox phoneBox;

        protected Button clearButton;
        protected Button submitButton;
        protected Button backButton;
        public event EventHandler BackClick;

        private void InvokeBackClick(EventArgs e)
        {
            EventHandler handler = BackClick;
            if (handler != null) handler(this, e);
        }

        public event EventHandler OnSubmit;

        private void InvokeOnSubmit(EventArgs e)
        {
            EventHandler handler = OnSubmit;
            if (handler != null) handler(this, e);
        }

        public SendMessageControl()
        {
            this.validationSumary = new ValidationSummary { ValidationGroup = ValidationGroup };
            this.emailValidator = new RegularExpressionValidator
            {
                ValidationExpression = ValidationExpressionFormats.EmailValidation,
                SetFocusOnError = true,
                ErrorMessage =
                    GetResources(EntityGroup.OnlineAdMessaging, ContentItem.Form,
                                 "invalidEmail.Text"),
                Text = "&nbsp;",
                ValidationGroup = ValidationGroup
            };
            nameRequired = new RequiredFieldValidator
            {
                SetFocusOnError = true,
                ErrorMessage =
                    GetResources(EntityGroup.OnlineAdMessaging, ContentItem.Form,
                                 "invalidName.Text"),
                Text = "&nbsp;",
                ValidationGroup = ValidationGroup
            };
            this.emailRequiredField = new RequiredFieldValidator
            {
                SetFocusOnError = true,
                ErrorMessage =
                    GetResources(EntityGroup.OnlineAdMessaging, ContentItem.Form,
                                 "invalidEmail.Text"),
                Text = "&nbsp;",
                ValidationGroup = ValidationGroup
            };
            this.messageRequiredField = new RequiredFieldValidator
            {
                SetFocusOnError = true,
                Text = "&nbsp;",
                ErrorMessage =
                    GetResources(EntityGroup.OnlineAdMessaging, ContentItem.Form,
                                 "messageRequired.Text"),
                ValidationGroup = ValidationGroup
            };
            this.successText = new Label { CssClass = "inner-text" };

            this.nameBox = new TextBox { CssClass = "input-text full", ID = "nameBox", MaxLength = 100 };


            this.messageBox = new TextBox
            {
                TextMode = TextBoxMode.MultiLine,
                CssClass = "textFieldInput full",
                // Columns = 40,
                Rows = 5,
                ID = "messageBox"
            };

            this.emailBox = new TextBox { CssClass = "input-text full", ID = "emailBox", MaxLength = 50 };
            this.captcha = new RadCaptcha
            {
                ErrorMessage = "The code you entered is not valid.",
                Display = ValidatorDisplay.Dynamic,
                Width = Unit.Pixel(150)
            };

            this.captcha.Style.Add("float", "left");

            this.phoneBox = new TextBox { CssClass = "input-text full", MaxLength = 12, ID = "phoneBox" };

            this.clearButton = new Button
            {
                Text =
                    GetResources(EntityGroup.OnlineAdMessaging, ContentItem.Form, "clear.Text"),
                CausesValidation = false,
                CssClass = "btn radius"
            };
            this.backButton = new Button
            {
                CausesValidation = false,
                Text = GetResources(EntityGroup.OnlineAdMessaging, ContentItem.Form,
                    "back.Text"),
                CssClass = "btn radius"
            };
            this.submitButton = new Button
            {
                Text =
                    GetResources(EntityGroup.OnlineAdMessaging, ContentItem.Form,
                                 "submit.Text"),
                ValidationGroup = ValidationGroup,
                CssClass = "btn radius"
            };

            this.clearButton.Click += ClearButtonClick;
            this.submitButton.Click += SubmitButtonClick;
            this.backButton.Click += BackButtonClick;
            this.validator = new CustomValidator { Text = "&nbsp;", ValidationGroup = ValidationGroup };
            this.validator.ServerValidate += ValidatorServerValidate;
        }

        public int? AdId
        {
            get
            {
                try
                {
                    int adId;
                    if (!int.TryParse(GetQueryString(PageConstant.AdNumber), out adId))
                    {
                        return null;
                    }
                    var idType = GetQueryString(PageConstant.AdIdType);

                    if (idType == "dsId")
                    {
                        var onlineAd = AdController.GetOnlineAdEntityByDesign(adId, false);
                        if (onlineAd != null)
                        {
                            return onlineAd.OnlineAdId;
                        }
                    }

                    return adId;
                }
                catch (FormatException)
                {
                    // in case user provided not a number
                    return null;
                }
            }
        }

        protected void Clear()
        {
            //this.titleBox.Text = string.Empty;
            this.nameBox.Text = string.Empty;
            this.phoneBox.Text = string.Empty;
            this.emailBox.Text = string.Empty;
            this.messageBox.Text = string.Empty;
        }

        void BackButtonClick(object sender, EventArgs e)
        {
            InvokeBackClick(e);
        }

        void SubmitButtonClick(object sender, EventArgs e)
        {
            captcha.Validate();

            if (!this.captcha.IsValid)
            {
                this.validator.ErrorMessage = "Invalid captcha code";
                return;
            }

            if (!this.Page.IsValid)
            {
                return;
            }

            if (AdId != null) 
                BetterclassifiedsCore.Controller.OnlineAdEnquiryController.CreateOnlineAdEnquiry(AdId.Value, 1,this.nameBox.Text, this.emailBox.Text, this.phoneBox.Text, this.messageBox.Text);
            successText.Text = GetResources(EntityGroup.OnlineAdMessaging, ContentItem.Form, "success.Text");
            InvokeOnSubmit(e);
            Clear();
        }

        void ClearButtonClick(object sender, EventArgs e)
        {
            this.Clear();
            successText.Text = string.Empty;
            this.InvokeBackClick(e);
        }

        void ValidatorServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(this.nameBox.Text))
            {
                args.IsValid = false;
                this.validator.ErrorMessage = GetResources(EntityGroup.OnlineAdMessaging, ContentItem.Form,
                                                           "invalidName.Text");
            }

            if (string.IsNullOrEmpty(this.emailBox.Text))
            {
                args.IsValid = false;
                this.validator.ErrorMessage = GetResources(EntityGroup.OnlineAdMessaging, ContentItem.Form,
                                                           "invalidEmail.Text");
            }

            if (string.IsNullOrEmpty(this.messageBox.Text))
            {
                args.IsValid = false;
                this.validator.ErrorMessage = GetResources(EntityGroup.OnlineAdMessaging, ContentItem.Form,
                                                           "messageRequired.Text");
            }

            //if (!this.captcha.IsValid)
            //{
            //    args.IsValid = false;
            //    this.validator.ErrorMessage = "Invalid captcha code";
            //}
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        protected override void CreateChildControls()
        {
            if (AdId.HasValue)
            {
                var headerPanel = new Panel { CssClass = "messageHead" };
                var messageBody = new Panel { CssClass = "infoMessage" };
                var messageTail = new Panel { CssClass = "messageBottom" };

                //
                this.emailRequiredField.ControlToValidate = this.emailBox.ID;
                this.emailValidator.ControlToValidate = this.emailBox.ID;
                this.nameRequired.ControlToValidate = this.nameBox.ClientID;
                this.messageRequiredField.ControlToValidate = this.messageBox.ID;


                this.Controls.Add(this.nameRequired);
                this.Controls.Add(this.emailRequiredField);
                this.Controls.Add(this.emailValidator);
                this.Controls.Add(this.messageRequiredField);
                this.Controls.Add(this.validator);
                var namePanel = new Panel();
                var nameWatermark = new TextBoxWatermarkExtender { TargetControlID = this.nameBox.ClientID, WatermarkText = "Name", ID = "xx1" };

                namePanel.Controls.Add(this.nameBox);
                namePanel.Controls.Add(nameWatermark);
                messageBody.Controls.Add(namePanel);

                var emailWatermark = new TextBoxWatermarkExtender { TargetControlID = this.emailBox.ClientID, WatermarkText = "Email", ID = "xx2" };
                messageBody.Controls.Add(this.emailBox);
                messageBody.Controls.Add(emailWatermark);

                var phoneWatermark = new TextBoxWatermarkExtender { TargetControlID = this.phoneBox.ClientID, WatermarkText = "Phone", ID = "xx3" };
                messageBody.Controls.Add(this.phoneBox);
                messageBody.Controls.Add(phoneWatermark);

                var messageWatermark = new TextBoxWatermarkExtender { TargetControlID = this.messageBox.ClientID, WatermarkText = "Message", ID = "xx4" };
                messageBody.Controls.Add(this.messageBox);
                messageBody.Controls.Add(messageWatermark);

                var errorPanel = new Panel { CssClass = "input-text" };
                errorPanel.Controls.Add(successText.DivWrap(".success-panel"));
                errorPanel.Controls.Add(validationSumary);
                messageBody.Controls.Add(errorPanel);

                messageBody.Controls.Add(this.captcha);
                messageBody.Controls.Add(this.submitButton.DivWrap("messageButton fr"));
                messageBody.Controls.Add(this.clearButton.DivWrap("messageButton"));



                //add controls
                Controls.Add(headerPanel);
                Controls.Add(messageBody);
                Controls.Add(messageTail);
            }
        }
    }
}