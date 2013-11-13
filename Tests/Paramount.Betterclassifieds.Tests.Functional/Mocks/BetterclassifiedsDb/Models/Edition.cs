using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class Edition
    {
        public int EditionId { get; set; }
        public Nullable<int> PublicationId { get; set; }
        public Nullable<System.DateTime> EditionDate { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
        public Nullable<bool> Active { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
