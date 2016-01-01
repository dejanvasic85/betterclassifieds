using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class NewBooking : IDocType
    {
        public string DocumentType { get { return GetType().Name; } }
        public IList<EmailAttachment> Attachments { get; set; }


        [Placeholder("username")]
        public string UserId { get; set; }

        [Placeholder("adid")]
        public string AdId { get; set; }

        [Placeholder("price")]
        public string TotalPrice { get; set; }
        
        [Placeholder("adheading")]
        public string AdHeading { get; set; }

        [Placeholder("addescription")]
        public string AdDescription { get; set; }
        
        [Placeholder("startdate")]
        public string StartDate { get; set; }

        [Placeholder("enddate")]
        public string EndDate { get; set; }
    }
}