using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class SchemaVersion
    {
        public int Id { get; set; }
        public string ScriptName { get; set; }
        public System.DateTime Applied { get; set; }
    }
}
