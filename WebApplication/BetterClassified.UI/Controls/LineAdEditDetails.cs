using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BetterClassified.UIController.ViewObjects;
using Paramount.Common.UI;
using Paramount.Common.UI.BaseControls;
using BetterClassified.UIController;
using System;
using System.Web;

namespace BetterClassified.UI
{
    public class LineAdEditDetails : ParamountCompositeControl
    {
        #region UI declarations

        private readonly PaddedPanel _paddedPanelContainer;
        private readonly Panel _boldHeaderPanel;
        private readonly Label _boldHeaderLabel;
        private readonly TextBox _boldHeaderTextbox;
        private readonly Panel _superBoldHeaderPanel;
        private readonly CheckBox _superBoldHeaderCheckbox;
        private readonly Label _adTextLabel;
        private readonly LineAdTextBox _adTextBox;
        private readonly Panel _colourHeadingPanel;
        private readonly Label _colourHeadingLabel;
        private readonly LineAdColourPicker _colourHeadingPicker;
        private readonly Panel _colourBorderPanel;
        private readonly Label _colourBorderLabel;
        private readonly LineAdColourPicker _colourBorderPicker;
        private readonly Panel _colourBackgroundPanel;
        private readonly Label _colourBackgroundLabel;
        private readonly LineAdColourPicker _colourBackgroundPicker;
        private readonly CustomValidator _serverValidator;

        private readonly GenericMessagePanel _genericMessagePanel;
        private readonly ValidationSummary _validationSummary;

        private readonly Panel _commandPanel;
        private readonly LinkButton _buttonCancel;
        private readonly LinkButton _buttonSubmit;
        private readonly LinkButton _buttonViewImages;

        public event EventHandler Submit;

        #endregion

        public LineAdEditDetails()
        {

            var maxHeaderCharLength = ClientSetting.LineAdHeadingCharMaxLength;

            _validationSummary = new ValidationSummary();
            _serverValidator = new CustomValidator { Text = @"&nbsp;", ID = "serverValidationControl" };
            _serverValidator.ServerValidate += ServerValidatorServerValidate;
            _paddedPanelContainer = new PaddedPanel
            {
                HelpContextTemplate = new HelpPopupContentTemplate(GetHelpContentTemplate()),
                HeadingText = GetResource(EntityGroup.Betterclassified, ContentItem.EditLineAdControl, "Heading.Text")
            };

            _boldHeaderPanel = new Panel();
            _boldHeaderLabel = new Label
                                   {
                                       CssClass = "editLineAd-Label",
                                       ID = "lblBoldHeader",
                                       Text = GetResource(EntityGroup.Betterclassified,
                                                                     ContentItem.EditLineAdControl, "BoldHeader.Label")
                                   };

            _boldHeaderTextbox = new TextBox
            {
                CssClass = "editLineAd-HeaderInput",
                ID = "txtBoldHeaderText",
                MaxLength = maxHeaderCharLength
            };

            _adTextLabel = new Label
            {
                CssClass = "editLineAd-Label",
                ID = "lblAdText",
                Text = GetResource(EntityGroup.Betterclassified, ContentItem.EditLineAdControl, "BodyText.Label")
            };
            _adTextBox = new LineAdTextBox();

            _superBoldHeaderPanel = new Panel { CssClass = "editLineAd-superBoldPanel" };
            _superBoldHeaderCheckbox = new CheckBox { Text = GetResource(EntityGroup.Betterclassified, ContentItem.EditLineAdControl, "SuperBoldHeaderCheck.Label") };

            _colourHeadingPanel = new Panel();
            _colourHeadingLabel = new Label
            {
                CssClass = "editLineAd-Label",
                ID = "lblColourHeading",
                Text = GetResource(EntityGroup.Betterclassified, ContentItem.EditLineAdControl, "ColourHeading.Label")
            };
            _colourHeadingPicker = new LineAdColourPicker();
            _colourBorderPanel = new Panel();
            _colourBorderLabel = new Label
            {
                CssClass = "editLineAd-Label",
                ID = "lblColourBorder",
                Text = GetResource(EntityGroup.Betterclassified, ContentItem.EditLineAdControl, "ColourBorder.Label")
            };
            _colourBorderPicker = new LineAdColourPicker();
            _colourBackgroundPanel = new Panel();
            _colourBackgroundLabel = new Label
            {
                CssClass = "editLineAd-Label",
                ID = "lblColourBackground",
                Text = GetResource(EntityGroup.Betterclassified, ContentItem.EditLineAdControl, "ColourBackground.Label")
            };
            _colourBackgroundPicker = new LineAdColourPicker();

            _commandPanel = new Panel { CssClass = "btn-group pull-right" };
            _buttonCancel = new LinkButton
            {
                Text = GetResource(EntityGroup.Betterclassified, ContentItem.EditLineAdControl, "Cancel.Button"),
                CssClass = "btn btn-warning"
            };
            
            _buttonSubmit = new LinkButton
            {
                Text = "Save Changes",
                CssClass = "btn btn-default"
            };
            _buttonSubmit.Click += ButtonSubmitClick;
            _buttonViewImages = new LinkButton {Text = "Manage Images", CssClass = "btn btn-default", ID = "ViewImages"};

            _genericMessagePanel = new GenericMessagePanel();
        }

        void ServerValidatorServerValidate(object source, ServerValidateEventArgs args)
        {
            var errorMessage = string.Empty;

            if (_adTextBox.WordCount > OriginalWordCount && !IsAdminMode)
            {
                errorMessage = GetResource(EntityGroup.Betterclassified, ContentItem.EditLineAdControl, "WordCountExceeded.Error");
            }
            else if (_boldHeaderTextbox.Text.Length > ClientSetting.LineAdHeadingCharMaxLength)
            {
                errorMessage = GetResource(EntityGroup.Betterclassified, ContentItem.EditLineAdControl, "BoldHeaderLengthExceeded.Error");
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                _genericMessagePanel.Visible = false;
                args.IsValid = false;
                _serverValidator.ErrorMessage = errorMessage;
                return;
            }

        }

        void ButtonSubmitClick(object sender, EventArgs e)
        {
            SubmitChanges();
        }

        [UrlPropertyAttribute]
        public string HelpContextImageUrl { get; set; }
        public bool IsAdminMode { get; set; }
        public LineAdEditParamType QueryParamType { get; set; }
        public bool CancelButtonVisible
        {
            get { return _buttonCancel.Visible; }
            set { _buttonCancel.Visible = value; }
        }
        public string QueryParamName { get; set; }
        private int? LineAdId
        {
            get
            {
                if (QueryParamType == LineAdEditParamType.AdBookingId)
                    return Convert.ToInt32(ViewState["lineadid"]);

                int lineAdId;
                if (int.TryParse(Page.Request.QueryString["lineAdId"], out lineAdId))
                {
                    return lineAdId;
                }
                _genericMessagePanel.MessageText = "Unable to load ad details";
                _genericMessagePanel.MessageType = MessagePanelType.Error;
                return null;
            }
            set { ViewState["lineadid"] = value; }
        }
        private int? AdBookingId
        {
            get
            {
                int adBookingId;
                if (int.TryParse(Page.Request.QueryString[QueryParamName], out adBookingId))
                {
                    return adBookingId;
                }
                _genericMessagePanel.MessageText = "Unable to load ad details";
                _genericMessagePanel.MessageType = MessagePanelType.Error;
                return null;
            }
        }
        private int OriginalWordCount
        {
            get { return Convert.ToInt32(ViewState["originalWordCount"]); }
            set { ViewState["originalWordCount"] = value; }
        }
        public string CancelNavigateUrl
        {
            get { return _buttonCancel.PostBackUrl; }
            set { _buttonCancel.PostBackUrl = value; }
        }
        public bool ManageImageButtonVisible
        {
            get { return _buttonViewImages.Visible; }
            set { _buttonViewImages.Visible = value; }
        }
        
        public string GetManageImagesButtonClientID()
        {
            return _buttonViewImages.ClientID;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            _paddedPanelContainer.HelpContextImageUrl = HelpContextImageUrl;
            _paddedPanelContainer.IsHelpContextVisible = !IsAdminMode;

            _boldHeaderPanel.Controls.Add(_boldHeaderLabel);
            _boldHeaderPanel.Controls.Add(_boldHeaderTextbox);
            _boldHeaderPanel.Controls.Add(_superBoldHeaderPanel);
            _superBoldHeaderPanel.Controls.Add(_superBoldHeaderCheckbox);
            _colourHeadingPanel.Controls.Add(_colourHeadingLabel);
            _colourHeadingPanel.Controls.Add(_colourHeadingPicker);
            _colourBorderPanel.Controls.Add(_colourBorderLabel);
            _colourBorderPanel.Controls.Add(_colourBorderPicker);
            _colourBackgroundPanel.Controls.Add(_colourBackgroundLabel);
            _colourBackgroundPanel.Controls.Add(_colourBackgroundPicker);

            _paddedPanelContainer.Controls.Add(_boldHeaderPanel);
            _paddedPanelContainer.Controls.Add(_adTextLabel);
            _paddedPanelContainer.Controls.Add(_adTextBox);
            _paddedPanelContainer.Controls.Add(_colourHeadingPanel);
            _paddedPanelContainer.Controls.Add(_colourBorderPanel);
            _paddedPanelContainer.Controls.Add(_colourBackgroundPanel);

            _commandPanel.Controls.Add(_buttonCancel);
            _commandPanel.Controls.Add(_buttonViewImages);
            _commandPanel.Controls.Add(_buttonSubmit);

            Controls.Add(_genericMessagePanel);
            Controls.Add(_validationSummary);
            Controls.Add(_serverValidator);
            Controls.Add(_paddedPanelContainer);
            Controls.Add(_commandPanel);

            if (LineAdId.HasValue && QueryParamType == LineAdEditParamType.LineAdId)
                DataBindLineAd(LineAdId.Value);
            else if (AdBookingId.HasValue && QueryParamType == LineAdEditParamType.AdBookingId)
                DataBindLineAdByBookingId(AdBookingId.Value);
        }

        private string GetHelpContentTemplate()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<span class='text-wrapper'><strong>{0}:</strong>",
                GetResource(EntityGroup.Betterclassified, ContentItem.EditLineAdControl, "HelpTitle.Text")));
            sb.Append(string.Format("{0} </span>",
                GetResource(EntityGroup.Betterclassified, ContentItem.EditLineAdControl, "HelpInfo.Text")));
            return sb.ToString();
        }

        private static LineAdVo GetLineAd(int lineAdId)
        {
            return new LineAdController().GetLineAd(lineAdId);
        }

        private static LineAdVo GetLineAdByBookingId(int adBookingId)
        {
            return new LineAdController().GetLineAdByBookingId(adBookingId);
        }

        private void DataBindLineAd(LineAdVo lineAdVo)
        {
            LineAdId = lineAdVo.LineAdId;
            _boldHeaderTextbox.Text = lineAdVo.AdHeader;
            _adTextBox.AdBodyText = lineAdVo.AdText;
            _colourHeadingPicker.SelectedHtmlColour = lineAdVo.HeaderColourCode;
            _colourBorderPicker.SelectedHtmlColour = lineAdVo.BorderColourCode;
            _colourBackgroundPicker.SelectedHtmlColour = lineAdVo.BackgroundColourCode;

            _superBoldHeaderCheckbox.Checked = lineAdVo.IsSuperBoldHeading;
            OriginalWordCount = lineAdVo.LineAdBookingView.NumberOfUnits;

            // If Administration is not viewing this control then ensure security by hiding elements
            if (IsAdminMode) return;
            _boldHeaderPanel.Visible = lineAdVo.LineAdBookingView.BoldHeading;
            _adTextBox.MaximumWords = lineAdVo.LineAdBookingView.NumberOfUnits;
            _colourHeadingPanel.Visible = lineAdVo.LineAdBookingView.ColourHeading;
            _colourBorderPanel.Visible = lineAdVo.LineAdBookingView.ColourBorder;
            _colourBackgroundPanel.Visible = lineAdVo.LineAdBookingView.ColourBackground;
            _superBoldHeaderPanel.Visible = lineAdVo.LineAdBookingView.SuperBoldHeading;
        }

        private void DataBindLineAd(int lineAdId)
        {
            DataBindLineAd(GetLineAd(lineAdId));
        }

        private void DataBindLineAdByBookingId(int adBookingId)
        {
            DataBindLineAd(GetLineAdByBookingId(adBookingId));
        }

        private void SubmitChanges()
        {
            if (LineAdId.HasValue && _serverValidator.IsValid)
            {
                // Increase the word count if administrator has added more words to the text!
                var wordCount = IsAdminMode && _adTextBox.WordCount > OriginalWordCount ? 
                    _adTextBox.WordCount : 
                    OriginalWordCount;

                new LineAdController().UpdateLineAd(LineAdId.Value, _boldHeaderTextbox.Text, wordCount, _adTextBox.AdBodyText, _superBoldHeaderCheckbox.Checked,
                                _colourHeadingPicker.SelectedHtmlColour, _colourBorderPicker.SelectedHtmlColour, _colourBackgroundPicker.SelectedHtmlColour);


                //_genericMessagePanel.MessageText = "Details have been updated successfully.";
                //_genericMessagePanel.MessageType = MessagePanelType.Success;
                //_genericMessagePanel.Visible = true;
            }

            EventHandler handler = Submit;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }

    public enum LineAdEditParamType
    {
        LineAdId,
        AdBookingId
    }
}
