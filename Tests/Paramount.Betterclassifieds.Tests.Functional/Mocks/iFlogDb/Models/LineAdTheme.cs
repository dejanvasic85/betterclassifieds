using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class LineAdTheme
    {
        public int LineAdThemeId { get; set; }
        public string ThemeName { get; set; }
        public string Description { get; set; }
        public string DescriptionHtml { get; set; }
        public string ImageUrl { get; set; }
        public string HeaderColourCode { get; set; }
        public string HeaderColourName { get; set; }
        public string BorderColourCode { get; set; }
        public string BorderColourName { get; set; }
        public string BackgroundColourCode { get; set; }
        public string BackgroundColourName { get; set; }
        public Nullable<bool> IsHeadingSuperBold { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedByUser { get; set; }
    }
}
