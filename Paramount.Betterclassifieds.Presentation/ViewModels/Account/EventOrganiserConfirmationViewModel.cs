using System.Collections.Generic;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class EventOrganiserConfirmationViewModel
    {
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
        public bool IsSubmitted { get; set; }
    }
}