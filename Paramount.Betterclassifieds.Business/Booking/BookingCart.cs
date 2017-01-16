using System;
using System.Linq;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Business.Booking
{
    using Print;

    /// <summary>
    /// View model representing what can be selected in a regular booking steps/stages
    /// </summary>
    public class BookingCart : IAdRateContext, IBookingCart
    {
        public static BookingCart Create(string sessionId, string userId)
        {
            return Create(sessionId, userId, null, null);
        }

        public static BookingCart Create(string sessionId, string userId, AdBookingModel bookingModel, IClientConfig clientConfig)
        {
            var id = Guid.NewGuid().ToString();
            var cart = new BookingCart
            {
                Id = id,
                BookingReference = id.Substring(0,5).ToUpper(),
                SessionId = sessionId,
                UserId = userId,
                OnlineAdModel = new OnlineAdModel(),
                LineAdModel = new LineAdModel()
            };


            if (bookingModel != null)
            {
                cart.OnlineAdModel = bookingModel.OnlineAd;
                cart.CategoryId = bookingModel.CategoryId;
                cart.SubCategoryId = bookingModel.SubCategoryId;
            }


            // The brands are no longer supporting print.
            // But JUST IN Case we land a new client that wants print then we are doing this!
            if (clientConfig != null && clientConfig.IsPrintEnabled && bookingModel != null)
            {
                cart.LineAdModel = bookingModel.LineAd;
                cart.Publications = bookingModel.Publications;
                cart.SetSchedule(clientConfig, DateTime.Today, firstEditionDate: DateTime.Today, numberOfInsertions: bookingModel.Insertions);
            }
            
            return cart;
        }

        public string SessionId { get; set; }

        public string Id { get; set; }

        public string UserId { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }
        public string CategoryAdType { get; set; }

        public int[] Publications { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? GetStartDateOrMinimum()
        {
            IDateService dateService = new ServerDateService();

            if (!StartDate.HasValue)
                return dateService.Today;

            if (StartDate.Value < dateService.Today)
                return dateService.Today;

            return StartDate;
        }

        public DateTime? EndDate { get; set; }

        public OnlineAdModel OnlineAdModel { get; set; }

        public LineAdModel LineAdModel { get; set; }

        public decimal TotalPrice { get; set; }

        public string PaymentReference { get; set; }

        public bool IsLineAdIncluded
        {
            get { return Publications != null && Publications.Any(); }
        }

        public string BookingReference { get; set; }

        public bool NoPaymentRequired()
        {
            return TotalPrice == 0;
        }

        public void SetSchedule(IClientConfig clientConfig, DateTime? startDate, DateTime? firstEditionDate = null, int? numberOfInsertions = null)
        {
            if (!startDate.HasValue)
            {
                return;
            }

            this.StartDate = startDate;
            EndDate = StartDate.Value.AddDays(clientConfig.RestrictedOnlineDaysCount);

            if (IsLineAdIncluded && firstEditionDate.HasValue)
            {
                this.PrintFirstEditionDate = firstEditionDate;
                this.PrintInsertions = numberOfInsertions;
            }
        }

        public int? PrintInsertions { get; private set; }

        public DateTime? PrintFirstEditionDate { get; private set; }

        public EventModel Event { get; set; }

        public void UpdateByPricingFactors(PricingFactors pricingFactors)
        {
            if (!IsLineAdIncluded)
                return;

            if (LineAdModel == null)
            {
                LineAdModel = new LineAdModel();
            }

            LineAdModel.AdHeader = pricingFactors.LineAdHeader;
            LineAdModel.AdText = pricingFactors.LineAdText;
            LineAdModel.IsSuperBoldHeading = pricingFactors.IsSuperBoldHeader;
            LineAdModel.UsePhoto = pricingFactors.UsePhoto;
        }

        public ICategoryAd GetCategoryAd()
        {
            if (CategoryAdType.IsNullOrEmpty())
                return null;

            // By convention, the ViewName should be the same property 
            var prop = this.GetType().GetProperty(this.CategoryAdType);
            return prop.GetValue(this) as ICategoryAd;
        }
    }
}