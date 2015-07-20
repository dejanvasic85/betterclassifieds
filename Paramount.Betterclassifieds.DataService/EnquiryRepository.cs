using System;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.DataService.Classifieds;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class EnquiryRepository : IEnquiryRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public EnquiryRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void CreateSupportEnquiry(string name, string email, string phone, string comments,
            string enquiryTypeName = "SupportGeneralEnquiry")
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                context.SupportEnquiries.InsertOnSubmit(new SupportEnquiry
                {
                    CreatedDate = DateTime.Now,
                    CreatedDateUtc = DateTime.UtcNow,
                    Email = email,
                    Phone = phone,
                    FullName = name,
                    EnquiryText = comments,
                    EnquiryTypeName = enquiryTypeName
                });
                context.SubmitChanges();
            }
        }
    }
}