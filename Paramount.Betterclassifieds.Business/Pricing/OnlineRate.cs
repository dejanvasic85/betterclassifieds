﻿namespace Paramount.Betterclassifieds.Business.Models
{
    public class OnlineRate
    {
        public int OnlineRateId { get; set; }
        public int? CategoryId { get; set; }
        public decimal MinimumCharge { get; set; }
    }
}