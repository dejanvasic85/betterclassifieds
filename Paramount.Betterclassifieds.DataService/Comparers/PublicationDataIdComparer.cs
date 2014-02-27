using System.Collections.Generic;
using Paramount.Betterclassifieds.DataService.Classifieds;

namespace Paramount.Betterclassifieds.DataService.Comparers
{
    public class PublicationDataIdComparer : IEqualityComparer<Publication>
    {
        public bool Equals(Publication x, Publication y)
        {
            return x.PublicationId == y.PublicationId;
        }

        public int GetHashCode(Publication p)
        {
            return p.GetHashCode();
        }
    }
}