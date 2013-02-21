using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using Paramount.Common.UI;
using Paramount.Common.UI.BaseControls;

namespace Paramount.Banners.UI.BannerAdmin
{
    public class BannerTagTemplate : ITemplate
    {
        

        
        public void InstantiateIn(Control container)
        {
            var tagName = new TextBox() { ID = "tagName" };
            var tagValue = new TextBox() {ID = "tagValue"};
            var removeTag = new CheckBox() {ID = "removeTag"};


            var header = new Panel(){CssClass="banner-tags"};
            
            container.Controls.Add(header);

            header.Controls.Add(tagName);
            header.Controls.Add(tagValue);
            header.Controls.Add(removeTag);

            tagName.DataBinding += TagDataBinding;
            tagValue.DataBinding += TagValueDataBinding;
            
        }

        

        private static void TagDataBinding(object sender, EventArgs e)
        {
            var tagName = (TextBox) sender;
            var dataItem = (RepeaterItem)tagName.NamingContainer;
         
            tagName.Text = dataItem.DataItem.ToString();
            
        }

        private static void TagValueDataBinding(object sender, EventArgs e)
        {
            var tagValue = (TextBox)sender;
            var dataItem = (RepeaterItem)tagValue.NamingContainer;

            var ds = (NameValueCollection)((Repeater)dataItem.BindingContainer).DataSource;

            tagValue.Text = ds[dataItem.DataItem.ToString()];
            

         
        }
    }
}
