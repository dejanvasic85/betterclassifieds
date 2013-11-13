using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class PublicationSpecialRate
    {
        public int PublicationSpecialRateId { get; set; }
        public Nullable<int> SpecialRateId { get; set; }
        public Nullable<int> PublicationAdTypeId { get; set; }
        public Nullable<int> PublicationCategoryId { get; set; }
        public virtual PublicationAdType PublicationAdType { get; set; }
        public virtual PublicationCategory PublicationCategory { get; set; }
        public virtual SpecialRate SpecialRate { get; set; }
    }
}
