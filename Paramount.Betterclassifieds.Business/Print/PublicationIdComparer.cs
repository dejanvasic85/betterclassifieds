using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Print
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