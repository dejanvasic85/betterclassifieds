using System;
using System.Web.UI.WebControls;
using Paramount.Common.DataTransferObjects.Enums;
using Paramount.Common.UI;

namespace Paramount.Billing.UI.Invoice
{
    public class PaymentOptions : BillingCompositeControl
    {
        protected RadioButtonList paymentOptionList;
        protected Label header;

        public PaymentOptions()
        {
            paymentOptionList = new RadioButtonList() { ID = "paymentOptionList" };
            header = new Label() { Text = "Payment Options" };
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.MainPanel.Controls.Add(this.header.DivWrapLabel());
            this.MainPanel.Controls.Add(this.paymentOptionList);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.LoadData();
        }

        private void LoadData()
        {
            foreach (var type in Enum.GetNames(typeof(PaymentType)))
            {
                this.paymentOptionList.Items.Add(new ListItem(type, type));
            }
        }

        protected override string ErrorText
        {
            get { return "Payment Options are temporarily unavailable"; }
        }

        public PaymentType OptionId
        {
            get { return (PaymentType)Enum.Parse(typeof(PaymentType), this.paymentOptionList.SelectedValue); }
            set
            {
                var item = this.paymentOptionList.Items.FindByValue(Enum.GetName(typeof(PaymentType), value));
                if (item == null) return;
                this.paymentOptionList.ClearSelection();
                item.Selected = true;
            }
        }
    }
}