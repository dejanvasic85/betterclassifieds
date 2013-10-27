using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class NonPublicationDate
    {
        public int NonPublicationDateId { get; set; }
        public int PublicationId { get; set; }
        public System.DateTime EditionDate { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
