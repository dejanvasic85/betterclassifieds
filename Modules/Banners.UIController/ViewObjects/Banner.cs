using System;
using System.Collections.Specialized;

namespace Paramount.Banners.UIController.ViewObjects
{
    public class Banner
    {
        public Guid ID { get; set; }

        public Guid BannerId
        {
            get { return this.ID; }
            set { this.ID = value; }
        }

        public string Subject{
            get { return this.Title; }
            set { this.Title = value; }
        }
        public string Title { get; set; }
        public string AlternateText { get; set; }
        public string ImageId { get; set; }
        public string ClientCode { get; set; }
        public string Url { get; set; }
        public Guid GroupId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Group { get; set; }

        public NameValueCollection BannerTags { get; set; }

       
    }
}
