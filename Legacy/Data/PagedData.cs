namespace Paramount.ApplicationBlock.Data
{
    using System.Data;

    public class PagedData
    {
        public DataTable Data { get; set; }
        public int TotalPopulationSize { get; set; }

        public PagedData(DataTable data, int totalPopulationSize)
        {
            Data = data;
            TotalPopulationSize = totalPopulationSize;
        }
    }
}
