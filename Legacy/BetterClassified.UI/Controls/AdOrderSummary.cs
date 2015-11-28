using System;
using System.Web.UI;
using Paramount.Common.UI;
using Paramount.Common.UI.BaseControls;

namespace BetterClassified.UI
{
    public class AdOrderSummary : ParamountCompositeControl
    {
        private PaddedPanel _paddedPanelContainer;
        private AdPriceSummary _priceSummary;

        public AdOrderSummary()
        {
            _paddedPanelContainer = new PaddedPanel()
            {
                IsHelpContextVisible = true,
                HeadingText = "Order Summary",
                CssClass = "adordersummary-container",
                HelpContextTemplate = new HelpPopupContentTemplate(HtmlHelpContent.OrderSummary)
            };

            _priceSummary = new AdPriceSummary();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            _paddedPanelContainer.HelpContextImageUrl = this.HelpContextImageUrl;
            _paddedPanelContainer.Controls.Add(_priceSummary);

            this.Controls.Add(_paddedPanelContainer);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (IsFloatingOnPage)
            {
                Page.ClientScript.RegisterClientScriptResource(GetType(), "BetterClassified.UI.JavaScript.jquery-floatobject.js");
                Page.ClientScript.RegisterClientScriptResource(GetType(), "BetterClassified.UI.JavaScript.adordersummary-float.js");
            }

            if (IsAutoPriceCheck)
            { 
                Page.ClientScript.RegisterClientScriptResource(GetType(), "BetterClassified.UI.JavaScript.adordersummary-pricecheck.js");
            }
        }

        [UrlPropertyAttribute]
        public string HelpContextImageUrl { get; set; }

        public bool IsFloatingOnPage { get; set; }

        public bool IsAutoPriceCheck { get; set; }

        private struct HtmlHelpContent
        {
            internal static string OrderSummary = "<span class='text-wrapper'><strong>Order Summary:</strong> All your selections and pricing are detailed below.</span>";
        }
    }
}
