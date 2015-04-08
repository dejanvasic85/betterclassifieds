using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Booking
{
    public class OnlineAdModel : IAd
    {
        public OnlineAdModel()
        {
            Images = new List<AdImage>();
        }

        public int OnlineAdId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string HtmlText { get; set; }
        public int LocationId { get; set; }
        public int LocationAreaId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public int NumOfViews { get; private set; }

        public List<AdImage> Images { get; set; }

        public void IncrementHits()
        {
            this.NumOfViews++;
        }

        public void SetDescription(string html)
        {
            // Set the Description Html
            this.HtmlText = html;

            // We also want to convert the html to normal plaintext to appear normally everywhere else
            this.Description = html.FromHtmlToPlaintext();
        }

        public void AddImage(string documentId)
        {
            if (this.Images == null)
                this.Images = new List<AdImage>();
            this.Images.Add(new AdImage(documentId));
        }

        public void RemoveImage(string documentId)
        {
            if (this.Images == null || this.Images.Count == 0)
            {
                return;
            }

            var img = this.Images.FirstOrDefault(i => i.DocumentId == documentId);
            if (img == null)
                return;

            this.Images.Remove(img);
        }

        public AdImage DefaultImageId
        {
            get { return this.Images.Count == 0 ? null : this.Images.First(); }
        }
    }
}