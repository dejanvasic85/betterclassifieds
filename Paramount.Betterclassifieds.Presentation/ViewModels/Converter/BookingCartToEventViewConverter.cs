using System;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class BookingCartToEventViewConverter : ITypeConverter<IBookingCart, EventViewModel>
    {
        private readonly IDateService _dateService;

        public BookingCartToEventViewConverter(IDateService dateService)
        {
            _dateService = dateService;
        }

        public EventViewModel Convert(ResolutionContext context)
        {
            if (context.IsSourceValueNull)
            {
                throw new ArgumentNullException("BookingCart cannot be null");
            }

            var eventViewModel = context.DestinationValue as EventViewModel ?? new EventViewModel();
            var bookingCart = (IBookingCart)context.SourceValue;

            eventViewModel.AdStartDate = _dateService.ConvertToString(bookingCart.StartDate);

            if (bookingCart.OnlineAdModel != null)
            {
                eventViewModel.Title = bookingCart.OnlineAdModel.Heading;
                if (bookingCart.OnlineAdModel.Description.HasValue())
                {
                    eventViewModel.Description = bookingCart.OnlineAdModel.HtmlText.Replace("<br>", Environment.NewLine);
                }
                eventViewModel.OrganiserName = bookingCart.OnlineAdModel.ContactName;
                eventViewModel.OrganiserPhone = bookingCart.OnlineAdModel.ContactPhone;
            }

            if (bookingCart.Event == null)
            {
                bookingCart.Event = new AdFactory(_dateService).CreateEvent();
            }

            eventViewModel.EventStartDate = _dateService.ConvertToString(bookingCart.Event.EventStartDate);
            eventViewModel.EventStartTime = _dateService.ConvertToString(bookingCart.Event.EventStartDate, "HH:mm");
            eventViewModel.EventEndDate = _dateService.ConvertToString(bookingCart.Event.EventEndDate);
            eventViewModel.EventEndTime = _dateService.ConvertToString(bookingCart.Event.EventEndDate, "HH:mm");
            eventViewModel.Location = bookingCart.Event.Location;
            eventViewModel.LocationLatitude = bookingCart.Event.LocationLatitude;
            eventViewModel.LocationLongitude = bookingCart.Event.LocationLongitude;

            return eventViewModel;
        }
    }
}