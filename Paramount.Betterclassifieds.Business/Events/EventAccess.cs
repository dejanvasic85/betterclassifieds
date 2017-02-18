using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventAccess : ICategoryAdAuthoriser
    {
        private readonly IEventRepository _eventRepository;


        public EventAccess(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public bool IsUserAuthorisedForAdId(string username, AdBookingModel adBookingModel)
        {
            var onlineAdOnlineAdId = adBookingModel?.OnlineAd?.OnlineAdId;

            if (!onlineAdOnlineAdId.HasValue)
                throw new NullReferenceException($"Cannot authorise {username} for null booking");

            var eventDetails = _eventRepository.GetEventDetailsForOnlineAdId(
                onlineAdOnlineAdId.Value);

            return eventDetails
                ?.EventOrganisers
                ?.Any(org => org.UserId == username)
                ?? false;
        }

        public IEnumerable<int> GetAdsForUser(string username)
        {
            return _eventRepository.GetEventsForOrganiser(username)
                .Select(e => e.OnlineAdId);
        }
    }
}
