﻿using System;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

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
            
            // Treat the incoming text from user as html
            var adText = AdText.FromHtml(eventViewModel.Description);
            bookingCart.OnlineAdModel.HtmlText = adText.HtmlTextEncoded;
            bookingCart.OnlineAdModel.Description = adText.Plaintext;

            bookingCart.OnlineAdModel.ContactName = eventViewModel.OrganiserName;
            bookingCart.OnlineAdModel.ContactPhone = eventViewModel.OrganiserPhone;
         
            // Event details
            bookingCart.Event.EventStartDate = eventViewModel.EventStartDate;
            bookingCart.Event.EventEndDate = eventViewModel.EventEndDate;
            bookingCart.Event.VenueName = eventViewModel.VenueName;
            bookingCart.Event.Location = eventViewModel.Location;
            bookingCart.Event.LocationLatitude = eventViewModel.LocationLatitude;
            bookingCart.Event.LocationLongitude = eventViewModel.LocationLongitude;
            bookingCart.Event.TimeZoneId = eventViewModel.TimeZoneId;
            bookingCart.Event.TimeZoneName = eventViewModel.TimeZoneName;
            bookingCart.Event.TimeZoneDaylightSavingsOffsetSeconds = eventViewModel.TimeZoneDaylightSavingsOffsetSeconds;
            bookingCart.Event.TimeZoneUtcOffsetSeconds = eventViewModel.TimeZoneUtcOffsetSeconds;
            bookingCart.Event.LocationFloorPlanDocumentId = eventViewModel.LocationFloorPlanDocumentId;
            bookingCart.Event.LocationFloorPlanFilename = eventViewModel.LocationFloorPlanFilename;

            // Address
            bookingCart.Event.Address.StreetNumber = eventViewModel.StreetNumber;
            bookingCart.Event.Address.StreetName = eventViewModel.StreetName;
            bookingCart.Event.Address.Country = eventViewModel.Country;
            bookingCart.Event.Address.Postcode = eventViewModel.Postcode;
            bookingCart.Event.Address.State = eventViewModel.State;
            bookingCart.Event.Address.Suburb = eventViewModel.Suburb;

            // Schedule
            bookingCart.SetSchedule(_clientConfig, eventViewModel.AdStartDate);

            return bookingCart;
        }
    }
}