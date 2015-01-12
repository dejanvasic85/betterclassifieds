using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Print
{
    public class PublicationEditionModel
    {
        public int PublicationId { get; set; }
        public string PublicationName { get; set; }
        public List<BookEntryModel> Editions { get; set; }

        public PublicationEditionModel()
        {
            this.Editions = new List<BookEntryModel>();
        }
    }
}
