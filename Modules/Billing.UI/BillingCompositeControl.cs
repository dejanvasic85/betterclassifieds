using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Paramount.Billing.UIController;
using Paramount.Common.UI.BaseControls;

namespace Paramount.Billing.UI
{
    public abstract class BillingCompositeControl : ParamountCompositeControl
    {
        private UIController.BillingController controller;

        private Panel mainPanel;
        protected Panel errorPanel;


        protected Panel MainPanel
        {
            get { return this.mainPanel ?? (this.mainPanel = new Panel() { ID = "main-panel" }); }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.mainPanel.Visible = !this.IsError;
            this.errorPanel = new Panel { CssClass = "error-panel", Visible = this.IsError };
            this.errorPanel.Controls.Add(new Label { Text = this.ErrorText });
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.Controls.Add(this.MainPanel);
        }

        protected abstract string ErrorText { get; }

        protected bool IsError { get; set; }

        protected BillingController Controller
        {
            get
            {
                return this.controller ?? (controller = new BillingController());
            }
        }
    }
}
