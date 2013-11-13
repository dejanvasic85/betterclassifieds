using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class AdGraphic
    {
        public int AdGraphicId { get; set; }
        public Nullable<int> AdDesignId { get; set; }
        public string DocumentID { get; set; }
        public string Filename { get; set; }
        public string ImageType { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual AdDesign AdDesign { get; set; }
    }
}
