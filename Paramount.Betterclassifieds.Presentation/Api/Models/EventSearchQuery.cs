using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation.Api.Models
{
    public class EventSearchQuery
    {
        public int? TakeMax { get; set; }
        public string User { get; set; }
    }
}