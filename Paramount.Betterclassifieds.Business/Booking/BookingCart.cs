using System;
using System.Collections.Generic;
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

        public BookingCart(string sessionId, string userId)
        {
            Id = Guid.NewGuid().ToString();
            SessionId = sessionId;
            UserId = userId;
            BookingReference = Id.Substring(0, 6).ToUpper();
            Publications = new int[] { };
            CompletedSteps = new List<int>();
            OnlineAdModel = new OnlineAdModel();
            LineAdModel = new LineAdModel();
        }

        public static BookingCart Create(string sessionId, string userId, AdBookingModel bookingModel, IClientConfig clientConfig)
        {
            // Create the new booking for user and session
            // Map the online/linead and category details
            var cart = new BookingCart(sessionId, userId)
            {
                OnlineAdModel = bookingModel.OnlineAd,
                CategoryId = bookingModel.CategoryId,
                SubCategoryId = bookingModel.SubCategoryId
            };

            // The brands are no longer supporting print.
            // But JUST IN Case we land a new client that wants print then we are doing this!
            if (clientConfig.IsPrintEnabled)
            {
                cart.LineAdModel = bookingModel.LineAd;
                cart.Publications = bookingModel.Publications;
            }

            // Set the schedule
            cart.SetSchedule(clientConfig, DateTime.Today, firstEditionDate: DateTime.Today, numberOfInsertions: bookingModel.Insertions);
            return cart;
        }

        public string SessionId { get; private set; }

        public string Id { get; private set; }

        public bool Completed { get; private set; }

        public List<int> CompletedSteps { get; private set; }

        public string UserId { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }
        public string CategoryAdType { get; set; }

        public int[] Publications { get; set; }

        public DateTime? StartDate { get; private set; }

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

        public void SetSchedule(IClientConfig clientConfig, DateTime startDate, DateTime? firstEditionDate = null, int? numberOfInsertions = null)
        {
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

        public void CompleteStep(int step)
        {
            if (CompletedSteps.Contains(step))
            {
                return;
            }

            CompletedSteps.Add(step);
        }

        public int GetLastCompletedStepNumber()
        {
            if (CompletedSteps.Count == 0)
                return 0;

            return CompletedSteps.Last();
        }

        public void Complete()
        {
            Completed = true;
        }

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