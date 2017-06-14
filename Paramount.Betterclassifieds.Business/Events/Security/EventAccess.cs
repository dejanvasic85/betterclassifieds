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
        private readonly ISearchService _searchService;

        public EventAccess(IEventRepository eventRepository, ISearchService searchService)
        {
            _eventRepository = eventRepository;
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
            
            return _eventRepository.GetEventsForOrganiser(username)
                .Select(e => e.OnlineAdId);
        }
    }
}
