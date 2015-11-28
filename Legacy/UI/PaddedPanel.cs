using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Paramount.Common.UI.BaseControls;
using System.Web.UI;

namespace Paramount.Common.UI
{
    public class PaddedPanel : ParamountCompositeControl
    {
        private Panel _pnlContainer;
        private Panel _pnlTopContainer;
        private Panel _pnlBottomContainer;
        private Panel _pnlHelpContextContainer;
        private Label _lblHeading;
        private Label _lblSubHeading;
        private HelpContextControl _hlpContextControl;

        public PaddedPanel()
        {
            _pnlContainer = new Panel { CssClass = "paddedpanel-container-rounded" };
            _pnlTopContainer = new Panel { CssClass = "paddedpanel-topcontainer" };
            _pnlBottomContainer = new Panel { CssClass = "paddedpanel-bottomcontainer-rounded" };
            _lblHeading = new Label { CssClass = "paddedpanel-heading" };
            _lblSubHeading = new Label { CssClass = "paddedpanel-subheading" };
            _pnlHelpContextContainer = new Panel { CssClass = "paddedpanel-helpcontainer" };
            _hlpContextControl = new HelpContextControl();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            
            _pnlTopContainer.Controls.Add(_lblHeading);
            _pnlTopContainer.Controls.Add(_lblSubHeading);

            if (IsHelpContextVisible)
            {
                _pnlHelpContextContainer.Controls.Add(_hlpContextControl);
                _pnlTopContainer.Controls.Add(_pnlHelpContextContainer);

                _hlpContextControl.ContentTemplate = HelpContextTemplate;
                _hlpContextControl.ImageUrl = HelpContextImageUrl;
            }

            base.Controls.Add(_pnlTopContainer);
            base.Controls.Add(_pnlContainer);

            if (IsBottomContainerVisible)
            {
                base.Controls.Add(_pnlBottomContainer);
                _pnlContainer.CssClass = "paddedpanel-container";
            }
        }

        public override System.Web.UI.ControlCollection Controls
        {
            get
            {
                return _pnlContainer.Controls;
            }
        }

        public bool IsBottomContainerVisible { get; set; }

        public bool IsHelpContextVisible { get; set; }

        public Panel BottomContainerPanel
        {
            get
            {
                return _pnlBottomContainer;
            }
        }

        public string HeadingText
        {
            get
            {
                return _lblHeading.Text;
            }
            set
            {
                _lblHeading.Text = value;
            }
        }

        public string SubHeadingText
        {
            get
            {
                return _lblSubHeading.Text;
            }
            set
            {
                _lblSubHeading.Text = value;
            }
        }

        public ITemplate HelpContextTemplate { get; set;}
        
        [UrlPropertyAttribute]
        public string HelpContextImageUrl { get; set; }
    }
}
