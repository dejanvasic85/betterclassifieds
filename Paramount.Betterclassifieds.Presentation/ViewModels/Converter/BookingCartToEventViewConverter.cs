using System;
using System.Linq;
using System.Web;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

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

                var adText = AdText.FromHtmlEncoded(bookingCart.OnlineAdModel.HtmlText);
                eventViewModel.Description = adText.HtmlText;
                
                eventViewModel.OrganiserName = bookingCart.OnlineAdModel.ContactName;
                eventViewModel.OrganiserPhone = bookingCart.OnlineAdModel.ContactPhone;
                var photo = bookingCart.OnlineAdModel.Images.FirstOrDefault();
                if (photo != null) 
                {
                    eventViewModel.EventPhoto = photo.DocumentId;
                }
            }

            if (bookingCart.Event == null)
            {
                bookingCart.Event = new AdFactory(_dateService).CreateEvent();
            }

            eventViewModel.EventStartDate = _dateService.ConvertToString(bookingCart.Event.EventStartDate);
            eventViewModel.EventStartTime = _dateService.ConvertToStringTime(bookingCart.Event.EventStartDate);
            eventViewModel.EventEndDate = _dateService.ConvertToString(bookingCart.Event.EventEndDate);
            eventViewModel.EventEndTime = _dateService.ConvertToStringTime(bookingCart.Event.EventEndDate);
            eventViewModel.Location = bookingCart.Event.Location;
            eventViewModel.LocationLatitude = bookingCart.Event.LocationLatitude;
            eventViewModel.LocationLongitude = bookingCart.Event.LocationLongitude;

            return eventViewModel;
        }
    }
}