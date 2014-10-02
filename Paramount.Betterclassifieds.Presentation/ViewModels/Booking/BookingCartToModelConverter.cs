using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingCartToModelConverter : ITypeConverter<BookingCart, AdBookingModel>
    {
        public AdBookingModel Convert(ResolutionContext context)
        {
            var bookingCart = (BookingCart)context.SourceValue;

            var model = new AdBookingModel
            {
                BookingStatus = BookingStatusType.Booked,
                BookingType = BookingType.Regular,
                StartDate = bookingCart.StartDate.Value,
                EndDate = bookingCart.EndDate.Value,
                TotalPrice = bookingCart.TotalPrice,
                UserId = bookingCart.UserId,
            };

            model.Ads.Add(new OnlineAdModel
            {
                Heading = bookingCart.OnlineAdCart.Heading,
                Description = bookingCart.OnlineAdCart.Description,
                HtmlText = bookingCart.OnlineAdCart.DescriptionHtml,
                Images = bookingCart.OnlineAdCart.Images.Select(i => new AdImage { ImageUrl = i }).ToList(),
                ContactEmail = bookingCart.OnlineAdCart.Email,
                ContactPhone = bookingCart.OnlineAdCart.Phone,
                ContactName = bookingCart.OnlineAdCart.ContactName
            });

            return model;
        }
    }
}