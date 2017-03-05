using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels.Email;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IMailService
    {
        IMailService Initialise(Controller controller);
        void SendEventOrganiserInvite(string to,
            AdSearchResult ad,
            int eventId,
            string inviteToken);
    }

    public class MailService : IMailService
    {
        private readonly ITemplatingService _templatingService;
        private readonly IUrl _url;
        private readonly IUserManager _userManager;
        private readonly IMailSender<EmailDetails> _mailSender;

        public MailService(ITemplatingService templatingService, IUrl url, IUserManager userManager, IMailSender<EmailDetails> mailSender)
        {
            _templatingService = templatingService;
            _url = url;
            _userManager = userManager;
            _mailSender = mailSender;
        }

        public IMailService Initialise(Controller controller)
        {
            _templatingService.Init(controller);
            return this;
        }

        public void SendEventOrganiserInvite(string to,
            AdSearchResult ad, int eventId, string inviteToken)
        {
            var fakeVm = new EventOrganiserInviteViewModel
            {
                EventName = ad.Heading,
                HomeUrl = _url.Home().WithFullUrl().Build(),
                EventUrl = _url.EventUrl(ad.HeadingSlug, ad.AdId).WithFullUrl().Build(),
                FullName = _userManager.GetCurrentUser().FullName,
                AcceptInvitationUrl = _url.EventOrganiserInviteUrl(eventId, inviteToken, to).WithFullUrl().Build()
            };

            var body = _templatingService.Generate(fakeVm, "~/Views/Email/EventOrganiserInvite.cshtml");

            _mailSender.Send(new EmailDetails
            {
                Body = body,
                Subject = "You have an invite!",
                To = to
            });
        }
    }
}