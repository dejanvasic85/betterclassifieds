using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business.Booking
{
    public interface IBookingCart
    {
        string SessionId { get; }
        string Id { get; }
        bool Completed { get; }
        List<int> CompletedSteps { get; }
        string UserId { get; set; }
        int? CategoryId { get; set; }
        int? SubCategoryId { get; set; }
        string ViewName { get; set; }
        int[] Publications { get; set; }
        DateTime? StartDate { get; }
        DateTime? EndDate { get; set; }
        OnlineAdModel OnlineAdModel { get; set; }
        LineAdModel LineAdModel { get; set; }
        decimal TotalPrice { get; set; }
        string PaymentReference { get; set; }
        bool IsLineAdIncluded { get; }
        string BookingReference { get; set; }
        int? PrintInsertions { get; }
        DateTime? PrintFirstEditionDate { get; }
        EventModel Event { get; set; }
        DateTime? GetStartDateOrMinimum();
        bool NoPaymentRequired();
        void SetSchedule(IClientConfig clientConfig, DateTime startDate, DateTime? firstEditionDate = null, int? numberOfInsertions = null);
        void CompleteStep(int step);
        int GetLastCompletedStepNumber();
        void Complete();
        void UpdateByPricingFactors(PricingFactors pricingFactors);
    }
}