using System.Collections.Generic;
using System.Linq;

namespace BetterClassified.UI.Models
{
    [OnlineAdType(OnlineAdName = "Tutors")]
    public class TutorAdModel
    {
        public TutorAdModel()
        { }

        public TutorAdModel(int? ageGroupMin, int? ageGroupMax, string level, string travelOption,
            string pricingOption, string whatToBring, string objective, string subjects,
            long onlineAdId = 0, long tutorAdId = 0)
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

        public long OnlineAdId { get; set; }
        public long TutorAdId { get; set; }
        public int? AgeGroupMin { get; set; }
        public int? AgeGroupMax { get; set; }
        public string Level { get; set; }
        public string TravelOption { get; set; }
        public string PricingOption { get; set; }
        public string WhatToBring { get; set; }
        public string Objective { get; set; }
        public List<string> Subjects { get; set; }

        public string GetSubjectsAsCsv()
        {
            return string.Join(",", this.Subjects.ToArray());
        }
    }
}