using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using BetterClassified.UIController;
using BetterClassified.UIController.Booking;
using BetterclassifiedsCore;
using Paramount.Betterclassified.Utilities.Configuration;
using Paramount.Common.UI;
using Paramount.Common.UI.BaseControls;

namespace BetterClassified.UI
{
    public class LineAdNewDesign : ParamountCompositeControl
    {
        #region Private Variables

        //
        // Ad Header
        //
        private Panel _pnlContainerHeader;
        private PaddedPanel _panelAdHeader;
        private Panel _pnlChkHeaderContent;
        private CheckBox _chkNormalHeader;
        private Label _lblPriceNormalHeader;
        private Panel _pnlChkSuperHeaderContent;
        private CheckBox _chkSuperBoldHeader;
        private Label _lblPriceSuperBoldHeader;
        private readonly TextBox _txtBoldHeader;

        //
        // Ad Text
        //
        private Panel _pnlContainerAdText;
        private PaddedPanel _panelAdText;
        private readonly TextBox _txtAdText;
        private readonly Label _lblAdTextWordCount;
        private readonly Label _lblAdTextWordLabel;

        //
        // Colour Options
        //
        private Panel _pnlContainerColours;
        private PaddedPanel _panelColourSelection;

        private CheckBox _chkColourheader;
        private CheckBox _chkColourBorder;
        private CheckBox _chkColourBackground;

        private LineAdColourPicker _colHeaderPicker;
        private LineAdColourPicker _colBorderPicker;
        private LineAdColourPicker _colBackgroundPicker;

        private Panel _pnlChkHeaderColourContent;
        private Panel _pnlChkBorderColourContent;
        private Panel _pnlChkBackgroundColourContent;

        private Panel _pnlHeaderColourSelectPanel;
        private Panel _pnlBorderColourSelectPanel;
        private Panel _pnlBackgroundColourSelectPanel;

        private Label _lblPriceColourHeader;
        private Label _lblPriceColourBorder;
        private Label _lblPriceColourBackground;
        private GenericMessagePanel _gmpColourHeader;
        private GenericMessagePanel _gmpColourBorder;
        private GenericMessagePanel _gmpColourBackground;

        #endregion

        #region Constructor

        public LineAdNewDesign()
        {
            //
            // Ad Header
            //
            _pnlContainerHeader = new Panel { CssClass = "paddedPanel" };
            
            _panelAdHeader = new PaddedPanel
            {
                IsHelpContextVisible = true,
                HelpContextTemplate = new HelpPopupContentTemplate(HtmlHelpContent.BoldHeader),
                HeadingText = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "BoldHeaderHeading.Text"),
                SubHeadingText = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "BoldHeaderSubHeading.Text")
            };

            _pnlChkHeaderContent = new Panel { CssClass = "checkSelectionPanel" };

            _chkNormalHeader = new CheckBox
            {
                CssClass = "normal-header-checked",
                Text = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "ProvideHeaderCheckBox.Text")
            };
            _chkNormalHeader.InputAttributes.Add("chkNormalHeader", "aspCheckBox");

            _lblPriceNormalHeader = new Label { CssClass = "priceLabel", ID = "lblPriceNormalHeader" };
            _txtBoldHeader = new TextBox { CssClass = "adheadertext", ID = "txtBoldHeaderText", };
            _pnlChkSuperHeaderContent = new Panel { CssClass = "checkSelectionPanel" };
            _chkSuperBoldHeader = new CheckBox { Text = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "ProvideSuperBoldHeaderCheckBox.Text") };
            _chkSuperBoldHeader.InputAttributes.Add("chkSuperHeader", "aspCheckBox");
            _lblPriceSuperBoldHeader = new Label { CssClass = "priceLabel", ID = "lblPriceSuperBoldHeader" };

            //
            // Ad Text 
            //
            _pnlContainerAdText = new Panel { CssClass = "paddedPanel" };

            _panelAdText = new PaddedPanel
            {
                IsHelpContextVisible = true,
                HelpContextTemplate = new HelpPopupContentTemplate(HtmlHelpContent.BodyText),
                HeadingText = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "AdTextHeading.Text"),
                SubHeadingText = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "AdTextSubHeading.Text"),
                IsBottomContainerVisible = true
            };

            _txtAdText = new TextBox { CssClass = "adtext", ID = "txtLineAdText", TextMode = TextBoxMode.MultiLine };

            _lblAdTextWordLabel = new Label
            {
                CssClass = "wordcountlabel",
                Text = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "WordCountLabel.Text")
            };

            _lblAdTextWordCount = new Label { CssClass = "adword-count" };

            //
            // Colour Options
            //
            _pnlContainerColours = new Panel { CssClass = "paddedPanel" };
            _panelColourSelection = new PaddedPanel
            {
                IsHelpContextVisible = true,
                HelpContextTemplate = new HelpPopupContentTemplate(HtmlHelpContent.ColourText),
                HeadingText = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "ColourOptionsHeading.Text"),
                SubHeadingText = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "ColourOptionsSubHeading.Text")
            };

            _pnlChkHeaderColourContent = new Panel { CssClass = "checkSelectionPanel" };
            _pnlChkBorderColourContent = new Panel { CssClass = "checkSelectionPanel" };
            _pnlChkBackgroundColourContent = new Panel { CssClass = "checkSelectionPanel" };

            _pnlHeaderColourSelectPanel = new Panel { CssClass = "headerColourPanel" };
            _pnlBorderColourSelectPanel = new Panel() { CssClass = "borderColourPanel" };
            _pnlBackgroundColourSelectPanel = new Panel() { CssClass = "backgroundColourPanel" };

            _chkColourheader = new CheckBox
            {
                CssClass = "check-colour-header",
                Text = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "ColourHeaderCheckBox.Text")
            };
            _chkColourBorder = new CheckBox
            {
                CssClass = "check-colour-border",
                Text = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "ColourBorderCheckBox.Text")
            };
            _chkColourBackground = new CheckBox
            {
                CssClass = "check-colour-background",
                Text = GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "ColourBackgroundCheckBox.Text")
            };

            _chkColourheader.InputAttributes.Add("chkColourheader", "aspCheckBox");
            _chkColourBorder.InputAttributes.Add("chkColourBorder", "aspCheckBox");
            _chkColourBackground.InputAttributes.Add("chkColourBackground", "aspCheckBox");

            _lblPriceColourBorder = new Label { CssClass = "priceLabel" };
            _lblPriceColourBackground = new Label { CssClass = "priceLabel" };
            _lblPriceColourHeader = new Label { CssClass = "priceLabel" };

            _colHeaderPicker = new LineAdColourPicker() { CssClass = "colour-header-picker", OnClientColorChange = "changeHeader", OnClientLoad = "loadHeader" };
            _colBorderPicker = new LineAdColourPicker() { CssClass = "colour-border-picker", OnClientColorChange = "changeBorder", OnClientLoad = "loadBorder" };
            _colBackgroundPicker = new LineAdColourPicker() { CssClass = "colour-background-picker", OnClientColorChange = "changeBackground", OnClientLoad = "loadBackground" };

            _gmpColourHeader = new GenericMessagePanel { CssClass = "messagepanel-headercolour", MessageType = MessagePanelType.Suggestion, IsAlwaysVisible = true };
            _gmpColourBorder = new GenericMessagePanel { CssClass = "messagepanel-bordercolour", MessageType = MessagePanelType.Suggestion, IsAlwaysVisible = true };
            _gmpColourBackground = new GenericMessagePanel { CssClass = "messagepanel-backgroundcolour", MessageType = MessagePanelType.Suggestion, IsAlwaysVisible = true };
        }

        #endregion

        #region Method Overrides

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            _txtBoldHeader.MaxLength = BookCartController.GetHeaderCharacterLimit();

            // Check session variables
            GetSessionVariables();
            GetSessionPrices(BetterclassifiedSetting.IsAveragePriceUsedForLineAd);

            //
            // Ad Header
            //
            _pnlChkHeaderContent.Controls.Add(_chkNormalHeader);
            _pnlChkHeaderContent.Controls.Add(_lblPriceNormalHeader);
            _pnlChkSuperHeaderContent.Controls.Add(_chkSuperBoldHeader);
            _pnlChkSuperHeaderContent.Controls.Add(_lblPriceSuperBoldHeader);
            _panelAdHeader.HelpContextImageUrl = HelpContextImageUrl;
            _panelAdHeader.Controls.Add(_pnlChkHeaderContent);

            _panelAdHeader.Controls.Add(_txtBoldHeader);
            _panelAdHeader.Controls.Add(_pnlChkSuperHeaderContent);
            _pnlContainerHeader.Controls.Add(_panelAdHeader);

            //
            // Ad Text
            //
            _panelAdText.HelpContextImageUrl = HelpContextImageUrl;
            _panelAdText.Controls.Add(_txtAdText);
            _panelAdText.BottomContainerPanel.Controls.Add(_lblAdTextWordLabel);
            _panelAdText.BottomContainerPanel.Controls.Add(_lblAdTextWordCount);
            _pnlContainerAdText.Controls.Add(_panelAdText);

            //
            // Colour Options
            //
            _pnlChkHeaderColourContent.Controls.Add(_chkColourheader);
            _pnlChkHeaderColourContent.Controls.Add(_lblPriceColourHeader);

            _pnlChkBorderColourContent.Controls.Add(_chkColourBorder);
            _pnlChkBorderColourContent.Controls.Add(_lblPriceColourBorder);

            _pnlChkBackgroundColourContent.Controls.Add(_chkColourBackground);
            _pnlChkBackgroundColourContent.Controls.Add(_lblPriceColourBackground);

            _panelColourSelection.Controls.Add(_pnlChkHeaderColourContent);
            _pnlHeaderColourSelectPanel.Controls.Add(_gmpColourHeader);
            _pnlHeaderColourSelectPanel.Controls.Add(_colHeaderPicker);
            _panelColourSelection.Controls.Add(_pnlHeaderColourSelectPanel);

            _panelColourSelection.Controls.Add(_pnlChkBorderColourContent);
            _pnlBorderColourSelectPanel.Controls.Add(_gmpColourBorder);
            _pnlBorderColourSelectPanel.Controls.Add(_colBorderPicker);
            _panelColourSelection.Controls.Add(_pnlBorderColourSelectPanel);

            _panelColourSelection.Controls.Add(_pnlChkBackgroundColourContent);
            _pnlBackgroundColourSelectPanel.Controls.Add(_gmpColourBackground);
            _pnlBackgroundColourSelectPanel.Controls.Add(_colBackgroundPicker);
            _panelColourSelection.Controls.Add(_pnlBackgroundColourSelectPanel);

            _panelColourSelection.HelpContextImageUrl = HelpContextImageUrl;
            _pnlContainerColours.Controls.Add(_panelColourSelection);

            this.Controls.Add(_pnlContainerHeader);
            this.Controls.Add(_pnlContainerAdText);
            this.Controls.Add(_pnlContainerColours);
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);
            Page.ClientScript.RegisterClientScriptResource(GetType(), "BetterClassified.UI.JavaScript.linead-newdesign.js");
        }

        #endregion

        #region Help Methods

        private void GetSessionVariables()
        {
            // Check the selections already made in the session
            IBookCartContext bookingCart = BookCartController.GetCurrentBookCart();
            LineAdController lineAdController = new LineAdController();

            // Bind the current booking cart information to the control
            _chkNormalHeader.Checked = bookingCart.LineAdIsNormalAdHeading;
            _chkSuperBoldHeader.Checked = bookingCart.LineAdIsSuperBoldHeading;
            _txtBoldHeader.Text = bookingCart.LineAdHeadingText;
            _txtAdText.Text = bookingCart.LineAdText;
            _chkColourheader.Checked = bookingCart.LineAdIsColourHeading;

            if (bookingCart.LineAdIsColourHeading)
            {
                _colHeaderPicker.SelectedColor = ColorTranslator.FromHtml(bookingCart.LineAdHeaderColourCode);
            }

            _chkColourBorder.Checked = bookingCart.LineAdIsColourBorder;

            if (bookingCart.LineAdIsColourBorder)
            {
                _colBorderPicker.SelectedColor = ColorTranslator.FromHtml(bookingCart.LineAdBorderColourCode);
            }

            _chkColourBackground.Checked = bookingCart.LineAdIsColourBackground;

            if (bookingCart.LineAdIsColourBackground)
            {
                _colBackgroundPicker.SelectedColor = ColorTranslator.FromHtml(bookingCart.LineAdBackgroundColourCode);
            }

            // Display suggestions
            if (bookingCart.LineAdIsColourBorder || bookingCart.LineAdIsColourBackground)
            {
                _gmpColourHeader.MessageText = lineAdController.GetLineAdHeaderColourSuggestion(
                    bookingCart.LineAdBorderColourCode, bookingCart.LineAdBackgroundColourCode);
            }

            if (bookingCart.LineAdIsColourHeading || bookingCart.LineAdIsColourBackground)
            {
                _gmpColourBorder.MessageText = lineAdController.GetLineAdBorderColourSuggestion(
                    bookingCart.LineAdHeaderColourCode, bookingCart.LineAdBackgroundColourCode);
            }

            if (bookingCart.LineAdIsColourBorder || bookingCart.LineAdIsColourBorder)
            {
                _gmpColourBackground.MessageText = lineAdController.GetLineAdBackgroundColourSuggestion(
                    bookingCart.LineAdHeaderColourCode, bookingCart.LineAdBorderColourCode);
            }
        }

        private void GetSessionPrices(bool isAveragePriceUsed)
        {
            string endText = isAveragePriceUsed ?
                GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "AveragePriceLabel.Text") :
                GetResource(EntityGroup.Betterclassified, ContentItem.LineAdControl, "TotalPriceLabel.Text");

            IBookCartContext bookingCart = BookCartController.GetCurrentBookCart();
            _lblPriceNormalHeader.Text = string.Format("${0:N} {1}",
                bookingCart.BookingOrderPrice.GetLineAdBoldHeaderPrice(isAveragePriceUsed), endText);

            _lblPriceSuperBoldHeader.Text = string.Format("${0:N} {1}",
                bookingCart.BookingOrderPrice.GetLineAdSuperBoldHeaderPrice(isAveragePriceUsed), endText);

            _lblPriceColourHeader.Text = string.Format("${0:N} {1}",
                bookingCart.BookingOrderPrice.GetLineAdColourHeaderPrice(isAveragePriceUsed), endText);

            _lblPriceColourBorder.Text = string.Format("${0:N} {1}",
                bookingCart.BookingOrderPrice.GetLineAdColourBorderPrice(isAveragePriceUsed), endText);

            _lblPriceColourBackground.Text = string.Format("${0:N} {1}",
                bookingCart.BookingOrderPrice.GetLineAdColourBackgroundPrice(isAveragePriceUsed), endText);
        }

        #endregion

        #region Public Properties

        [UrlPropertyAttribute]
        public string HelpContextImageUrl { get; set; }

        public string LineAdHeading
        {
            get
            {
                return _txtBoldHeader.Text;
            }
        }

        public bool IsLineAdHeadingSelected
        {
            get
            {
                return _chkNormalHeader.Checked;
            }
        }

        public bool IsSuperBoldHeadingSelected
        {
            get
            {
                return this._chkSuperBoldHeader.Checked;
            }
        }

        public bool IsColourHeadingSelected
        {
            get
            {
                return this._chkColourheader.Checked;
            }
        }

        public string ColourHeaderCode
        {
            get
            {
                return ColorTranslator.ToHtml(_colHeaderPicker.SelectedColor);
            }
        }

        public bool IsColourBorderSelected
        {
            get
            {
                return this._chkColourBorder.Checked;
            }
        }

        public string BorderColourCode
        {
            get
            {
                return ColorTranslator.ToHtml(this._colBorderPicker.SelectedColor);
            }
        }

        public bool IsColourBackgroundSelected
        {
            get
            {
                return this._chkColourBackground.Checked;
            }
        }

        public string BackgroundColourCode
        {
            get
            {
                return ColorTranslator.ToHtml(this._colBackgroundPicker.SelectedColor);
            }
        }

        public string LineAdText
        {
            get
            {
                return _txtAdText.Text;
            }
        }

        private List<string> _errorMessages;

        // Perform validation on the server component controls
        public bool IsValid
        {
            get
            {
                _errorMessages = new List<string>();
                var isValid = true;

                if (string.IsNullOrEmpty(_txtAdText.Text))
                {
                    _errorMessages.Add(GetResource(EntityGroup.Betterclassified,
                        ContentItem.LineAdControl, "AdTextIsRequired.Text"));
                    isValid = false;
                }

                if (_chkNormalHeader.Checked && string.IsNullOrEmpty(_txtBoldHeader.Text))
                {
                    _errorMessages.Add(GetResource(EntityGroup.Betterclassified,
                        ContentItem.LineAdControl, "AdHeaderIsRequired.Text"));
                    isValid = false;
                }

                if (_chkColourheader.Checked && _colHeaderPicker.SelectedColor.IsEmpty)
                {
                    _errorMessages.Add(GetResource(EntityGroup.Betterclassified,
                        ContentItem.LineAdControl, "AdHeaderColourIsRequired.Text"));
                    isValid = false;
                }

                if (_chkColourBorder.Checked && _colBorderPicker.SelectedColor.IsEmpty)
                {
                    _errorMessages.Add(GetResource(EntityGroup.Betterclassified,
                        ContentItem.LineAdControl, "AdBorderColourIsRequired.Text"));
                    isValid = false;
                }

                if (_chkColourBackground.Checked && _colBackgroundPicker.SelectedColor.IsEmpty)
                {
                    _errorMessages.Add(GetResource(EntityGroup.Betterclassified,
                        ContentItem.LineAdControl, "AdBackgroundColourIsRequired.Text"));
                    isValid = false;
                }

                return isValid;
            }
        }

        public List<string> ValidationSummaryList
        {
            get
            {
                return _errorMessages;
            }
        }
        #endregion

        #region Event Handling

        #endregion

        private struct HtmlHelpContent
        {
            internal static string BoldHeader = "<span class='text-wrapper'><strong>Bold Header:</strong> Decide a heading to appear above your ad. E.g. 'Guitarist Wanted'.</span>";
            internal static string BodyText = "<span class='text-wrapper'><strong>Body Text</strong>: First 30 words free! Words will be counted automatically and price is updated in the Order Summary. Describe your ad here. For e.g. the type of band you have and the type of person you are looking for.</span>";
            internal static string ColourText = "<span class='text-wrapper'><strong>Bold Header:</strong> Spice up your ad with colour headings, border and backgrounds. We will try to provide suggestions for best suited colours.</span>";
        }
    }

}
