using System;

namespace Paramount.Betterclassifieds.Business
{
    [Obsolete]
    public class AdTypeCode
    {
        public const string ONLINE = "ONLINE";
        public const string LINE = "LINE";

        // These values are hardcoded in the database
        // see 0002-BaselineData-1.sq
        public const int LineCodeId = 1;
        public const int OnlineCodeId = 2;
    }
}