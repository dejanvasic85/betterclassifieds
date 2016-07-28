using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.Business.Csv
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
}
