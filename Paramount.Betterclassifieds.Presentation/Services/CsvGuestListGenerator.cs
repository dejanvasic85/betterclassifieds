using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Monads;
using System.Text;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class CsvGenerator<T> where T : class
    {
        private readonly IEnumerable<T> _data;
        private readonly ICsvLineProvider<T> _csvLineProvider;

        public CsvGenerator(IEnumerable<T> data)
            : this(data, new GenericCsvLineProvider<T>())
        { }

        public CsvGenerator(IEnumerable<T> data, ICsvLineProvider<T> csvLineProvider)
        {
            _data = data;
            _csvLineProvider = csvLineProvider;
        }

        public IEnumerable<string> GetCsvLines()
        {
            if (_data == null || !_data.Any())
                return Enumerable.Empty<string>();

            return _data.Select(item => _csvLineProvider.GetCsvLine(item));
        }

        public byte[] GetData()
        {
            if (_data == null || !_data.Any())
                return null;

            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream, Encoding.UTF8))
            {
                var firstItem = _data.First();
                streamWriter.WriteLine(_csvLineProvider.GetHeader(firstItem));

                foreach (var csvLine in GetCsvLines())
                {
                    streamWriter.WriteLine(csvLine);
                }

                streamWriter.Flush();
                return stream.ToArray();
            }
        }
    }

    public interface ICsvLineProvider<in T> where T : class
    {
        string GetHeader(T target);
        string GetCsvLine(T target);
    }

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

    public class EventGuestListCsvLineProvider : ICsvLineProvider<EventGuestListViewModel>
    {
        public string GetHeader(EventGuestListViewModel guest)
        {
            var builder = new StringBuilder($"Ticket Number,Ticket Name,Guest Email,Guest Full Name");
            foreach (var field in guest.DynamicFields)
            {
                builder.AppendFormat(",{0}", field.FieldName);
            }
            return builder.ToString();
        }

        public string GetCsvLine(EventGuestListViewModel guest)
        {
            var builder = new StringBuilder($"{guest.TicketNumber},{guest.TicketName},{guest.GuestEmail},{guest.GuestFullName}");
            foreach (var field in guest.With(g => g.DynamicFields))
            {
                builder.AppendFormat(",{0}", field.With(f => f.FieldValue));
            }
            return builder.ToString();
        }
    }
}