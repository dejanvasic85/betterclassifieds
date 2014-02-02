namespace Paramount.Betterclassifieds.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using ApplicationBlock.Data;

    public class BetterclassifiedDataService
    {
        private const string ConfigSection = @"paramount/services";
        private const string ConfigKey = "Betterclassifieds";

        public static List<ExpiredAdRow> GetExpiredAdByLastEdition(DateTime editionDate)
        {
            var df = new DatabaseProxy("psp_Betterclassified_GetLineAdBookingByLastEdition", ConfigSection, ConfigKey);
            df.AddParameter("@EditionDate", editionDate);
            var dt = df.ExecuteQuery().Tables[0];
            var list = (from DataRow item in dt.Rows select new ExpiredAdRow(item)).ToList();
            return list;
        }

    }
}