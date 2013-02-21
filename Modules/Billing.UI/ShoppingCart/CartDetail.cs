using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Paramount.Billing.UI.Invoice;
using Paramount.Common.UI.BaseControls;
using Telerik.Web.UI;

namespace Paramount.Billing.UI.ShoppingCart
{
    public class CartDetail : BillingCompositeUpdateControl
    {
        protected Panel mainPanel;
        protected Panel headerPanel;
        protected Panel addressPanel;
        protected Panel itemsPanel;
        protected Panel buttonsPanel;

        protected Label header;
        protected AddressView billingAddress;
        protected AddressView deliveryAddress;
        protected RadGrid cartItems;
        protected PaymentOptions paymentOptions;

      
       
        
    

        public CartDetail()
        {
            this.mainPanel = new Panel() { ID = "mainPanel" };
            this.headerPanel = new Panel() { ID = "headerPanel" };
            this.addressPanel = new Panel() { ID = "addressPanel" };
            this.itemsPanel = new Panel() { ID = "itemsPanel" };
            this.buttonsPanel = new Panel() { ID = "buttonsPanel" };


            this.header = new Label() { ID = "header" };

            this.billingAddress = new AddressView() { ID = "billingAddress", TitleText = "Billing Address" };
            this.deliveryAddress = new AddressView() { ID = "deliveryAddress", TitleText = "Delivery Address" };
            this.cartItems = new RadGrid() { ID = "cartItems" };
            this.paymentOptions = new PaymentOptions() { ID = "paymentOptions" };

            this.cancel = new Button() { ID = "cancel" };
    
            this.cancel.Click += CancelClicked;
    
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            
                this.InvokeCancelled(EventArgs.Empty);
            

        }

        protected override bool Cancel()
        {
            return true;
        }

        protected override bool Save()
        {
            throw new NotImplementedException();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.Controls.Add(this.mainPanel);
            this.mainPanel.Controls.Add(this.headerPanel);
            header.Text = "Please confirm the following details:";
            this.headerPanel.Controls.Add(this.header);
                
            this.mainPanel.Controls.Add(this.addressPanel);
            this.addressPanel.Controls.Add(this.billingAddress);
            this.addressPanel.Controls.Add(this.deliveryAddress);

            this.mainPanel.Controls.Add(this.itemsPanel);
            this.itemsPanel.Controls.Add(this.cartItems);

        }

        protected override string ErrorText
        {
            get { return "Paramount shopping cart is temporarily unavialable"; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadControls();
        }

        private void LoadControls()
        {
            //todo
        }
    }
}
