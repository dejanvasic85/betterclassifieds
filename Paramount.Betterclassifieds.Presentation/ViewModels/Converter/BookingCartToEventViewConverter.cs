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

            eventViewModel.EventStartDate = bookingCart.Event.EventStartDate.GetValueOrDefault();
            eventViewModel.EventEndDate = bookingCart.Event.EventEndDate.GetValueOrDefault();
            eventViewModel.VenueName = bookingCart.Event.VenueName;
            eventViewModel.Location = bookingCart.Event.Location;
            eventViewModel.LocationLatitude = bookingCart.Event.LocationLatitude;
            eventViewModel.LocationLongitude = bookingCart.Event.LocationLongitude;
            eventViewModel.LocationFloorPlanDocumentId = bookingCart.Event.LocationFloorPlanDocumentId;
            eventViewModel.LocationFloorPlanFilename = bookingCart.Event.LocationFloorPlanFilename;
            
            // Address
            eventViewModel.StreetNumber = bookingCart.Event.Address.StreetNumber;
            eventViewModel.StreetName = bookingCart.Event.Address.StreetName;
            eventViewModel.Country = bookingCart.Event.Address.Country;
            eventViewModel.Postcode = bookingCart.Event.Address.Postcode;
            eventViewModel.State = bookingCart.Event.Address.State;
            eventViewModel.Suburb = bookingCart.Event.Address.Suburb;

            return eventViewModel;
        }
    }
}