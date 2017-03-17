using System;
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
        void SendTicketGuestEmail(AdSearchResult ad, EventBooking eventBooking, EventBookingTicket eventBookingTicket, byte[] ticket);
        void SendWelcomeEmail(string email, string username);
        void SendForgotPasswordEmail(string email, string newPassword);
        void SendGuestRemoval(AdSearchResult ad, EventModel eventModel, EventBookingTicket eventBookingTicket);
        void SendTicketTransfer(AdSearchResult ad, string previousGuestEmail, string newGuestEmail);
        void SendEventPaymentRequest(AdSearchResult ad, EventModel eventModel, string preferredPayment, decimal requestedAmount);
    }

    public class MailService : IMailService
    {
        private readonly ITemplatingService _templatingService;
        private readonly IUrl _url;
        private readonly IUserManager _userManager;
        private readonly IMailSender _mailSender;
        private readonly IClientConfig _clientConfig;
        private readonly IPdfGenerator _pdfGenerator;
        private const string EventDateFormat = "dd-MMM-yyyy hh:mm";

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
            public static string EventOrganiserView = "~/Views/Email/EventOrganiserInvite.cshtml";
            public static string EventPurchaserNotificationView = "~/Views/Email/EventTicketBuyer.cshtml";
            public static string EventBookingInvoiceView = "~/Views/Templates/Invoice.cshtml";
            public static string WelcomeView = "~/Views/Email/Welcome.cshtml";
            public static string ForgotPasswordView = "~/Views/Email/ForgotPassword.cshtml";
            public static string EventGuestTicketView = "~/Views/Email/EventTicketGuest.cshtml";
            public static string EventTicketTransferView = "~/Views/Email/EventTicketTransfer.cshtml";
            public static string EventGuestRemoved = "~/Views/Email/EventGuestRemoved.cshtml";
            public static string EventPaymentRequestView = "~/Views/Email/EventPaymentRequest.cshtml";
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
                StartDate = eventModel.EventStartDate?.ToString(EventDateFormat)
            };

            var body = _templatingService.Generate(viewModel, Views.EventPurchaserNotificationView);
            var attachments = new List<MailAttachment>();

            if (eventBooking.TotalCost > 0)
            {
                var invoiceViewModel = new EventBookingInvoiceViewModel(_clientConfig, eventBooking,
                    _userManager.GetCurrentUser(), ad.Heading);

                var invoiceHtml = _templatingService.Generate(invoiceViewModel, Views.EventBookingInvoiceView);
                var invoicePdf = _pdfGenerator.BuildFromHtml(invoiceHtml);

                var invoiceFileName = "Invoice - " + ad.Heading + ".pdf";

                attachments.Add(MailAttachment.Create(invoiceFileName, invoicePdf));
            }

            _mailSender.Send(to, body, "Event booking for " + ad.Heading, attachments.ToArray());
        }

        public void SendTicketGuestEmail(AdSearchResult ad, EventBooking eventBooking, EventBookingTicket eventBookingTicket, byte[] ticketAttachment)
        {
            var eventModel = eventBooking.Event;

            var eventUrl = _url.EventUrl(ad.HeadingSlug, ad.AdId);
            var body = _templatingService.Generate(new EventTicketGuestEmail
            {
                EventName = ad.Heading,
                EventUrl = eventUrl,
                EventLocation = eventModel.Location,
                BuyerName = eventBooking.GetFullName(),
                EventStartDateTime = eventModel?.EventStartDate.GetValueOrDefault().ToString(EventDateFormat),
                IsGuestTheBuyer = eventBooking.Email == eventBookingTicket.GuestEmail

            }, Views.EventGuestTicketView);

            var subject = "Tickets to " + ad.Heading;

            var attachments = new MailAttachment[]
            {
                MailAttachment.Create(subject + ".pdf", ticketAttachment),
                MailAttachment.CreateCalendarAttachment(_clientConfig.ClientName,
                    eventModel.EventId.GetValueOrDefault(),
                    ad.Heading,
                    ad.Description,
                    eventModel.EventStartDate.GetValueOrDefault(),
                    eventModel.EventEndDate.GetValueOrDefault(),
                    eventBookingTicket.GuestEmail,
                    eventModel.Location,
                    eventModel.LocationLatitude.GetValueOrDefault(),
                    eventModel.LocationLongitude.GetValueOrDefault(),
                    eventUrl,
                    eventModel.TimeZoneId),
            };

            _mailSender.Send(eventBookingTicket.GuestEmail, body, subject, attachments);
        }

        public void SendWelcomeEmail(string email, string username)
        {
            var clientName = _clientConfig.ClientName;

            var body = _templatingService.Generate(new WelcomeEmail
            {
                Email = email,
                Username = username,
                HomeUrl = _url.Home(),
                BrandName = clientName
            }, Views.WelcomeView);

            _mailSender.Send(email, body, "Welcome to " + clientName);
        }

        public void SendForgotPasswordEmail(string email, string newPassword)
        {
            var body = _templatingService.Generate(new ForgotPasswordEmail
            {
                Email = email,
                NewPassword = newPassword,
                LoginUrl = _url.Login()

            }, Views.ForgotPasswordView);

            _mailSender.Send(email, body, "Password Reset");
        }

        public void SendGuestRemoval(AdSearchResult ad, EventModel eventModel, EventBookingTicket eventBookingTicket)
        {
            var viewModel = new EventGuestRemovedEmail
            {
                EventName = ad.Heading,
                EventUrl = _url.EventUrl(ad.HeadingSlug, ad.AdId),
                EventStartDate = eventModel.EventStartDate.GetValueOrDefault().ToString(EventDateFormat),
                TicketNumber = eventBookingTicket.EventBookingTicketId.ToString()
            };

            var body = _templatingService.Generate(viewModel, Views.EventGuestRemoved);

            var subject = $"Ticket Cancelled for {ad.Heading}";

            _mailSender.Send(eventBookingTicket.GuestEmail, body, subject);
        }

        public void SendTicketTransfer(AdSearchResult ad, string previousGuestEmail, string newGuestEmail)
        {
            var eventUrl = _url.EventUrl(ad.HeadingSlug, ad.AdId);

            var body = _templatingService.Generate(new
                TicketTransferEmail
                {
                    EventName = ad.Heading,
                    EventUrl = eventUrl,
                    NewGuestEmail = newGuestEmail,
                    PreviousGuestEmail = previousGuestEmail
                }
                , Views.EventTicketTransferView);

            var subject = $"Tickets transfer for event {ad.Heading}";

            _mailSender.Send(previousGuestEmail,
                body,
                subject);
        }

        public void SendEventPaymentRequest(AdSearchResult ad, EventModel eventModel, string preferredPayment, decimal requestedAmount)
        {
            var subject = $"Event payment request for {ad.Heading} {ad.AdId}";
            var body = _templatingService.Generate(new EventPaymentRequestEmail
            {
                EventId = eventModel.EventId.GetValueOrDefault(),
                EventName = ad.Heading,
                PreferredPaymentMethod = preferredPayment,
                RequestedAmount = requestedAmount,
                RequestUsername = ad.Username

            }, Views.EventPaymentRequestView);

            _clientConfig.SupportEmailList.ForEach(email =>
            {
                _mailSender.Send(email, body, subject);
            });

        }
    }
}