using System;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class EventViewToBookingCartConverter : ITypeConverter<EventViewModel, IBookingCart>
    {
        private readonly IDateService _dateService;
        private readonly IClientConfig _clientConfig;

        public EventViewToBookingCartConverter(IDateService dateService, IClientConfig clientConfig)
        {
            _dateService = dateService;
            _clientConfig = clientConfig;
        }

        public IBookingCart Convert(ResolutionContext context)
        {

            if (context.IsSourceValueNull)
            {
                throw new ArgumentNullException("EventViewModel cannot be null");
            }

            if (context.DestinationValue == null)
            {
                throw new ArgumentNullException("Unable to convert EventViewModel to an empty or NULL booking cart");
            }


            var bookingCart = (IBookingCart)context.DestinationValue;
            var eventViewModel = (EventViewModel)context.SourceValue;

            if (bookingCart.Event == null)
            {
                bookingCart.Event = new EventModel();
            }

            if (bookingCart.OnlineAdModel == null)
            {
                bookingCart.OnlineAdModel = new OnlineAdModel();
            }

            // Generic online details
            bookingCart.OnlineAdModel.Heading = eventViewModel.Title;
            bookingCart.OnlineAdModel.SetDescription(eventViewModel.Description.Replace(Environment.NewLine, "<br>"));
            bookingCart.OnlineAdModel.ContactName = eventViewModel.OrganiserName;
            bookingCart.OnlineAdModel.ContactPhone = eventViewModel.OrganiserPhone;
            if (eventViewModel.EventPhoto.HasValue())
            {
                bookingCart.OnlineAdModel.AddImage(eventViewModel.EventPhoto);
            }

            // Event details
            bookingCart.Event.EventStartDate = _dateService.ConvertFromString(eventViewModel.EventStartDate, eventViewModel.EventStartTime);
            bookingCart.Event.EventEndDate = _dateService.ConvertFromString(eventViewModel.EventEndDate, eventViewModel.EventEndTime);
            bookingCart.Event.Location = eventViewModel.Location;
            bookingCart.Event.LocationLatitude = eventViewModel.LocationLatitude;
            bookingCart.Event.LocationLongitude = eventViewModel.LocationLongitude;
            bookingCart.Event.Tickets = eventViewModel.Tickets.Select(ToModel).ToList();

            // Schedule
            bookingCart.SetSchedule(_clientConfig, _dateService.ConvertFromString(eventViewModel.AdStartDate));


            return bookingCart;
        }

        private EventTicket ToModel(EventTicketViewModel eventTicket)
        {
            return new EventTicket
            {
                TicketId = eventTicket.TicketId,
                TicketName = eventTicket.TicketName,
                Price = eventTicket.Price,
                AvailableQuantity = eventTicket.AvailableQuantity
            };
        }
    }
}