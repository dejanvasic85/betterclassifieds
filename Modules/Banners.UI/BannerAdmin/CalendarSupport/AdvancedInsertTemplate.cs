using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI;

namespace Paramount.Banners.UI.BannerAdmin.CalendarSupport
{
    public class AdvancedInsertTemplate : ITemplate
    {
        #region IBindableTemplate Members

        public IOrderedDictionary ExtractValues(Control container)
        {
            var dict = new OrderedDictionary();
            var banner = ((AddBanner)container.Controls[0]);
            dict.Add("banner", banner.Banner);
            return dict;
        }

        #endregion

        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            var details = new AddBanner { ID = "addBanner" };
            container.Controls.Add(details);
            details.DataBinding += DetailsDataBinding;
        }

        #endregion

        private static void DetailsDataBinding(object sender, EventArgs e)
        {
            var details = (AddBanner)sender;
            var dataItem = (SchedulerAppointmentContainer)details.NamingContainer;
            Appointment appointmentData = dataItem.Appointment;
            details.BannerId = new Guid(appointmentData.ID.ToString());
        }

    }
}
