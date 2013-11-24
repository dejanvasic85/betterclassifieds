using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Domain
{
    public partial class Lookup
    {
        public long LookupId { get; set; }
        public string GroupName { get; set; }
        public string LookupValue { get; set; }
    }
}
