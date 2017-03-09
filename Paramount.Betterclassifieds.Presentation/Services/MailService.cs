using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels.Email;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IMailService
    {
        IMailService Initialise(Controller controller);

        void SendEventOrganiserInvite(string to, AdSearchResult ad, int eventId, string inviteToken);

        void SendTicketBuyerEmail(string to, AdSearchResult ad, EventBooking eventBooking);
    }

    public class MailService : IMailService
    {
        private readonly ITemplatingService _templatingService;
        private readonly IUrl _url;
        private readonly IUserManager _userManager;
        private readonly IMailSender _mailSender;
        private readonly IClientConfig _clientConfig;
        private readonly IPdfGenerator _pdfGenerator;

        public MailService(ITemplatingService templatingService, IUrl url, IUserManager userManager, IMailSender mailSender, IClientConfig clientConfig, IPdfGenerator pdfGenerator)
        {
            _templatingService = templatingService;
            _url = url;
            _url.WithAbsoluteUrl(); // All outgoing links need to be absolute
            _userManager = userManager;
            _mailSender = mailSender;
            _clientConfig = clientConfig;
            _pdfGenerator = pdfGenerator;
        }

        struct Views
        {
            public const string EventOrganiserView = "~/Views/Email/EventOrganiserInvite.cshtml";
            public static string EventPurchaserNotificationView = "~/Views/Email/EventTicketBuyer.cshtml";
            public static string EventBookingInvoiceView = "~/Views/Templates/Invoice.cshtml";
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

            var viewModel = new EventOrganiserInviteEmail
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

        public void SendTicketBuyerEmail(string to, AdSearchResult ad, EventBooking eventBooking)
        {
            Guard.NotNullOrEmpty(to);
            Guard.NotNull(ad);
            Guard.NotNull(eventBooking);
            Guard.NotNull(eventBooking.Event);

            var eventModel = eventBooking.Event;

            var viewModel = new EventTicketBuyerEmail
            {
                EventName = ad.Heading,
                EventUrl = _url.EventUrl(ad.HeadingSlug, ad.AdId),
                Address = eventModel.Location,
                StartDate = eventModel.EventStartDate?.ToString("dd-MMM-yyyy hh:mm")
            };

            var body = _templatingService.Generate(viewModel, Views.EventPurchaserNotificationView);
            var attachments = new List<Attachment>();

            if (eventBooking.TotalCost > 0)
            {
                var invoiceViewModel = new EventBookingInvoiceViewModel(_clientConfig, eventBooking,
                    _userManager.GetCurrentUser(), ad.Heading);

                var invoiceHtml = _templatingService.Generate(invoiceViewModel, Views.EventBookingInvoiceView);
                var invoicePdf = _pdfGenerator.BuildFromHtml(invoiceHtml);

                var stream = new MemoryStream(invoicePdf);
                var invoiceFileName = "Invoice - " + ad.Heading + ".pdf";
                var attachment = new Attachment(stream, invoiceFileName, ContentType.Pdf);
                attachments.Add(attachment);
            }

            _mailSender.Send(to, body, "Event booking for " + ad.Heading, attachments.ToArray());
        }


    }
}