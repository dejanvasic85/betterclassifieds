using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Paramount.DSL.UI;
using Telerik.Web.UI;

namespace Paramount.Banners.UI.BannerAdmin.CalendarSupport
{
    public class AppointmentTemplate : ITemplate
    {
       
        public void InstantiateIn(Control container)
        {
            var subject = new Label();
            var image = new Image();
            container.Controls.Add(subject);
            subject.DataBinding += DetailsBinding;
        }

        private static void DetailsBinding(object sender, EventArgs e)
        {
            var details = (Label)sender;
            var dataItem = (Telerik.Web.UI.SchedulerAppointmentContainer)details.NamingContainer;

            var appointmentData = dataItem.Appointment;

            var firstRow = new Panel() { CssClass = "firs-row" };
            var secondRow = new Panel() { CssClass = "second-row" };



            firstRow.Controls.Add(new Image() { ImageUrl = "Image.ashx?" + GetImageQueryString(dataItem.Appointment.Attributes["ImageId"]) });
            // firstRow.Controls.Add(new DslThumbImage( { ImageUrl = "ThumbImage.axd?id=" + dataItem.Appointment.Attributes["ImageId"] });
            firstRow.Controls.Add(new Label() { Text = string.Format(CultureInfo.InvariantCulture, "[{0}] {1}", appointmentData.Attributes["Group"], dataItem.Appointment.Subject) });
            secondRow.Controls.Add(new Label() { Text = appointmentData.Attributes["Url"] });

            details.Controls.Add(firstRow);
            details.Controls.Add(secondRow);

        }

        private static string GetImageQueryString(string imageId)
        {
            var qparams = new[] { imageId, ApplicationBlock.Configuration.ConfigSettingReader.ClientCode, "10px", "10px" };

            return string.Format(CultureInfo.CurrentCulture, "docId={0};entity={1};height={2};width={3}", qparams);
        }
    }
}
