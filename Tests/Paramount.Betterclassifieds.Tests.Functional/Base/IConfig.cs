using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public interface IConfig
    {
        string BaseUrl { get; }
        string BrowserType { get; }
    }

    public class TestConfiguration : IConfig
    {
        public string BaseUrl { get { return ConfigurationManager.AppSettings.Get("BaseUrl"); } }
        public string BrowserType { get { return ConfigurationManager.AppSettings.Get("Browser"); } }
    }
}
