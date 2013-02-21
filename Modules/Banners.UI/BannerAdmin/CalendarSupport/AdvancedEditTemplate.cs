using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Paramount.Banners.UI.BannerAdmin.CalendarSupport
{
    public class AdvancedEditTemplate : IBindableTemplate
    {

        public bool EditMode;
        #region IBindableTemplate Members

        public IOrderedDictionary ExtractValues(Control container)
        {
            var dict = new OrderedDictionary();
            var banner = ((BannerDetails)container.FindControl("bannerDetails"));
            dict.Add("banner", banner.Banner);
            return dict;
        }

        #endregion

        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            var details = new EditBanner {ID = "bannerDetails"};
            container.Controls.Add(details);
            details.DataBinding += DetailsDataBinding;
        }

        #endregion

        private  void DetailsDataBinding(object sender, EventArgs e)
        {
            var details = (BannerDetails)sender;
            var dataItem = (SchedulerAppointmentContainer) details.NamingContainer;
            
            if (this.EditMode)
            {
                details.BannerId = new  Guid(dataItem.Appointment.Attributes["BannerId"]);
            }
            else
            {
               
                details.Start =  dataItem.Appointment.Start.Date;
                details.End = dataItem.Appointment.End.Date;
            }
        }
    }
}

//public class GridViewTextBoxTemplate : IBindableTemplate
//{
//    ListItemType templateType;
//    string fieldName;
//    public GridViewTextBoxTemplate(ListItemType type, string fieldName)
//    {
//        this.templateType = type;
//        this.fieldName = fieldName;
//    }

//    public void InstantiateIn(System.Web.UI.Control container)
//    {
//        switch (templateType)
//        {
//            case ListItemType.Item:
//                Label lbl = new Label();
//                lbl.Text = String.Empty; //you'll fill this in later in databinding
//                lbl.DataBinding += new EventHandler(OnDataBinding);
//                container.Controls.Add(lbl);
//                break;
//            case ListItemType.EditItem:
//                TextBox txt = new TextBox();
//                txt.Text = String.Empty; //you'll fill this in later in databinding
//                txt.DataBinding += new EventHandler(OnDataBinding);
//                container.Controls.Add(txt);
//                break;
//        }

//    }

//    public IOrderedDictionary ExtractValues(Control container)
//    {
//        OrderedDictionary dict = new OrderedDictionary();
//        if (templateType == ListItemType.EditItem)
//        {
//            string value = ((TextBox)container.Controls[0]).Text;
//            dict.Add(this.fieldName, value);
//        }
//        else
//        {
//            string value = ((Label)container.Controls[0]).Text;
//            dict.Add(this.fieldName, value);
//        }
//        return dict;
//    }

//    private void OnDataBinding(object sender, EventArgs e)
//    {
//        object databoundValue = DataBinder.Eval(((IDataItemContainer)((Control)sender).NamingContainer).DataItem, this.fieldName);

//        switch (templateType)
//        {
//            case ListItemType.Item:
//                Label literal = (Label)sender;
//                literal.Text = databoundValue.ToString();
//                break;
//            case ListItemType.EditItem:
//                TextBox textBox = sender as TextBox;
//                textBox.Text = databoundValue.ToString();
//                break;
//        }


//    }
//}