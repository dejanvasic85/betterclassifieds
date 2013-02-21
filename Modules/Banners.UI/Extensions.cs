using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI;

namespace Paramount.Banners.UI
{
    public static class Extensions
    {
        public static Appointment Convert(this UIController.ViewObjects.Banner banner)
        {
            var appointmentData = new Appointment
            {
                Start = banner.Start,
                End = banner.End,
                ID = banner.ID,
                Subject = banner.Title,
            };

            

            appointmentData.Resources.Add(new Resource
            {
                Key = banner.GroupId,
                Type = "Banner Group"
            });

            appointmentData.Attributes.Add("Group", banner.Group);
            appointmentData.Attributes.Add("AlternateText", banner.AlternateText);
            appointmentData.Attributes.Add("Url", banner.Url);
            appointmentData.Attributes.Add("ImageId", banner.ImageId.ToString());
            appointmentData.Attributes.Add("BannerId", banner.ID.ToString());
            return appointmentData;
        }

        public static IEnumerable<Appointment> Convert(this IEnumerable<UIController.ViewObjects.Banner> banners)
        {
            return banners.Select(Convert).ToList();
        }

        public static AppointmentData ConvertData(this UIController.ViewObjects.Banner banner)
        {
            var appointmentData = new AppointmentData
            {
                Start = banner.Start,
                End = banner.End,
                ID = banner.ID,
                Subject = banner.Title,
            };

            appointmentData.Resources.Add(new ResourceData
            {
                Key = banner.GroupId,
                Type = "Banner Group"
            });
            appointmentData.Attributes.Add(new KeyValuePair<string, string>("Group", banner.Group));
            appointmentData.Attributes.Add(new KeyValuePair<string, string>("AlternateText", banner.AlternateText));
            appointmentData.Attributes.Add(new KeyValuePair<string, string>("Url", banner.Url));
            appointmentData.Attributes.Add(new KeyValuePair<string, string>("ImageId", banner.ImageId.ToString()));
            appointmentData.Attributes.Add(new KeyValuePair<string, string>("BannerId", banner.ID.ToString()));
            return appointmentData;
        }

        public static IEnumerable<AppointmentData> ConvertData(this IEnumerable<UIController.ViewObjects.Banner> banners)
        {
            return banners.Select(ConvertData).ToList();
        }

        public static UIController.ViewObjects.Banner Convert(this AppointmentData appointmentData)
        {
            return new UIController.ViewObjects.Banner
            {
                AlternateText = appointmentData.Attributes["AlternateText"],
                ID = new Guid(appointmentData.Attributes["BannerId"]),
                Start = appointmentData.Start,
                End = appointmentData.End,
                Title = appointmentData.Subject,
                Url = appointmentData.Attributes["Url"],
                ImageId = appointmentData.Attributes["ImageId"]
            };
        }
    }
}
