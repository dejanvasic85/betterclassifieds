using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    /// <summary>
    /// View model representing what can be selected in a regular booking steps/stages
    /// </summary>
    public class BookingCart
    {
        public string SessionId { get; set; }

        public string Id { get; set; }

        public string UserId { get; set; }

        public int? CategoryId { get; set; }

        public List<int> Publications { get; set; }

        public BookingCart()
        {
            Publications = new List<int>();
        }

        public bool IsStep1NotValid()
        {
            return !CategoryId.HasValue;
        }
    }


    public class BookingCartFactory
    {
        public static BookingCart CreateBookingCart(string sessionId, string username)
        {
            return new BookingCart
            {
                SessionId = sessionId,
                UserId = username,
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}