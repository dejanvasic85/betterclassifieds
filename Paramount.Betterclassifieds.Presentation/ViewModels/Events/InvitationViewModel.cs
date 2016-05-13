using System;
using System.Linq;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class InvitationViewModel
    {
        private readonly AdSearchResult _adSearchResult;
        private readonly EventModel _eventModel;
        private readonly UserNetworkModel _userNetwork;
        private readonly IClientConfig _clientConfig;
        private readonly EventInvitation _invitation;

        public InvitationViewModel(AdSearchResult adSearchResult, EventModel eventModel, UserNetworkModel userNetwork, 
            IClientConfig clientConfig, EventInvitation invitation)
        {
            _adSearchResult = adSearchResult;
            _eventModel = eventModel;
            _userNetwork = userNetwork;
            _clientConfig = clientConfig;
            _invitation = invitation;
        }

        public string EventName => _adSearchResult.Heading;
        public int EventId => _eventModel.EventId.GetValueOrDefault();
        public string EventStartDate => _eventModel.EventStartDate.GetValueOrDefault().ToString("hh:mm tt");
        public string Location => _eventModel.Location;
        public string GuestFullName => _userNetwork.FullName;
        public string GuestEmail => _userNetwork.UserNetworkEmail;
        public bool IsEventClosed => _eventModel.IsClosed;
        public bool IsAlreadyConfirmed => _invitation.ConfirmedDate.HasValue;

        public EventTicketViewModel[] Tickets
        {
            get
            {
                return _eventModel.Tickets.Select(t => new EventTicketViewModel
                {
                    EventId = t.EventId,
                    AvailableQuantity = t.AvailableQuantity,
                    EventTicketId = t.EventTicketId,
                    Price = _eventModel.IncludeTransactionFee.GetValueOrDefault()
                        ? new TicketFeeCalculator(_clientConfig).GetTotalTicketPrice(t).PriceIncludingFee
                        : t.Price,
                    RemainingQuantity = t.RemainingQuantity,
                    TicketName = t.TicketName

                }).ToArray();
            }
        }
    }
}