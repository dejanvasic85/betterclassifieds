using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Paramount.Billing.UI
{
    using Common.UI;

    public abstract class BillingCompositeUpdateControl : BillingCompositeControl
    {
        public event EventHandler Saved;
        protected Panel buttonPanel;
        protected Button save;
        protected Button cancel;

        protected BillingCompositeUpdateControl()
        {
            this.buttonPanel = new Panel() { ID = "buttonPanel", CssClass ="button-panel" };

            this.save = new Button() { ID = "save", Text = this.SaveButtonText, ValidationGroup = this.ValidationGroupName, CssClass = "btn radius" };
            this.cancel = new Button() { ID = "cancel", Text = this.CancelButtonText, CausesValidation = false, CssClass = "btn radius" };

            this.save.Click += SaveClicked;
            this.cancel.Click += CancelClicked;
        }

        protected string ValidationGroupName { get { return string.Empty; } }
        
        protected virtual  string CancelButtonText { get { return "Cancel"; } }

        protected virtual  string SaveButtonText { get { return "Save"; } }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
           this.buttonPanel.Controls.Add(this.save.DivWrap("fr"));
            this.buttonPanel.Controls.Add(this.cancel.DivWrap());
            
            buttonPanel.Controls.Add(new Panel {CssClass = "clear"});
            this.Controls.Add(buttonPanel);
        }


        private void CancelClicked(object sender, EventArgs e)
        {
            if (Cancel())
            {
                this.InvokeCancelled(EventArgs.Empty);
            }
        }

        protected abstract bool Cancel();

        private void SaveClicked(object sender, EventArgs e)
        {
            if (Save())
            {
                this.InvokeSaved(EventArgs.Empty);
            }
        }

        protected abstract bool Save();

        public void InvokeSaved(EventArgs e)
        {
            var handler = Saved;
            if (handler != null) handler(this, e);
        }

        public event EventHandler Cancelled;

        public void InvokeCancelled(EventArgs e)
        {
            var handler = Cancelled;
            if (handler != null) handler(this, e);
        }
    }
}
