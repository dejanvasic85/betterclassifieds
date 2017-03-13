﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IEventBookingManager
    {
        IEventBookingManager WithEventBooking(int? eventBookingId);
        IEventBookingManager WithTemplateService(ITemplatingService templateService);
        EventTicketsBookedNotification CreateTicketPurchaserNotification();
        byte[] CreateTicketAttachment(EventBookingTicket ticket);
        EventTicketPrintViewModel CreateEventTicketPrintViewModel(EventBookingTicket ticket);
        IEnumerable<EventTicketPrintViewModel> CreateEventTicketPrintViewModelsForBooking();
        byte[] CreateInvoiceAttachment();
        IEnumerable<EventGuestNotification> CreateEventGuestNotifications();
        IEnumerable<EventGuestNotification> CreateEventGuestNotifications(string targetGuestEmail);
        IEnumerable<EventGuestResendNotification> CreateEventGuestResendNotifications();
        EventBookedViewModel CreateEventBookedViewModel();
        EventGuestTransferFromNotification CreateEventTransferEmail(string ticketName,
            string newGuestEmail, string newGuestFullName);
    }

    public class EventBookingManager : IMappingBehaviour, IEventBookingManager
    {
        private readonly HttpContextBase _httpContextBase;
        private readonly IClientConfig _clientConfig;
        private readonly IEventManager _eventManager;
        private readonly ISearchService _searchService;
        private readonly IUserManager _userManager;

        private ITemplatingService _templateService;
        private EventBookedViewModel _eventBookedViewModel;

        public EventBookingManager(
            HttpContextBase httpContextBase,
            ISearchService searchService,
            IClientConfig clientConfig,
            IEventManager eventManager,
            IUserManager userManager)
        {
            _httpContextBase = httpContextBase;
            _searchService = searchService;
            _clientConfig = clientConfig;
            _eventManager = eventManager;
            _userManager = userManager;
        }


        public int? EventBookingId { get; private set; }

        public IEventBookingManager WithEventBooking(int? eventBookingId)
        {
            Guard.NotNull(eventBookingId);

            if (EventBookingId.HasValue && EventBookingId.Value != eventBookingId)
                throw new ArgumentException("Cannot set a different event booking at the moment. You can only do this once until we figure out how to reset lazy objects in a nice way...");

            if (EventBookingId.HasValue)
                return this;

            EventBookingId = eventBookingId.GetValueOrDefault();

            CreateEventBookedViewModel();

            return this;
        }

        public EventBookedViewModel CreateEventBookedViewModel()
        {
            if (_eventBookedViewModel != null)
                return _eventBookedViewModel;

            if (!EventBookingId.HasValue)
                throw new NullReferenceException("Please call WithEventBooking before attempting to create the EventBookedViewModel");

            _eventBookedViewModel = new EventBookedViewModel(Ad.Value, EventDetails.Value, EventBooking.Value, _clientConfig, _httpContextBase, EventGroups.Value);

            return _eventBookedViewModel;
        }

        public IEventBookingManager WithTemplateService(ITemplatingService templateService)
        {
            _templateService = templateService;
            return this;
        }

        private Lazy<EventBooking> EventBooking => new Lazy<EventBooking>(() =>
            {
                if (!EventBookingId.HasValue)
                    throw new NullReferenceException("Please call WithEventBooking before trying to fetch the object");

                return _eventManager.GetEventBooking(EventBookingId.Value);
            });

        private Lazy<AdSearchResult> Ad => new Lazy<AdSearchResult>(
            () => _searchService.GetByAdOnlineId(EventDetails.Value.OnlineAdId));

        private Lazy<ApplicationUser> EventBookingUser => new Lazy<ApplicationUser>(
            () => _userManager.GetUserByEmail(EventBooking.Value.Email));

        private Lazy<EventModel> EventDetails => new Lazy<EventModel>(
            () => _eventManager.GetEventDetails(EventBooking.Value.EventId));

        private Lazy<IEnumerable<EventGroup>> EventGroups => new Lazy<IEnumerable<EventGroup>>(
            () => _eventManager.GetEventGroups(EventDetails.Value.EventId.GetValueOrDefault()));

        private Lazy<string> EventUrl => new Lazy<string>(
            () =>
            {
                var helper = new UrlHelper(_httpContextBase.Request.RequestContext);
                var adDetails = Ad.Value;
                return helper.AdUrl(adDetails.HeadingSlug, adDetails.AdId, adDetails.CategoryAdType).WithFullUrl();
            });

        public EventGuestTransferFromNotification CreateEventTransferEmail(string ticketName,
            string newGuestEmail, string newGuestFullName)
        {
            return new EventGuestTransferFromNotification
            {
                EventName = Ad.Value.Heading,
                EventStartDate = EventDetails.Value.EventStartDate.GetValueOrDefault().ToString(Constants.DATE_FORMAT),
                EventUrl = EventUrl.Value,
                TicketName = ticketName,
                NewGuestEmail = newGuestEmail,
                NewGuestName = newGuestFullName
            };
        }

        public EventTicketsBookedNotification CreateTicketPurchaserNotification()
        {
            var notification = this.Map<EventBookedViewModel, EventTicketsBookedNotification>(_eventBookedViewModel);

            if (EventBooking.Value.TotalCost > 0)
                notification.WithInvoice(CreateInvoiceAttachment());

            return notification;
        }

        public byte[] CreateTicketAttachment(EventBookingTicket ticket)
        {
            var viewModel = CreateEventTicketPrintViewModel(ticket);

            var ticketHtml = _templateService.Generate(new List<EventTicketPrintViewModel> { viewModel }, "~/Views/Templates/Tickets.cshtml");
            var ticketPdfData = new NReco.PdfGenerator.HtmlToPdfConverter().GeneratePdf(ticketHtml);

            _eventManager.CreateEventTicketDocument(ticket.EventBookingTicketId, ticketPdfData);

            return ticketPdfData;
        }

        public EventTicketPrintViewModel CreateEventTicketPrintViewModel(EventBookingTicket ticket)
        {
            var viewModelFactory = new EventTicketPrintViewModelFactory();
            return viewModelFactory.Create(Ad.Value, EventDetails.Value, ticket, EventGroups.Value);
        }

        public IEnumerable<EventTicketPrintViewModel> CreateEventTicketPrintViewModelsForBooking()
        {
            var viewModelFactory = new EventTicketPrintViewModelFactory();
            return EventBooking.Value.EventBookingTickets.Select(t => viewModelFactory.Create(Ad.Value, EventDetails.Value, t, EventGroups.Value));
        }

        public byte[] CreateInvoiceAttachment()
        {
            var invoiceViewModel = new EventBookingInvoiceViewModel(_clientConfig,
                EventBooking.Value, EventBookingUser.Value, Ad.Value.Heading);

            var invoiceHtml = _templateService.Generate(invoiceViewModel, "~/Views/Templates/Invoice.cshtml");
            return new NReco.PdfGenerator.HtmlToPdfConverter().GeneratePdf(invoiceHtml);
        }

        public IEnumerable<EventGuestNotification> CreateEventGuestNotifications()
        {
            var eventGuestNotificationFactory = new EventGuestNotificationFactory();
            return EventBooking.Value.EventBookingTickets.Select(ToEventGuestNotification(eventGuestNotificationFactory));
        }

        public IEnumerable<EventGuestNotification> CreateEventGuestNotifications(string targetGuestEmail)
        {
            var eventGuestNotificationFactory = new EventGuestNotificationFactory();

            return EventBooking.Value.EventBookingTickets
                .Where(t => t.GuestEmail.Equals(targetGuestEmail, StringComparison.OrdinalIgnoreCase))
                .Select(ToEventGuestNotification(eventGuestNotificationFactory));
        }

        private Func<EventBookingTicket, EventGuestNotification> ToEventGuestNotification(EventGuestNotificationFactory eventGuestNotificationFactory)
        {
            return eventBookingTicket => eventGuestNotificationFactory.Create(
                _httpContextBase,
                _clientConfig,
                EventDetails.Value,
                eventBookingTicket,
                Ad.Value,
                EventGroups.Value,
                EventUrl.Value,
                EventBooking.Value.GetFullName(),
                CreateTicketAttachment(eventBookingTicket));
        }

        public IEnumerable<EventGuestResendNotification> CreateEventGuestResendNotifications()
        {
            var eventGuestNotificationFactory = new EventGuestNotificationFactory();

            return EventBooking.Value.EventBookingTickets.Select(eventBookingTicket => eventGuestNotificationFactory.CreateGuestResendNotification(
                _httpContextBase,
                _clientConfig,
                EventDetails.Value,
                eventBookingTicket,
                Ad.Value,
                EventGroups.Value,
                EventUrl.Value,
                EventBooking.Value.GetFullName(),
                CreateTicketAttachment(eventBookingTicket)));
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<EventBookedViewModel, EventTicketsBookedNotification>()
                .ForMember(m => m.DocumentType, options => options.Ignore());
        }
    }
}