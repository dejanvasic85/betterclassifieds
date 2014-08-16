using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Models
{
    [OnlineAdType(OnlineAdName = "Tutors")]
    public class TutorAdModel
    {
        public int OnlineAdId { get; set; }
        public int TutorAdId { get; set; }
        public int? AgeGroupMin { get; set; }
        public int? AgeGroupMax { get; set; }
        public string ExpertiseLevel { get; set; }
        public string TravelOption { get; set; }
        public string PricingOption { get; set; }
        public string WhatToBring { get; set; }
        public string Objective { get; set; }
        public string Subjects { get; set; }

        public List<string> SubjectList { get { return Subjects.Split(',').ToList(); }}

        public bool IsOpenForAllAges()
        {
            return !AgeGroupMin.HasValue && !AgeGroupMax.HasValue;
        }
    }
}