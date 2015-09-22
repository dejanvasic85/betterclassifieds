using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Tests
{
    internal class AdEnquiryMockBuilder : MockBuilder<AdEnquiryMockBuilder, AdEnquiry>
    {
        public AdEnquiryMockBuilder WithAdId(int id)
        {
            return WithBuildStep(s => s.AdId = id);
        }
        public AdEnquiryMockBuilder WithEmail(string email)
        {
            return WithBuildStep(s => s.Email = email);
        }

        public AdEnquiryMockBuilder WithEnquiryId(int enquiryId)
        {
            return WithBuildStep(s => s.EnquiryId = enquiryId);
        }

        public AdEnquiryMockBuilder WithFullName(string fullName)
        {
            return WithBuildStep(s => s.FullName = fullName);
        }

        public AdEnquiryMockBuilder WithPhone(string phone)
        {
            return WithBuildStep(s => s.Phone = phone);
        }

        public AdEnquiryMockBuilder WithQuestion(string question)
        {
            return WithBuildStep(s => s.Question = question);
        }
    }
}