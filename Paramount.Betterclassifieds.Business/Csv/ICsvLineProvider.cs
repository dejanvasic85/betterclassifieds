namespace Paramount.Betterclassifieds.Business.Csv
{
    public interface ICsvLineProvider<in T> where T : class
    {
        string GetHeader(T target);
        string GetCsvLine(T target);
    }
}