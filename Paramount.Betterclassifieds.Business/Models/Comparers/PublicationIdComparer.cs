using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Business.Models.Comparers
{
    public class PublicationIdComparer : IEqualityComparer<PublicationModel>
    {
        public bool Equals(PublicationModel x, PublicationModel y)
        {
            return x.PublicationId == y.PublicationId;
        }

        public int GetHashCode(PublicationModel p)
        {
            return p.GetHashCode();
        }
    }
}