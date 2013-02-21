namespace Paramount.DataService.LinqObjects
{
    using System.Linq;

    public class PagedSource<T> 
    {
        public IQueryable<T> Data { get; set; }

        public int TotalCount { get; set; }
    }
}