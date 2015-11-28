namespace Paramount.Common.UIController
{
    using System.Collections;

    public class PagedSource
    {
        public int TotalPageSize { get; set; }

        public virtual IEnumerable Datasource { get; set; }
    }
}
