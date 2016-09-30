using System;
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
    public interface IEventNotificationBuilder
    {
        IEventNotificationBuilder WithEventBooking(int? eventBookingId);
        IEventNotificationBuilder WithTemplateService(ITemplatingService templateService);
        EventTicketsBookedNotification CreateTicketPurchaserNotification();
        byte[] CreateTicketsAttachment();
        IEnumerable<EventTicketPrintViewModel> CreateEventTicketPrintViewModels();
        byte[] CreateInvoiceAttachment();
        IEnumerable<EventGuestNotification> CreateEventGuestNotifications();
        EventBookedViewModel CreateEventBookedViewModel();
    }

    public class EventNotificationBuilder : IMappingBehaviour, IEventNotificationBuilder
    {
        private readonly HttpContextBase _httpContextBase;
        private readonly IClientConfig _clientConfig;
        private readonly IEventManager _eventManager;
        private readonly ISearchService _searchService;
        private readonly IUserManager _userManager;

        private ITemplatingService _templateService;
        private EventBookedViewModel _eventBookedViewModel;

        public EventNotificationBuilder(
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

        public IEventNotificationBuilder WithEventBooking(int? eventBookingId)
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

        public IEventNotificationBuilder WithTemplateService(ITemplatingService templateService)
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
            () => Task.Run(() => _eventManager.GetEventGroups(EventDetails.Value.EventId.GetValueOrDefault())).Result);

        private Lazy<string> EventUrl => new Lazy<string>(
            () =>
            {
                var helper = new UrlHelper(_httpContextBase.Request.RequestContext);
                var adDetails = Ad.Value;
                return helper.AdUrl(adDetails.HeadingSlug, adDetails.AdId, adDetails.CategoryAdType).WithFullUrl();
            });

        public EventTicketsBookedNotification CreateTicketPurchaserNotification()
        {
            var notification = this.Map<EventBookedViewModel, EventTicketsBookedNotification>(_eventBookedViewModel)
                .WithTickets(CreateTicketsAttachment());

            if (EventBooking.Value.TotalCost > 0)
                notification.WithInvoice(CreateInvoiceAttachment());

            return notification;
        }

        public byte[] CreateTicketsAttachment()
        {
            var viewModels = CreateEventTicketPrintViewModels().ToList();
            var ticketHtml = _templateService.Generate(viewModels, "~/Views/Templates/Tickets.cshtml");
            var ticketPdfData = new NReco.PdfGenerator.HtmlToPdfConverter().GeneratePdf(ticketHtml);

            return ticketPdfData;
        }

        public IEnumerable<EventTicketPrintViewModel> CreateEventTicketPrintViewModels()
        {
            var viewModelFactory = new EventTicketPrintViewModelFactory();
            var viewModels = EventBooking.Value.EventBookingTickets.Select(t =>
                viewModelFactory.Create(Ad.Value, EventDetails.Value, t, EventGroups.Value));

            return viewModels;
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

            return EventBooking.Value.EventBookingTickets.Select(eventBookingTicket => eventGuestNotificationFactory.Create(
                _httpContextBase,
                _clientConfig,
                EventDetails.Value,
                eventBookingTicket,
                Ad.Value,
                EventUrl.Value,
                EventBooking.Value.GetFullName()));
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<EventBookedViewModel, EventTicketsBookedNotification>()
                .ForMember(m => m.DocumentType, options => options.Ignore());
        }
    }
}