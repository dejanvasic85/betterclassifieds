using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Paramount.Banners.UIController;
using Paramount.Common.UI.BaseControls;
using Telerik.Web.UI;

namespace Paramount.Banners.UI.BannerAdmin
{
    public class Calendar : ParamountCompositeControl
    {
        protected RadScheduler scheduler;

        public string WebServicePath { get; set; }

        public Calendar()
        {
            scheduler = new RadScheduler
                            {
                                
                                AppointmentTemplate = new CalendarSupport.AppointmentTemplate(),
                                AdvancedEditTemplate = new CalendarSupport.AdvancedEditTemplate(){},
                                AdvancedInsertTemplate = new CalendarSupport.AdvancedEditTemplate(),
                                DataKeyField = "ID",
                                DataSubjectField = "Subject",
                                DataStartField = "Start",
                                DataEndField = "End",
                                CustomAttributeNames = new[] { "BannerId", "Group", "Url" },
                                StartEditingInAdvancedForm=true,
                                StartInsertingInAdvancedForm=true,
                                SelectedView = SchedulerViewType.MonthView
                            };

            //scheduler.WebServiceSettings.Path = WebServicePath;
            scheduler.WebServiceSettings.ResourcePopulationMode = SchedulerResourcePopulationMode.Manual;
            scheduler.AppointmentInsert += AddBanner;
            scheduler.AppointmentUpdate += EditBanner;
            scheduler.AppointmentDelete += DeleteBanner;
            scheduler.AppointmentCommand += AppointmentCommand;
            scheduler.NavigationCommand += NavigateCalendar;
            scheduler.FormCreating+= FormCreating;
            
           //  scheduler.AppointmentUpdate += AppointmentInsert;
        }

        private static void FormCreating(object sender, SchedulerFormCreatingEventArgs e)
        {
            SessionManager.BannerTags = null;
        }

        //private void AppointmentInsert(object sender, SchedulerCancelEventArgs e)
        //{
        //    var bannerDetails =
        //        (BannerDetails)
        //        ((SchedulerAppointmentContainer) e.Appointment.DataItem).BindingContainer.FindControl("bannerDetails");
        //    if (bannerDetails != null)
        //    {
        //       if (!bannerDetails.Save())
        //       {
        //           e.Cancel = true;
        //       }

        //        this.scheduler.HideEditForm();
        //        RenderAppointments();
        //    }
        //}


        private void AppointmentCommand(object sender, AppointmentCommandEventArgs e)
        {
            if (e.CommandName == "Insert" || e.CommandName == "Update")
            {
                var bannerDetails = (BannerDetails)e.Container.FindControl("bannerDetails");
                if (bannerDetails != null)
                {
                    
                     if (bannerDetails.Save())
                     {
                         this.scheduler.HideEditForm();   
                         RenderAppointments();
                     }
                }
            }
        }

        public event EventHandler Add;

        public void InvokeAdd(EventArgs e)
        {
            EventHandler handler = Add;
            if (handler != null) handler(this, e);
        }

        public event EventHandler Edit;

        public void InvokeEdit(EventArgs e)
        {
            EventHandler handler = Edit;
            if (handler != null) handler(this, e);
        }

        public event EventHandler Remove;

        public void InvokeRemove(EventArgs e)
        {
            EventHandler handler = Remove;
            if (handler != null) handler(this, e);
        }

        private void NavigateCalendar(object sender, SchedulerNavigationCommandEventArgs e)
        {
            this.RenderAppointments();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Controls.Add(scheduler);

            //ApplicationBlock.Configuration.ConfigSettingReader.ClientCode

            // var dates = new List<DateTime>();

            //var startDate = DateTime.Now;
            //var endDate = DateTime.Now.AddDays(30);
            //var currentDate = startDate;

            //while (currentDate < endDate)
            //{
            //    currentDate = currentDate.AddDays(1);
            //    dates.Add(currentDate);
            //}


            //var bs = new Banners();


            //var aps = new List<AppointmentData>();
            //while (currentDate < endDate)
            //{
            //    var bs=from item 
            //    dates.Add(currentDate);
            //}
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.Page.IsPostBack)
            {
                RenderAppointments();
            }
        }

        private void RenderAppointments()
        {
            var dateRange = new DateRange(scheduler.SelectedDate, scheduler.SelectedView);
            scheduler.DataSource = BannerController.GetBanners(Guid.Empty, scheduler.VisibleRangeStart, scheduler.VisibleRangeEnd); //GetBanners(scheduler.VisibleRangeStart,scheduler.VisibleRangeEnd);
            scheduler.DataBind();

        }

        private static IEnumerable<AppointmentData> GetBanners(DateTime fromDate, DateTime toDate)
        {
            var banners = BannerController.GetBanners(Guid.Empty, fromDate, toDate);
            
            return banners.ConvertData();
        }

        protected void AddBanner(object sender, SchedulerCancelEventArgs e)
        {
            InvokeAdd(EventArgs.Empty);
        }

        protected void EditBanner(object sender, AppointmentUpdateEventArgs e)
        {
            InvokeEdit(EventArgs.Empty);
        }

        protected void DeleteBanner(object sender, SchedulerCancelEventArgs e)
        {
            InvokeRemove(EventArgs.Empty);
        }
    }


    internal class DateRange
    {
        public DateRange(DateTime selectedDate, SchedulerViewType schedulerViewType)
        {
            SelectedDate = selectedDate;

            switch (schedulerViewType)
            {
                case SchedulerViewType.DayView:
                    {
                        StartDate = SelectedDate;
                        EndDate = SelectedDate;
                        break;
                    }
                case SchedulerViewType.WeekView:
                    {
                        GetWeekRange(SelectedDate);
                        break;
                    }
                case SchedulerViewType.MonthView:
                    {
                        int daysMonth = DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month);
                        var sbDate = new StringBuilder();
                        sbDate.Append("01/");
                        sbDate.Append(SelectedDate.Month);
                        sbDate.Append("/");
                        sbDate.Append(SelectedDate.Year);
                        StartDate = DateTime.Parse(sbDate.ToString());
                        sbDate.Remove(0, sbDate.Length);

                        sbDate.Append(daysMonth);
                        sbDate.Append("/");
                        sbDate.Append(SelectedDate.Month);
                        sbDate.Append("/");
                        sbDate.Append(SelectedDate.Year);
                        EndDate = DateTime.Parse(sbDate.ToString());
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public DateTime SelectedDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        private void GetWeekRange(DateTime dateIn)
        {
            double offset = 0;
            switch (dateIn.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    offset = 0;
                    break;
                case DayOfWeek.Tuesday:
                    offset = -1;
                    break;
                case DayOfWeek.Wednesday:
                    offset = -2;
                    break;
                case DayOfWeek.Thursday:
                    offset = -3;
                    break;
                case DayOfWeek.Friday:
                    offset = -4;
                    break;
                case DayOfWeek.Saturday:
                    offset = -5;
                    break;
                case DayOfWeek.Sunday:
                    offset = -6;
                    break;
            }
            StartDate = dateIn.AddDays(offset);
            EndDate = StartDate.AddDays(6);
        }
    }
}