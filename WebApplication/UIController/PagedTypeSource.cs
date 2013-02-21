namespace Paramount.Common.UIController
{
    using System.Collections;
    using System.Collections.Generic;

    public class PagedTypeSource<T> : PagedSource
    {
        public override IEnumerable Datasource
        {
            get
            {
                return TypedDatasource;
            }
            set
            {
                TypedDatasource = (IEnumerable<T>)value;
            }
        }

        public IEnumerable<T> TypedDatasource { get; set; }
    }
}
