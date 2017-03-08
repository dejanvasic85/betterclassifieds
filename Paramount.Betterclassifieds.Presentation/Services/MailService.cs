using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels.Email;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IMailService
    {
        IMailService Initialise(Controller controller);
        void SendEventOrganiserInvite(string to, AdSearchResult ad, int eventId, string inviteToken);
    }

    public class MailService : IMailService
    {
        private readonly ITemplatingService _templatingService;
        private readonly IUrl _url;
        private readonly IUserManager _userManager;
        private readonly IMailSender _mailSender;

        public MailService(ITemplatingService templatingService, IUrl url, IUserManager userManager, IMailSender mailSender)
        {
            _templatingService = templatingService;
            _url = url;
            _url.WithAbsoluteUrl(); // All outgoing links need to be absolute
            _userManager = userManager;
            _mailSender = mailSender;
        }

        struct Views
        {
            public const string EventOrganiserView = "~/Views/Email/EventOrganiserInvite.cshtml";
            public static string EventPurchaserNotificationView = "~/Views/Email/EventTicketBuyer.cshtml";
        }

        public IMailService Initialise(Controller controller)
        {
            _templatingService.Init(controller);
            return this;
        }

        public void SendEventOrganiserInvite(string to, AdSearchResult ad, int eventId, string inviteToken)
        {
            Guard.NotNullOrEmpty(to);
            Guard.NotNull(ad);
            Guard.MustBePositive(eventId);
            Guard.NotNullOrEmpty(inviteToken);

            var viewModel = new EventOrganiserInviteViewModel
            {
                EventName = ad.Heading,
                HomeUrl = _url.Home(),
                EventUrl = _url.EventUrl(ad.HeadingSlug, ad.AdId),
                FullName = _userManager.GetCurrentUser().FullName,
                AcceptInvitationUrl = _url.EventOrganiserInviteUrl(eventId, inviteToken, to)
            };

            var body = _templatingService.Generate(viewModel, Views.EventOrganiserView);

            _mailSender.Send(to, body, "Invite to manage " + ad.Heading);
        }

        public void SendTicketBuyerNotification(string to, AdSearchResult ad, EventModel eventModel)
        {
            Guard.NotNullOrEmpty(to);
            Guard.NotNull(ad);
            Guard.NotNull(eventModel);

            var viewModel = new EventTicketBuyerViewModel
            {
                EventName = ad.Heading,
                EventUrl = _url.EventUrl(ad.HeadingSlug, ad.AdId),
                Address = eventModel.Location,
                StartDate = eventModel.EventStartDate?.ToString("dd-MMM-yyyy hh:mm"),
            };

            var body = _templatingService.Generate(viewModel, Views.EventPurchaserNotificationView);

            _mailSender.Send(to, body, "Tickets booked for " + ad.Heading);
        }

        
    }
}