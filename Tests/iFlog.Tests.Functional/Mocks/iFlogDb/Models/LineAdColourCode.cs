using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class LineAdColourCode
    {
        public int LineAdColourId { get; set; }
        public string LineAdColourName { get; set; }
        public string ColourCode { get; set; }
        public Nullable<int> Cyan { get; set; }
        public Nullable<int> Magenta { get; set; }
        public Nullable<int> Yellow { get; set; }
        public Nullable<int> KeyCode { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedByUser { get; set; }
    }
}
