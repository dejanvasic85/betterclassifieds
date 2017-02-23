using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventAccess : ICategoryAdAuthoriser
    {
        private readonly IEventRepository _eventRepository;
        private readonly IBookingManager _bookingManager;
        private readonly ISearchService _searchService;

        public EventAccess(IEventRepository eventRepository, IBookingManager bookingManager, ISearchService searchService)
        {
            _eventRepository = eventRepository;
            _bookingManager = bookingManager;
            _searchService = searchService;
        }

        public bool IsUserAuthorisedForAd(string username, AdBookingModel adBookingModel)
        {
            Guard.NotNull(username);
            Guard.NotNull(adBookingModel);

            if (adBookingModel.UserId == username)
                return true;

            var onlineAdOnlineAdId = adBookingModel?.OnlineAd?.OnlineAdId;

            if (!onlineAdOnlineAdId.HasValue)
                throw new ArgumentNullException($"Cannot authorise {username} for null booking");

            var eventDetails = _eventRepository.GetEventDetailsForOnlineAdId(
                onlineAdOnlineAdId.Value);

            return CheckEventAccess(username, eventDetails);
        }

        private static bool CheckEventAccess(string username, EventModel eventDetails)
        {
            return eventDetails
                       ?.EventOrganisers
                       ?.Any(org => org.UserId == username && org.IsActive)
                   ?? false;
        }

        public bool IsUserAuthorisedForEvent(string username, int eventId)
        {
            var eventModel = _eventRepository.GetEventDetails(eventId);
            
            if (CheckEventAccess(username, eventModel))
                return true;

            var adBooking = _searchService.GetByAdOnlineId(eventModel.OnlineAdId);

            return adBooking?.Username == username;
        }

        public IEnumerable<int> GetOnlineAdsForUser(string username)
        {
            Guard.NotNull(username);

            var adsAsOwner = _bookingManager.GetBookingsForUser(username, 1000)
                .Select(b => b.OnlineAd.OnlineAdId)
                .ToList();
            
            var adsAsOrganiser = _eventRepository.GetEventsForOrganiser(username)
                .Select(e => e.OnlineAdId)
                .ToList();

            return adsAsOwner.Union(adsAsOrganiser).Distinct();
        }
    }
}
