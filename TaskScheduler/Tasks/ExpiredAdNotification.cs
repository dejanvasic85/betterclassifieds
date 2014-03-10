using BetterClassified.UIController;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.Repository;
using Paramount.Broadcast.Components;
using System;
using System.Linq;
using System.Text;

namespace Paramount.TaskScheduler
{
    public class ExpiredAdNotification : IScheduler
    {
        private const string DaysBeforeExpiry = "DAYSBEFOREEXPIRY";
        private readonly IUserRepository userRepository;
        private readonly IApplicationConfig _applicationConfig;
        

        public ExpiredAdNotification()
        {
            userRepository = new UserRepository();  // hard code for now
            _applicationConfig = new AppConfig();
        }

        public void Run(SchedulerParameters parameters)
        {
            if (parameters == null)
                return;

            if (!parameters.ContainsKey(DaysBeforeExpiry))
                return;

            DateTime expiryDate = DateTime.Today.AddDays(int.Parse(parameters[DaysBeforeExpiry]));

            // Fetch the expiry list
            var expiryAdList = AdBookingController.GetExpiredAdList(expiryDate);
            if (expiryAdList.Count > 0)
            {
                // Construct and send the email ( per user )
                var email = new AdExpiryNotification();

                foreach (var ads in expiryAdList.GroupBy(e => e.Username))
                {
                    // Construct the email content
                    var sb = new StringBuilder();
                    foreach (var expiredAd in ads)
                    {
                        sb.AppendFormat("Ad ID: {0} has last print date of {1}<br />", expiredAd.AdId, expiredAd.LastPrintInsertionDate.ToString("dd/MM/yyyy"));
                    }

                    // Fetch the membership user 
                    ApplicationUser applicationUser = userRepository.GetUserByUsername(ads.Key);

                    // Construct the parameters for broadcasting
                    EmailRecipientView recipient = new EmailRecipientView { Email = applicationUser.Email, Name = applicationUser.Username }.AddTemplateItem("adReference", sb.ToString()).AddTemplateItem("linkForExtension", _applicationConfig.BaseUrl + "/MemberAccount/Bookings.aspx");
                    email.Recipients.Add(recipient);
                }

                // Send
                email.Send();
            }
        }

        public string Name
        {
            get { return "EXPAD"; }
        }
    }
}
