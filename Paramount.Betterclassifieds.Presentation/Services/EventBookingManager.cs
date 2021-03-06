﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Services.Mail;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public interface IEventBookingManager
    {
        IEventBookingManager WithEventBooking(int? eventBookingId);
        IEventBookingManager WithTemplateService(ITemplatingService templateService);
        IEventBookingManager WithMailService(IMailService mailService);
        EventTicketPrintViewModel CreateEventTicketPrintViewModel(EventBookingTicket ticket);
        IEnumerable<EventTicketPrintViewModel> CreateEventTicketPrintViewModelsForBooking();
        EventBookedViewModel CreateEventBookedViewModel();
        IEventBookingManager SendTicketBuyerNotification();
        IEventBookingManager SendTicketsToAllGuests();
        IEventBookingManager SendTicketToGuest(string guestEmail);
        IEventBookingManager SendTicketToGuest(EventBookingTicket eventBookingTicket);
        IEventBookingManager SendTicketTransfer(string previousGuestEmail, string newGuestEmail);
        IEventBookingManager SendOrganisersNotification();
    }

    public class EventBookingManager : IEventBookingManager
    {
        private readonly HttpContextBase _httpContextBase;
        private readonly IClientConfig _clientConfig;
        private readonly IEventManager _eventManager;
        private readonly ISearchService _searchService;
        private readonly IUserManager _userManager;
        private readonly IUrl _url;
        private IMailService _mailService;
        private ITemplatingService _templateService;
        private EventBookedViewModel _eventBookedViewModel;

        public EventBookingManager(
            HttpContextBase httpContextBase,
            ISearchService searchService,
            IClientConfig clientConfig,
            IEventManager eventManager,
            IUserManager userManager,
            IUrl url)
        {
            _httpContextBase = httpContextBase;
            _searchService = searchService;
            _clientConfig = clientConfig;
            _eventManager = eventManager;
            _userManager = userManager;
            _url = url;
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

        public IEventBookingManager WithMailService(IMailService mailService)
        {
            _mailService = mailService;
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

        public IEventBookingManager SendTicketBuyerNotification()
        {
            _mailService.SendTicketBuyerEmail(EventBooking.Value.Email, Ad.Value, EventBooking.Value);
            return this;
        }

        public IEventBookingManager SendTicketsToAllGuests()
        {
            foreach (var eventBookingTicket in EventBooking.Value.EventBookingTickets)
            {
                SendTicketToGuest(eventBookingTicket);
            }
            return this;
        }

        public IEventBookingManager SendTicketToGuest(string guestEmail)
        {
            SendTicketToGuest(EventBooking.Value.EventBookingTickets.Single(t => t.GuestEmail.EqualTo(guestEmail)));
            return this;
        }

        public IEventBookingManager SendTicketTransfer(string previousGuestEmail, string newGuestEmail)
        {
            _mailService.SendTicketTransfer(Ad.Value,
                previousGuestEmail,
                newGuestEmail);

            return this;
        }

        public IEventBookingManager SendTicketToGuest(EventBookingTicket eventBookingTicket)
        {
            var ticketAttachmentContent = CreateTicketAttachment(eventBookingTicket);
            _mailService.SendTicketGuestEmail(Ad.Value, EventBooking.Value, eventBookingTicket, ticketAttachmentContent);
            return this;
        }

        private byte[] CreateTicketAttachment(EventBookingTicket ticket)
        {
            var viewModel = CreateEventTicketPrintViewModel(ticket);

            var ticketHtml = _templateService.Generate(new List<EventTicketPrintViewModel> { viewModel }, "~/Views/Templates/Tickets.cshtml");
            var ticketPdfData = new NReco.PdfGenerator.HtmlToPdfConverter().GeneratePdf(ticketHtml);

            _eventManager.CreateEventTicketDocument(ticket.EventBookingTicketId, ticketPdfData);

            return ticketPdfData;
        }

        public EventTicketPrintViewModel CreateEventTicketPrintViewModel(EventBookingTicket ticket)
        {
            var viewModelFactory = new EventTicketPrintViewModelFactory(_url, _clientConfig);
            return viewModelFactory.Create(Ad.Value, 
                EventDetails.Value, 
                ticket, 
                _clientConfig.ClientName,
                _clientConfig.PublisherHomeUrl,
                EventGroups.Value);
        }

        public IEnumerable<EventTicketPrintViewModel> CreateEventTicketPrintViewModelsForBooking()
        {
            return EventBooking.Value.EventBookingTickets.Select(CreateEventTicketPrintViewModel);
        }
        
        public IEventBookingManager SendOrganisersNotification()
        {
            var eventOwner = _userManager.GetUserByUsername(Ad.Value.Username);

            _mailService.SendEventOrganiserTicketsSold(eventOwner, EventDetails.Value.EventOrganisers,
                Ad.Value, EventBooking.Value);

            return this;
        }
    }
}