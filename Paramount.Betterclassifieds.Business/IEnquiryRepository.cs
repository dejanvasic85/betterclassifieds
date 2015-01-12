namespace Paramount.Betterclassifieds.Business
{
    public interface IEnquiryRepository
    {
        void CreateSupportEnquiry(string name, string email, string phone, string comments, string enquiryTypeName = "SupportGeneralEnquiry");
    }
}