using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class SchemaVersion
    {
        public int Id { get; set; }
        public string ScriptName { get; set; }
        public System.DateTime Applied { get; set; }
    }
}
