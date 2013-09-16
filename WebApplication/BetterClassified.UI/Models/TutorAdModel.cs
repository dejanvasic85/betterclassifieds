using System.Collections.Generic;
using System.Linq;

namespace BetterClassified.UI.Models
{
    public class TutorAdModel
    {
        public TutorAdModel(int? ageGroupMin, int? ageGroupMax, string level, string travelOption,
            string pricingOption, string whatToBring, string objective, string subjects,
            int? onlineAdId = null, int? tutorAdId = null)
        {
            this.AgeGroupMin = ageGroupMin;
            this.AgeGroupMax = ageGroupMax;
            this.Level = level;
            this.TravelOption = travelOption;
            this.PricingOption = pricingOption;
            this.WhatToBring = whatToBring;
            this.Objective = objective;
            this.Subjects = subjects.Split(',').ToList();
            this.OnlineAdId = onlineAdId;
            this.TutorAdId = tutorAdId;
        }

        public long? OnlineAdId { get; private set; }
        public long? TutorAdId { get; private set; }
        public int? AgeGroupMin { get; private set; }
        public int? AgeGroupMax { get; private set; }
        public string Level { get; private set; }
        public string TravelOption { get; private set; }
        public string PricingOption { get; private set; }
        public string WhatToBring { get; private set; }
        public string Objective { get; private set; }
        public List<string> Subjects { get; private set; }

        public string GetSubjectsAsCsv()
        {
            return string.Join(",", this.Subjects.ToArray());
        }
    }
}