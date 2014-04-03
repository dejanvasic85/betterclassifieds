using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService;
using Paramount.Betterclassifieds.DataService.Broadcast;
using Paramount.Betterclassifieds.DataService.Repository;
using System;
using System.Linq;
using System.Text;

namespace Paramount.TaskScheduler
{
    public class ExpiredAdNotification : IScheduler
    {
        private const string DaysBeforeExpiry = "DAYSBEFOREEXPIRY";
        private readonly IUserRepository _userRepository;
        private readonly IApplicationConfig _applicationConfig;
        private readonly IBroadcastManager _broadcastManager;

        public ExpiredAdNotification()
        {
            _userRepository = new UserRepository();  // hard code for now
            _applicationConfig = new AppConfig();
            
            IBroadcastRepository broadcastRepository = new BroadcastRepository();
            INotificationProcessor processor = new EmailProcessor(broadcastRepository);

            _broadcastManager = new BroadcastManager(broadcastRepository, new[] {processor});

        }

        public void Run(SchedulerParameters parameters)
        {
            if (parameters == null)
                return;

            if (!parameters.ContainsKey(DaysBeforeExpiry))
                return;

            DateTime expiryDate = DateTime.Today.AddDays(int.Parse(parameters[DaysBeforeExpiry]));

            // Fetch the expiry list
            // var expiryAdList = AdBookingController.GetExpiredAdList(expiryDate);
            var expiryAdList = BetterclassifiedDataService.GetExpiredAdByLastEdition(expiryDate);

            if (expiryAdList.Count == 0) 
                return;

            foreach (var ads in expiryAdList.GroupBy(e => e.Username))
            {
                // Construct the email content
                var adReference = new StringBuilder();
                foreach (var expiredAd in ads)
                {
                    adReference.AppendFormat("Ad ID: {0} has last print date of {1}<br />", expiredAd.AdBookingId, expiredAd.LastEditionDate.ToString("dd/MM/yyyy"));
                }

                // Send the email to the user
                ExpirationReminder reminder = new ExpirationReminder
                {
                    AdReference = adReference.ToString(),
                    LinkForExtension = _applicationConfig.BaseUrl + "/MemberAccount/Bookings.aspx"
                };

                ApplicationUser applicationUser = _userRepository.GetUserByUsername(ads.Key);

                _broadcastManager.SendEmail(reminder, applicationUser.Email);
            }
        }

        public string Name
        {
            get { return "EXPAD"; }
        }
    }
}
