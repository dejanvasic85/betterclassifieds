using System.Linq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Csv;

namespace Paramount.Betterclassifieds.Tests.Csv
{
    [TestFixture]
    public class CsvGeneratorTests
    {
        [Test]
        public void GetCsvLines_WithNoData_ReturnsEmptyCollection()
        {
            var generator = new CsvGenerator<Person>(null);
            var lines = generator.GetCsvLines();
            CollectionAssert.IsEmpty(lines);
        }

        [Test]
        public void GetCsvLines_WithGenericProvider_Returns_Lines_IgnoresArray()
        {
            var data = new[]
            {
                new Person { Age = 10, Height = 10, Name = "Foo Bar", Attributes = new int[] {1,2,3}},
                new Person { Age = 20, Height = 20, Name = "Foo Bar Two", Attributes = new int[] {1,2,3}},
            };
            var generator = new CsvGenerator<Person>(data);
            var lines = generator.GetCsvLines().ToList();

            Assert.That(lines.Count, Is.EqualTo(2));
            Assert.That(lines[0], Is.EqualTo("Foo Bar,10,10"));
            Assert.That(lines[1], Is.EqualTo("Foo Bar Two,20,20"));
        }

        [Test]
        public void GetCsvLines_WithCustomProvider_ReturnsLines()
        {
            var data = new[]
            {
                new Person { Age = 10, Height = 10, Name = "Foo Bar", Attributes = new int[] {1,2,3}},
                new Person { Age = 20, Height = 20, Name = "Foo Bar Two", Attributes = new int[] {1,2,3}},
            };
            var generator = new CsvGenerator<Person>(data, new PersonCsvLineProvider());
            var lines = generator.GetCsvLines().ToList();

            Assert.That(lines.Count, Is.EqualTo(2));
            Assert.That(lines[0], Is.EqualTo("Person,Foo Bar"));
            Assert.That(lines[1], Is.EqualTo("Person,Foo Bar Two"));
        }

        [Test]
        public void GetData_ReturnsByteArray()
        {
            var data = new[]
            {
                new Person { Age = 10, Height = 10, Name = "Foo Bar", Attributes = new int[] {1,2,3}},
                new Person { Age = 20, Height = 20, Name = "Foo Bar Two", Attributes = new int[] {1,2,3}},
            };
            var generator = new CsvGenerator<Person>(data, new PersonCsvLineProvider());
            var generatedData = generator.GetData();

            Assert.That(generatedData, Is.Not.Null);
            Assert.That(generatedData.Length, Is.GreaterThan(1));
        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public decimal Height { get; set; }
            public int[] Attributes { get; set; }
        }

        class PersonCsvLineProvider : ICsvLineProvider<Person>
        {
            public string GetHeader(Person target)
            {
                return "headerinfo";
            }

            public string GetCsvLine(Person target)
            {
                return $"Person,{target.Name}";
            }
        }
    }
}
