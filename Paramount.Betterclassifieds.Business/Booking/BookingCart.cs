using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Booking
{
    using Print;

    /// <summary>
    /// View model representing what can be selected in a regular booking steps/stages
    /// </summary>
    public class BookingCart : IAdRateContext
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

        public string SessionId { get; private set; }

        public string Id { get; private set; }

        public bool Completed { get; private set; }

        public List<int> CompletedSteps { get; private set; }

        public string UserId { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public int[] Publications { get; set; }

        public DateTime? StartDate { get; private set; }

        public DateTime? GetStartDateOrMinimum()
        {
            if (!StartDate.HasValue)
                return DateTime.Today;

            if (StartDate.Value < DateTime.Today)
                return DateTime.Today;

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

        public void SetSchedule(IClientConfig clientConfig, DateTime startDate, DateTime? firstEditionDate, int? numberOfInsertions = null)
        {
            this.StartDate = startDate;
            EndDate = StartDate.Value.AddDays(clientConfig.RestrictedOnlineDaysCount);

            if (IsLineAdIncluded)
            {
                this.PrintFirstEditionDate = firstEditionDate;
                this.PrintInsertions = numberOfInsertions;
            }
        }

        public int? PrintInsertions { get; private set; }

        public DateTime? PrintFirstEditionDate { get; private set; }

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
            this.Completed = true;
        }

        public void UpdateByPricingFactors(PricingFactors pricingFactors)
        {
            if (!this.IsLineAdIncluded) return;
            this.LineAdModel.AdHeader = pricingFactors.LineAdHeader;
            this.LineAdModel.AdText = pricingFactors.LineAdText;
            this.LineAdModel.IsSuperBoldHeading = pricingFactors.IsSuperBoldHeader;
            this.LineAdModel.UsePhoto = pricingFactors.UsePhoto;
        }
    }
}