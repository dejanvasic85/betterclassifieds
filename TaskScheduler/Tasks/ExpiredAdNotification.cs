using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BetterClassified.Models;
using BetterClassified.Repository;
using BetterClassified.UIController;
using BetterClassified.UIController.ViewObjects;
using Paramount.Broadcast.Components;

namespace Paramount.TaskScheduler
{
    public class ExpiredAdNotification : IScheduler
    {
        private const string DaysBeforeExpiry = "DAYSBEFOREEXPIRY";
        private readonly IUserRepository userRepository;
        private readonly IConfigSettings configSettings;

        public ExpiredAdNotification()
        {
            userRepository = new UserRepository();  // hard code for now
            configSettings = new ConfigSettings();
        }

        public void Run(SchedulerParameters parameters)
        {
            if (parameters == null)
                return;

            if (!parameters.ContainsKey(DaysBeforeExpiry))
                return;

            DateTime expiryDate = DateTime.Today.AddDays(int.Parse(parameters[DaysBeforeExpiry]));

            // Fetch the expiry list
            List<ExpiredAdView> expiryAdList = AdBookingController.GetExpiredAdList(expiryDate);
            if (expiryAdList.Count > 0)
            {
                // Construct and send the email ( per user )
                var email = new AdExpiryNotification();

                foreach (var ads in expiryAdList.GroupBy(e => e.Username))
                {
                    // Construct the email content
                    StringBuilder sb = new StringBuilder();
                    foreach (var expiredAd in ads)
                    {
                        sb.AppendFormat("iFlog ID: {0} has last print date of {1}<br />", expiredAd.AdId, expiredAd.LastPrintInsertionDate.ToString("dd/MM/yyyy"));
                    }

                    // Fetch the membership user 
                    ApplicationUser applicationUser = userRepository.GetClassifiedUser(ads.Key);

                    // Construct the parameters for broadcasting
                    EmailRecipientView recipient = new EmailRecipientView { Email = applicationUser.Email, Name = applicationUser.Username }.AddTemplateItem("adReference", sb.ToString()).AddTemplateItem("linkForExtension", configSettings.BaseUrl + "/MemberAccount/Bookings.aspx");
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
