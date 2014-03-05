using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Presentation.Models
{
    public class ListingSummaryViewModel
    {
        private readonly OnlineListingModel listingModel;

        public ListingSummaryViewModel(OnlineListingModel listingModel)
        {
            this.listingModel = listingModel;
        }


        public string TitleSlug { get { return Slug.Create(true, listingModel.Heading); } }
        public int AdId { get { return listingModel.AdId; } }
        public string CategoryTitle { get { return listingModel.CategoryTitle; } }
        public string DocumentID { get { return listingModel.DocumentID; } }

        public string Description { get { return listingModel.Description; } }
        public string Title { get { return listingModel.Heading; } }
    }
}