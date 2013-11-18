using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Domain
{
    public partial class Publication
    {
        public Publication()
        {
            this.BookEntries = new List<BookEntry>();
            this.Editions = new List<Edition>();
            this.NonPublicationDates = new List<NonPublicationDate>();
            this.PublicationAdTypes = new List<PublicationAdType>();
            this.PublicationCategories = new List<PublicationCategory>();
        }

        public int PublicationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> PublicationTypeId { get; set; }
        public string ImageUrl { get; set; }
        public string FrequencyType { get; set; }
        public string FrequencyValue { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public virtual ICollection<BookEntry> BookEntries { get; set; }
        public virtual ICollection<Edition> Editions { get; set; }
        public virtual ICollection<NonPublicationDate> NonPublicationDates { get; set; }
        public virtual PublicationType PublicationType { get; set; }
        public virtual ICollection<PublicationAdType> PublicationAdTypes { get; set; }
        public virtual ICollection<PublicationCategory> PublicationCategories { get; set; }
    }
}
