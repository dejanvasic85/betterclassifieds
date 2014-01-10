using Paramount.ApplicationBlock.Data;
using Paramount.Betterclassifieds.DataService.LinqObjects;
using Paramount.Common.DataTransferObjects.Common;
using Paramount.Common.DataTransferObjects.LocationService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.DataService
{
    public class CommonDataProvider : IDisposable
    {
        private CommonDataContext _context;
        private const string ConfigSection = "paramount/services";
        private const string SourceKey = "ConnectionString";
        public string ClientCode { get; private set; }

        public CommonDataProvider(string clientCode)
        {
            _context = new CommonDataContext(ConnectionString);
            ClientCode = clientCode;
        }

        public string ConnectionString
        {
            get { return ConfigReader.GetConnectionString(ConfigSection, SourceKey); }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
            GC.SuppressFinalize(this);
        }

        #endregion

        public List<CurrencyEntity> GetCurrencyList()
        {
            return _context.Currencies.Select(a => a.Convert()).ToList();
        }

        public List<CountryEntity> GetCountryList()
        {
            return _context.Countries.Select(c => c.Convert()).ToList();
        }

        public List<StateEntity> GetStateList(string countryCode)
        {
            return _context.States.Where(s => s.CountryCode == countryCode).Select(s => s.Convert()).ToList();
        }

        public void Commit()
        {
            _context.SubmitChanges();
        }
    }
}
