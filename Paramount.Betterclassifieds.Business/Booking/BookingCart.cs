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
        public BookingCart()
        {
            Publications = new int[] { };
            CompletedSteps = new List<int>();
        }

        public string SessionId { get; set; }

        public string Id { get; set; }

        public bool Completed { get; set; }

        public string UserId { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public int[] Publications { get; set; }

        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public OnlineAdCart OnlineAdCart { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsLineAdIncluded
        {
            get { return Publications != null && Publications.Any(); }
        }

        public List<int> CompletedSteps { get; set; }

        public bool NoPaymentRequired()
        {
            return TotalPrice == 0;
        }

        public void SetEndDate(IClientConfig clientConfig)
        {
            if (!StartDate.HasValue)
            {
                throw new NullReferenceException("StartDate cannot be null when setting the end date");
            }

            EndDate = StartDate.Value.AddDays(clientConfig.RestrictedOnlineDaysCount);
        }
    }
}