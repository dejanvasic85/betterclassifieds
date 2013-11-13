using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class AppSetting
    {
        public string Module { get; set; }
        public string AppKey { get; set; }
        public string DataType { get; set; }
        public string SettingValue { get; set; }
        public string Description { get; set; }
    }
}
