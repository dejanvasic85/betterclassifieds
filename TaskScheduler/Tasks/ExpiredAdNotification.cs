using System;

namespace Paramount.Products.TaskScheduler
{
    public class ExpiredAdNotification:IScheduler 
    {
        private const string DaysBeforeExpiry = "DAYSBEFOREEXPIRY";
        public void Run(SchedulerParameters parameters)
        {
            if (parameters == null) return;
            if (!parameters.ContainsKey(DaysBeforeExpiry)) return;
            int daysBeforeExpiry;
            if(int.TryParse(parameters[DaysBeforeExpiry], out daysBeforeExpiry))
                using (var proxy = new NotificationService.NotificationServiceSoapClient())
                {
                    var result = proxy.ExpiredAdEmailNotification(DateTime.Today.AddDays(daysBeforeExpiry));
                }
        }

        public string Name
        {
            get { return "EXPAD"; }
        }
    }
}
