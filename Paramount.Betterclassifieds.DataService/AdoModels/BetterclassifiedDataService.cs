using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Paramount.ApplicationBlock.Data;

namespace Paramount.Betterclassifieds.DataService
{
    [Obsolete]
    public class BetterclassifiedDataService
    {
        public static List<ExpiredAdRow> GetExpiredAdByLastEdition(DateTime editionDate)
        {
            var df = new DatabaseProxy("psp_Betterclassified_GetLineAdBookingByLastEdition", "ClassifiedConnection");
            df.AddParameter("@EditionDate", editionDate);
            var dt = df.ExecuteQuery().Tables[0];
            var list = (from DataRow item in dt.Rows select new ExpiredAdRow(item)).ToList();
            return list;
        }
    }
}