namespace Paramount.Betterclassifieds.Business
{
    public class EnquiryManager : IEnquiryManager
    {
        private readonly IEnquiryRepository _enquiryRepository;

        public EnquiryManager(IEnquiryRepository enquiryRepository)
        {
            _enquiryRepository = enquiryRepository;
        }

        public void CreateSupportEnquiry(string name, string email, string phone, string comments,
            string enquiryTypeName = "SupportGeneralEnquiry")
        {
            _enquiryRepository.CreateSupportEnquiry(name, email, phone, comments, enquiryTypeName);
        }
    }
}