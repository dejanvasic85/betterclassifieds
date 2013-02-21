using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using Paramount.Banners.UIController;
using Telerik.Web.UI;

namespace Paramount.Banners.UI
{
    [WebService]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class BannerWebService : WebService
    {
        //private WebServiceAppointmentController _controller;

        //private WebServiceAppointmentController Controller
        //{
        //    get
        //    {
        //        if (_controller == null)
        //        {
        //            _controller =
        //                new WebServiceAppointmentController(
        //                    new XmlSchedulerProvider(HttpContext.Current.Server.MapPath("~/App_Data/Appointments_Outlook.xml"),
        //                        false));
        //        }
        //        return _controller;
        //    }
        //}

        [WebMethod(EnableSession = true)]
        public IEnumerable<AppointmentData> GetAppointments(SchedulerInfo schedulerInfo)
        {
            return GetBanners(schedulerInfo);
        }

        [WebMethod(EnableSession = true)]
        public IEnumerable<AppointmentData> InsertAppointment(SchedulerInfo schedulerInfo,
                                                              AppointmentData appointmentData)
        {
            var banner = appointmentData.Convert();
            BannerController.SaveBanner(banner);

            return GetBanners(schedulerInfo);
        }

        [WebMethod(EnableSession = true)]
        public IEnumerable<AppointmentData> UpdateAppointment(SchedulerInfo schedulerInfo,
                                                              AppointmentData appointmentData)
        {
            UIController.ViewObjects.Banner banner = appointmentData.Convert();
            BannerController.RebookBanner(banner);

            return GetBanners(schedulerInfo);
        }


        private static IEnumerable<AppointmentData> GetBanners(ISchedulerInfo schedulerInfo)
        {
            var banners = BannerController.GetBanners(Guid.Empty, schedulerInfo.ViewStart,schedulerInfo.ViewEnd);
            return banners.ConvertData();
        }
        

        [WebMethod(EnableSession = true)]
        public IEnumerable<AppointmentData> CreateRecurrenceException(SchedulerInfo schedulerInfo,
                                                                      AppointmentData recurrenceExceptionData)
        {
            throw new NotImplementedException();
            //return Controller.CreateRecurrenceException(schedulerInfo, recurrenceExceptionData);
        }

        [WebMethod(EnableSession = true)]
        public IEnumerable<AppointmentData> RemoveRecurrenceExceptions(SchedulerInfo schedulerInfo,
                                                                       AppointmentData masterAppointmentData)
        {
            throw new NotImplementedException();
            // return Controller.RemoveRecurrenceExceptions(schedulerInfo, masterAppointmentData);
        }

        [WebMethod(EnableSession = true)]
        public IEnumerable<AppointmentData> DeleteAppointment(SchedulerInfo schedulerInfo,
                                                              AppointmentData appointmentData,
                                                              bool deleteSeries)
        {

             BannerController.DeleteBanner(new Guid(appointmentData.Attributes["BannerId"]));
            return GetBanners(schedulerInfo);
        }

        [WebMethod(EnableSession = true)]
        public IEnumerable<ResourceData> GetResources(SchedulerInfo schedulerInfo)
        {
            var groups = BannerController.GetBannerGroups();
            var resources = new List<ResourceData>();
            foreach (UIController.ViewObjects.BannerGroup group in groups)
            {
                resources.Add(Convert(group));
            }
            return resources;
        }

        private static ResourceData Convert(UIController.ViewObjects.BannerGroup group)
        {
            return new ResourceData
                       {
                           Key = group.GroupId,
                           Text = group.Title,
                           Type = "Banner Group"
                       };
        }
    }
}