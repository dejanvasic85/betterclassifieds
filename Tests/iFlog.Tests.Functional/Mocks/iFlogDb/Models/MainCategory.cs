using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class MainCategory
    {
        public MainCategory()
        {
            this.AdBookings = new List<AdBooking>();
            this.PublicationCategories = new List<PublicationCategory>();
        }

        public int MainCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string OnlineAdTag { get; set; }
        public virtual ICollection<AdBooking> AdBookings { get; set; }
        public virtual ICollection<PublicationCategory> PublicationCategories { get; set; }
        public virtual MainCategory ParentCategory { get; set; }
        public virtual ICollection<MainCategory> Children { get; set; }
    }
}
