using System;
using System.Linq;
using System.Monads;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    public class SendEventDailySummary : ITask
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventManager _eventManager;
        private readonly IUserManager _userManager;
        private readonly IBroadcastManager _broadcastManager;
        private readonly ISearchService _searchService;


        public SendEventDailySummary(IEventRepository eventRepository, IEventManager eventManager, IUserManager userManager,
            IBroadcastManager broadcastManager, ISearchService searchService)
        {
            _eventRepository = eventRepository;
            _eventManager = eventManager;
            _userManager = userManager;
            _broadcastManager = broadcastManager;
            _searchService = searchService;
        }

        public void HandleArgs(TaskArguments args)
        {
        }

        public void Run()
        {
            // Fetch all the current events and send an email to each organiser
            foreach (var eventSearchResult in _searchService.GetCurrentEvents())
            {
                var adSearchResult = eventSearchResult.With(e => e.AdSearchResult);
                var eventDetails = eventSearchResult.With(e => e.EventDetails);

                var user = _userManager.GetUserByEmailOrUsername(adSearchResult.With(r => r.Username));
                var totalTicketsSold = _eventRepository.GetEventBookingTicketsForEvent(eventDetails.EventId.GetValueOrDefault()).ToList();
                var todaysTickets = totalTicketsSold.Where(t => t.CreatedDateTimeUtc >= DateTime.Today.ToUniversalTime()).ToList();

                var notification = new EventDailySummaryNotification
                {
                    EventName = adSearchResult.Heading,
                    TodaysDate = DateTime.Today.ToLongDateString(),

                    DailyTicketsCount = todaysTickets.Count.ToString(),
                    DailyTicketsValue = todaysTickets.Sum(t => t.With(ticket => ticket.TotalPrice)).ToString("C"),

                    TotalTicketsCount = totalTicketsSold.Count.ToString(),
                    TotalTicketsValue = totalTicketsSold.Sum(t => t.With(ticket => ticket.TotalPrice)).ToString("C")
                };

                _broadcastManager.Queue(notification, user.Email);
            }
        }

        public bool Singleton { get; }
    }
}
