using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class PublicationType
    {
        public PublicationType()
        {
            this.Publications = new List<Publication>();
        }

        public int PublicationTypeId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
    }
}
