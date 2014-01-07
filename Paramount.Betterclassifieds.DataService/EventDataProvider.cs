namespace Paramount.DataService
{
    using System;
    using System.Collections.ObjectModel;
    using System.Data;
    using ApplicationBlock.Data;
    using Common.DataTransferObjects;
    using LinqObjects;
    using System.Linq;
    public class EventDataProvider : IDataProvider
    {
        private EvenServiceClassesDataContext _context;
        private const string SourceKey = "ConnectionString";

        public string ClientCode { get; private set; }

        public string ConfigSection
        {
            get { return @"paramount/services"; }
        }

        public Collection<ListItem> GetCategories()
        {
            return new Collection<ListItem>( _context.Event_Categories.Select(
                a => new ListItem {Code = a.CategoryId.ToString(), Description = a.Description}).ToList());
        }

        public void Commit()
        {
            _context.SubmitChanges();
        }

        public string ConnectionString
        {
            get { return ConfigReader.GetConnectionString(ConfigSection, SourceKey); }
        }

        public EventDataProvider(string clientCode)
        {
            _context = new EvenServiceClassesDataContext(ConnectionString);
            ClientCode = clientCode;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}
