using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using Paramount.Banners.UI;
using Telerik.Web.UI;

namespace Paramount.Banners.UI
{
    public class BannerWcf
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

        [OperationContract]
        public IEnumerable<AppointmentData> GetAppointments(SchedulerInfo schedulerInfo)
        {
            return GetBanners(schedulerInfo);
        }

        [OperationContract]
        public IEnumerable<AppointmentData> InsertAppointment(SchedulerInfo schedulerInfo, AppointmentData appointmentData)
        {
            var banner = appointmentData.Convert();
            UIController.BannerController.SaveBanner(banner);

            return GetBanners(schedulerInfo);
        }

        [OperationContract]
        public IEnumerable<AppointmentData> UpdateAppointment(SchedulerInfo schedulerInfo, AppointmentData appointmentData)
        {
            var banner = appointmentData.Convert();
            UIController.BannerController.UpdateBanner(banner);

            return GetBanners(schedulerInfo);
        }

     

        private static IEnumerable<AppointmentData> GetBanners(ISchedulerInfo schedulerInfo)
        {
            var banners = UIController.BannerController.GetBanners(Guid.Empty, schedulerInfo.ViewStart, schedulerInfo.ViewEnd);
            return banners.ConvertData();
        }
        
        [OperationContract]
        public IEnumerable<AppointmentData> CreateRecurrenceException(SchedulerInfo schedulerInfo,
         AppointmentData recurrenceExceptionData)
        {
            throw new NotImplementedException();
            //return Controller.CreateRecurrenceException(schedulerInfo, recurrenceExceptionData);
        }

        [OperationContract]
        public IEnumerable<AppointmentData> RemoveRecurrenceExceptions(SchedulerInfo schedulerInfo,
         AppointmentData masterAppointmentData)
        {
            throw new NotImplementedException();
            // return Controller.RemoveRecurrenceExceptions(schedulerInfo, masterAppointmentData);
        }

        [OperationContract]
        public IEnumerable<AppointmentData> DeleteAppointment(SchedulerInfo schedulerInfo, AppointmentData appointmentData,
         bool deleteSeries)
        {
            throw new NotImplementedException();
            //return Controller.DeleteAppointment(schedulerInfo, appointmentData, deleteSeries);
        }

        [OperationContract]
        public IEnumerable<ResourceData> GetResources(SchedulerInfo schedulerInfo)
        {
            var groups = UIController.BannerController.GetBannerGroups();
            var resources = new Collection<ResourceData>();
            foreach (var group in groups)
            {
                resources.Add(new ResourceData()
                {
                    Key = group.GroupId,
                    Text = group.Title,
                    Type = "Banner Group"
                });
            }
            return resources;
        }

    }
}
