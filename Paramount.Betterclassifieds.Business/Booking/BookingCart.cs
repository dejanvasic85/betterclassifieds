using System;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Managers;

namespace Paramount.Betterclassifieds.Business
{
    /// <summary>
    /// View model representing what can be selected in a regular booking steps/stages
    /// </summary>
    public class BookingCart
    {
        private const int LastStepNumber = 4;

        public BookingCart()
        {
            Publications = new int[] { };
            CompletedSteps = new List<int>();
        }

        public string SessionId { get; set; }

        public string Id { get; set; }

        public bool Completed { get; private set; }

        public List<int> CompletedSteps { get; private set; }

        public string UserId { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public int[] Publications { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public OnlineAdCart OnlineAdCart { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsLineAdIncluded
        {
            get { return Publications != null && Publications.Any(); }
        }

        public string Reference { get; set; }

        public bool NoPaymentRequired()
        {
            return TotalPrice == 0;
        }

        public void SetSchedule(IClientConfig clientConfig, DateTime startDate)
        {
            this.StartDate = startDate;
            EndDate = StartDate.Value.AddDays(clientConfig.RestrictedOnlineDaysCount);
        }

        public void CompleteStep(int step)
        {
            if (CompletedSteps.Contains(step))
            {
                return;
            }

            CompletedSteps.Add(step);

            if (LastStepNumber == step)
            {
                Completed = true;
            }
        }

        public int GetLastCompletedStepNumber()
        {
            if (CompletedSteps.Count == 0)
                return 0;

            return CompletedSteps.Last();
        }

       
    }
}