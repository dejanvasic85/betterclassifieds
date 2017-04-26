using System;
using System.Linq;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Business.Events.Organisers
{
    public interface IEventOrganiserService
    {
        EventOrganiser CreateEventOrganiser(int eventId, string email);

        OrganiserConfirmationResult ConfirmOrganiserInvite(int eventId, string token, string recipient);

        void UpdateOrganiserNotifications(int eventId, ApplicationUser eventOrganiser, bool subscribeToPurchaseNotifications, bool subscribeToDailyNotifications);

        void RevokeOrganiserAccess(int eventOrganiserId);
    }

    public class EventOrganiserService : IEventOrganiserService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IBookingManager _bookingManager;
        private readonly IDateService _dateService;
        private readonly IUserManager _userManager;
        private readonly ILogService _logService;

        public EventOrganiserService(IEventRepository eventRepository, IBookingManager bookingManager, IDateService dateService, IUserManager userManager, ILogService logService)
        {
            _eventRepository = eventRepository;
            _bookingManager = bookingManager;
            _dateService = dateService;
            _userManager = userManager;
            _logService = logService;
        }

        public EventOrganiser CreateEventOrganiser(int eventId, string email)
        {
            var currentUser = _userManager.GetCurrentUser();
            var eventOrganiser = new EventOrganiserFactory(_dateService).Create(eventId, email, currentUser.Username);
            _eventRepository.CreateEventOrganiser(eventOrganiser);

            return eventOrganiser;
        }


        public OrganiserConfirmationResult ConfirmOrganiserInvite(int eventId, string token, string recipient)
        {
            var organisers = _eventRepository.GetEventOrganisersForEvent(eventId);

            var organiserToActivate = organisers?.FirstOrDefault(o =>
                o.IsActive &&
                o.Email.Equals(recipient, StringComparison.OrdinalIgnoreCase) &&
                o.InviteToken.ToString() == token);

            if (organiserToActivate == null)
            {
                _logService.Info($"Organiser not found for token {token}, event {eventId}, email {recipient}");
                return OrganiserConfirmationResult.NotFound;
            }

            if (organiserToActivate.UserId.HasValue())
            {
                _logService.Info($"Organiser {recipient} is already activated for event {eventId}");
                return OrganiserConfirmationResult.AlreadyActivated;
            }

            var applicationUser = _userManager.GetCurrentUser();

            if (applicationUser.Email.DoesNotEqual(recipient))
            {
                _logService.Info($"Mistatched email. Current: {applicationUser.Email}. Recipient: {recipient}");
                return OrganiserConfirmationResult.MismatchedEmail;
            }

            _logService.Info($"Activating organiser {recipient} for event {eventId}");
            organiserToActivate.LastModifiedDate = _dateService.Now;
            organiserToActivate.LastModifiedDateUtc = _dateService.UtcNow;
            organiserToActivate.UserId = applicationUser.Username;

            _eventRepository.UpdateEventOrganiser(organiserToActivate);

            return OrganiserConfirmationResult.Success;
        }

        public void UpdateOrganiserNotifications(int eventId, ApplicationUser eventOrganiser, bool subscribeToPurchaseNotifications,
            bool subscribeToDailyNotifications)
        {
            var eventDetails = _eventRepository.GetEventDetails(eventId);
            var ad = _bookingManager.GetBookingForOnlineAdId(eventDetails.OnlineAdId);
            var organiser = eventDetails.EventOrganisers
                .Where(o => o.IsActive)
                .FirstOrDefault(o => o.Email.Equals(eventOrganiser.Email, StringComparison.OrdinalIgnoreCase));

            if (organiser != null)
            {
                organiser.SubscribeToPurchaseNotifications = subscribeToPurchaseNotifications;
                organiser.SubscribeToDailyNotifications = subscribeToDailyNotifications;
                organiser.LastModifiedDate = _dateService.Now;
                organiser.LastModifiedDateUtc = _dateService.UtcNow;
                organiser.LastModifiedBy = eventOrganiser.Username;

                _eventRepository.UpdateEventOrganiser(organiser);

                return;
            }


            var isOwner = ad.UserId.Equals(eventOrganiser.Username, StringComparison.OrdinalIgnoreCase);

            if (organiser == null && !isOwner)
            {
                throw new UnauthorizedAccessException("You are not registered as an organiser for this event");
            }

            var newOrganiserProfile = new EventOrganiserFactory(_dateService)
                .Create(eventId, eventOrganiser.Email, eventOrganiser.Username);

            newOrganiserProfile.SubscribeToPurchaseNotifications = subscribeToPurchaseNotifications;

            // Todo SubscribeToDailyNotifications
            // newOrganiserProfile.SubscribeToDailyNotifications = subscribeToDailyNotifications;
            newOrganiserProfile.UserId = eventOrganiser.Username;

            _eventRepository.CreateEventOrganiser(newOrganiserProfile);
        }

        public void RevokeOrganiserAccess(int eventOrganiserId)
        {
            var eventOrganiser = _eventRepository.GetEventOrganiser(eventOrganiserId);
            if (eventOrganiser == null)
                throw new NullReferenceException($"The eventOrganiser id {eventOrganiserId} does not exist");

            var currentUser = _userManager.GetCurrentUser();
            eventOrganiser.LastModifiedDate = _dateService.Now;
            eventOrganiser.LastModifiedDateUtc = _dateService.UtcNow;
            eventOrganiser.LastModifiedBy = currentUser.Username;
            eventOrganiser.IsActive = false;

            _eventRepository.UpdateEventOrganiser(eventOrganiser);
        }
    }
}
