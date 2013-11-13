using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class WebContent
    {
        public int WebContentId { get; set; }
        public string Title { get; set; }
        public string PageId { get; set; }
        public string WebContent1 { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string LastModifiedUser { get; set; }
    }
}
