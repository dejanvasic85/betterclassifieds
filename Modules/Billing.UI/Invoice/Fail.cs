using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Paramount.Billing.UIController;
using Paramount.Common.UI.BaseControls;

namespace Paramount.Billing.UI.Invoice
{
   public  class Fail: ParamountCompositeControl
   {
       private Label message;
       private Label header;
       private BillingSessionManager billingSessionManager;

       private UIController.BillingController controller;
       protected BillingController Controller
       {
           get
           {
               return this.controller ?? (controller = new BillingController());
           }
       }


       public Fail()
       {
           message=new Label(){ID="message",CssClass = "message"};
           header = new Label() {ID = "header", CssClass = "header"};
       }

       protected override void OnLoad(EventArgs e)
       {
           base.OnLoad(e);

           if (this.Page.IsPostBack) return;
           var action = Page.Request.QueryString["action"];

           switch (action)
           {
               case "fail":
                   message.Text = "Please try again. Contact us if problem persists.";
                   header.Text = "Payment failed";
                   this.CssClass = "payment-failed";
                   this.Controller.InvoiceFailed(this.BillingSessionManager.InvoiceId);
                   break;
               case "cancel":
                   message.Text = "Successfully cancelled your booking.";
                   header.Text = "Booking cancelled";
                   this.Controller.InvoiceCancelled(this.BillingSessionManager.InvoiceId);
                   break;
               default:
                   message.Text = "";
                   header.Text = "";
                   break;
           }

           ////clear any bookings
           this.BillingSessionManager.Clear();
       }

       private BillingSessionManager BillingSessionManager
       {
           get
           {
               return this.billingSessionManager ??
                      (this.billingSessionManager = BillingSessionManager.GetInstance(HttpContext.Current));
           }
       }
    }
}
