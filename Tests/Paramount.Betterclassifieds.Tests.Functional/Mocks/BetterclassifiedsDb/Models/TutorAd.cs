using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class TutorAd
    {
        public int TutorAdId { get; set; }
        public int OnlineAdId { get; set; }
        public string Subjects { get; set; }
        public Nullable<int> AgeGroupMin { get; set; }
        public Nullable<int> AgeGroupMax { get; set; }
        public string ExpertiseLevel { get; set; }
        public string TravelOption { get; set; }
        public string PricingOption { get; set; }
        public string WhatToBring { get; set; }
        public string Objective { get; set; }
        public virtual OnlineAd OnlineAd { get; set; }
    }
}
