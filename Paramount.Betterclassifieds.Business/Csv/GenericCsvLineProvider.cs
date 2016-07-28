using System.Text;

namespace Paramount.Betterclassifieds.Business.Csv
{
    public class GenericCsvLineProvider<T> : ICsvLineProvider<T> where T : class
    {
        public string GetHeader(T target)
        {
            var props = typeof(T).GetProperties();
            var builder = new StringBuilder();
            foreach (var prop in props)
            {
                if (prop.PropertyType.IsArray)
                    continue;

                builder.AppendFormat(",{0}", prop.Name);
            }

            return builder.ToString().TrimStart(',');
        }

        public string GetCsvLine(T target)
        {
            var properties = typeof(T).GetProperties();
            var builder = new StringBuilder();
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.PropertyType.IsArray)
                    continue;

                var val = propertyInfo.GetValue(target);
                builder.AppendFormat(",{0}", val);
            }
            return builder.ToString().TrimStart(',');
        }
    }
}