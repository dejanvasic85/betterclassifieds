using System.Data;

namespace Paramount.Betterclassifieds.DataService
{
    public class ActivitySummaryRow
    {
        private readonly DataRow row;
        public ActivitySummaryRow(DataRow row)
        {
            this.row = row;
        }

        public int TotalBookings
        {
            get { return row.Field<int>("TotalBookings"); }
        }

        public decimal TotalIncome
        {
            get { return row.Field<decimal>("TotalIncome"); }
        }
    }
}