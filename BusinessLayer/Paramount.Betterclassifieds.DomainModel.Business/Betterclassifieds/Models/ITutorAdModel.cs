using System.Collections.Generic;

namespace Paramount.DomainModel.Business.OnlineClassies.Models
{
    public interface ITutorAdModel
    {
        long OnlineAdId { get; set; }
        long TutorAdId { get; set; }
        int? AgeGroupMin { get; set; }
        int? AgeGroupMax { get; set; }
        string ExpertiseLevel { get; set; }
        string TravelOption { get; set; }
        string PricingOption { get; set; }
        string WhatToBring { get; set; }
        string Objective { get; set; }
        string Subjects { get; set; }
        List<string> SubjectList { get; }
        bool IsOpenForAllAges();
    }
}