namespace BetterClassified.UI.Messaging
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
using BetterclassifiedsCore.DataModel;
    using Telerik.Web.UI;

    public class ActionButtonTemplate:ITemplate
    {
        public event EventHandler<ActionEventArgs> ActionClick;

        private void InvokeActionClick(object sender, ActionEventArgs e)
        {
            EventHandler<ActionEventArgs> handler = ActionClick;
            if (handler != null) handler(sender, e);
        }
        public void InstantiateIn(Control container)
        {
            var actionButton = new ActionButton
                                   {
                                                ShowDeleteButton=true,
                                                ShowMarkButton= true,
                                                MarkButtonToolTip="Mark as Read",
                                                DeleteButtonToolTip="Delete"
                                   };
            actionButton.DataBinding +=(ActionButtonDataBinding);
            actionButton.ActionClick += ActionButtonActionClick;
            container.Controls.Add(actionButton);
        }

        void ActionButtonActionClick(object sender, ActionEventArgs e)
        {
            InvokeActionClick(sender, e);
        }

        static void ActionButtonDataBinding(object sender, EventArgs e)
        {
            var control = sender as ActionButton;
            if (control == null)
            {
                return;
            }

            var dataItem = control.NamingContainer as GridDataItem;
            if (dataItem == null)
            {
                return;
            }

            var message = dataItem.DataItem as OnlineAdEnquiry;
            if (message == null)
            {
                return;
            }
            control.Key = message.OnlineAdEnquiryId;
            if(message.OpenDate.HasValue)
            {
                control.ShowMarkButton  = false;
            }
        }
    }
}